<script setup lang="ts">
import { ref } from "vue";
import useOrderStore from "@stores/OrderStore";

const orderStore = useOrderStore();
orderStore.fetchOrders();

const drawer = ref(false);
</script>

<template>
  <q-layout view="hHh Lpr lff" style="height: auto">
    <q-drawer
      v-model="drawer"
      show-if-above
      :width="150"
      :breakpoint="950"
      class="bg-primary text-white"
    >
      <q-scroll-area
        :content-style="{
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
        }"
        class="fit"
      >
        <q-list separator class="text-h5 text-weight-medium">
          <q-item
            class="q-my-xs"
            clickable
            v-ripple
            to="/adminOrder"
            :active="false"
          >
            <q-item-section> Orders </q-item-section>
          </q-item>

          <q-item
            class="q-my-xs"
            clickable
            v-ripple
            to="/accounts"
            :active="false"
          >
            <q-item-section> Accounts </q-item-section>
          </q-item>

          <q-item
            class="q-my-xs"
            clickable
            v-ripple
            to="/statistics"
            :active="false"
          >
            <q-item-section> Statistics </q-item-section>
          </q-item>

          <q-item
            class="q-my-xs"
            clickable
            v-ripple
            to="/admin"
            :active="false"
          >
            <q-item-section> Product </q-item-section>
          </q-item>
        </q-list>
      </q-scroll-area>
    </q-drawer>

    <q-page-container>
      <q-page padding style="position: relative">
        <slot />
      </q-page>
    </q-page-container>
  </q-layout>
</template>

<style scoped>
.menu-list {
  border-radius: 0 32px 32px 0;
}
</style>
