<script setup>
import { postUserData, loginUser } from '../../api';
</script>
<template>

  <div>
  <div class="container">
    <h1>Log ind</h1>
    <p>Fyld venligst formen ud for at logge ind og administere dit køleskab.</p>
    <hr>
    <label for="username"><b>Username</b></label>
    <input v-model="userNameInput" type="text" placeholder="Enter Username" name="username" id="username" required>

    <label for="psw"><b>Password</b></label>
    <input v-model="passwordInput" type="password" placeholder="Enter Password" name="psw" id="psw" required>

    <hr>
    <button class="loginbtn" @click="handleButtonClick">Log ind</button>
  </div>

  <div class="container signin">
    <p>Har du ikke en konto? <a @click="navigateToRegister">Registre</a>.</p>
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
  // might neeed a new endpoint for login
  handleButtonClick: async function() {
    let response;
    try {
      response = await loginUser(this.userNameInput, this.passwordInput);
      if (response==null)
      {
        return console.log("Response is not valid")
      }
      // if user can login, show success and account page 
    } catch (error) {
      // also show error to user, maybe a popup,or a hidden p
      console.error('Error during login: ', error);
    }
    console.log('Account was successfully logged in.')
    this.userData.push(response);
    this.userNameInput = "";
    this.emailInput = "";
    this.passwordInput = "";
    this.$root.check()
    //const event = new Event("loggedin")
    //window.dispatchEvent(event)
    
    this.$router.push('/account');
  },
  navigateToRegister: async function() {
    this.$router.push('/account');
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
input[type=text], input[type=password], input[type=email] {
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
.loginbtn {
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

label, h1, p {
  color: #D4F1F4;
  text-align: center;
}
</style>