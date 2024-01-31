<template>
  <div class="background">
    <div class="header">
      <button @click="backToAreas" class="undescore-button">&lt;</button>
      <button @click="backToAreas" class="back-button">{{ $t('Back') }}</button>
      <span class="text">{{ $t('Edit trigger fields') }}</span>
    </div>
    <div class="body">
      <img alt="Weather-logo" class="image" src="@/assets/Weather.png" width="120" height="110" />
      <span class="text">{{ $t('Location') }}</span>
      <div class="dropdown" :class="{ open: isDropdownOpenCity }">
        <button class="dropbtn" @click="toggleDropdownCity">
          {{ selectedCity ? selectedCity : $t('Select a city') }}
        </button>
        <div class="dropdown-content" v-show="isDropdownOpenCity">
          <div class="scrollable">
            <a href="#" class="city-option" v-for="city in cities" @click="selectCity(city)">{{ city }}</a>
          </div>
        </div>
      </div>
      <span class="text">{{ $t('Weather') }}</span>
      <div class="dropdown" :class="{ open: isDropdownOpenWeather }">
        <button class="dropbtn" @click="toggleDropdownWeather">
          {{ selectedWeather ? selectedWeather : $t('Select a weather') }}
        </button>
        <div class="dropdown-content" v-show="isDropdownOpenWeather">
          <div class="scrollable">
            <a href="#" class="city-option" v-for="weather in weatherc" @click="selectWeather(weather)">{{ weather }}</a>
          </div>
        </div>
      </div>
      <button @click="returnButton" class="validate-button">{{ $t('Create') }}</button>
    </div>
  </div>
</template>


<script setup lang="ts">

import { onMounted } from 'vue';
import { ref } from 'vue';
import i18n from '../../../../../locales/i18n';

const selectedLanguage = ref(localStorage.getItem('language') || 'en');

const isValidLanguage = (language :string) => ['English', 'French', 'Spanish'].includes(language);

const t = (key :string) => i18n.global.t(key);

onMounted(() => {
  if (isValidLanguage(selectedLanguage.value)) {
    i18n.global.locale = selectedLanguage.value as 'English' | 'French' | 'Spanish';
  } else {
    selectedLanguage.value = 'English';
    i18n.global.locale = 'English';
  }
});

const isDropdownOpenCity = ref(false);
const isDropdownOpenWeather = ref(false);

const cities = [
  'Paris', 'New York', 'London', 'Berlin', 'Tokyo', 'Beijing', 'Rome', 'Madrid', 'Moscow', 'Sydney', 'Seoul', 'Rio de Janeiro'
];

const weatherc = [
  'Rainy', 'Snowy', 'Sunny', 'Nuageux',
];

const emit = defineEmits(['back-to-areas', 'return-value']);

const selectedCity = ref('');
const selectedWeather = ref('');


const returnButton = () => {
  if (selectedCity.value === '')
    alert('Ville invalide. Veuillez saisir une Ville valide.');
  else if (selectedWeather.value === '')
    alert('Ville invalide. Veuillez saisir une météo valide.');
  else {
    const result = `{"weather": "${selectedWeather.value}", "city": "${selectedCity.value}"}`;
    emit('return-value', { value: result, name: 'Weather(Action: Weather)',});
    emit('back-to-areas');
  }
};

const backToAreas = () => {
  emit('back-to-areas');
};

const toggleDropdownCity = () => {
  isDropdownOpenCity.value = !isDropdownOpenCity.value;
};

const selectCity = (cities:string) => {
  selectedCity.value = cities;
  isDropdownOpenCity.value = false;
};

const toggleDropdownWeather = () => {
  isDropdownOpenWeather.value = !isDropdownOpenWeather.value;
};

const selectWeather = (weatherc:string) => {
  selectedWeather.value = weatherc;
  isDropdownOpenWeather.value = false;
};


</script>

<style scoped>

.background {
  position: fixed;
  width: 100vw;
  height: 100vh;
  left: 0px;
  top: 0px;
  opacity: 1;
  background: #4f5d73;

  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  overflow-y: auto;
}

.header {
  width: 100vw;
  height: 100px;
  position: absolute;
  top: 0px;
  left: 0px;
  align-items: center;
  justify-content: center;
  display: flex;
  border-bottom: 1px solid #EEEEEE;
}

.text {
  font-size: 48px;
  color: white;
  font-weight: 800;
}

.body {
  width: 80vw;
  margin: 100px auto;
  position: absolute;
  top: 100px;
  left: 10vw;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.back-button {
  background: transparent;
  border: 4px solid white;
  color: white;
  border-radius: 28px;
  width: 144px;
  height: 48px;
  position: absolute;
  left: 2vw;
  font-weight: 800;
}
.undescore-button {
  background: transparent;
  border: none;
  font-size: 40px;
  display: none;
  position: absolute;
  left: 1vw;
  color: white;
}

@media (max-width: 700px) {
  .undescore-button {
    display: block;
  }
  .back-button {
    display: none;
  }
}


.dropbtn {
  background-color: #ffffff;
  color: rgb(0, 0, 0);
  padding: 10px;
  font-size: 24px;
  border-radius: 12px;
  border: none;
  cursor: pointer;
  border: 6px solid #EEEEEE;
  width: 440px;
  height: 62px;
}
.dropdown {
  position: relative;
  display: inline-block;
}

.city-option {
  padding: 10px;
  text-decoration: none;
  display: block;
  color: black;
}

.city-option:hover {
  background-color: #f2f2f2;
}

.scrollable {
  max-height: 200px;
  overflow-y: auto;
}

.space {
  margin: 20px;
}



.dropdown-content {
  display: none;
  background-color: #ffffff;
}

.dropdown.open .dropdown-content {
  display: block;
}

.validate-button {
  background: rgba(255, 255, 255, 1);
  border-radius: 56px;
  width: 422px;
  height: 112px;
  font-size: 36px;
  margin-top: 30px;
  font-weight: 600;
}

</style>
