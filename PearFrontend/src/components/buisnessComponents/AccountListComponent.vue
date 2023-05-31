<script setup lang="ts">
import SingleAccountComponentVue from "./SingleAccountComponent.vue";
import useUserStore from "@/stores/UserStore";

const store = useUserStore();
store.fetchUsers();
</script>

<template>
  <ul class="row">
    <SingleAccountComponentVue
      v-for="user in store.all"
      :id="user.id"
      :name="user.name"
      :email="user.email"
      :phone="user.phone"
      :adress="user.adress"
      :role="user.role"
      :lastSignIn="user.lastSignIn"
      :createdAt="user.createdAt"
      :updatedAt="user.updatedAt"
      :bannedUntil="user.bannedUntil"
      :key="user.id"
    ></SingleAccountComponentVue>
  </ul>
</template>

<style></style>
