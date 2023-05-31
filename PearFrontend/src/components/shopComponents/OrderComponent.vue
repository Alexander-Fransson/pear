<script setup lang="ts">
import useOrderStore from "@stores/OrderStore";
import LineItemComponent from "@shopComponents/LineItemComponent.vue";
import { ref, Ref } from "vue";
import Invoice from "@/invoice/index";
import useLineItemStore from "@/stores/LineItemStore";
import useUserStore from "@/stores/UserStore";

const userStore = useUserStore();
const lineItemStore = useLineItemStore();
const lineItemRefs: Ref<any[]> = ref([]);
const orderStore = useOrderStore();
const props = defineProps<{
  id: number;
  customerId: number;
  price: number;
  adress: string;
  date: string;
  status: string;
}>();
userStore.fetchUsers();

// Changes order status
function changeStatusToPackaging() {
  orderStore.updateOrderStatus(props.id, "packaging");
}

// Changes order status
function changeStatusToBeingDelivered() {
  orderStore.updateOrderStatus(props.id, "beingDelivered");
}

// Changes order status
function changeStatusToDelivered() {
  orderStore.updateOrderStatus(props.id, "delivered");
}

// Changes order status
function changeStatusToPaid() {
  orderStore.updateOrderStatus(props.id, "paid");
}

// Removes an order
function removeOrder() {
  if (lineItemRefs.value.length == 0) {
    orderStore.deleteOrder(props.id);
  } else {
    console.log("You can only remove empty orders!");
  }
}

// Generate invoice for customer in question
function generateInvoice() {
  const customerData = userStore.getUserFromId(props.customerId);
  const invoice = new Invoice(
    props.id,
    customerData!.name,
    customerData!.email,
    props.date,
    props.adress,
    customerData!.phone,
    Array.from(lineItemRefs.value)
  );
  invoice.create();
}
</script>

<template>
  <div class="q-pa-md" style="max-width: 350px">
    <q-list bordered class="rounded-borders">
      <q-expansion-item
        expand-separator
        icon="shopping_cart "
        :label="`order # ${props.id}`"
        :caption="props.date.split('T')[0]"
      >
        <q-card>
          <q-card-section>
            <h6>{{ props.adress }}</h6>
            <h6>{{ props.price }} kr</h6>
            <br />
            <LineItemComponent
              v-for="lineItem in lineItemStore.getLineItemsFromOrderId(
                props.id
              )"
              :key="lineItem.id"
              :id="lineItem.id"
              :full-product="lineItem.fullProduct"
              :price="Math.floor(Math.random() * 1000)"
              ref="lineItemRefs"
            ></LineItemComponent>
            <br />
            <q-btn color="red-14" label="Remove" @click="removeOrder" />
            <q-btn-dropdown color="primary" label="Edit">
              <q-list>
                <q-item
                  clickable
                  v-close-popup
                  @click="changeStatusToPackaging()"
                >
                  <q-item-section>
                    <q-item-label>Packaging</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item
                  clickable
                  v-close-popup
                  @click="changeStatusToBeingDelivered"
                >
                  <q-item-section>
                    <q-item-label>Being Delivered</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item
                  clickable
                  v-close-popup
                  @click="changeStatusToDelivered"
                >
                  <q-item-section>
                    <q-item-label>Delivered</q-item-label>
                  </q-item-section>
                </q-item>

                <q-item clickable v-close-popup @click="changeStatusToPaid">
                  <q-item-section>
                    <q-item-label>Paid</q-item-label>
                  </q-item-section>
                </q-item>
              </q-list>
            </q-btn-dropdown>
            <q-btn color="green" @click="generateInvoice()"
              >GENERATE INVOICE</q-btn
            >
          </q-card-section>
        </q-card>
      </q-expansion-item>
    </q-list>
  </div>
</template>

<style scoped></style>
