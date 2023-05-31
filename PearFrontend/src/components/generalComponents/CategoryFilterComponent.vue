<script setup lang="ts">
const props = defineProps<{ categories: Set<string> }>();
const emit = defineEmits(["ChangeCategory"]);
</script>

<template>
  <div class="card">
    <fieldset>
      <legend>Select a category:</legend>
      <div v-for="(category, index) in props.categories" :key="index">
        <input
          v-if="category === 'All'"
          type="radio"
          @click="emit('ChangeCategory', category)"
          name="category"
          :value="category"
          checked
        />
        <input
          v-else
          type="radio"
          @click="emit('ChangeCategory', category)"
          name="category"
          :value="category"
        />
        <label :for="category">{{ category }}</label>
      </div>
    </fieldset>
  </div>
</template>

<style scoped>
fieldset {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

legend {
  color: black;
}

p,
label {
  font: 1rem "Fira Sans", sans-serif;
  color: black;
}

input {
  margin: 0.4rem;
}

.card {
  background-color: white;
  border: 1px solid black;
  border-radius: 5px;
  padding: 10px;
  margin: 10px;
}
</style>
