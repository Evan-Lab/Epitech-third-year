<template>

  <HeaderVue></HeaderVue>
  <h2 class="Home-list-Areas">{{ $t('List of all the Areas') }}</h2>
  <ul class="areas-list">
    <li v-for="area in areas" :key="area.id" class="area-box">
      <p class="List-element">{{ $t('ID of the Area : ') }} {{ area.id }}</p>
      <p class="List-element">{{ $t('Name of the Area : ') }} {{ area.name }}</p>
    </li>
  </ul>

</template>

<script setup lang="ts">

import { useRouter } from 'vue-router'
import axios from 'axios'
import { ref, onMounted } from 'vue';
import HeaderVue from '../components/HeaderVue.vue';
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

const router = useRouter();
const areas = ref<any[]>([]);
const check_security = () => {
const authToken = localStorage.getItem('authToken');

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
})
.catch((error) => {
  console.error(error);
});
});

if (!authToken) {
  router.push('/Login');
  }
}

const ListAreas = () => {
axios.get('http://localhost:8080/area/user/area', {
  headers: {
    Authorization: `Bearer ${localStorage.getItem('authToken')}`,
  },
})
.then((response) => {
  areas.value = response.data;
})
.catch((error) => {
  console.error(error);
});
}

onMounted(() => {
  check_security();
  ListAreas();
});

</script>

<style>

.Home-list-Areas {
  position: absolute;
  top: 18%;
  left: 50%;
  text-align: center;
  transform: translateX(-50%);
  font-size: 40px;
  color: #000;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 600;
  transition: 0.3s;
  display: inline-block;
}

.areas-list-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 20px;
}

.areas-list {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 25px;
  list-style-type: none;
  padding: 0;
  margin-top: 180px;
  margin-left: -5%;
}

.area-box {
  background-color: #333;
  padding: 1vw;
  border-radius: 2vw;
  min-height: 10%;
  display: flex;
  flex-direction: column;
  margin: 1.5vw;
}

.List-element {
  font-size: 1.5vw;
  margin-left: 0.5vw;
  margin-top: 0.7vw;
  color: #fff;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 600;
  transition: 0.3s;
  display: inline-block;
  text-align: center;
}

@media (max-width: 768px) {
  .areas-list {
    grid-template-columns: repeat(2, 1fr);
    margin-left: -2.5%;
  }
}

@media (max-width: 2000px) {
  .areas-list {
    grid-template-columns: repeat(3, 1fr);
    margin-left: 10%;
    margin-top: 20%;
  }
}

@media (max-width: 500px) {
  .areas-list {
    grid-template-columns: repeat(1, 1fr);
    margin-left: -1.25%;
  }
  .area-box {
    padding: 5vw;
    border-radius: 5vw;
    margin: 2.5vw;
  }
  .List-element {
    font-size: 5vw;
    margin-left: 2.5vw;
    margin-top: 2.5vw;
  }
}

</style>
