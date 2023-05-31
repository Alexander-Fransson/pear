import jwtDecode from "jwt-decode";
import {
  ResponsePromise,
  AuthResponse,
  AuthError,
  User,
  AuthenticateBody,
} from "./types";

interface IPearAuth {
  signIn: (
    email: string,
    password: string
  ) => ResponsePromise<AuthResponse, AuthError>;
  signUp: (
    email: string,
    password: string
  ) => ResponsePromise<AuthResponse, AuthError>;
  signOut: () => ResponsePromise<void>;

  refresh: (token?: string) => ResponsePromise<AuthResponse, AuthError>;

  getUser: () => Promise<User | null>;
}

export class PearAuth implements IPearAuth {
  _API_URL: string;
  accessToken = "";
  _refreshToken = "";

  _refreshInterval: NodeJS.Timeout | undefined;

  constructor(url: string) {
    this._API_URL = url;

    const localRefreshToken = localStorage.getItem("refreshToken");
    const tokenExists = localRefreshToken !== null && localRefreshToken;

    if (tokenExists) {
      this.refresh(localRefreshToken);
    }
  }

  refresh = async (token = this._refreshToken) => {
    clearTimeout(this._refreshInterval);

    const body = {
      refreshToken: token,
    };

    const response = await fetch(`${this._API_URL}/auth/refresh-token`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
      body: JSON.stringify(body),
    });

    console.log(response);

    if (response.status !== 200) {
      return { error: { message: "Wrong credentials" }, data: null };
    }

    const json = await response.json();
    const { accessToken, refreshToken } = json;

    this.saveToken({ refreshToken });
    this.registerRefresh(accessToken);

    return { data: { accessToken, refreshToken }, error: null };
  };

  private registerRefresh = (token: string) => {
    const { iat } = jwtDecode<{ iat: number }>(token);
    console.log(iat);

    this._refreshInterval = setTimeout(() => this.refresh(), iat - Date.now());
  };

  private saveToken = ({ refreshToken }: { refreshToken: string }) => {
    localStorage.setItem("refreshToken", refreshToken);
  };

  signIn = async (email: string, password: string) => {
    const body: AuthenticateBody = {
      email,
      password,
    };
    const response = await fetch(`${this._API_URL}/auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });

    if (response.status !== 200) {
      return { error: { message: "Wrong credentials" }, data: null };
    }

    const json = await response.json();
    const { accessToken, refreshToken } = json;

    this.accessToken = accessToken;
    this._refreshToken = refreshToken;

    this.saveToken(refreshToken);
    this.registerRefresh(accessToken);

    return { data: { accessToken, refreshToken }, error: null };
  };

  signUp = async (email: string, password: string) => {
    const body: AuthenticateBody = {
      email,
      password,
    };
    const response = await fetch(`${this._API_URL}/auth/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body),
    });

    if (response.status !== 200) {
      return { error: { message: "Wrong credentials" }, data: null };
    }

    const json = await response.json();
    const { accessToken, refreshToken } = json;

    this.accessToken = accessToken;
    this._refreshToken = refreshToken;

    this.saveToken(refreshToken);
    this.registerRefresh(accessToken);

    return { data: { accessToken, refreshToken }, error: null };
  };

  signOut = async () => {
    await fetch(`${this._API_URL}/auth/logout`, {
      method: "POST",
      headers: {
        Authorization: `Bearer ${this.accessToken}`,
      },
    });

    this.accessToken = "";
    this._refreshToken = "";

    return { error: null, data: null };
  };

  getUser = async () => {
    const response = await fetch(`${this._API_URL}/auth/whoami`, {
      headers: {
        Authorization: `Bearer ${this.accessToken}`,
      },
    });

    if (!response.ok) {
      return null;
    }

    const user = await response.json();
    return user;
  };
}
