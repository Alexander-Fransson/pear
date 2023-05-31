export class PearOrders {
  private _getAccessToken: () => void;
  private _API_URL: string;

  constructor(apiUrl: string, getAccessToken: () => string) {
    this._API_URL = apiUrl;
    this._getAccessToken = getAccessToken;
  }

  // requests the orders for the signed in users
  getOrders = () => {
    const accessToken = this._getAccessToken();

    console.log(accessToken);
  };

  // posts an order to the database
  postOrder = () => {
    const accessToken = this._getAccessToken();

    fetch(`${this._API_URL}/api/Orders`, {
      method: "Post",
      headers: {
        Authorization: `Bearer ${accessToken}`,
      },
    });
    console.log(accessToken);
  };
}
