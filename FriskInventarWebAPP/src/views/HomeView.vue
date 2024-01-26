<script setup>
import { onMounted } from 'vue';
import { getFridgeData, postFridgeData } from '../../api';
</script>

<template>
    <div class="home">
      <img src="C:\Users\Karina.Laursen\Documents\Skole\HF5\Tværfagligt_Projekt\Projekt\Img\fridge.png" style="float:right; padding-right: 100px; padding-top: 100px;">
    <h1 class="frontpageh1">Velkommen</h1>
    <p>Her kan du komme godt i gang med at få styr på dit køleskab.</p>
    <p v-for="(fridge, index) in fridgeData" :key="index">{{ `${index+1}: ${fridge.fridgeName}` }}</p>
    <button class="ready" @click="handleButtonClick">Kom i gang</button>
  </div>
</template>
<script>
export default {


  methods: {
    handleButtonClick: async function() {
      // show the user to the register tab
    }
  }
}
</script>
<style scoped>
h1.frontpageh1 {
  font-family: Inter;
  font-size: 70px;
  color: #D4F1F4;
  padding-top: 70px;
  padding-left: 80px;
  text-align: left;
}

p {
  font-size: 20px;
  color: #D4F1F4;
  padding-top: 30px;
  padding-left: 90px;
  font-family: Inter;
  text-align: left;
}

button.ready {
  font-family: Inter;
  text-decoration-color: #D4F1F4;
  border: none;
  color: white;
  padding: 15px 32px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
  background-color: #008CBA;
  border-radius: 20px;
  position: absolute;
  top: 400px;
  left: 80px;
}
</style>
