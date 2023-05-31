import { defineStore } from "pinia";
import { AddressbarColor } from "quasar";
interface UserList {
  id: number;
  name: string;
  email: string;
  phone: string;
  adress: string;
  role: string;
  lastSignIn: string;
  createdAt: string;
  updatedAt: string;
  bannedUntil: string;
  key: string;
}

const baseUrl = "http://localhost:5187/api";

const useUserStore = defineStore("UserStore", {
  state: () => {
    return {
      user: [] as UserList[],
    };
  },

  actions: {
    async fetchUsers() {
      const response = await fetch(`${baseUrl}/User`);
      const data = await response.json();
      this.user = data;
    },

    async createUser(
      name: string,
      email: string,
      password: string,
      adress: string,
      phone: string,
      role: string
    ) {
      const response = await fetch(`${baseUrl}/auth/register`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          name: name,
          email: email,
          password: password,
          adress: adress,
          phone: phone,
          role: role,
        }),
      });
    },
    async loginUser(email: string, password: string) {
      const response = await fetch(`${baseUrl}/auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
          password: password,
        }),
      });
    },

    async fetchSingleUser(id: number) {
      const response = await fetch(`${baseUrl}/User/${id}`);
      const data = await response.json();
      return data;
    },

    async userBan(id: number, date: string) {
      const item = this.user.find((x) => x.id === id);

      if (item === undefined) {
        throw new Error(`item with id=${id} not found`);
      }

      if (item?.bannedUntil.length > 0) {
        item.bannedUntil = "";
      } else {
        item.bannedUntil = date;
      }

      const response = await fetch(`${baseUrl}/User/${id}`, {
        method: "PUT",
        headers: {
          accept: "text/plain",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(item),
      });
      await this.fetchUsers();
    },

    async deleteUser(id: number) {
      const response = await fetch(`${baseUrl}/User/${id}`, {
        method: "DELETE",
        headers: {
          accept: "text/plain",
        },
      });
      await this.fetchUsers();
    },
  },
  getters: {
    all(): UserList[] {
      return this.user;
    },
    getUserFromId() {
      return (userId: number) => this.user.find((user) => user.id === userId);
    },
  },
});
export default useUserStore;
