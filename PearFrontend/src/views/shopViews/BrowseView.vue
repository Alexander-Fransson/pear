<script setup lang="ts">
import { useRoute } from "vue-router";
import { ref } from "vue";
import useProductStore from "@stores/ProductStore";
import CategoryFilterComponent from "@generalComponents/CategoryFilterComponent.vue";
import ProductComponent from "@shopComponents/ProductComponent.vue";
import SortDropdownComponent from "@generalComponents/SortDropdownComponent.vue";

interface IProduct {
  id: number;
  name: string;
  description: string;
  price: number;
  discount: number;
  category: string;
  amountInStorage: number;
  weight: number;
  dimensions: string;
}

const productStore = useProductStore();
const products: IProduct[] = productStore.getProducts;

// Creates unique list of all the categories in the database
const categories = new Set(["All"]);
products.forEach((product) => categories.add(product.category));
const currentCategory = ref("All");

// Reads current route and creates a visual for it
// EG. "products > example"
const route = useRoute();
const breadcrumb =
  route.path.charAt(1).toUpperCase() +
  route.path.slice(2).replaceAll("/", " > ");
const search = route.path.split("/").pop()!;

// Sorts all product based on selected option in select
const sort = (selected: string) => {
  switch (selected) {
    case "Product Name A-Z":
      products.sort((a, b) => (a.name > b.name ? 1 : b.name > a.name ? -1 : 0));
      break;
    case "Product Name Z-A":
      products.sort((a, b) => (a.name > b.name ? -1 : b.name > a.name ? 1 : 0));
      break;
    case "Price Low-High":
      products.sort((a, b) =>
        a.price * a.discount > b.price * b.discount
          ? 1
          : b.price * b.discount > a.price * a.discount
          ? -1
          : 0
      );
      break;
    case "Price High-Low":
      products.sort((a, b) =>
        a.price * a.discount > b.price * b.discount
          ? -1
          : b.price * b.discount > a.price * a.discount
          ? 1
          : 0
      );
      break;
  }
};

// Changes current category to update which products are showing
const updateCategory = (category: string) => {
  currentCategory.value = category;
};
</script>

<template>
  <h3 class="ml-4 pt-16">{{ breadcrumb }}</h3>
  <CategoryFilterComponent
    @change-category="updateCategory"
    :categories="categories"
  />
  <SortDropdownComponent @update-sort="sort" />
  <section class="flex flex-wrap justify-center">
    <ProductComponent
      v-for="product in products.filter(
        (prod) =>
          prod.name.toLowerCase().includes(search.toLowerCase()) &&
          (prod.category === currentCategory || currentCategory === 'All')
      )"
      :key="product.id"
      :prodNumb="product.id"
      :name="product.name"
      :price="product.price"
      :storage="product.amountInStorage"
      :discount="product.discount"
    />
  </section>
</template>

<style scoped>
h3 {
  margin-left: 1rem;
}
section {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
</style>
