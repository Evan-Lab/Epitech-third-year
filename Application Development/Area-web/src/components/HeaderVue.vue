<template>

  <div class="header-vue">
    <div class="logo-container">
      <img alt="Vue-logo" class="logo" src="@/assets/Logo.png"/>
      <img src="../assets/Text-logo.png" alt="Text-logo" class="text-logo" />
    </div>

    <nav class="nav-container">
      <RouterLink class="link" :to="{ name: 'home' }" :class="{ active: route.name === 'home' }">{{ $t('HOME') }}</RouterLink>
      <RouterLink class="link" :to="{ name: 'contact' }" :class="{ active: route.name === 'contact' }">{{ $t('CONTACT') }}</RouterLink>
      <RouterLink class="link" :to="{ name: 'create' }" :class="{ active: route.name === 'create' }">AREA</RouterLink>
      <RouterLink class="link" :to="{ name: 'MyArea' }" :class="{ active: route.name === 'MyArea' }">{{ $t('MY AREA') }}</RouterLink>
    </nav>

    <div class="nav-block">
      <!-- {{ "MENU" }} -->
      <span class="menu" @click="showLink =!showLink">MENU</span>
      <ul v-if="showLink" class="link-options">
        <li><RouterLink class="link" :to="{ name: 'home' }" :class="{ active: route.name === 'home' }">{{ $t('HOME') }}</RouterLink></li>
        <li><RouterLink class="link" :to="{ name: 'contact' }" :class="{ active: route.name === 'contact' }">{{ $t('CONTACT') }}</RouterLink></li>
        <li><RouterLink class="link" :to="{ name: 'create' }" :class="{ active: route.name === 'create' }">AREA</RouterLink></li>
        <li><RouterLink class="link" :to="{ name: 'MyArea' }" :class="{ active: route.name === 'MyArea' }">{{ $t('MY AREA') }}</RouterLink></li>
      </ul>
    </div>

    <div class="NameContainer">
      <span class="First-Name" v-if="logged">{{ firstname }}</span>
      <span class="Name" v-if="logged">{{ lastname }}</span>
    </div>

      <div class="profile-container">
        <img v-if="getPhotoOrLogo() === ''" alt="Vue-logo" class="profile-icon" src="@/assets/User.png" @click="showProfile = !showProfile" />
        <img v-else alt="Vue-logo" class="profile-icon" :src="getPhotoOrLogo()" @click="showProfile = !showProfile"/>
        <ul v-if="showProfile" class="profile-options">
          <li v-if="logged"><LanguageSelector></LanguageSelector></li>
          <li v-if="!logged"><LanguageSelector></LanguageSelector></li>
          <li v-if="logged"><button @click="Account" class="Account-button">{{ $t('Account') }}</button></li>
          <li v-if="isAdmin"><button @click="AdminPage" class="Account-button">{{ $t('Admin') }}</button></li>
          <li v-if="!logged"><button @click="login" class="login-button">{{ $t('Connexion') }}</button></li>
          <li v-if="logged"><button @click="logout" class="logout-button">{{ $t('Logout') }}</button></li>
        </ul>
      </div>
  </div>
</template>


<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { RouterLink, useRoute, useRouter } from 'vue-router';
import LanguageSelector from '../components/LanguageSelector.vue';
import axios from 'axios';

const route = useRoute();
const router = useRouter();
const showProfile = ref(false);
const logged = ref(false);
const showLink = ref(false);
const isAdmin = ref(false);
const firstname = ref('');
const lastname = ref('');

const logout = () => {
  localStorage.removeItem('authToken');
  window.location.href = '/login';
}

const login = () => {
  router.push('/login');
}

const Account = () => {
  router.push('/Account');
}

const LinkAccount = () => {
  router.push('/App-account');
}


const AdminPage = () => {
  router.push('/Admin');
}

const isLogged = () => {
  const authToken = localStorage.getItem('authToken');
  if (authToken) {
    logged.value = true;
  } else {
    logged.value = false;
  }
}

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
    if (response.data.admin === 1) {
      isAdmin.value = true;
    } else {
      isAdmin.value = false;
    }
    firstname.value = response.data.firstname;
    lastname.value = response.data.lastname;
    Photo_Url.value = response.data.photoUrl;
    localStorage.setItem('Photo_Url', Photo_Url.value);
    getPhotoOrLogo();
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

onMounted(() => {
  isLogged();
});

</script>

<style scoped>

.header-vue {
  width: 100%;
  height: 100px;
  background-color: #333333;
  position: absolute;
  top: 0;
  left: 0;

  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;

  border-bottom-left-radius: 40px;
  border-bottom-right-radius: 40px;
}

.menu {
  font-size: 24px;
  margin-right: 20px;
  text-decoration: none;
  position: relative;
  color: #ffffff;
}

.profile-icon {
  width: 75px;
  height: 75px;
  cursor: pointer;
  border-radius: 50%;
}

.nav-block {
  font-size: 100%;
  margin-right: 20px;
  text-decoration: none;
  position: relative;
  cursor: pointer;
  display: none;
}

@media (max-width: 768px) {
  .nav-block {
    display: inline-block;
  }
}

.profile-options {
  position: absolute;
  top: 100%;
  list-style: none;
  right: 5px;
  padding: 0;
  margin: 0;
  background-color: #333333;
  color: #9C9C9C;
  box-shadow: #9C9C9C;
  font-size: 24px;
  border-radius: 10px;
  padding: 5px;
  z-index: 10;
}

.logo-container {
  display: flex;
  align-items: center;
}

.logo {
  border-radius: 50%;
  width: auto;
  height: 80px;
  transform: scale(1);
  margin-right: 20px;
}

.text-logo {
  width: auto;
  height: 70px;
  transform: scale(1);
}
@media (max-width: 1026px) {
  .text-logo {
    display: none;
  }
}

.profile-container {
  align-items: center;
  justify-content: flex-end;
}

.nav-container {
  align-items: left;
  justify-content: left;
}

@media (max-width: 768px) {
  .nav-container {
    display: none;
  }
}

.link {
  font-size: 24px;
  margin-right: 20px;
  text-decoration: none;
  position: relative;
  color: #9C9C9C;
}


.link-options {
  position: absolute;
  top: 170%;
  list-style: none;
  right: -30%;
  padding: 0;
  margin: 0;
  background-color: #333333;
  color: #9C9C9C;
  box-shadow: #9C9C9C;
  font-size: 24px;
  z-index: 10;
  border-radius: 10px;
  padding: 5px;

  text-align: center;
}


.link.active {
  color: white;
  text-decoration: none !important;
  cursor: default;
}

.link:hover {
  text-decoration: underline;
  background-color: transparent;
}

.link::after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #EB8113;
  opacity: 0;
  transition: opacity 0.3s;
}

.link.active::after {
  opacity: 1;
}

.logout-button {
  font-size: 1.5rem;
  font-weight: 500;
  text-align: center;
  color: #ffffff;
  transition: background-color 0.3s;
  background-color: #333333;
  border: none;
  border-radius: 7px;
  text-decoration: none;
  width: 100%;
  margin: 0;
  padding: 0;
}

.logout-button:hover {
  background-color: #f6805b;
  color: #ffffff;
}

.login-button {
  font-size: 1.5rem;
  font-weight: 500;
  text-align: center;
  color: #ffffff;
  transition: background-color 0.3s;
  background-color: #333333;
  border: none;
  border-radius: 7px;
  text-decoration: none;
}

.login-button:hover {
  background-color: #f6805b;
  color: #ffffff;
}

.Account-button {
  font-size: 1.5rem;
  font-weight: 500;
  color: #ffffff;
  transition: background-color 0.3s;
  background-color: #333333;
  border: none;
  border-radius: 7px;
  text-decoration: none;
  text-align: center;
  width: 100%;
  margin: 0;
  padding: 0;
}

.Account-button:hover {
  background-color: #f6805b;
  color: #ffffff;
}

.Account_manager_button {
    font-size: 1.5rem;
  font-weight: 500;
  color: #ffffff;
  transition: background-color 0.3s;
  background-color: #333333;
  border: none;
  border-radius: 7px;
  text-decoration: none;
  text-align: center;
  width: 100%;
  margin: 0;
  padding: 0;
}

.Account_manager_button:hover {
  background-color: #f6805b;
  color: #ffffff;
}

.Admin-button {
  font-size: 1.5rem;
  font-weight: 500;
  color: #ffffff;
  transition: background-color 0.3s;
  background-color: #333333;
  border: none;
  border-radius: 7px;
  text-decoration: none;
  text-align: center;
  width: 100%;
  margin: 0;
  padding: 0;
  font-weight: 800
}

.Admin-button:hover {
  background-color: #f6805b;
  color: #ffffff;
}

.NameContainer {
  position: absolute;
  top: 35%;
  right: 110px;
  font-size: 20px;
  color: #ffffff;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 800;
  transition: 0.3s;
  display: inline-block;
}

.First-Name, .Name {
  display: inline-block;
  margin: 0;
  margin-right: 10px;
  font-weight: 800;
}

@media (max-width: 1400px) {
  .NameContainer {
    display: none;
  }
}

</style>