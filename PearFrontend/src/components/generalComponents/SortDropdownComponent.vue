<script setup lang="ts">
import { ref } from "vue";

const emit = defineEmits(["updateSort"]);
const setting = ref("");
// Emits the select sort method to parent component/view
const changeSort = () => {
  emit("updateSort", setting.value);
};
</script>
<template>
  <label for="sort">Sort by:</label>
  <select name="sort" @change="changeSort" v-model="setting" class="mb-4">
    <option value="Product Name A-Z">Product Name A-Z</option>
    <option value="Product Name Z-A">Product Name Z-A</option>
    <option value="Price Low-High">Price Low-High</option>
    <option value="Price High-Low">Price High-Low</option>
  </select>
</template>

<style scoped>
label {
  margin-right: 1rem;
  margin-left: 1rem;
}
</style>
