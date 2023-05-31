<script setup lang="ts">
import useLineItemStore from "@stores/LineItemStore";

const lineItemStore = useLineItemStore();
const props = defineProps<{
  id: number;
  fullProduct: string;
  price: number;
}>();

defineExpose({
  props,
});

// Removes a line item based on it's ID
function removeLineItem() {
  lineItemStore.deleteLineItem(props.id);
}
</script>

<template>
  <q-card flat class="line-item-card fit">
    <q-card-section class="q-pa-sm bg-blue-grey-7">
      <p class="text-h6">{{ props.fullProduct }}</p>
      <q-card-actions class="">
        <q-btn @click="removeLineItem">Remove product</q-btn>
      </q-card-actions>
    </q-card-section>
  </q-card>
</template>

<style scoped></style>
