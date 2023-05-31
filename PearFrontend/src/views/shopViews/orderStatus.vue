<script setup lang="ts">
import useOrderStore from "@/stores/OrderStore";
import { useRoute } from "vue-router";

const route = useRoute();

const orderStore = useOrderStore();
// Collects information on order specified in the URL
const orderInfo = orderStore.getOrderFromId(
  parseInt(route.params.id as string)
);
</script>

<template>
  <section class="flex flex-col space-y-10 justify-center content-center">
    <section
      class="flex justify-center content-center items-center space-y-10 pt-10"
    >
      <section class="flex flex-col space-y-4 justify-center items-center">
        <div class="flex flex-col space-y-2 text-center">
          <h3 class="font-medium">Your order has been confirmed!</h3>
          <h5>Order-ID: {{ orderInfo.id }}</h5>
          <h5>Total: {{ orderInfo.totalPrice }} SEK</h5>
          <h5>Date: {{ new Date(orderInfo.orderDate).toDateString() }}</h5>
          <h5>Status: {{ orderInfo.status }}</h5>
        </div>
        <div class="flex bg-[#87f182] rounded-lg text-center m-1 w-90">
          <p class="text-black overflow-clip m-5 text-bold">
            Your invoice is pending
          </p>
        </div>
      </section>
    </section>
  </section>
</template>

<style scoped></style>
