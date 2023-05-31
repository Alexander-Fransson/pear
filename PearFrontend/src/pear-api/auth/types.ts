export type PearResponse<T, K = null> = {
  data: T | null;
  error: K | null;
};

export type ResponsePromise<T, K = null> = Promise<PearResponse<T, K>>;

export type AuthError = {
  message: string;
};
export type AuthResponse = {
  accessToken: string;
  refreshToken: string;
};

export type AuthenticateBody = {
  email: string;
  password: string;
};

export type User = {
  id: number;
  email: string;
};
