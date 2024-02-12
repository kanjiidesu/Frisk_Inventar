<script setup>
import { postFridgeData ,getFridgeData, getUser, postUserFridgeData, getUserFridgeData, postProductData, deleteProductData, getFridgeCategoryData } from '../../api';

</script>
<template>
  <div id="app" class="fridge">
    <h1 class ="fridgetitle">Dit Køleskab</h1>
    <div v-for="userFridge in userFridges" :key="userFridge">
      <div class="fridge-header">
        <h3 class = "fridgename">{{ userFridge.fridge.fridgeName }}</h3>
        <h3 class = "fridgeId">ID:</h3>
        <h3 class = "fridgeId">{{ userFridge.fridge.fridgeId }}</h3>
        <button class="toggleAddProduct" @click="toggleAddProduct">Tilføj Product</button>
      </div>
      <div class="new-product">
        <label class="productnamelabel">Køleskab Id</label>
        <input v-model="fridgeIdInput" type="number" id="fridgeIdInput" placeholder="e.g 1.." required>
        <label class="productnamelabel">Produkt navn</label>
        <input v-model="productNameInput" type="text" id="productNameInput" placeholder="e.g Skinke.." required>
        <label>Vælg Kategori</label>
        <select v-model="categoryId">
          <option v-for="category in fridgeCategories" :key="category.categoryId" :value="category.categoryId">{{ category.categoryName }}</option>
        </select>
        <br>
        <label class="expiryDate">Expiry Date</label>
        <input v-model="expiryDateInput" type="date" id="expiryDateInput" required>
      </div>
      <div v-for="product in userFridge.fridge.products" :key="product">
        <p class="productname">
          <span class="product-name">{{ product.productName }}</span>
          <span class="expiry-date">{{ product.expiryDate }}</span>
          <button class="delete-product" @click="deleteProduct(product.productId)">Fjern</button>
        </p>
      </div>
    </div>
    <div v-if="showTextbox">
      <input v-model="fridgeNameInput" class="fridgeName" type="text" id="fridgeNameInput" placeholder="Skriv navn på køleskab..">
      <button class="addFridge" @click="addFridge()">Tilføj</button>
    </div>
    <div v-if="hasFridge">
      <button class="toggleFridgeTextbox" @click="toggleAddFridge">Tilføj nyt køleskab</button>
    </div>
  </div>
</template>
<script>
export default {
  async mounted() {
    var fridge = await getFridgeData()
    this.fridgeName = fridge[0].fridgeName
    console.log(fridge)

    this.fridgeCategories = await getFridgeCategoryData(fridge[0].fridgeId)

    this.user = await getUser()
    console.log(this.user);

    if (this.user) {
      this.userFridges = await getUserFridgeData(this.user.userId)
    }

    this.toggleFridgeTextbox();
  },
  data() {
    // Data to be used in this class
    return {
      fridgeNameInput: "",
      userFridges: [],
      productData: [],
      showTextbox: false,
      hasFridge: true,
      fridgeName: "",
      fridgeCategories: [],
      categoryId: "",
      user: undefined,
      fridgeIdInput: "",
      productNameInput: "",
      expiryDateInput: ""
    }
  },
  computed() {
    // When adding new product, user should see it right away
  },
  methods: {
    toggleAddFridge: async function() {
      this.showTextbox = !this.showTextbox;

      if (this.userFridge !== null) {
        this.hasFridge = false
        console.log(this.hasFridge);
      }
      console.log("LOOK", this.userFridge)
    },
    addFridge: async function() {
      var inputValue = document.getElementById("fridgeNameInput").value; 
      console.log("Input value: " + inputValue);
      let response = await postFridgeData(this.fridgeNameInput)
      if (response==null) {
        return console.log("Response is: ", response);
      }
      let userFridge = await postUserFridgeData(this.user.userId, response.fridgeId);
      console.log(userFridge);
      this.fridgeNameInput = "";
      this.toggleAddFridge();
    },
    toggleFridgeTextbox: async function() {
      console.log(this.hasFridge);
      if (this.userFridges.length > 0) {
        this.hasFridge = false;
      } else {
        this.hasFridge = true;
      }
      console.log("hasFridge:", this.hasFridge);
    },
    // TODO: This function needs work
    toggleAddProduct: async function() {
      var fridgeIdInput = document.getElementById("fridgeIdInput").value;
      console.log("Input value: " + fridgeIdInput);

      var productNameInput = document.getElementById("productNameInput").value;
      console.log("Input value: " + productNameInput);

      var expiryDateInput = document.getElementById("expiryDateInput").value;
                 
      var betterExpiryDateInput = new Date(expiryDateInput).toISOString();
      console.log("Input value: " + betterExpiryDateInput);
      
      let response = await postProductData(this.fridgeIdInput, this.productNameInput, this.expiryDateInput, this.categoryId);
      if (response==null) 
      {
        return console.log("Response has returned null, so there will be no post.");
      }
      this.userFridges = await getUserFridgeData(this.user.userId)

      this.productData.push(response);
      this.fridgeIdInput = "";
      this.productNameInput = "";
      this.expiryDateInput = "";
    },
    deleteProduct: async function(productId) {
      let response = await deleteProductData(productId);
      this.userFridges = await getUserFridgeData(this.user.userId)
    if (response==null) 
    {
      return console.log("Response is returning null, delete was not possible.");
    }
    }
  }
}
</script>

<style>
.fridge-header{
  display: flex;
  align-items: baseline;
}
input[type=date] {
  width: 100%;
  padding: 12px 20px;
  margin: 1%;
  box-sizing: border-box;
  border: none;
  font-family: Inter;
  font-size: 20px;
  display: flex;
  border-radius: 5px;
}
.new-product {
  align-items: baseline;
  float: right;
  padding-right: 10%;
}
@media (min-width: 1024px) {
  .about {
    min-height: 50vh;
    display: flex;
    align-items: center;
  }
}
.productname {
  justify-content: space-between;
}
.expiry-date {
  margin-left: 10%;
}
h1.fridgetitle {
  font-family: Inter;
  font-size: 50px;
  color: #D4F1F4;
  padding-top: 10px;
  text-align: center;
}
h3.fridgename {
  font-family: Inter;
  font-size: 30px;
  color: #D4F1F4;
  padding-top: 10px;
  text-align: left;
  padding-left: 3%;
  padding-right: 30%;
}
h3.fridgeId {
  font-family: Inter;
  font-size: 25px;
  color: #D4F1F4;
}
p.productname {
  font-family: Inter;
  font-size: 20px;
  color: #D4F1F4;
  padding-top: 10px;
  text-align: left;
  padding-left: 6%;
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
button.toggleAddProduct {
  font-family: Inter;
  text-decoration-color: #D4F1F4;
  border: none;
  color: white;
  padding: 15px 32px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 0px 42% 0px;
  cursor: pointer;
  background-color: #008CBA;
  border-radius: 20px;
  position: left;
}
button.delete-product {
  font-family: Inter;
  text-decoration-color: #D4F1F4;
  color: white;
  border: none;
  padding: 10px 20px;
  margin: 0px 6% 0px;
  cursor: pointer;
  background-color: #008CBA;
  border-radius: 10px;
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
