<template>
  <div class="background">
    <div class="header">
      <button @click="backToAreas" class="undescore-button">&lt;</button>
      <button @click="backToAreas" class="back-button">{{ $t('Back') }}</button>
      <span class="text">{{ $t('Choose a trigger') }}</span>
    </div>
    <div class="body">
      <img alt="Spotify-logo" class="image" src="@/assets/Spotify.png" width="120" height="110" />
      <span class="text">{{ $t('Choose your spotify music and spotify playist in the trigger') }}</span>
      <input type="text" v-model="urlMusic" :placeholder="$t('Please enter the URL of the Spotify music')" class="text-input">
      <input type="text" v-model="urlPlaylist" :placeholder="$t('Please enter the URL of the spotify playlist')" class="text-input">
      <button @click="validateButton" class="validate-button">{{ $t('Create') }}</button>
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


const emit = defineEmits(['back-to-areas', 'return-value']);

const urlMusic = ref('');
const urlPlaylist = ref('');

const validateButton = () => {
  if (urlMusic.value === '')
    alert('Please enter a valide Spotify music url');
  else if (urlPlaylist.value === '')
    alert('Please enter a valide Spotify playlist url');
    else {
      const urlObject = { urlTrack: urlMusic.value, urlPlaylist: urlPlaylist.value };
      const urlString = JSON.stringify(urlObject);
      emit('return-value', { value: `${urlString}`, name: 'Spotify(Reaction: MusicToPlaylist)',id:2});
      emit('back-to-areas');
    }
};

const backToAreas = () => {
  emit('back-to-areas');
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
  background: #1db954;

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
  text-align: center;
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

.image {
  margin-bottom: 10px;
  border-radius: 12px;
  border: 10px solid #EEEEEE;
}

.text-input {
  background: rgba(255, 255, 255, 1);
  border-radius: 12px;
  width: 440px;
  height: 72px;
  margin: 20px 0;
  text-align: center;
  font-weight: 600;
}

.validate-button {
  background: rgba(255, 255, 255, 1);
  border-radius: 56px;
  width: 422px;
  height: 112px;
  font-size: 36px;
  margin-top: 15px;
  font-weight: 600;
}
</style>
