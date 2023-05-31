import { PearAuth } from "./auth";
import { PearOrders } from "./orders";

export interface IPearApi {
  orders: {
    getOrders: () => void;
    postOrder: () => void;
  };
}

export class PearApi implements IPearApi {
  orders: PearOrders;
  auth: PearAuth;

  constructor(apiUrl: string) {
    this.auth = new PearAuth(apiUrl);

    const getAccessToken = () => this.auth.accessToken;
    this.orders = new PearOrders(apiUrl, getAccessToken.bind(this));
  }
}

export const pearApi = new PearApi("http://localhost:5187/api");
