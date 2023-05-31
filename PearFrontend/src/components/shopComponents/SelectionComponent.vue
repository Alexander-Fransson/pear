<script setup lang="ts">
import OptionComponent from "@shopComponents/OptionComponent.vue";

const props = defineProps<{ optionGroups: any }>();
const emit = defineEmits(["addToCart"]);

let selectedOptions: { [key: string]: number | string | null | [] } = {
  amount: 1,
  config: [],
};

// Emits selected options/configuration to parent component
const emitOptions = () => {
  emit("addToCart", selectedOptions);
  selectedOptions = {
    amount: 1,
    config: [],
  };
};

// Saves selected option
const selectOption = (option: any) => {
  if (option.selected) {
    selectedOptions.config = selectedOptions.config!.filter(
      (item) => item.type !== option.type
    );
    selectedOptions.config!.push({
      name: option.name,
      type: option.type,
      price: option.price,
    });
  } else {
    selectedOptions.config = selectedOptions.config!.filter(
      (item) => item.type !== option.type
    );
  }
};
</script>

<template>
  <div>
    <form @submit.prevent="emitOptions">
      <div class="z-20 mb-8">
        <h2 class="font-normal mb-4">Specifications</h2>
        <OptionComponent
          v-for="group in props.optionGroups"
          :data="group"
          :key="group.id"
          @selected="selectOption"
        />
        <label class="font-bold text-lg">Amount: </label>
        <select class="p-4 m-1 rounded-lg" v-model="selectedOptions['amount']">
          <option value="1">1</option>
          <option value="2">2</option>
          <option value="3">3</option>
          <option value="4">4</option>
          <option value="5">5</option>
          <option value="6">6</option>
          <option value="7">7</option>
          <option value="8">8</option>
          <option value="9">9</option>
          <option value="10">10</option>
        </select>

        <button
          class="w-full bg-blue-500 hover:bg-blue-400 text-white text-2xl font-bold py-2 px-8 border-b-4 rounded-lg mt-5 mb-3"
        >
          Add to cart
        </button>
      </div>
    </form>
  </div>
</template>
