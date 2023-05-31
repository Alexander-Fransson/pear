<script setup lang="ts">
/*eslint-disable*/
//Some untyped variables here but hopefully it does not brake anything.

import { ref } from "vue";
import useCartStore from "@/stores/CartStore";
import useOrderStore from "@/stores/OrderStore";
import useLineItemStore from "@/stores/LineItemStore";
import router from "@/router";

const cartStore = useCartStore();
const orderStore = useOrderStore();
const lineStore = useLineItemStore();

let checkoutDetails = {
  firstName: null,
  lastName: null,
  email: null,
  adress: null,
  city: null,
};

const cart = cartStore.getCart();

// Updates total price based on cart
const total = ref(0);
cart.forEach((item) => (total.value += item.price));

// Creates order and lineitem(s) for that order and sends them to the backend
async function finalizeOrder() {
  const orderId = await orderStore.addOrder(
    0,
    Math.floor(total.value),
    checkoutDetails.adress!,
    new Date()
  );
  cart.forEach((item) => {
    const product = Object.values(item);
    let fullProduct = "";
    fullProduct += product[0] + " " + product[2];

    if (product[1].length > 0) {
      fullProduct += "(s) with ";
      product[1].forEach((spec) => {
        fullProduct += spec.name + " ";
      });
    }
    lineStore.addLineItem(orderId, fullProduct);
  });
  router.push(`/order/${orderId}`);
}
</script>

<template>
  <form @submit.prevent="finalizeOrder" class="flex flex-col sm:flex-row gap-6">
    <div>
      <div
        class="bg-blue-50 px-4 pt-10 pb-10 lg:mt-0 rounded-lg mt-10 max-h-[630px] overflow-y-scroll"
      >
        <p class="text-xl font-medium mb-3 text-black">Your articles</p>
        <p class="text-black text-bold text-lg">
          Total:
          {{
            new Intl.NumberFormat("se-SE", {
              style: "currency",
              currency: "SEK",
            }).format(total)
          }}
        </p>
        <div
          class="mt-8 rounded-lg border bg-white h-auto px-2 py-4"
          v-for="product in cart"
          :key="product.id"
        >
          <div
            class="flex flex-col rounded-lg bg-white sm:flex-row justify-center"
          >
            <img
              class="m-2 h-12 w-16 rounded-md border object-cover"
              src="https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/mbp-spacegray-select-202206"
              alt="Product visualisation"
            />
            <div class="flex w-full flex-col px-4 py-4 text-center">
              <span class="font-semibold text-black"> {{ product.name }} </span>
              <span class="float-right text-gray-800"
                >Amount: {{ product.amount }}</span
              >
              <span
                class="float-right text-gray-800"
                v-for="spec in product.config"
                :key="spec.type"
              >
                {{ spec.type[0].toUpperCase() + spec.type.slice(1) }} -
                {{ spec.name }}
              </span>
              <p class="mt-auto text-lg font-bold text-black">
                {{
                  new Intl.NumberFormat("se-SE", {
                    style: "currency",
                    currency: "SEK",
                  }).format(product.price)
                }}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="mt-10 bg-gray-50 px-4 pt-10 lg:mt-0 rounded-lg">
      <p class="text-xl font-medium mb-3 text-black">Details</p>

      <div class="flex flex-col sm:flex-row gap-3">
        <input
          v-model="checkoutDetails.firstName"
          type="text"
          id="first-name"
          name="first-name"
          class="w-full rounded-md border border-black px-4 py-3 pl-6 text-sm shadow-sm outline-none focus:border-black-500 bg-white text-black"
          placeholder="First name"
        />
        <input
          v-model="checkoutDetails.lastName"
          type="text"
          id="last-name"
          name="last-name"
          class="w-full rounded-md border border-black px-4 py-3 pl-6 text-sm shadow-sm outline-none focus:border-black-500 bg-white text-black"
          placeholder="Surname"
        />
      </div>

      <div class="relative mt-3">
        <input
          v-model="checkoutDetails.email"
          type="email"
          id="email"
          name="email"
          class="w-full rounded-md border border-black px-4 py-3 pl-11 text-sm shadow-sm outline-none focus:z-10 focus:border-black-500 bg-white text-black"
          placeholder="mail@example.com"
        />
        <div
          class="pointer-events-none absolute inset-y-0 left-0 inline-flex items-center px-3"
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            class="h-4 w-4 text-gray-400"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            stroke-width="2"
          >
            <path
              stroke-linecap="round"
              stroke-linejoin="round"
              d="M16 12a4 4 0 10-8 0 4 4 0 008 0zm0 0v1.5a2.5 2.5 0 005 0V12a9 9 0 10-9 9m4.5-1.206a8.959 8.959 0 01-4.5 1.207"
            />
          </svg>
        </div>
      </div>

      <input
        v-model="checkoutDetails.adress"
        type="text"
        id="street"
        name="street"
        class="w-full rounded-md border border-black px-4 py-3 pl-6 text-sm shadow-sm outline-none focus:z-10 mt-3 focus:border-black-500 bg-white text-black"
        placeholder="Address"
      />
      <input
        v-model="checkoutDetails.city"
        type="text"
        id="city"
        name="city"
        class="w-full rounded-md border border-black px-4 py-3 pl-6 text-sm shadow-sm outline-none focus:z-10 mt-3 focus:border-black-500 bg-white text-black"
        placeholder="City"
      />

      <div class="mt-5 grid gap-6">
        <p class="text-xl font-medium text-black">Payment Method</p>
        <div class="relative">
          <input
            class="peer hidden"
            id="payment_method"
            type="radio"
            name="radio"
            checked
          />
          <span
            class="peer-checked:border-gray-700 absolute right-4 top-1/2 box-content block h-3 w-3 -translate-y-1/2 rounded-full border-8 border-gray-300 bg-white"
          ></span>
          <label
            class="peer-checked:border-2 peer-checked:border-gray-700 peer-checked:bg-gray-50 flex cursor-pointer select-none rounded-lg border border-gray-300 p-4"
            for="payment_method"
          >
            <svg
              class="mt-2.5 h-6 w-6 text-gray-400"
              xmlns="http://www.w3.org/2000/svg"
              width="16"
              height="16"
              fill="currentColor"
              viewBox="0 0 16 16"
            >
              <path
                d="M11 5.5a.5.5 0 0 1 .5-.5h2a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-.5.5h-2a.5.5 0 0 1-.5-.5v-1z"
              />
              <path
                d="M2 2a2 2 0 0 0-2 2v8a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V4a2 2 0 0 0-2-2H2zm13 2v5H1V4a1 1 0 0 1 1-1h12a1 1 0 0 1 1 1zm-1 9H2a1 1 0 0 1-1-1v-1h14v1a1 1 0 0 1-1 1z"
              />
            </svg>
            <div class="ml-5">
              <span class="mt-2 font-semibold text-black"
                >Payment via invoice</span
              >
              <p class="text-slate-500 text-sm">
                - Will be sent to your e-mail
              </p>
            </div>
          </label>
        </div>

        <button
          class="bg-green-500 hover:bg-green-400 text-white font-bold py-2 px-4 border-b-4 border-green-700 hover:border-green-500 rounded mb-6"
        >
          Complete purchase
        </button>
      </div>
    </div>
  </form>
</template>

<style></style>
