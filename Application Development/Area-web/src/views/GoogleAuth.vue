<template>
  <HeaderVue></HeaderVue>
  <img alt="Vue-logo" class="logo" src="@/assets/Logo.png"/>
  <div class="GoogleAuth">
    <h2>{{ $t('Waiting for connection...') }}</h2>
    <img src="../assets/GIF-Loading.gif" alt="Loading..." class="loading-animation">
  </div>

</template>
<script lang="ts">
import HeaderVue from '../components/HeaderVue.vue';
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import i18n from '../../locales/i18n';

export default {
  created() {
    const router = useRouter();
    const params = new URLSearchParams(window.location.search);
    const _code = params.get('code');

    fetch("http://localhost:8080/google/token", {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        code: _code,
        pageName: 'login',
      }),
    })
      .then(async (response) => {
        if (response.status === 200) {
          const data = await response.json();
          const token = data.token;
          localStorage.setItem('authToken', token);
          localStorage.setItem('GoogleConnectedApp', 'true');
          if (token != null) {
            router.push('/Create');
            return token;
          }
        } else {
          console.error('Erreur lors de la connexion');
        }
      })
      .catch((error) => {
        console.error('Erreur lors de la connexion :', error);
      });

    const selectedLanguage = ref(localStorage.getItem('language') || 'en');
    const isValidLanguage = (language: string): language is 'English' | 'French' | 'Spanish' => {
      return ['English', 'French', 'Spanish'].includes(language);
    };

    if (isValidLanguage(selectedLanguage.value)) {
      i18n.global.locale = selectedLanguage.value as 'English' | 'French' | 'Spanish';
    } else {
      selectedLanguage.value = 'English';
      i18n.global.locale = 'English';
    }
  },
  components: { HeaderVue },
};

</script>


<style scoped>

.GoogleAuth {
    position: absolute;
    text-align: center;
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

.loading-animation {
  position: absolute;
  top: 200%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: auto;
  height: 100%;
  animation: fade-in 2s;
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

</style>
