import { defineStore } from "pinia";

const baseURL = "http://localhost:5187/api";

export const useCartStore = defineStore("CartStore", {
  state: () => {
    return {
      cart: [] as any[],
    };
  },
  actions: {
    getCart() {
      return this.cart;
    },
    addToCart(product: any) {
      this.cart.push(product);
    },
    removeFromCart(i: number) {
      this.cart = this.cart.filter((_product, index) => index !== i);
    },
  },
  getters: {},
});

export default useCartStore;
