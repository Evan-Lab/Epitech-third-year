<template>
  <HeaderVue></HeaderVue>
  <div class="background">
    <div class="login-container">
      <span class="login-header">{{ $t('Login') }}</span>
        <form class="input-container" @submit.prevent="submit">
          <input type="email" v-model=email class="text-input" placeholder="E-mail"/>
          <input type="password" v-model="password" class="text-input password-position" :placeholder="$t('password')"/>
          <button type="submit" class="sign-in-button" @click="submit">{{ $t('Login') }}</button>
          <ErrorPopup :isVisible="showError" :message="errorMessageP" @close="closeErrorPopup" />
          <span type="submit" class="google-link" @click="login_google">{{ $t('Continue with Google') }}</span>
          <div v-if="errorMessage" class="error-message">
            {{ errorMessage }}
          </div>
        </form>
      <div class="register-link">
        <span class="text">{{ $t('New on CraftArea ?') }}</span>
        <router-link to="/register" class="link">{{ $t('Register') }}</router-link>
      </div>
    </div>

  </div>
</template>

<script setup lang="ts">
import { RouterLink, routeLocationKey, routerKey, useRouter } from 'vue-router'
import axios from 'axios'
import HeaderVue from '../components/HeaderVue.vue';
import { onMounted, ref } from 'vue';
import i18n from '../../locales/i18n';
import ErrorPopup from '../components/Popup.vue'


const selectedLanguage = ref(localStorage.getItem('language') || 'en');
let GoogleConnected = false;
localStorage.setItem('GoogleConnectedApp', 'false');
localStorage.setItem('SpotifyConnectedCheck', 'false')
localStorage.setItem('GithubConnectedCheck', 'false')
localStorage.setItem('FacebookConnectedCheck', 'false')
localStorage.setItem('GoogleConnectedCheck', 'false')
localStorage.setItem('AdminConnectedCheck', 'false')

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

const router = useRouter()

let email = ''
let password = ''
let errorMessage = ''
let authToken = ''
let google_url = ''
const showError = ref(false);
const errorMessageP = ref('');

async function submit() {
  errorMessage = ''

  let tempurl = "http://localhost:8080/auth/login?email=" + email + "&password=" + password

  var item = {
    Email: email,
    Password: password,
  }

  try {
    const response = await axios.post(tempurl, {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      }
    })

    if (response.status === 200) {
        authToken = response.data.token;
        localStorage.setItem('authToken', authToken);
        router.push('/Create');
    } else if (response.status === 400) {
      showError.value = true;
      errorMessageP.value = "Compte inexistant";
    } else {
      errorMessage = "Erreur de connexion : Votre message d'erreur ici"
    }
  } catch (error) {
  if (axios.isAxiosError(error) && error.response) {
    console.log(error.response.status)
    showError.value = true;
    errorMessageP.value = "Email ou mot de passe incorect";
  } else {
    errorMessage = "Erreur de connexion : Votre message d'erreur ici";
  }
  console.log(authToken);
  return authToken;
  }
}

localStorage.setItem('TestConnected', 'false')
localStorage.setItem('TestConnectedGithub', 'false')
localStorage.setItem('TestConnectedYoutube', 'false')

function closeErrorPopup() {
  showError.value = false;
}

async function login_google() {
  errorMessage = ''

    fetch("http://localhost:8080/google/link", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          pageName: 'login',
        }),
      })
        .then(async (response) => {
          if (response.status === 200) {
            response.text().then((data) => {
          console.log(data);
          google_url = data;
          open(google_url, '_self');
            });
        } else {
            console.error('Erreur lors de la connexion');
          }
        })
        .catch((error) => {
          console.error('Erreur lors de la connexion :', error);
        });
}

</script>

<style scoped>

.background {
  width: 100vw;
  min-height: calc(100vh - 100px);
  position: absolute;
  top: 100px;
  left: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  overflow-y: auto;
}

.login-container {
  width: 475px;
  height: 550px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background-color:  #333333;
  border-radius: 5%;
  opacity: 0.94;
}

.login-header {
  color: white;
  font-size: 32px;
  margin-top: 20px;
  display: flex;
  justify-content: center;
  margin-bottom: 15px;
  font-weight: 700;
}

.text-input {
  width: 370px;
  height: 60px;
  background-color: #0a0a0a;
  color: #9C9C9C;
  border: none;
  outline: none;
  border-radius: 5px;
  font-size: 24px;
  padding: 10px;
  text-align: center;
  margin-block: 15px;
  font-weight: 700;
}

.input-container {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.google-link {
  font-size: 24px;
  color: white;
  cursor: pointer;
  margin-block: 20px;
  font-weight: 700;
}

.google-link:hover {
  text-decoration: underline;
  background-color: transparent;
}

.sign-in-button {
  width: 370px;
  height: 60px;
  margin-top: 15px;
  background-color: #EB8113;
  color: white;
  text-align: center;
  border: none;
  text-decoration: none;
  font-size: 24px;
  border-radius: 5px;
  font-weight: 700;
}

.sign-in-button:hover {
  background-color: #c9522e;
  color: #ffffff;
}

.register-link {
  display: flex;
  align-items: center;
  margin-top: 20px;
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
  font-weight: 700;
}

</style>