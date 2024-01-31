<template>
  <div class="background">
    <div class="header">
      <button @click="backToAreas" class="undescore-button">&lt;</button>
      <button @click="backToAreas" class="back-button">{{ $t('Back') }}</button>
      <span class="text">{{ $t('Edit trigger fields') }}</span>
    </div>
    <div class="body">
      <img alt="Weather-logo" class="image" src="@/assets/Time.png" width="150" height="150" />
      <span class="text">{{ $t('Time of day') }}</span>
      <input type="time" v-model="selectedTime" class="time-input">
      <span class="text">{{  $t('Day of the year') }}</span>
      <input type="date" v-model="selectedDate" class="time-input">


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

const isDropdownOpen = ref(false);
const cities = [
  'Paris', 'New York', 'London', 'Berlin', 'Tokyo', 'Beijing', 'Rome', 'Madrid', 'Moscow', 'Sydney', 'Seoul', 'Rio de Janeiro'
];


const emit = defineEmits(['back-to-areas', 'return-value']);


const selectedDate = ref('');
const selectedTime = ref('');


const returnButton = () => {
  if (selectedTime.value === '')
    alert('Heure invalide. Veuillez saisir une heure valide.');
  else if (selectedDate.value === '')
    alert('Date invalide. Veuillez saisir une date valide.');
  else {
    console.log(selectedDate.value);
    const result = `{"year": ${selectedDate.value.split('-')[0]}, "month": ${selectedDate.value.split('-')[1]}, "day": ${selectedDate.value.split('-')[2]}, "hourParam": ${selectedTime.value.split(':')[0]}, "minuteParam": ${selectedTime.value.split(':')[1]}}`;
    console.log(result);
    emit('return-value', { value: result, name: 'Weather(Action: Time)',id: 12});
    emit('back-to-areas');
  }
};

const backToAreas = () => {
  emit('back-to-areas');
};

const toggleDropdown = () => {
  isDropdownOpen.value = !isDropdownOpen.value;
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
  margin-top: 20px;
  font-weight: 600;
}

</style>
