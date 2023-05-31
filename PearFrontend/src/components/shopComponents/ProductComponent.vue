<script setup lang="ts">
import router from "@router";

const props = defineProps<{
  prodNumb: number;
  name: string;
  price: number;
  storage: number;
  discount: number;
}>();

const hasDiscount = props.discount !== 100;

// Redirect user when they click on "read more" based on the product's ID
const viewProduct = () => {
  router.push(`/product/${props.prodNumb}`);
};
</script>

<template>
  <div
    class="flex flex-col rounded-lg bg-white sm:flex-row items-center ml-2 mb-2 justify-center"
  >
    <img
      class="m-2 h-24 w-28 rounded-md border object-cover"
      src="https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/mbp-spacegray-select-202206"
      alt="Product visualisation"
    />
    <div class="flex w-full flex-col px-4 py-4">
      <span class="font-semibold text-black text-center">{{ props.name }}</span>
      <span class="float-right text-gray-800 text-center"
        >Amount: {{ props.storage }}</span
      >
      <p
        :class="{ 'line-through': hasDiscount }"
        class="text-lg font-bold text-black text-center"
      >
        {{
          new Intl.NumberFormat("se-SE", {
            style: "currency",
            currency: "SEK",
          }).format(props.price)
        }}
      </p>
      <p class="text-lg font-bold text-center text-red" v-if="hasDiscount">
        {{
          new Intl.NumberFormat("se-SE", {
            style: "currency",
            currency: "SEK",
          }).format(props.price * (props.discount / 100))
        }}
        {{ props.discount }}% off
      </p>
    </div>
    <button
      class="hover:bg-green-400 text-white font-bold py-2 px-4 border-b-4 hover:border-green-400 rounded mb-2"
      @click="viewProduct"
    >
      Learn more
    </button>
  </div>
</template>

<style scoped></style>
