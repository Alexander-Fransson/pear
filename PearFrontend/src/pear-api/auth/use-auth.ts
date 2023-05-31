import { defineStore } from "pinia";
import { onMounted, ref } from "vue";
import { User } from "./types";

export const useAuth = defineStore("auth-store", () => {
  const currentUser = ref<User | null>(null);

  return {};
});
