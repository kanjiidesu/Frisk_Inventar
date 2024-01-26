<script setup>
import { getUser } from '../../api';
</script>
<template>
  <div>
  <h1 class="accounttitle">Velkommen til Frisk Inventar!</h1>
  <p>Dette er din konto side, informationen kan ikke blive rettet.</p>
  <h2>Username</h2>
  <p>{{ userName }}</p>
  <h2>Email</h2>
  <p>{{ email }}</p>
  </div>
</template>
<script>
export default {
  async mounted() {
    var user = await getUser()
    if (user == null) {
      this.$router.push( { path : "/account-login"})
      return;
    }
    this.userName = user.userName
    this.email = user.email
    console.log(user)
  }, 
  data() {
    
    return {
      userData: [],
      userName: "",
      email: "",
    }
  }
}
</script>

<style>
h1.accounttitle {
  font-family: Inter;
  font-size: 50px;
  color: #D4F1F4;
  padding-top: 10px;
  text-align: center;
}
h2 {
  color: #008CBA;
  padding-top: 40px;
  text-align: center;
}
</style>