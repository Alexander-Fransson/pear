import { defineStore } from "pinia";

interface ILineItem {
  id: number;
  orderId: number;
  fullProduct: string;
  uri: string;
}

const baseURL = "http://localhost:5187/api";

export const useLineItemStore = defineStore("LineItemStore", {
  state: () => {
    return {
      lineItems: [] as ILineItem[],
    };
  },
  actions: {
    async fetchLineItems() {
      const response = await fetch(`${baseURL}/LineItem`);
      const data = await response.json();
      this.lineItems = data;
    },
    async addLineItem(orderId: number, fullProduct: string) {
      const response = await fetch(`${baseURL}/LineItem`, {
        method: "POST",
        headers: {
          Accept: "application.json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          orderID: orderId,
          fullProduct: fullProduct,
          uri: "i-am-a-value-that-will-not-persist",
        }),
      });
      const data = await response.json();
      this.lineItems.push(data as ILineItem);
    },
    async deleteLineItem(lineItemId: number) {
      const response = await fetch(`${baseURL}/LineItem/${lineItemId}`, {
        method: "DELETE",
        headers: {
          Accept: "application.json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ id: lineItemId }),
      });
      this.lineItems = this.lineItems.filter(
        (LineItem: ILineItem) => LineItem.id !== lineItemId
      );
    },
  },
  getters: {
    allLineItems(): ILineItem[] {
      return this.lineItems;
    },
    getLineItemsFromId() {
      return (lineItemId: number) =>
        this.lineItems.filter(
          (lineItem: ILineItem) => lineItem.id === lineItemId
        );
    },
    getLineItemsFromOrderId() {
      return (orderId: number) =>
        this.lineItems.filter(
          (lineItem: ILineItem) => lineItem.orderId === orderId
        );
    },
  },
});

export default useLineItemStore;
