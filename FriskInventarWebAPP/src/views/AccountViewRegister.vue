<script setup>
import { postUserData } from '../../api';
</script>
<template>

  <div>
  <div class="container">
    <h1 class="signuptitle">Tilmeld</h1>
    <p class="signuptext">Fyld venligst formen ud for at lave en konto.</p>
    <hr>
    <label for="username"><b>Username</b></label>
    <input v-model="userNameInput" type="text" placeholder="Enter Username" name="username" id="username" required>

    <label for="email"><b>Email</b></label>
    <input v-model="emailInput" type="email" placeholder="Enter Email" name="email" id="email" required>

    <label for="psw"><b>Password</b></label>
    <input v-model="passwordInput" type="password" placeholder="Enter Password" name="psw" id="psw" required>

    <hr>
    <button class="registerbtn" @click="handleButtonClick">Tilmeld</button>
  </div>
  
  <div class="container signin">
    <p>Har du allerede en konto? <a @click="navigateToLogin">Log ind</a>.</p>
  </div>
</div>

</template>
<script>
export default {

 data() {
  return {
    userData: [],
    userNameInput: "",
    emailInput: "",
    passwordInput: ""
  }
},

methods: {
  handleButtonClick: async function() {
    let response = await postUserData(this.userNameInput, this.emailInput, this.passwordInput)
    if (response==null)
    {
      return console.log("Response is not valid, so there will be no post.");
    }
    this.userData.push(response);
    this.userNameInput = "";
    this.emailInput = "";
    this.passwordInput = "";

    this.$router.push('/account-login');
    // show popup that post was successful, tell user that account was created
  },
  navigateToLogin: async function() {
    this.$router.push('/account-login');
  },
}
}
</script>

<style>
body {
  font-family: Arial, Helvetica, sans-serif;
  background-color: black;
  margin: 0;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

* {
  box-sizing: border-box;
}

/* Add padding to containers */
.container {
  width: 400px;
  border-radius: 10px;
  padding-left: 80px;
  padding-right: 80px;
  padding-top: 20px;
  background-color: #008CBA;
  margin: auto;
}

/* Full-width input fields */
input[type=text], input[type=password], input[type=email], input[type=number] {
  width: 100%;
  padding: 15px;
  margin: 5px 0 22px 0;
  display: inline-block;
  border: none;
  background: #f1f1f1;
  border-radius: 5px;
}

input[type=text]:focus, input[type=password]:focus {
  background-color: #D4F1F4;
  outline: none;
}

hr {
  border: 1px solid #07597c;
  margin-bottom: 25px;
}

/* Set a style for the submit button */
.registerbtn {
  background-color: #07597c;
  color: #D4F1F4;
  padding: 16px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
  border-radius: 20px;
}

.registerbtn:hover {
  opacity: 1;
}

/* Add a blue text color to links */
a {
  color: dodgerblue;
}

/* Set a grey background color and center the text of the "sign in" section */
.signin {
  background-color: #07597c;
  text-align: center;
  padding: 16px 20px;
  margin: 8px 0;
  border: none;
  cursor: pointer;
  width: 100%;
  opacity: 0.9;
}

label, h1.signuptitle, p.signuptext {
  color: #D4F1F4;
  text-align: center;
}
</style>