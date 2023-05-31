<script setup lang="ts">
import { ref } from "vue";
import { pearApi } from "@/pear-api";
import { useRouter } from "vue-router";

const router = useRouter();

const email = ref("");
const password = ref("");

// Checks if it is possible to log in with data given by user
async function login() {
  const { data, error } = await pearApi.auth.signIn(
    email.value,
    password.value
  );

  if (data) {
    // test
    router.push("/account");
  }
}
</script>

<template>
  <div class="container pt-20">
    <q-card class="card">
      <q-form @submit.prevent="login">
        <q-input v-model="email" placeholder="Email" type="email" />

        <q-input v-model="password" placeholder="Password" type="password" />

        <q-btn type="submit" color="green" class="mt-2 full-width">Login</q-btn>
      </q-form>
    </q-card>
  </div>
</template>

<style scoped>
.container {
  flex: 1;
  display: flex;
  flex-direction: column;
  align-items: center;
}
.card {
  width: 100%;
  max-width: 30vw;
  margin: auto;
}
</style>
