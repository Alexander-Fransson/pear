<script setup lang="ts">
import NavbarComponent from "@generalComponents/NavbarComponent.vue";
import FooterComponent from "@generalComponents/FooterComponent.vue";
</script>

<template>
  <NavbarComponent />
  <main>
    <router-view :key="$route.path"></router-view>
  </main>
  <FooterComponent />
</template>

<style scoped></style>
