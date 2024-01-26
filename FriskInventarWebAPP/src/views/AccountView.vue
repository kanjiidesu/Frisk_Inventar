<script setup>
import { getUser } from '../../api';
</script>
<template>
  <div>
  <h1>Velkommen til Frisk Inventar!</h1>
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
  },

}
</script>

<style>
</style>