<template>
  <div class="background">
    <div :style="{ backgroundColor: props.selectedTrigger?.background_color }" class="header">
      <button @click="backToAreas" class="undescore-button">&lt;</button>
      <button @click="backToAreas" class="back-button">{{ $t('Back') }}</button>
      <span class="text">{{ $t('Choose a trigger') }}</span>
    </div>
    <div class="body">
      <div class="trigger-container">
        <div v-for="section in props.selectedTrigger?.areas" :key="section.name">
          <div :style="{ backgroundColor: props.selectedTrigger?.background_color }" class="image-container" @click="selectAreas(section)">
            <p class="image-container-title">{{ section.title }}</p>
            <p class="image-container-description">{{ section.description }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
  <component :is="selectedComponent" @back-to-areas="resetSelectedComponent" @return-value="receiveValueFromComponent" />

</template>

<script setup lang="ts">
import { ref } from 'vue';

import type { Trigger } from '@/components/Actions.vue'

import { onMounted } from 'vue';
import { useRouter } from 'vue-router';
import i18n from '../../locales/i18n';



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

const selectedComponent = ref(null);
const router = useRouter();


const props = defineProps({
  selectedTrigger: Object as () => Trigger | null,
});




const selectAreas = (action: any) => {
  if (action.show)
    selectedComponent.value = action.component;
  else
    router.push("/App-account");
};

const resetSelectedComponent = () => {
  selectedComponent.value = null;
};


const receiveValueFromComponent = (data: { value: string, name: string }) => {
  emit('return-value', data);
  backToAreas();
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
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  overflow-y: auto;
}

.image-container {
  width: 300px;
  height: 300px;
  margin: 10px;
  text-align: center;
  cursor: pointer;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  font-weight: 800;
}

.image-container p {
  font-weight: 800;
}

.image-container-title {
  font-size: 24px;
  color: white;
  margin-inline: 28px;
}

.image-container-description {
  font-size: 20px;
  color: white;
  margin-inline: 28px;
}

.trigger-container {
  margin-top: 100px;
  width: 100%;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
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
  font-weight: 700;
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

</style>