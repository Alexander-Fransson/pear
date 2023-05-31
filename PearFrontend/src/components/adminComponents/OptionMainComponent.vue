<template>
  <div>
    <q-input v-model="newType"></q-input>
    <q-btn @click="createNewType()" label="Create"></q-btn>
    <option-component
      v-for="optionGroup in optionGroups"
      :key="optionGroup"
      :optionGroup="optionGroup"
      :importData="getCurrentDataIndex(optionGroup)"
      @saveData="updateData"
    ></option-component>

    <q-btn label="Back" to="/admin" />
    <q-btn @click="saveOptions" label="Save"></q-btn>
  </div>
</template>

<script setup lang="ts">
// TODO:Use store for import
import useAdminStore from "@stores/AdminStore";
import OptionComponent from "@adminComponents/OptionComponent.vue";
import { storeToRefs } from "pinia";
import { ref } from "vue";
import { Ref } from "vue";
import { findSourceMap } from "module";
import { Console } from "console";

const store = useAdminStore();

store.fetchOptions;

interface IOption {
  id: number;
  name: string;
  price: string;
  isInvalidValue: boolean;
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
  const dbData = await store.fetchOptions();

  //console.log(dbData)

  DbFormatToCurrentData(await dbData);

  console.log(currentData);

  createOptionGroupsFromCurrentData(currentData);

  currentDBData = currentData;

  currentDBData = currentDBData.flatMap((data) => {
    return {
      optionGroup: data.optionGroup,
      options: JSON.parse(JSON.stringify(data.options)),
    };
  });
};

// Importing data

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
            name: element.name,
            price: element.price.toString(),
            isInvalidValue: false,
          },
        ],
      });
    } else {
      currentData[index].options.push({
        id: element.id,
        name: element.name,
        price: element.price.toString(),
        isInvalidValue: false,
      });
    }
  });
}

function createOptionGroupsFromCurrentData(currentData: ICurrentData[]) {
  currentData.forEach((element) => {
    optionGroups.value.push(element.optionGroup.toLowerCase());
    console.log(element.optionGroup);
  });
}

// Import to component

function getCurrentDataIndex(optionGroup: string) {
  const index = currentData.findIndex((element) => {
    return element.optionGroup === optionGroup;
  });
  if (index === -1) {
    return [{ name: "", price: "", isInvalidValue: false }];
  } else {
    return currentData[index].options;
  }
}

// create new option type

function createNewType() {
  let alreadyExists = false;
  newType.value = newType.value.toLowerCase();
  optionGroups.value.forEach((group) => {
    if (group === newType.value) {
      alreadyExists = true;
      console.log("alreadyExists");
    }
  });
  if (!alreadyExists && newType.value.replaceAll(" ", "") !== "") {
    optionGroups.value.push(newType.value);
  }
}

// saving and updating

function updateData(input: ICurrentData) {
  const index = currentData.findIndex((element) => {
    return element.optionGroup === input.optionGroup;
  });
  if (index === -1) {
    currentData.push(input);
  } else {
    currentData[index].options = input.options;
  }
}

function isNotInvalidData() {
  const output = currentData.every((element) => {
    const errorCheck = element.options.every((el) => {
      return !el.isInvalidValue;
    });
    return errorCheck;
  });
  return output;
}

function finalDataConvertion(trimmedData: ICurrentData[]) {
  const finishedData = trimmedData.flatMap((element) => {
    const data2 = element.options.filter((el) => {
      if (el.name !== "" && el.price !== "") {
        return el;
      }
    });

    const data = data2.map((el) => {
      const output: IDbFormat = {
        id: el.id,
        name: el.name,
        price: parseInt(el.price),
        type: element.optionGroup,
      };

      return output;
    });

    return data;
  });

  return finishedData;
}

async function OptionCUD(
  incommingData: IDbFormat[],
  finishedDbData: IDbFormat[]
) {
  incommingData.forEach(async (data) => {
    let doesDataAlreadyExist = false;

    finishedDbData.forEach(async (option: any) => {
      if (option.id === data.id) {
        if (option.name === data.name && option.price === data.price) {
          doesDataAlreadyExist = true;
        } else {
          store.editExistingOption(data);
        }
      }
      if (option.name === data.name && option.type === data.type) {
        doesDataAlreadyExist = true;
      }
    });
    if (data.id === undefined && !doesDataAlreadyExist) {
      store.createOption(data);
    }
  });

  finishedDbData.forEach(async (option: any) => {
    let hasDataBeenDeleted = true;
    incommingData.forEach((data) => {
      if (data.id === option.id) {
        hasDataBeenDeleted = false;
      }
    });

    if (hasDataBeenDeleted) {
      store.deleteOption(option);
    }
  });
}

async function saveOptions() {
  if (isNotInvalidData()) {
    const finishedDbData = finalDataConvertion(currentDBData);
    const finishedData = finalDataConvertion(currentData);

    OptionCUD(finishedData, finishedDbData);
  }
}

let currentDBData: ICurrentData[] = [];
const newType = ref("");
const optionGroups: Ref<Array<string>> = ref([]);
let currentData: ICurrentData[] = [];
onLoad();
</script>

<style scoped></style>
