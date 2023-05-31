<script setup lang="ts">
import { ref } from "vue";
import useUserStore from "@stores/UserStore";
const store = useUserStore();
const user = ref({
  name: "",
  email: "",
  password: "",
  adress: "",
  phone: "",
  role: "",
});

// Sends correct information to the UserStore which creates a user
async function submit() {
  console.log(
    user.value.name,
    user.value.email,
    user.value.password,
    user.value.adress,
    user.value.phone,
    user.value.role
  );
  store.createUser(
    user.value.name,
    user.value.email,
    user.value.password,
    user.value.adress,
    user.value.phone,
    user.value.role
  );
}
</script>

<template>
  <div class="pt-16 px-4">
    <form @submit.prevent="submit" class="p-4 border-2 border-black rounded-lg">
      <h3 class="mb-3">Sign Up</h3>

      <input v-model="user.name" placeholder="Name" type="text" />

      <input v-model="user.email" placeholder="Email" type="email" />

      <input v-model="user.password" placeholder="Password" type="password" />

      <input v-model="user.adress" placeholder="Adress" type="text" />

      <input v-model="user.phone" placeholder="Phonenumber" type="text" />

      <input type="radio" id="admin" v-model="user.role" value="admin" />
      <label for="admin">Admin</label>

      <input type="radio" id="customer" v-model="user.role" value="customer" />
      <label for="customer">Customer</label>

      <button type="submit" class="bg-black text-white ml-2">Register</button>
    </form>
  </div>
</template>

<style scoped>
::placeholder {
  color: black;
}

input {
  border: solid 1px black;
  border-radius: 0.5rem;
  width: 98%;
  margin: 0.5rem;
  padding: 0.2rem 0.6rem;
}

#admin {
  width: 10px;
}

#customer {
  width: 10px;
}
</style>
