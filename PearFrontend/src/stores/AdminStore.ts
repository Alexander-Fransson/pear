import { type } from "os";
import { defineStore } from "pinia";

interface IProduct {
  name: string;
  description: string;
  price: number;
  discount: number;
  category: string;
  amountInStorage: number;
  weight: number;
  dimensions: string;
  optionGroups: IOptionGroup[];
}

interface IOptionGroup {
  type: string;
  product: IProduct;
  optionRelation: IOptionRelation;
}

interface IOptionRelation {
  optionGroup: IOptionGroup;
  option: IOption;
}

interface IOption {
  type: string;
  name: string;
  price: number;
}

/*
todo lägg till role/value/type i option table och ändra cpu/minne/screensize/color från text input till dropdown select
*/

const baseURL = "http://localhost:5187/api/";
const header = {
  "Content-Type": "application/json",
  Authorization: "Bearer 12312dfsfq",
};

interface IDbFormat {
  id?: number;
  name: string;
  price: number;
  type: string;
}

const useAdminStore = defineStore("AdminStore", {
  state: () => {
    return {
      options: [] as IDbFormat[],
      filter: "",
    };
  },
  actions: {
    async createProduct(product: IProduct) {
      const resp = await fetch(baseURL + "Product", {
        method: "POST",
        headers: header,
        body: JSON.stringify(product),
      });
      return resp;
    },

    // Option Functions (CUD) : Create, Update, Delete

    async createOption(data: IDbFormat) {
      const resp = await fetch(baseURL + "Option", {
        method: "POST",
        headers: header,
        body: JSON.stringify({
          name: data.name,
          price: data.price,
          type: data.type,
        }),
      });

      console.log(
        "CreateProduct: " +
          data.name +
          " | price: " +
          data.price +
          " | status: " +
          resp.status
      );

      return resp;
    },

    async editExistingOption(data: IDbFormat) {
      const resp = await fetch(baseURL + `Option/${data.id}`, {
        method: "PUT",
        headers: header,
        body: JSON.stringify({
          id: data.id,
          type: data.type,
          name: data.name,
          price: data.price,
        }),
      });

      console.log(
        "EditExistingProduct: " +
          data.name +
          " | price: " +
          data.price +
          " | status: " +
          resp.status
      );

      return resp;
    },

    async deleteOption(data: IDbFormat) {
      const resp = await fetch(baseURL + `Option/${data.id}`, {
        method: "DELETE",
        headers: header,
        body: JSON.stringify({
          id: data.id,
        }),
      });

      console.log("DeletedProduct: " + data.name + " | status: " + resp.status);
    },

    async fetchOptions() {
      const resp = await (
        await fetch(baseURL + "Option", {
          method: "GET",
        })
      ).json();
      this.options = resp;
      return resp;
    },

    async fetchOptionGroups(id: number) {
      const resp = await (
        await fetch(baseURL + `OptionGroup/${id}`, {
          method: "GET",
        })
      ).json();
      return resp;
    },
  },
  getters: {},
});
export default useAdminStore;
