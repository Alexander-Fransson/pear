<script setup lang="ts">
import Chart from "chart.js/auto";
import { onMounted } from "vue";

onMounted(() => {
  const ctx = new Chart(
    document.getElementById("myChart") as HTMLCanvasElement,
    {
      type: "line",
      data: {
        labels: [
          "Monday",
          "Thusday",
          "Wednesday",
          "Thursday",
          "Friday",
          "Saturday",
          "Sunday",
        ],
        datasets: [
          {
            label: "Products sold",
            data: [12, 19, 3, 300, 2, 3],
            borderWidth: 1,
          },
        ],
      },
      options: {
        scales: {
          y: {
            beginAtZero: true,
          },
        },
      },
    }
  );
});
</script>

<template>
  <div>
    <canvas
      class="flex justify-center align-center mt-10"
      id="myChart"
      width="100px"
      height="100px"
    ></canvas>
  </div>
</template>

<style scoped>
div {
  width: 650px;
  height: 400px;
}
</style>
