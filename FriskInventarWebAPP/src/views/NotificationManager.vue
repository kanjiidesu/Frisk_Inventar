<script setup>

</script>
<template>
  <div id="app" class="notifikation-manager">
      <div class="container">
        <h1 class ="notifikationtitle">Opsætning af Notifikation</h1>
        <hr>
        <label for="notificationTitle"><b>Titel</b></label>
        <input v-model="notificationTitleInput" type="text" placeholder="Skriv titel på notifikation" name="notifikationTitel" id="notificationTitel" required>
        <label for="notifikation"><b>Notifikation</b></label>
        <input v-model="notificationInput" type="text" placeholder="Skriv notifikation" name="notifikation" id="notification" required>
        <label for="notificationDelay"><b>Hvornår vil du have din reminder?</b></label>
        <input v-model="notificationDelayInput" type="date" id="notificationDelayInput">
        <hr>
        <button class="loginbtn" @click="sendNotificationBtn" id="sendNotification">Send</button>
      </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      notificationTitleInput: "",
      notificationInput: "",
      notificationDelayInput: "",
      notificationTimeInput: "11:51"
    }
  },
  methods: {
    // TODO: How would this work on the phone?
    // make a service worker and add to main.js
    // we need a server to handle push messages and send notifications to users
    // new endpoint?
    // https://progressier.com/how-to/augmenting-your-vuejs-app-with-push-notifications
    // OR firbase Cloud Messaging
    sendNotificationBtn: async function() {
      const sendbtn = document.getElementById('sendNotification');
      const registration = await navigator.serviceWorker.getRegistration();

      const sendNotification = async () => {
        const title = this.notificationTitleInput;
        const body = this.notificationInput;

        // Combine date and time into a single string
        const combinedDateTime = this.notificationDelayInput + " " + this.notificationTimeInput;
        const selectedDate = new Date(combinedDateTime);

        if (Notification.permission === 'granted') {
          scheduleNotification(title, body, selectedDate);
        } else {
          if (Notification.permission !== 'denied') {
            const permission = await Notification.requestPermission();

            if (permission === 'granted') {
              scheduleNotification(title, body, selectedDate);
            }
          }
        }
      };

      const scheduleNotification = (title, body, date) => {
        const now = new Date();
        const timeDifference = date.getTime() - now.getTime();

        if (timeDifference > 0) {
          setTimeout(() => {
            showNotification(title, body);
          }, timeDifference);
        } else {
          console.error("Selected date and time are in the past");
        }
      };

      const showNotification = (title, body) => {
        const payload = { body };

        if (registration && 'showNotification' in registration) {
          registration.showNotification(title, payload);
        } else {
          new Notification(title, payload);
        }
      };

      sendbtn.addEventListener('click', sendNotification);
    }
  }
}
</script>
<style>
h1.notifikationtitle {
  font-family: Inter;
  font-size: 30px;
  color: #D4F1F4;
  padding-top: 10px;
  text-align: center;
}
input[type=date] {
  width: 100%;
  padding: 15px;
  margin: 8px 0;
  display: inline-block;
  border: none;
  background: #f1f1f1;
  border-radius: 5px;
  font-family: Inter;
}
</style>
