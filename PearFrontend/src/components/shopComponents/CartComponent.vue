<script setup lang="ts">
import router from "@router";
import { ref } from "vue";
import useCartStore from "@/stores/CartStore";

const cartStore = useCartStore();

let cart = cartStore.getCart();

// Updates total amount based on items added to the cart
const total = ref(0);
cart.forEach((item) => (total.value += item.price));

// Redirects user to checkout page
const checkout = () => {
  router.push("/checkout");
};

const remove = (index: number) => {
  cart = cart.filter((_product, i) => i !== index);
  cartStore.removeFromCart(index);
  total.value = 0;
  cart.forEach((item) => (total.value += item.price));
};
</script>

<template>
  <section class="flex justify-center w-screen pb-10" v-if="cart.length > 0">
    <section class="space-y-10 align-center w-200">
      <section class="flex flex-col space-y-10">
        <div
          v-for="(product, index) in cart"
          class="flex align-center w-200 space-x-10 justify-center sm:[justify-content:flex-start]"
          :key="product.name"
        >
          <div class="w-100">
            <img
              class="rounded-lg"
              src="https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/mbp-spacegray-select-202206"
              alt=""
            />
          </div>

          <section class="flex flex-row md:space-x-10 w-120 pl-10">
            <section class="flex flex-col space-y-3 justify-center">
              <h4>{{ product.name }}</h4>
              <p>Amount: {{ product.amount }}</p>
              <p
                class="w-60 overflow-clip"
                v-for="spec in product.config"
                :key="spec.type"
              >
                {{ spec.type[0].toUpperCase() + spec.type.slice(1) }} -
                {{ spec.name }}
              </p>
            </section>

            <section class="flex flex-col w-25 justify-center">
              <h4>
                {{
                  new Intl.NumberFormat("se-SE", {
                    style: "currency",
                    currency: "SEK",
                  }).format(parseInt(product.price))
                }}
              </h4>
              <a class="cursor-pointer" @click="remove(index)">Remove</a>
            </section>
          </section>
        </div>
      </section>

      <section class="flex flex-col justify-center">
        <section
          class="flex justify-center sm:[justify-content:flex-end!important;]"
        >
          <div class="space-y-6">
            <h4>
              Total:
              {{
                new Intl.NumberFormat("se-SE", {
                  style: "currency",
                  currency: "SEK",
                }).format(total)
              }}
            </h4>
            <button
              @click="checkout()"
              class="bg-blue-500 w-60 hover:bg-blue-400 text-white text-2xl font-bold py-2 px-8 border-b-4 rounded-lg mb-6"
            >
              Check out
            </button>
          </div>
        </section>
      </section>
    </section>
  </section>

  <section
    class="flex p-10 items-center text-center justify-center flex-col gap-6"
    v-else
  >
    <h3>There are no items in your cart</h3>
    <router-link to="/">
      <router-link
        class="bg-blue-500 w-60 hover:bg-blue-400 text-white text-2xl font-bold py-2 px-8 border-b-4 rounded-lg mb-6"
        to="/"
        >Back to shop</router-link
      >
    </router-link>
  </section>
</template>

<style scoped></style>
