<script setup lang="ts">
const emit = defineEmits(["selected"]);

const props = defineProps<{ data: any }>();

// Clears all selections when component is loaded in
props.data.optionRelations.forEach((element: any) => {
  element.option.selected = false;
});

// Updates selected option and clears the rest
const select = (option: any) => {
  props.data.optionRelations.forEach((element: any) => {
    if (element.option !== option) element.option.selected = false;
  });
  option.selected = !option.selected;
  emit("selected", option);
};
</script>

<template>
  <h4>{{ props.data.type[0].toUpperCase() + props.data.type.slice(1) }}</h4>
  <label
    v-for="version in props.data.optionRelations"
    :for="props.data.type"
    :key="version.option.id"
    class="rounded-lg border-solid border-4 hover:border-black flex flex-col p-2 mb-2"
    :class="{
      'border-black': version.option.selected,
      'text-bold': version.option.selected,
    }"
    @click="select(version.option)"
  >
    {{ version.option.name }}
    <br />
    {{
      new Intl.NumberFormat("se-SE", {
        style: "currency",
        currency: "SEK",
      }).format(version.option.price)
    }}
  </label>
</template>

<style scoped></style>
