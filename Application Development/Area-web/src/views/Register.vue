<template>
  <HeaderVue></HeaderVue>
  <div class="background">
    <div class="register-container">
      <span class="register-header">{{ $t('Register') }}</span>

    <form class="input-container" @submit.prevent="submitForm">
      <input type="text" v-model="firstname" class="text-input" :placeholder="$t('FirstName')" required />
      <input type="text" v-model="lastname" class="text-input" :placeholder="$t('Name')" required />
      <input type="email" v-model="email" class="text-input" placeholder="E-mail" required />
      <input type="password" v-model="password" class="text-input" :placeholder="$t('password')" required />
      <input type="password" v-model="confirmpassword" class="text-input" :placeholder="$t('confirm password')" required />
      <button type="submit" class="sign-up-button">{{ $t('Continue') }}</button>

      <ErrorPopup :isVisible="showError" :message="errorMessage" @close="closeErrorPopup" />
      <div class="error-message" v-if="showErrorMessage">{{ errorMessage }}</div>
    </form>

    <div class="register-link">
      <span class="text">{{ $t('You already have an account ?') }}</span>
        <router-link to="/login" class="link">{{ $t('Identify yourself') }}</router-link>
      </div>
  </div>
</div>
</template>

<script setup lang="ts">
import axios from 'axios';
import HeaderVue from '../components/HeaderVue.vue'
import { ref, onMounted } from 'vue';
import { RouterLink } from 'vue-router';
import ErrorPopup from '../components/Popup.vue';
import { useRouter } from 'vue-router';

import i18n from '../../locales/i18n';

const selectedLanguage = ref(localStorage.getItem('language') || 'en');

const isValidLanguage = (language: string): language is 'English' | 'French' | 'Spanish' => {
  return ['English', 'French', 'Spanish'].includes(language);
};

onMounted(() => {
  if (isValidLanguage(selectedLanguage.value)) {
    i18n.global.locale = selectedLanguage.value as 'English' | 'French' | 'Spanish';
  } else {
    selectedLanguage.value = 'English';
    i18n.global.locale = 'English';
  }
});

const showError = ref(false);
const router = useRouter();

let firstname = '';
let lastname = '';
let email = '';
let password = '';
let confirmpassword = '';
let showErrorMessage = ref(false);
let errorMessage = ref('');

function submitForm() {
  if (firstname && lastname && email && password && confirmpassword) {
    if (password !== confirmpassword) {
      errorMessage.value = "Les mots de passe ne correspondent pas";
      showError.value = true;
      resetpassword();
    } else {
      let url = 'http://localhost:8080/auth/register';
      var item = {
        FirstName: firstname,
        LastName: lastname,
        Email: email,
        Password: password,
      };
      axios.post(url, item, {
        headers: {
          Accept: "application/json",
          "Content-Type": "application/json"
        }
      }).then(({data}) => {
        console.log(data);

        router.push('/');
      }).catch(function (error) {
        if (error.response) {
          console.log(error.response.status);
          if (error.response.status === 409) {
            errorMessage.value = "User already exists";
            showError.value = true;
            resetFormFields();
          } else {
            errorMessage.value = "Une erreur s'est produite";
          }
        }
      });
      console.log(email);
    }
  } else {
    errorMessage.value = "Tous les champs obligatoires doivent être remplis";
    showError.value = true;
  }
}

function closeErrorPopup() {
  showError.value = false;
}

function resetpassword() {
  password = '';
  confirmpassword = '';
}

function resetFormFields() {
  firstname = '';
  lastname = '';
  email = '';
  password = '';
  confirmpassword = '';
}

</script>

<style scoped>

.background {
  width: 100vw;
  min-height: calc(100vh - 150px);
  position: absolute;
  top: 120px;
  left: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  overflow-y: auto;
}

.register-container {
  width: 480px;
  height: 650px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color:  #333333;
  border-radius: 5%;
  opacity: 0.94;
}

.register-header {
  color: white;
  font-size: 32px;
  display: flex;
  justify-content: center;
  margin-bottom: 15px;
  margin-top: -10px;
  font-weight: 700;
}

.input-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.text-input {
  width: 375px;
  height: 60px;
  background-color: #0a0a0a;
  color: #9C9C9C;
  border: none;
  outline: none;
  border-radius: 5px;
  font-size: 24px;
  padding: 10px;
  text-align: center;
  margin-block: 10px;
  font-weight: 700;
}

.sign-up-button {
  width: 375px;
  height: 60px;
  margin-top: 10px;
  background-color: #EB8113;
  color: white;
  text-align: center;
  border: none;
  text-decoration: none;
  font-size: 24px;
  border-radius: 5px;
  font-weight: 700;
}

.sign-up-button:hover {
  background-color: #c9522e;
  color: #ffffff;
}

.register-link {
  display: flex;
  align-items: center;
  margin-top: 20px;
  margin-bottom: -50px
}

.link {
  font-size: 24px;
  color: white;
  cursor: pointer;
  margin-left: 10px;
  font-weight: 700;
}

.link:hover {
  text-decoration: underline;
  background-color: transparent;
}

.text {
  font-size: 20px;
  color: white;
  margin-left: 10px;
  font-weight: 700
}

</style>
