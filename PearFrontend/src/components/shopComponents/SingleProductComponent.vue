<script setup lang="ts">
import { ref } from "vue";
import SelectionComponent from "@shopComponents/SelectionComponent.vue";
import useProductStore from "@stores/ProductStore";
import useCartStore from "@/stores/CartStore";

const productStore = useProductStore();
const product = productStore.getCurrentProduct;
const cartStore = useCartStore();

const reviewsHidden = ref(true);
// Toggles if reviews are hidden or not
function toggleReviews() {
  reviewsHidden.value = !reviewsHidden.value;
}

function addProduct(options: any) {
  options.name = product.name;
  options.price = product.price * (product.discount / 100);
  options.config.forEach((item: any) => {
    options.price += item.price;
  });
  options.price *= options.amount;
  cartStore.addToCart(options);
}

let reviewCount = ref(0);

let reviews = [
  { id: 0, rating: 5, title: "Bra", description: "funkar bra", name: "Daniel" },
  {
    id: 1,
    rating: 4,
    title: "toppen dator!",
    description: "funkar bra",
    name: "John Doe",
  },
  {
    id: 2,
    rating: 5,
    title: "fina specs",
    description: "funkar bra",
    name: "Jane Doe",
  },
  {
    id: 3,
    rating: 4,
    title: "bra att ha",
    description: "funkar bra",
    name: "Test Testsson",
  },
  {
    id: 4,
    rating: 1,
    title: "dålig dator",
    description: "tyvärr",
    name: "Gurka Testsson",
  },
  {
    id: 5,
    rating: 1,
    title: "nej bara nej",
    description: ":(",
    name: "William Testsson",
  },
  {
    id: 6,
    rating: 3,
    title: "Bra",
    description: "helt ok",
    name: "Test Testsson",
  },
];

// Fetches new reviews
const getReviews = () => {
  let newReviews = [];
  for (let i = 0; i < 2; i++) {
    if (reviewCount.value == reviews.length) {
      return newReviews;
    } else {
      newReviews.push(reviews.at(reviewCount.value));
    }
    reviewCount.value += 1;
  }
  return newReviews;
};
let loadedReviews = ref(getReviews());

// Load more reviews based on total amount of reviews
const loadMoreReviews = () => {
  if (loadedReviews.value.length < reviews.length) {
    let newReviews = getReviews();
    loadedReviews.value.push(...newReviews);
  }
};
</script>

<template>
  <section class="flex flex-col w-full justify-center">
    <section class="flex flex-row bg-[#f8f8f8] justify-evenly gap-x-10 p-20">
      <section class="flex flex-col max-w-2xl">
        <div class="containerDesktop flex flex-col gap-y-20">
          <img
            class="object-contain rounded-2xl"
            src="img/pearbook.jpeg"
            alt="Product visualisation"
          />
          <img
            class="object-contain rounded-2xl"
            src="img/pearbook-side.jpg"
            alt="Product visualisation"
          />
        </div>

        <div class="containerMobile flex flex-col">
          <img
            class="object-contain rounded-2xl"
            src="img/pearbook.jpeg"
            alt="Product visualisation"
          />
        </div>
      </section>

      <section class="flex flex-col gap-y-5">
        <div class="bg-[#f8f8f8]">
          <section class="flex flex-col gap-y-10 max-w-xl text-black">
            <h1>{{ product.name }}</h1>
            <h4>
              fr.
              {{
                new Intl.NumberFormat("se-SE", {
                  style: "currency",
                  currency: "SEK",
                }).format(product.price * (product.discount / 100))
              }}
            </h4>
            <p>{{ product.description }}</p>
          </section>
          <SelectionComponent
            class="text-black"
            @addToCart="addProduct"
            :optionGroups="product.optionGroups"
          />
          <h3 class="text-black">Reviews</h3>
        </div>
        <section>
          <section class="flex flex-col flex-start gap-y-5">
            <div>
              <button
                @click="toggleReviews"
                class="text-[#06c] font-bold hover:border-transparent py-2 px-4 focus:outline-none"
              >
                {{ reviewsHidden ? "+ Show all reviews" : "- Hide reviews" }}
              </button>
            </div>
            <section
              class="reviews flex flex-col gap-y-5 z-10"
              :class="{
                reviewsHidden: reviewsHidden,
                reviewsDisplayed: !reviewsHidden,
              }"
            >
              <TransitionGroup
                name="list"
                tag="ul"
                class="flex flex-col gap-y-5 z-0"
              >
                <li
                  v-for="review in loadedReviews"
                  v-bind:key="review!.id"
                  class="review bg-white rounded-lg p-5"
                >
                  <div class="flex items-center mb-1">
                    <svg
                      v-for="n in review!.rating"
                      :key="n"
                      aria-hidden="true"
                      class="w-5 h-5 text-yellow-400"
                      fill="currentColor"
                      viewBox="0 0 20 20"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <title>First star</title>
                      <path
                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"
                      ></path>
                    </svg>
                    <svg
                      v-for="n in 5 - review!.rating"
                      :key="n"
                      aria-hidden="true"
                      class="w-5 h-5 text-gray-400"
                      fill="currentColor"
                      viewBox="0 0 20 20"
                      xmlns="http://www.w3.org/2000/svg"
                    >
                      <title>First star</title>
                      <path
                        d="M9.049 2.927c.3-.921 1.603-.921 1.902 0l1.07 3.292a1 1 0 00.95.69h3.462c.969 0 1.371 1.24.588 1.81l-2.8 2.034a1 1 0 00-.364 1.118l1.07 3.292c.3.921-.755 1.688-1.54 1.118l-2.8-2.034a1 1 0 00-1.175 0l-2.8 2.034c-.784.57-1.838-.197-1.539-1.118l1.07-3.292a1 1 0 00-.364-1.118L2.98 8.72c-.783-.57-.38-1.81.588-1.81h3.461a1 1 0 00.951-.69l1.07-3.292z"
                      ></path>
                    </svg>
                    <h3
                      class="ml-2 text-sm font-semibold text-gray-900 dark:text-white"
                    >
                      - {{ review!.name }}
                    </h3>
                  </div>
                  <h4>{{ review!.title }}</h4>
                  <p>{{ review!.description }}</p>
                </li>
              </TransitionGroup>
            </section>
            <section
              class="flex flex-row"
              :class="{
                moreReviewsHidden: reviewsHidden,
                moreReviewsDisplayed: !reviewsHidden,
              }"
            >
              <button
                @click="loadMoreReviews()"
                class="text-[#06c] hover:border-transparent py-2 px-4 focus:outline-none"
              >
                {{
                  reviewCount != reviews.length
                    ? "More reviews"
                    : "All reviews are displayed"
                }}<span id="uparrow"></span>
              </button>
            </section>
          </section>
        </section>
      </section>
    </section>
  </section>
</template>

<style scoped>
@media (min-width: 1450px) {
  .containerMobile {
    display: none;
  }
}

@media (max-width: 1450px) {
  .containerDesktop {
    display: none;
  }
}

.list-enter-active,
.list-leave-active {
  transition: all 1s ease;
}
.list-enter-from,
.list-leave-to {
  opacity: 0;
  transform: translateY(5px);
}

.reviewsHidden {
  visibility: hidden;
  opacity: 0;
  transform: translatey(-300px);
  transition-duration: 1s;
}

.reviewsDisplayed {
  visibility: visible;
  opacity: 1;
  transform: translatey(5px);
  transition: all 1s ease;
}

.moreReviewsHidden {
  opacity: 0;
  transition-duration: 0.8s;
}

.moreReviewsDisplayed {
  opacity: 1;
  transition-duration: 0.8s;
}

#uparrow:before {
  content: "\276F";
}

#uparrow {
  font-size: 15px;
  display: inline-block;
  -ms-transform: rotate(90deg);
  -webkit-transform: rotate(90deg);
  transform: rotate(90deg);
  padding: 10px;
}
</style>
