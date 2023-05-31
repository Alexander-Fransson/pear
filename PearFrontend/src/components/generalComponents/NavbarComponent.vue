<script setup lang="ts">
import { ref } from "vue";
import router from "@/router";

const searchValue = ref("");
// Redirects user based on search input
const search = () => {
  if (searchValue.value !== "") router.push(`/products/${searchValue.value}`);
  else router.push("/");
};
// Redirects user to the home/start page if logo is clicked & clears search input value
const home = () => {
  searchValue.value = "";
  router.push("/");
};
// Redirects user to their "cart page" if shopping cart is clicked
const cart = () => {
  router.push("/cart");
};
</script>

<template>
  <nav class="flex justify-evenly content-center z-20">
    <img
      src="img/pear-logo-white-pear.png"
      alt="Pear logo"
      class="h-12 cursor-pointer p-1"
      @click="home"
    />
    <form @submit.prevent="search">
      <input type="text" placeholder="Search..." v-model="searchValue" />
    </form>
    <aside>
      <span class="material-symbols-outlined" @click="search"> search </span>
      <span class="material-symbols-outlined" @click="cart"> local_mall </span>
    </aside>
  </nav>
</template>

<style scoped>
@import "https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200";
nav {
  display: flex;
  border-bottom: 1px solid black;
  position: sticky;
  top: 0;
  width: 100vw;
  z-index: 1;
}
aside {
  display: flex;
  align-items: center;
  flex-direction: row;
  justify-content: space-around;
  height: 3rem;
}
nav,
input {
  background-color: white;
  color: black;
}
input {
  border: 2px solid black;
  border-radius: 5px;
  margin-top: 10px;
  width: 18rem;
}
span {
  cursor: pointer;
  font-size: 2rem;
}
</style>
