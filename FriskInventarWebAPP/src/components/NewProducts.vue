<script setup>
import { postFridgeData ,getFridgeData, getUser, postUserFridgeData, getUserFridgeData, postProductData, deleteProductData, getFridgeCategoryData } from '../../api';
</script>
<template>
<div class="new-product">
        <label class="productnamelabel">Køleskab Id</label>
        <input v-model="fridgeIdInput" type="number" id="fridgeIdInput" placeholder="e.g 1.." required>
        <label class="productnamelabel">Produkt navn</label>
        <input v-model="productNameInput" type="text" id="productNameInput" placeholder="e.g Skinke.." required>
        <label>Vælg Kategori</label>
        <select class="selector" v-model="categoryId">
          <option v-for="category in fridgeCategories" :key="category.categoryId" :value="category.categoryId">{{ category.categoryName }}</option>
        </select>
        <label class="expiryDate">Expiry Date</label>
        <input v-model="expiryDateInput" type="date" id="expiryDateInput" required>
        <button class="toggleAddProduct" @click="toggleAddProduct">Tilføj Product</button>
      </div>
</template>

<script>
export default {
    props : [
        "userId"
    ],
    async mounted() {
    var fridge = await getFridgeData()

    this.fridgeCategories = await getFridgeCategoryData(fridge[0].fridgeId)
    
    if (this.user) {
      this.userFridges = await getUserFridgeData(this.user.userId)
    }
    },
    data() {
    // Data to be used in this class
    return {
      fridgeCategories: [],
      userFridges: [],
      categoryId: "",
      fridgeIdInput: "",
      productNameInput: "",
      user: undefined,
      expiryDateInput: ""
    }
  },
  methods: {
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
      this.userFridges = await getUserFridgeData(this.userId)

      //this.productData.push(response);
      this.$emit("newProduct",response);
      this.fridgeIdInput = "";
      this.productNameInput = "";
      this.expiryDateInput = "";
    },
  }
}
</script>

<style>
.selector {
    width: 100%;
    padding: 15px;
    margin: 5px 0 22px 0;
    box-sizing: border-box;
    border: none;
    font-family: Inter;
    font-size: 20px;
    display: flex;
    border-radius: 5px;
}
</style>