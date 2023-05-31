<script setup lang="ts">
import { ref } from "vue";
import useUserStore from "@stores/UserStore";

const store = useUserStore();

const props = defineProps<{
  id: number;
  name: string;
  email: string;
  phone: string;
  role: string;
  lastSignIn: string;
  createdAt: string;
  updatedAt: string;
  bannedUntil: string;
}>();

let date = ref("");

// Deletes a user based on it's ID
function deleteUser() {
  store.deleteUser(props.id);
}

// Bans user based on it's ID
function banUser() {
  console.log(date.value);
  store.userBan(props.id, date.value);
}
</script>

<template>
  <div class="q-pa-md row items-start q-gutter-md">
    <q-card flat bordered class="my-card">
      <q-card-section class="text-center">
        <div class="text-h4">{{ name }}</div>
        <div class="text-h6">{{ role }}</div>
      </q-card-section>

      <q-separator inset />

      <q-card-section>
        <div class="text-weight-bold text-h6">Account Information:</div>
        <div class="q-pl-sm">Last Sign In: {{ lastSignIn }}</div>
        <div class="q-pl-sm">Account Created At: {{ createdAt }}</div>
        <div class="q-pl-sm">Last Update: {{ updatedAt }}</div>
        <div class="q-pl-sm" v-if="bannedUntil.length !== 0">
          User is banned until {{ bannedUntil }}
        </div>
        <div class="q-pl-sm" v-else>User is not banned</div>
        <q-btn
          no-caps
          dense
          flat
          color="primary"
          label="View Orders"
          class="q-pl-sm"
        />
        <!-- You can add  to="/api/..."  with the correct route to make the button link to that route (the link won't reload the page) -->
      </q-card-section>

      <q-separator inset />

      <q-card-section>
        <div class="text-weight-bold text-h6">Contact Information:</div>
        <div class="q-pl-sm">Email: {{ email }}</div>
        <div class="q-pl-sm">Phone: {{ phone }}</div>
      </q-card-section>

      <q-separator />

      <q-expansion-item
        expand-separator
        icon="perm_identity"
        label="Account actions"
        :caption="name"
        header-class="bg-grey-2"
        dense
        class="q-pb-xs"
      >
        <q-card class="bg-grey-2">
          <q-card-section class="text-center q-pb-md">
            <q-btn
              @click="deleteUser()"
              color="red"
              text-color="black"
              label="Delete Account"
            />
          </q-card-section>

          <q-separator inset />

          <q-card-section>
            <div class="q-pa-md">
              <div class="q-gutter-md row items-start">
                <q-date
                  v-if="bannedUntil.length === 0"
                  v-model="date"
                  minimal
                />
              </div>
            </div>
            <q-btn
              v-if="bannedUntil.length === 0"
              @click="banUser()"
              flat
              color="red"
              text-color="black"
              label="Ban user"
            />
            <q-btn
              v-else
              @click="banUser()"
              flat
              color="red"
              text-color="black"
              label="Unban user"
            />
          </q-card-section>
        </q-card>
      </q-expansion-item>
    </q-card>
  </div>
</template>

<style scoped>
.my-card {
  min-width: 24rem;
  max-width: 24rem;
  border: 1px solid black;
}
</style>
