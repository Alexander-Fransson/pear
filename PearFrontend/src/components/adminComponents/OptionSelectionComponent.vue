<template>
  <div>
    <option-completed-component
      v-for="optionGroup in optionGroups"
      :key="optionGroup"
      :optionGroup="optionGroup"
      :importData="getCurrentDataIndex(optionGroup)"
      @options="getChosenOptions"
    ></option-completed-component>
  </div>
</template>

<script setup lang="ts">
// TODO:Use store for import
// import useAdminStore from '@stores/AdminStore';
import OptionCompletedComponent from "@adminComponents/OptionCompletedComponent.vue";
import { ref } from "vue";
import { Ref } from "vue";

// const store = useAdminStore()

interface IOption {
  id: number;
  type: string;
  name: string;
  price: string;
  selected: boolean;
}

interface ICurrentData {
  optionGroup: string;
  options: IOption[];
}

interface IDbFormat {
  id: number;
  name: string;
  price: number;
  type: string;
}

const onLoad = async () => {
  const dbData = await getAllOption();

  DbFormatToCurrentData(await dbData);
  createOptionGroupsFromCurrentData(currentData);
};

// Importing data

async function getAllOption() {
  const uri = "http://localhost:5187/api/Option";
  const data: Array<IDbFormat> = await (await fetch(uri)).json();
  return data;
}

function DbFormatToCurrentData(dbData: IDbFormat[]) {
  dbData.forEach((element) => {
    const index = currentData.findIndex((el) => {
      return el.optionGroup === element.type;
    });
    if (index === -1) {
      currentData.push({
        optionGroup: element.type,
        options: [
          {
            id: element.id,
            type: element.type,
            name: element.name,
            price: element.price.toString(),
            selected: false,
          },
        ],
      });
    } else {
      currentData[index].options.push({
        id: element.id,
        type: element.type,
        name: element.name,
        price: element.price.toString(),
        selected: false,
      });
    }
  });
}

function createOptionGroupsFromCurrentData(currentData: ICurrentData[]) {
  currentData.forEach((element) => {
    optionGroups.value.push(element.optionGroup.toLowerCase());
  });
}

// Import to component

function getCurrentDataIndex(optionGroup: string) {
  const index = currentData.findIndex((element) => {
    return element.optionGroup === optionGroup;
  });

  return currentData[index].options;
}

// emit options
function getChosenOptions(options: any) {
  changedOptions[options[0].type] = options;
  emit("options", changedOptions);
}

const changedOptions: any = {};
const emit = defineEmits(["options"]);
const optionGroups: Ref<Array<string>> = ref([]);
const currentData: ICurrentData[] = [];
onLoad();
</script>

<style scoped></style>
