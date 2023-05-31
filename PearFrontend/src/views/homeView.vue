<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";

const image = ref();
const images = ["PearStore_01.jpeg", "pears.jpeg"];
let interval: NodeJS.Timer;

// Randomly selects picture based on images array above
function changeImage() {
  image.value!.src = `img/${images[Math.floor(Math.random() * images.length)]}`;
}

// Starts an interval to change the image every 1.2 seconds
onMounted(() => {
  interval = setInterval(changeImage, 1200);
});

// Clears interval when you leave the page
onUnmounted(() => {
  clearInterval(interval);
});
</script>

<template>
  <header
    class="flex flex-col justify-center items-center pt-16 bg-[#f8f8f8] bg-fixed"
  >
    <h1
      class="bg-gradient-to-r from-black via-blue-600 to-violet-800 text-white px-2 font-medium rounded-lg"
    >
      PearBook Pro
    </h1>
    <h2
      class="text-slate-900 text-xl dark:text-black mt-5 font-medium tracking-tight"
    >
      The best PearBook we ever made
    </h2>
    <div class="flex justify-evenly w-40 m-5">
      <router-link
        class="text-base font-medium tracking-tight text-[#06c]"
        to="product/7"
        >Learn More ></router-link
      >
      <router-link
        class="text-base font-medium tracking-tight text-[#06c]"
        to="product/7"
        >Buy ></router-link
      >
    </div>
    <img class="object-scale-down h-60 w-45" src="img/pearbook.jpeg" alt="" />
  </header>

  <section class="items-center bg-[#f8f8f8]">
    <div class="flex items-center justify-center flex-col p-10">
      <h3 class="dark:text-black">Discover Pear</h3>
      <img
        class="object-scale-down h-60 w-45 m-10 rounded-lg transition ease-out duration-500"
        ref="image"
        src="img/PearStore_01.jpeg"
        alt=""
      />
    </div>
  </section>

  <section class="flex justify-center align-center mt-20">
    <div class="flex flex-row justify-evenly w-full">
      <div class="flex items-center flex-col">
        <img
          src="https://www.apple.com/v/mac/home/br/images/overview/buystrip/icon-delivery__cthkmrp24qoi_large.png"
          alt=""
        />
        <h3>Free delivery</h3>
        <p>we offer free delivery</p>
      </div>
      <div class="flex items-center flex-col">
        <img
          src="https://www.apple.com/v/business/mac/c/images/overview/icon_employee__ej0pwgttjh4y_large.png"
          alt=""
        />
        <h3>Get help buying</h3>
        <p>Have a question? Call us</p>
      </div>
      <div class="flex items-center flex-col">
        <img
          src="https://www.apple.com/v/business/mac/c/images/overview/icon_security__c9c5uydhkt6q_large.png"
          alt=""
        />
        <h3>SuperSafe</h3>
        <p>Pear's products are super safe</p>
      </div>
    </div>
  </section>

  <section class="flex flex-row justify-evenly items-center p-40 gap-y-10">
    <div class="flex items-center flex-col justify-center text-center gap-y-5">
      <h2 class="font-medium">
        Pear <span class="underline">Today.</span> Pear
        <span class="underline">Tomorrow.</span>
      </h2>
      <h4>
        We are always thinking about tomorrow. We strive to be climate neutral.
      </h4>
    </div>
    <div>
      <img
        class="object-scale-down h-90 w-80 rounded-[25px]"
        src="img/pear_climate.jpg"
        alt=""
      />
    </div>
  </section>

  <section class="flex justify-center align-center mt-20 flex-col">
    <h2 class="font-medium text-center">The products we recommend</h2>
    <section class="flex justify-center align-center mt-10">
      <div class="flex flex-row justify-evenly w-full">
        <div class="flex items-center flex-col">
          <img
            class="object-scale-down h-60 w-45 m-10 rounded-lg"
            src="img/pearbook.jpeg"
            alt=""
          />
          <h3>PearBook Pro</h3>
          <p>from 23 000kr</p>
        </div>
        <div class="flex items-center flex-col">
          <img
            class="object-scale-down h-60 w-45 m-10 rounded-lg"
            src="img/pearbook.jpeg"
            alt=""
          />
          <h3>PearBook Air</h3>
          <p>from 20 000kr</p>
        </div>
        <div class="flex items-center flex-col">
          <img
            class="object-scale-down h-60 w-45 m-10 rounded-lg"
            src="img/pearbook.jpeg"
            alt=""
          />
          <h3>PearBook</h3>
          <p>from 19 000kr</p>
        </div>
      </div>
    </section>
  </section>
</template>

<style scoped></style>
