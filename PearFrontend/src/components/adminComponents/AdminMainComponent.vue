<script setup lang="ts">
import { ref } from "vue";
import useAdminStore from "@stores/AdminStore";
import OptionSelectionComponent from "@adminComponents/OptionSelectionComponent.vue";
import Notify from "quasar";
import { useQuasar } from "quasar";
import { IOType } from "child_process";

interface iOptionGroupData {
  [key: string]: Array<string>;
}

interface IProduct {
  name: string;
  description: string;
  price: number;
  discount: number;
  category: string;
  amountInStorage: number;
  weight: number;
  dimensions: string;
  optionGroups: IOptionGroup[];
}

interface IOptionGroup {
  type: string;
  product: IProduct;
  optionRelation: IOptionRelation;
}

interface IOptionRelation {
  optionGroup: IOptionGroup;
  option: IOption;
}

interface IOption {
  id: number;
  type: string;
  name: string;
  price: number;
}

const inputs = ref({
  name: "",
  description: "",
  category: "",
  price: 0,
  discount: 0,
  vat: 0,
  amount: 0,
  articleNum: 0,
  weight: 0,
  dimensions: "",
});

const val = ref(false);
const options = ref(["Dator", "Tillbehör", "Annat"]);

const genericRule = [(val: string) => !!val || "Fältet är obligatoriskt"];
const dimensionRules = [
  (value: string) =>
    regexp.test(value.replace(/ /g, "")) ||
    "Måste följa formatet: längd x bredd x höjd",
];

const regexp = new RegExp("^\\d+(x\\d+){2}$");

const store = useAdminStore();

// Formating for submit
const chosenOptions: any = {};
function filterChosenOptions(changedOptions: any) {
  for (const optionGroupKey in changedOptions) {
    chosenOptions[optionGroupKey] = changedOptions[optionGroupKey].filter(
      (o: any) => {
        return o.selected === true;
      }
    );
  }
}

async function createProduct() {
  const product: IProduct = {
    name: inputs.value.name,
    description: inputs.value.description,
    price: inputs.value.price,
    discount: inputs.value.discount,
    category: inputs.value.category,
    amountInStorage: inputs.value.amount,
    weight: inputs.value.weight,
    dimensions: inputs.value.dimensions.replace(/ /g, ""),
    optionGroups: createOptionGroups(),
  };

  await store.createProduct(product);
  showNotif();
}

function createOptionGroups(): Array<any> {
  const optionGroups = [];
  for (const optionGroupType in chosenOptions) {
    if (chosenOptions[optionGroupType].length !== 0) {
      optionGroups.push({
        type: optionGroupType,
        optionRelations: createOptionRelations(chosenOptions[optionGroupType]),
      });
    }
  }
  return optionGroups;
}

function createOptionRelations(optionGroupType: Array<IOption>): Array<object> {
  const optionRelations: Array<object> = [];
  optionGroupType.forEach((option) => {
    optionRelations.push({ optionId: option.id });
  });
  return optionRelations;
}

const $q = useQuasar();

function showNotif() {
  $q.notify({
    type: "positive",
    message: "Product Created",
    color: "primary",
  });
}

function errorNotif() {
  $q.notify({
    type: "negative",
    message: "Please fill all the required field",
    color: "negative",
  });
}
</script>

<template>
  <div class="q-pa-md q-gutter-md">
    <q-card class="my-card">
      <h2 style="margin: 1rem">Lägg till ny produkt</h2>
      <q-separator></q-separator>

      <q-form @submit="createProduct" @validation-error="errorNotif">
        <q-card-section>
          <q-input
            ref="nameRef"
            filled
            v-model="inputs.name"
            label="Namn"
            :rules="genericRule"
          />
        </q-card-section>

        <q-card-section>
          <q-card class="my-card" flat bordered>
            <q-card-actions>
              <q-checkbox
                v-model="val"
                checked-icon="format_bold"
                unchecked-icon="format_bold"
                indeterminate-icon="help"
              />
              <q-checkbox
                v-model="val"
                checked-icon="border_color"
                unchecked-icon="border_color"
                indeterminate-icon="help"
              />
              <q-checkbox
                v-model="val"
                checked-icon="format_italic"
                unchecked-icon="format_italic"
                indeterminate-icon="help"
              />
            </q-card-actions>
            <q-input
              v-model="inputs.description"
              filled
              square
              autogrow
              label="Beskrivning"
            />
          </q-card>
        </q-card-section>

        <q-card-section>
          <h4>Produktbilder</h4>
        </q-card-section>

        <q-card-section>
          <h4>Kategori</h4>
          <q-select
            outlined
            v-model="inputs.category"
            :options="options"
            filled
            label="Kategori"
            style="max-width: 300px"
          />
        </q-card-section>

        <q-card-section class="row">
          <div class="col">
            <h4>Pris</h4>

            <q-input
              v-model="inputs.price"
              filled
              square
              style="margin-right: 1rem"
              mask="######"
              :rules="genericRule"
            />
          </div>
          <div class="col">
            <h4>Rea Pris</h4>
            <q-input
              v-model="inputs.discount"
              filled
              square
              style="margin-right: 1rem"
              mask="##"
              suffix="%"
            />
          </div>
          <div class="col">
            <h4>Moms %</h4>
            <q-input
              v-model="inputs.vat"
              filled
              square
              style="margin-right: 1rem"
            />
          </div>
        </q-card-section>

        <q-card-section class="row">
          <div class="col">
            <h4>Lager</h4>
            <q-input
              v-model="inputs.amount"
              filled
              square
              label="Antal"
              style="margin-right: 1rem"
              :rules="genericRule"
            />
          </div>
          <div class="col">
            <h4>Artikelnummer</h4>
            <q-input
              v-model="inputs.articleNum"
              filled
              square
              style="margin-right: 1rem"
              :rules="genericRule"
            />
          </div>
        </q-card-section>

        <q-card-section>
          <h4>Frakt</h4>
          <div class="row">
            <div class="col">
              <q-input
                v-model="inputs.weight"
                filled
                square
                label="Vikt"
                style="margin-right: 1rem"
                mask="#####"
                suffix="g"
                :rules="genericRule"
              />
            </div>
            <div class="col">
              <q-input
                v-model="inputs.dimensions"
                filled
                square
                label="Dimensioner"
                style="margin-right: 1rem"
                fill-mask=""
                :rules="dimensionRules"
                lazy-rules
              />
            </div>
          </div>
        </q-card-section>

        <q-card-section class="variants">
          <h4>Varianter</h4>
          <option-selection-component
            @options="filterChosenOptions"
          ></option-selection-component>
        </q-card-section>
        <q-btn label="Edit options" to="/admin/options" />
        <q-btn label="Submit" type="submit" />
      </q-form>
    </q-card>
  </div>
</template>

<style scoped>
h4 {
  margin-bottom: 1rem;
  font-weight: normal;
}

div {
  text-align: left;
}
</style>
