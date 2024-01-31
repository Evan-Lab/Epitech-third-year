<template>
  <HeaderVue></HeaderVue>
  <img alt="Vue-logo" class="logo" src="@/assets/Logo.png"/>
  <div class="Confirmation">
    <h2>{{ $t('Email confirmation') }}</h2>
    <p>{{ $t('Thanks you email has been confirmed with success !') }}</p>
    <img src="../assets/GIF-Loading.gif" alt="Loading..." class="loading-animation">

  </div>

  <span class="Credits fade-in">{{ $t('© 2023 AreaCraft. All right reserved') }}</span>

</template>

<script setup lang="ts">
import HeaderVue from '../components/HeaderVue.vue';
import { ref, onMounted } from 'vue';
import i18n from '../../locales/i18n';
import { useRouter } from 'vue-router';

const router = useRouter();
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

const params = new URLSearchParams(window.location.search);
const param1 = params.get('Email');

const confirmEmail = async () => {
  if (param1) {
    try {
      const response = await fetch(`http://localhost:8080/auth/confirmation?Set=true&Email=${param1}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.status === 200) {
        redirectToLogin();
      } else {
        console.error('Erreur lors de la confirmation de l\'e-mail');
      }
    } catch (error) {
      console.error('Erreur lors de la confirmation de l\'e-mail :', error);
    }
  }
};

const redirectToLogin = () => {
  setTimeout(() => {
    router.push('/login');
  }, 4000);
};

onMounted(confirmEmail);

</script>

<style scoped>

.Confirmation {
  position: absolute;
  text-align: center;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}

.Go {
  position: absolute;
  top: 130%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 1.0rem;
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

.Credits {
  font-size: 18px;
  color: #000000;
  font-weight: 800;
  text-align: center;
  position: absolute;
  bottom: 30px;
  left: 50%;
  transform: translateX(-50%);
}

.logo {
  position:absolute;
  top: 20%;
  border-radius: 55%;
  width: auto;
  height: 25%;
  transform: scale(1);
  font-weight: 800;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
}

.loading-animation {
  position: absolute;
  top: 200%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: auto;
  height: 100%;
  animation: fade-in 2s;
}

</style>
