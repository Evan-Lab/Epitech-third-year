<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import LanguageSelector from '../components/LanguageSelector.vue'
import HeaderVue from '../components/HeaderVue.vue';
import { onMounted, ref } from 'vue';
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
  setTimeout(() => {
    const elements = document.querySelectorAll('.fade-in');
    elements.forEach((el) => el.classList.remove('fade-in'));
  }, 100);
});

</script>

<template>
  <HeaderVue></HeaderVue>
  <div class="background">
    <div class="images-container">
    <img alt="Vue-logo" src="@/assets/Github.png" width="100" height="100" class="round-image" />
    <img alt="Vue-logo" src="@/assets/Youtube.png" width="100" height="75" class="round-image" />
    <img alt="Vue-logo" src="@/assets/Spotify.png" width="100" height="100" class="round-image" />
    <img alt="Vue-logo" src="@/assets/Worldtime.png" width="100" height="100" class="round-image"/>
    <img alt="Vue-Logo" src="@/assets/Weather.png" width="100" height="100" class="round-image" />
    <img alt="Vue-Logo" src="@/assets/Outlook.png" width="150" height="95" class="round-image" />
</div>
    <span class="Reactivity fade-in">{{ $t('Be reactive without moving a finger !') }}</span>
    <router-link to="/create" class="Go fade-in">Let's go</router-link>
    <RouterView />
    <span class="Credits fade-in">{{ $t('© 2023 AreaCraft. All right reserved') }}</span>
  </div>
</template>

<style scoped>
header {
  line-height: 1.5;
  max-height: 100vh;
}

.background {
  position: fixed;
  width: 100vw;
  height: calc(100vh - 100px);
  left: 0px;
  top: 100px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  overflow-y: auto;
}

.bottom {
  position: fixed;
  width: 100vw;
  height: 50px;
  bottom: 0;
  left: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}

.images-container {
  margin-top: 100px;
  margin-bottom: 100px;
  width: 100vw;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}


.Credits {
  font-size: 18px;
  color: #000000;
  font-weight: 800;
  text-align: center;
  position: fixed;
  bottom: 15px;
  left: 0;
  width: 100%;
}


.Go {
  margin-top: 60px;
  margin-bottom: 40px;
  font-size: 1.9rem;
  color: #000000;
  font-weight: 800;
  background-color: #f6805b;
  padding: 10px 20px;
  border: none;
  border-radius: 25px;
  cursor: pointer;
  text-decoration: none;
}

.Go:hover {
  background-color: #c9522e;
  color: #ffffff;
}

.Reactivity {
  font-size: 1.9rem;
  color: #000000;
  font-weight: 800;
  letter-spacing: 0.1rem;
  text-align: center;
}

.fade-in {
  opacity: 0;
  transition: opacity 1s;
}

.Download-APK {
  position: absolute;
  top: 7.5%;
  left: 87%;
  transform: translate(-50%, -50%);
  color: #000000;
  font-weight: 800;
  background-color: #f6805b;
  padding: 5px 5px;
  border: none;
  border-radius: 25px;
  cursor: pointer;
  text-decoration: none;
}

.round-image {
  width: 125px;
  height: 125px;
  border-radius: 50%;
  object-fit: cover;
  margin: 10px;
}

</style>
