<template>
  <h6>{{ optionGroup }}</h6>
  <div class="row">
    <q-card
      v-for="(value, index) in categories.options"
      :key="index"
      class="q-pa-sm"
      :class="{ invalidValue: categories.options[index].isInvalidValue }"
      style="max-width: 200px; margin: 2px"
    >
      <div class="row no-wrap">
        <q-input
          style="margin-right: 1rem"
          label="Name"
          v-model="categories.options[index].name"
          @update:model-value="() => categories.onValueChange()"
        ></q-input>
        <q-input
          label="Price"
          v-model="categories.options[index].price"
          @update:model-value="() => categories.onValueChange()"
        ></q-input>
      </div>
    </q-card>
  </div>
</template>

<script setup lang="ts">
import { ref } from "vue";

interface IOption {
  name: string;
  price: string;
  isInvalidValue: boolean;
}

class productOptions {
  options: IOption[];

  constructor() {
    this.options = [{ name: "", price: "", isInvalidValue: false }];
  }

  removeEmptyCards(input: IOption[]) {
    const length = input.length;

    if (length <= 1) {
      return input;
    }

    const output = input.filter((element) => {
      if (element.name !== "" || element.price !== "") {
        return element;
      }
    });
    return output;
  }

  InvalidValueCheck(input: IOption[]) {
    const numberCheck = new RegExp("[^0-9]");

    const output = input.map((element) => {
      element.isInvalidValue = numberCheck.test(element.price);
      return element;
    });
    return output;
  }

  addNewInputCardIfEmpty(input: IOption[]) {
    const index = this.options.length - 1;

    const isTextEmpty = this.options[index].name !== "";
    const isNumberEmpty = this.options[index].price !== "";
    const isEmpty = isTextEmpty && isNumberEmpty;
    if (isEmpty) {
      this.options.push({ name: "", price: "", isInvalidValue: false });
    }
    return input;
  }

  public onValueChange() {
    this.options = this.InvalidValueCheck(this.options);

    this.options = this.removeEmptyCards(this.options);

    this.options = this.addNewInputCardIfEmpty(this.options);

    emit("saveData", { optionGroup: optionGroup, options: this.options });
  }
}

const emit = defineEmits(["saveData"]);

const props = defineProps<{ optionGroup: string; importData: IOption[] }>();
const optionGroup = ref(props.optionGroup);
const importData = ref(props.importData);

const categories = ref(new productOptions());

categories.value.options = importData.value;
categories.value.onValueChange();
</script>

<style scoped>
.invalidValue {
  background-color: red;
}
</style>
