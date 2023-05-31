import { defineStore } from "pinia";

interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  discount: number;
  category: string;
  amountInStorage: number;
  weight: number;
  dimensions: string;
  optionGroups: any;
}

const baseURL = "http://localhost:5187/api";

export const useProductStore = defineStore("ProductStore", {
  state: () => {
    return { products: [] as IProduct[], currentProduct: {} as IProduct };
  },
  actions: {
    async fetchProducts() {
      const response = await fetch(`${baseURL}/Product`);
      const data = await response.json();
      this.products = data;
    },
    async fetchProductFromId(id: number) {
      const response = await fetch(`${baseURL}/Product/${id}`);
      const data = await response.json();
      this.currentProduct = data;
    },
  },
  getters: {
    getProducts(): IProduct[] {
      return this.products;
    },
    getCurrentProduct(): IProduct {
      return this.currentProduct;
    },
    getProductFromId() {
      return (id: number) =>
        this.products.find((product) => product.id === id)!;
    },
  },
});

export default useProductStore;
