import { S } from "chart.js/dist/chunks/helpers.core";
import { defineStore } from "pinia";

interface IOrder {
  id: number;
  customerId: number;
  totalPrice: number;
  userAddress: string;
  orderDate: string;
  status: string;
}

const baseURL = "http://localhost:5187/api";

export const useOrderStore = defineStore("OrderStore", {
  state: () => {
    return {
      orders: [] as IOrder[],
    };
  },
  actions: {
    async fetchOrders() {
      const response = await fetch(`${baseURL}/Orders`);
      const data = await response.json();
      this.orders = data;
    },
    async addOrder(
      customerId: number,
      totalPrice: number,
      userAdress: string,
      orderDate: Date
    ) {
      const response = await fetch(`${baseURL}/Orders`, {
        method: "POST",
        headers: {
          Accept: "application.json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          customerId: customerId,
          totalPrice: totalPrice,
          userAdress: userAdress,
          orderDate: orderDate,
          status: "packaging",
        }),
      });
      const data = await response.json();
      return data.id;
    },
    async updateOrderStatus(orderId: number, newStatus: string) {
      const orderToUpdate = this.orders.find((order) => order.id === orderId)!;
      const response = await fetch(`${baseURL}/Orders/${orderId}`, {
        method: "PUT",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          id: orderId,
          customerId: orderToUpdate.customerId,
          totalPrice: orderToUpdate.totalPrice,
          userAddress: orderToUpdate.userAddress,
          orderDate: orderToUpdate.orderDate,
          status: newStatus,
        }),
      });
      this.orders.find((order) => order.id === orderId)!.status = newStatus;
    },
    async deleteOrder(orderId: number) {
      const response = await fetch(`${baseURL}/Orders/${orderId}`, {
        method: "DELETE",
        headers: {
          Accept: "application.json",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ id: orderId }),
      });
      this.orders = this.orders.filter((order: IOrder) => order.id !== orderId);
    },
  },
  getters: {
    allOrders(): IOrder[] {
      return this.orders;
    },
    getOrderFromId() {
      return (orderId: number) =>
        this.orders.find((order: IOrder) => order.id === orderId)!;
    },
    getOrdersFromStatus() {
      return (orderStatus: string) =>
        this.orders.filter((order: IOrder) => order.status === orderStatus);
    },
  },
});

export default useOrderStore;
