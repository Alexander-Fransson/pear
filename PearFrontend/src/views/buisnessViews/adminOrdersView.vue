<script setup lang="ts">
import OrderComponent from "@shopComponents/OrderComponent.vue";
import SideNavbarComponentVue from "@generalComponents/SideNavbarComponent.vue";
import useOrderStore from "@stores/OrderStore";
// import { ref } from "vue";
// import useUserStore from "@stores/UserStore";

// const store = useUserStore();
const orderStore = useOrderStore();
orderStore.fetchOrders();
</script>

<template>
  <SideNavbarComponentVue>
    <div class="q-pa-md">
      <div class="row" style="align-items: center; justify-content: center">
        <div class="col" style="max-width: 350px">
          <h4 class="mb-3">Packaging</h4>
        </div>
        <div
          class="row"
          style="margin-top: 10px; text-align: center; justify-content: center"
        >
          <div class="col" style="max-width: 350px">
            <OrderComponent
              v-for="order in orderStore.getOrdersFromStatus('inspection')"
              :key="order.id"
              :id="order.id"
              :price="order.totalPrice"
              :address="order.userAddress"
              :date="order.orderDate"
              :status="order.status"
            ></OrderComponent>
          </div>
          <div class="col" style="max-width: 350px">
            <OrderComponent
              v-for="order in orderStore.getOrdersFromStatus('beingDelivered')"
              :key="order.id"
              :id="order.id"
              :price="order.totalPrice"
              :address="order.userAddress"
              :date="order.orderDate"
              :status="order.status"
            ></OrderComponent>
          </div>
          <div class="col" style="max-width: 350px">
            <OrderComponent
              v-for="order in orderStore.getOrdersFromStatus('delivered')"
              :key="order.id"
              :id="order.id"
              :price="order.totalPrice"
              :address="order.userAddress"
              :date="order.orderDate"
              :status="order.status"
            ></OrderComponent>
          </div>
          <div class="col" style="max-width: 350px">
            <OrderComponent
              v-for="order in orderStore.getOrdersFromStatus('paid')"
              :key="order.id"
              :id="order.id"
              :price="order.totalPrice"
              :address="order.userAddress"
              :date="order.orderDate"
              :status="order.status"
            ></OrderComponent>
          </div>
        </div>
      </div>
      <div
        class="row"
        style="margin-top: 10px; text-align: center; justify-content: center"
      >
        <div class="col" style="max-width: 350px">
          <OrderComponent
            v-for="order in orderStore.getOrdersFromStatus('packaging')"
            :key="order.id"
            :id="order.id"
            :customerId="order.customerId"
            :price="order.totalPrice"
            :adress="order.userAddress"
            :date="order.orderDate"
            :status="order.status"
          ></OrderComponent>
        </div>
        <div class="col" style="max-width: 350px">
          <OrderComponent
            v-for="order in orderStore.getOrdersFromStatus('beingDelivered')"
            :key="order.id"
            :id="order.id"
            :customerId="order.customerId"
            :price="order.totalPrice"
            :adress="order.userAddress"
            :date="order.orderDate"
            :status="order.status"
          ></OrderComponent>
        </div>
        <div class="col" style="max-width: 350px">
          <OrderComponent
            v-for="order in orderStore.getOrdersFromStatus('delivered')"
            :key="order.id"
            :id="order.id"
            :customerId="order.customerId"
            :price="order.totalPrice"
            :adress="order.userAddress"
            :date="order.orderDate"
            :status="order.status"
          ></OrderComponent>
        </div>
        <div class="col" style="max-width: 350px">
          <OrderComponent
            v-for="order in orderStore.getOrdersFromStatus('paid')"
            :key="order.id"
            :id="order.id"
            :customerId="order.customerId"
            :price="order.totalPrice"
            :adress="order.userAddress"
            :date="order.orderDate"
            :status="order.status"
          ></OrderComponent>
        </div>
      </div>
    </div>
  </SideNavbarComponentVue>
</template>

<style scoped>
.col {
  border: 2px solid black;
  border-radius: 5px;
  background-color: white;
}
</style>
