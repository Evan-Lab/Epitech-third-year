<template>
  <div class="language-selector" @click="toggleLanguageSelector">
    {{ "文A" }}
    <ul v-if="showLanguageSelector" class="language-options">
      <li @click="setLanguage('French')">Français</li>
      <li @click="setLanguage('English')">English</li>
      <li @click="setLanguage('Spanish')">Español</li>
    </ul>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import i18n from '../../locales/i18n';

const showLanguageSelector = ref(false)
const supportedLanguages = ["English", "French", "Spanish"];
const selectedLanguage = ref(localStorage.getItem('language') || 'French');


const setLanguage = (language: string) => {
if (supportedLanguages.includes(language)) {
  showLanguageSelector.value = false;
  selectedLanguage.value = language;
  localStorage.setItem('language', language);
  i18n.global.locale = language as 'English' | 'French' | 'Spanish';
  }
}

const toggleLanguageSelector = () => {
  showLanguageSelector.value = !showLanguageSelector.value;
}

</script>

<style scoped>
.language-selector {
  font-size: 1.5rem;
  font-weight: 500;
  color: white;
  transition: background-color 0.3s;
  border: none;
  border-radius: 7px;
  text-decoration: none;
  text-align: center;
  width: 100%;
  margin: 0;
  padding: 0;
  position: relative;
  top: 0;
}

.language-selector:hover {
  background-color: #f6805b;
}

.language-options {
  position: absolute;
  text-align: center;
  list-style: none;
  padding: 0;
  margin: 0;
  background-color: #333333;
  transition: background-color 0.3s;
  box-shadow: #9C9C9C;
  border-radius: 10px;
  margin: 0;
  padding: 0;
  left: 42%;
  top: 100%;
  transform: translateX(-50%);
}

.language-options li {
  padding: 4px 16px;
  font-weight: 400;
  border-radius: 10px;
  cursor: pointer;
  font-weight: 500;
  border-radius: 7px;
  border: none;
}

.language-options li:hover {
  background-color: #f6805b;
}
</style>
