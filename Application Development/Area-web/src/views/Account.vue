<template>
  <HeaderVue></HeaderVue>
  <div class="background">
    <div class="container">
      <button class="apk-button">{{ $t('Download APK') }}</button>
      <img v-if="getPhotoOrLogo() === ''" alt="Vue-logo" class="picture-Logo" src="@/assets/User-White.png" />
      <img v-else alt="Vue-logo" class="picture-Logo" :src="getPhotoOrLogo()" />

      <div class="user-info">
        <div v-if="displayConnectedWithGoogle()">
          <span class="Connected-with-google">{{ $t('Connected with Google') }}</span>
        <img src="../assets/Google.png" class="logo-Google" alt="Connecté avec Google" />
        </div>

        <span class="User-First-Name">{{ firstname }}</span>
        <span class="User-Name">{{ lastname }}</span>
        <span class="Email">{{ email }}</span>
      </div>
      <span class = "creation-date">{{ $t('Account created the : ') }}{{ creation_date }}</span>
      <router-link class="app-account" to="/App-account">{{ $t('Link Account') }}</router-link>

      <button v-if="!editingPassword" class="edit-password" @click="startEditingPassword">{{ $t('Edit password') }}</button>
      <div v-if="editingPassword" class="password-controls">
      <button class="save-password" @click="saveNewPassword">{{ $t('Save') }}</button>
      <input class="enter-password" v-model="newPassword" type="password" :placeholder="$t('new password')" />
      <button class="cancel" @click="cancelEditPassword">{{ $t('Cancel') }}</button>
  </div>
      <ErrorPopup :isVisible="showError" :message="errorMessage" @close="showError = false" />
      <button @click="logout" class="logout-button">{{ $t('Logout') }}</button>
    </div>

  </div>
</template>


<script setup lang="ts">
import { RouterLink, useRoute, useRouter } from 'vue-router'
import axios from 'axios'
import { ref, onMounted } from 'vue';
import HeaderVue from '../components/HeaderVue.vue';
import ErrorPopup from '../components/Popup.vue'

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

const route = useRoute()
const router = useRouter();
const firstname = ref('');
const lastname = ref('');
const id = ref('');
const email = ref('');
const creation_date = ref('');
const newPassword = ref('');
const editingPassword = ref(false);
const passwordUpdated = ref(false);
const showError = ref(false);
const errorMessage = ref('');
const Photo_Url = ref('');

const authTokenPromise = new Promise((resolve) => {
  const checkToken = () => {
    const authToken = localStorage.getItem('authToken');
    if (authToken) {
      resolve(authToken);
    } else {
      setTimeout(checkToken, 100);
    }
  };
  checkToken();
});

authTokenPromise.then((authToken) => {

  axios.get('http://localhost:8080/user/me', {
    headers: {
      Authorization: `Bearer ${authToken}`,
    },
  })
  .then((response) => {
    firstname.value = response.data.firstname;
    lastname.value = response.data.lastname;
    email.value = response.data.email;
    id.value = response.data.id;
    Photo_Url.value = response.data.photoUrl;
    localStorage.setItem('Photo_Url', Photo_Url.value);
    getPhotoOrLogo();

    const fullDate = response.data.dateCreation;
    const dateObj = new Date(fullDate);
    const formattedDate = dateObj.toISOString().split('T')[0];
    creation_date.value = formattedDate;
  })
  .catch((error) => {
    console.error(error);
  });
});

const getPhotoOrLogo = () => {
  if (Photo_Url.value !== '') {
    return Photo_Url.value;
  }
  return '';
};

const check_security = () => {
  const authToken = localStorage.getItem('authToken');
  if (!authToken) {
    router.push('/Login');
  }
}

const logout = () => {
  localStorage.removeItem('authToken');
  window.location.href = '/login';
}

const startEditingPassword = () => {
  editingPassword.value = true;
  passwordUpdated.value = false;
}

const saveNewPassword = () => {
  passwordUpdated.value = true;
  editingPassword.value = false;
  if (!newPassword.value) {
    showError.value = true;
    errorMessage.value = "Mot de passe vide";
  }
  send_new_password();
}

const GoogleConnected = localStorage.getItem('GoogleConnectedApp');

const displayConnectedWithGoogle = () => {
  if (GoogleConnected === 'true') {
    return true;
  }
  return false;
};

const cancelEditPassword = () => {
  editingPassword.value = false;
  newPassword.value = '';
}

const send_new_password = () => {
  const authToken = localStorage.getItem('authToken');
  axios.post('http://localhost:8080/user/resetPwd?Id=' + id.value + '&Password=' + newPassword.value, {
    headers: {
      Authorization: `Bearer ${authToken}`,
    },
  })
  .then((response) => {
    console.log(response)
  })
  newPassword.value = '';
}

const CheckIfAdmin = () => {

  if (localStorage.getItem('AdminConnectedCheck') === 'true') {
    return true;
  } else {
    return false;
  }
}

onMounted(() => {
  check_security();
  CheckIfAdmin();
});

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

.container {
  width: 510px;
  height: 570px;
  background-color:  #333333;
  border-radius: 5%;
  opacity: 0.94;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.picture-Logo {
  height: 100px;
  transform: scale(1);
  border-radius: 50px;
}

.user-info {
  color: #fff;
  font-size: 32px;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 800;
  transition: 0.3s;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  margin-block: 10px;
}

.creation-date {
  font-size: 1.3rem;
  font-weight: 800;
  margin-bottom: 10px;
  text-align: center;
  color: #ffffff;
  white-space: nowrap;
}

.app-account {
  font-size: 1.5rem;
  font-weight: 800;
  text-align: center;
  color: #ffffff;
  transition: background-color 0.3s;
  border-radius: 7px;
  text-decoration: none;
  white-space: nowrap;
}

.app-account:hover {
  background-color: #ff5151;
  cursor: pointer;
}

.logout-button {
  font-size: 1.3rem;
  color: #000000;
  font-weight: 800;
  background-color: #f6805b;
  padding: 10px 10px;
  border: none;
  border-radius: 25px;
  cursor: pointer;
  text-decoration: none;
  text-align: center;
  margin-top: 10px;
}

.logout-button:hover {
  background-color: #c9522e;
  color: #ffffff;
}

.apk-button {
  font-size: 1.3rem;
  color: #000000;
  font-weight: 800;
  background-color: #f6805b;
  padding: 10px 10px;
  border: none;
  border-radius: 25px;
  cursor: pointer;
  text-decoration: none;
  text-align: center;
  margin-bottom: 10px;
  margin-top: 0px;
}

.apk-button:hover {
  background-color: #c9522e;
  color: #ffffff;
}

.edit-password {
  font-size: 1.5rem;
  color: #ffffff;
  font-weight: 800;
  transition: background-color 0.3s;
  border: none;
  border-radius: 7px;
  cursor: pointer;
  text-decoration: none;
  text-align: center;
  background: none;
  line-height: 1;
  margin-top: 10px;
}

.edit-password:hover {
  background-color: #ff5151;
}

.enter-password {
  font-size: 1.3rem;
  color: #000000;
  font-weight: 800;
  padding: 3px 3px;
  border: none;
  border-radius: 10px;
  cursor: pointer;
  text-decoration: none;
  text-align: center;
}

.save-password {
  font-size: 1.2rem;
  color: #000000;
  font-weight: 800;
  padding: 5px 5px;
  border: none;
  border-radius: 15px;
  cursor: pointer;
  text-decoration: none;
  text-align: center;
}

.cancel {
  font-size: 1.2rem;
  color: #000000;
  font-weight: 800;
  padding: 5px 5px;
  border: none;
  border-radius: 15px;
  cursor: pointer;
  text-decoration: none;
  text-align: center;
}

.password-controls {
  display: flex;
  align-items: center;
}

.save-password, .enter-password, .cancel {
  margin: 02px;
}

.enter-password {
  flex: 1;
}

.logo-Google {
  height: 30px;
  margin-left: 10px;
}

.User-First-Name {
  font-size: 1.5rem;
  font-weight: 800;
  margin-bottom: 10px;
  text-align: center;
  color: #ffffff;
  white-space: nowrap;
}

.User-Name {
  font-size: 1.5rem;
  font-weight: 800;
  margin-bottom: 10px;
  text-align: center;
  color: #ffffff;
  white-space: nowrap;
}

.Email {
  font-size: 1.5rem;
  font-weight: 800;
  margin-bottom: 10px;
  text-align: center;
  color: #ffffff;
  white-space: nowrap;
}

.Connected-with-google {
  font-size: 1.5rem;
  font-weight: 800;
  margin-bottom: 10px;
  text-align: center;
  color: #ffffff;
  white-space: nowrap;
}

</style>