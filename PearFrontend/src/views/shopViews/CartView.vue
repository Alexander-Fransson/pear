<script setup lang="ts">
import CartComponent from "@shopComponents/CartComponent.vue";
</script>

<template>
  <section class="flex flex-col space-y-10">
    <section
      class="flex justify-center content-center items-center space-y-10 pt-10"
    >
      <section class="flex flex-col space-y-4 justify-center">
        <div class="flex flex-col space-y-2 text-center">
          <h3 class="font-medium">Review your cart</h3>
          <h5>Free delivery and free returns.</h5>
        </div>
        <div class="flex bg-[#f5f5f7] rounded-lg text-center m-1 w-90">
          <p class="text-black overflow-clip m-5">
            Items not purchased with a one time payment may be eligible for
            $108.25/mo.per month at 0% when you check out with Pear Card Monthly
            Installments.
          </p>
        </div>
      </section>
    </section>

    <CartComponent />
  </section>
</template>

<style scoped></style>
