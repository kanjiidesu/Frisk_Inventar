
<template>
  <div>
    <header>
      <nav>
      <ul>
        <li><RouterLink to="/">Home</RouterLink></li>
        <li><RouterLink to="/fridge">Mit Køleskab</RouterLink></li>
        <li><RouterLink to="/shopping_list">Inkøbsliste</RouterLink></li>
        <li v-if="getUser() != null" style="float:right"><RouterLink to="/account">My Account</RouterLink></li>
        <li v-else style="float:right"><RouterLink to="/account-register">My Account</RouterLink></li>
      </ul>
    </nav>
      <meta name="viewport" content="width=device-width, initial-scale=1.0">
      <!-- Your header content goes here -->
    </header>
    <RouterView></RouterView>
    <!-- Your page content goes here -->

  </div>
</template>

<script setup>
import HomeView from './views/HomeView.vue';
import { RouterLink, RouterView } from 'vue-router';
import AccountView from './views/AccountViewRegister.vue';
import { getUser } from '../api';
</script>
<style scoped>
ul {
  list-style-type: none;
  margin: 0;
  padding: 0;
  overflow: hidden;
  background-color: #07597c;
}

li {
  float: left;
}

li a {
  display: block;
  color: white;
  text-align: center;
  padding: 14px 16px;
  text-decoration: none;
}

li a:hover {
  background-color: #054058;;
}
</style>
