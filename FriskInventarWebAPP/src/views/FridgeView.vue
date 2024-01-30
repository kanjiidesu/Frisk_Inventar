<script setup>
import { postFridgeData ,getFridgeData, getUser, postUserFridgeData, getUserFridgeData } from '../../api';
</script>
<template>
  <div id="app" class="fridge">
    <h1 class ="fridgetitle">Dit Køleskab</h1>
    <div v-for="userFridge in userFridges" :key="userFridge">
      <h3>{{ userFridge.fridge.fridgeName }}</h3>
      <div v-for="product in userFridge.fridge.products" :key="product">
        <p>{{ product.productName }}</p>
      </div>
    </div>
    <div v-if="showTextbox">
      <input v-model="fridgeNameInput" class="fridgeName" type="text" id="fridgeNameInput" placeholder="Skriv navn på køleskab..">
      <button class="addFridge" @click="addFridge()">Tilføj</button>
    </div>
    <button class="toggleFridgeTextbox" @click="toggleAddFridge">Tilføj nyt køleskab</button>
  </div>
</template>
<script>
export default {
  async mounted() {
    var fridge = await getFridgeData()
    this.fridgeName = fridge.fridgeName
    console.log(fridge)

    this.user = await getUser()
    console.log(this.user);

    if (this.user) {
      this.userFridges = await getUserFridgeData(this.user.userId)
    }
  },
  data() {
    // Data to be used in this class
    return {
      fridgeNameInput: "",
      userFridges: [],
      showTextbox: false,
      fridgeName: "",
      user: undefined
    }
  },
  methods: {
    toggleAddFridge: async function() {
      this.showTextbox = !this.showTextbox;
    },
    addFridge: async function() {
      var inputValue = document.getElementById("fridgeNameInput").value; 
      //TODO: How to push this fridgeId and userNameId that is logged in
      // After posting the fridge
      console.log("Input value: " + inputValue);
      let response = await postFridgeData(this.fridgeNameInput)
      if (response==null) {
        return console.log("Response is: ", response);
      }
      let userFridge = await postUserFridgeData(this.user.userId, response.fridgeId);
      console.log(userFridge);
      // this.fridgeNameInput.push(response);
      this.fridgeNameInput = "";
      this.toggleAddFridge();
    }
  }
}
</script>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 50vh;
    display: flex;
    align-items: center;
  }
}
h1.fridgetitle {
  font-family: Inter;
  font-size: 50px;
  color: #D4F1F4;
  padding-top: 10px;
  text-align: center;
}
button.toggleFridgeTextbox {
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
  position: relative;
  left: 43.5%;
}
button.addFridge {
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
  position: relative;
  left: 46.5%;
}
</style>
