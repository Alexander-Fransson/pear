<template>
  <h6>{{ optionGroup }}</h6>
  <div class="row" style="cursor: pointer">
    <q-card
      @click="categories.toggleSelection(index)"
      v-for="(value, index) in categories.options"
      :key="index"
      class="q-pa-sm"
      :class="{ invalidValue: categories.options[index].selected }"
      style="margin: 2px"
    >
      <div class="row no-wrap">
        <span style="margin-right: 1rem"
          >Name:<br />{{ categories.options[index].name }}</span
        >
        <span>Price:<br />{{ categories.options[index].price }}</span>
      </div>
    </q-card>
  </div>
</template>

<script setup lang="ts">
import { ref, Ref } from "vue";

interface IOption {
  id: number;
  type: string;
  name: string;
  price: string;
  selected: boolean;
}

class productOptions {
  options: IOption[];

  constructor() {
    this.options = [
      { id: 0, type: optionGroup.value, name: "", price: "", selected: false },
    ];
  }

  toggleSelection(index: number) {
    this.options[index].selected = !this.options[index].selected;
    emit("options", this.options);
  }
}

const props = defineProps<{ optionGroup: string; importData: IOption[] }>();
const optionGroup = ref(props.optionGroup);
const importData = ref(props.importData);

const categories = ref(new productOptions());
categories.value.options = importData.value;
const emit = defineEmits(["options"]);
</script>

<style scoped>
.invalidValue {
  background-color: lime;
}
</style>
