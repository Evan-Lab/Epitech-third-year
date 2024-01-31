<template>
  <div class="background">
    <div class="header">
      <button @click="backToAreas" class="undescore-button">&lt;</button>
      <button @click="backToAreas" class="back-button">Back</button>
      <span class="text">Edit trigger fields</span>
    </div>
    <div class="body">
      <img alt="Weather-logo" class="image" src="@/assets/Weather.png" width="120" height="110" />
      <div class="space"></div>
      <div class="space"></div>
      <!-- <span>Day</span>
      <input type="date" v-model="selectedDate" class="date-input">
      <div class="space"></div> -->
      <span>Time of day</span>
      <input type="time" v-model="selectedTime" class="time-input">
      <div class="space"></div>
      <span>Location</span>
      <div class="dropdown" :class="{ open: isDropdownOpen }">
        <button class="dropbtn" @click="toggleDropdown">
          {{ selectedCity ? selectedCity : 'Sélectionner une ville' }}
        </button>
        <div class="dropdown-content" v-show="isDropdownOpen">
          <div class="scrollable">
            <a href="#" class="city-option" v-for="city in city" @click="selectcity(city)">{{ city }}</a>
          </div>
        </div>
      </div>
      <button @click="returnValue" class="validate-button">Create</button>
    </div>
  </div>
</template>




<script setup>
import { ref } from 'vue';

const isDropdownOpen = ref(false);
const city = [
  'Paris', 'New York', 'London', 'Berlin', 'Tokyo', 'Beijing', 'Rome', 'Madrid', 'Moscow', 'Sydney', 'Seoul', 'Rio de Janeiro'
];


const emit = defineEmits();

const selectedCity = ref('');
const selectedDate = ref('');
const selectedTime = ref('');

const returnValue = () => {
  if (selectedTime.value === '')
    alert('Heure invalide. Veuillez saisir une heure valide.');
  else if (selectedCity.value === '')
    alert('Ville invalide. Veuillez saisir une Ville valide.');
  else {
    const result = `{"hour": ${selectedTime.value.split(':')[0]}, "minute": ${selectedTime.value.split(':')[1]}, "city": "${selectedCity.value}"}`;
    emit('return-value', { value: result, name: 'Weather',});
    emit('back-to-areas');
  }
};

const backToAreas = () => {
  emit('back-to-areas');
};

const toggleDropdown = () => {
  isDropdownOpen.value = !isDropdownOpen.value;
};

const selectcity = (city) => {
  selectedCity.value = city;
  isDropdownOpen.value = false;
};
</script>

<style scoped>


span {
  color: white;
  font-size: 20px;
}

.background {
  position: fixed;
  width: 100vw;
  height: 100vh;
  left: 0px;
  top: 0px;
  opacity: 1;
  background: #1E2023;

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
  margin-top: 50px;
}

</style>
