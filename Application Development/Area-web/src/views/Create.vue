<template>
    <div v-if="showCreate === CreateStatus.CREATE">
      <HeaderVue></HeaderVue>

      <div class="body">
        <div class="button-container">
          <button class="button" @click="showCreate = CreateStatus.ACTION" :class="{ 'to-choose': !actionStatus, 'choosen': actionStatus }">{{ $t('If This') }}</button>
          <button v-if="actionStatus" class="clear-button" @click="clearAction">{{ $t('Delete') }}</button>
        </div>
        <div class="button-container">
          <button class="button" @click="showCreate = CreateStatus.REACTION" :class="{ 'disabled': !actionStatus && !reactionStatus, 'to-choose': actionStatus && !reactionStatus, 'choosen': reactionStatus }">{{ $t('Then That') }}</button>
          <button v-if="reactionStatus" class="clear-button" @click="clearReactionValue">{{ $t('Delete') }}</button>
        </div>
        <button v-if="actionStatus && reactionStatus" class="button-valiated" @click="callBackEnd">{{ $t('Confirm Area') }}</button>
      <ErrorPopup :isVisible="showPopup" :message="popupMessage" @close="showPopup = false" />


      </div>
    </div>

    <div v-if="showCreate === CreateStatus.ACTION">
      <Action @update-action-values="setActionValues" @show-create="showCreate = CreateStatus.CREATE"/>
    </div>
    <div v-if="showCreate === CreateStatus.REACTION">
      <Reaction  @update-reaction-values="setReactionValues" @show-create="showCreate = CreateStatus.CREATE"/>
    </div>

</template>


<script setup lang="ts">
import axios from 'axios';
import { RouterLink, useRoute, useRouter } from 'vue-router'
import { ref, computed, onMounted } from 'vue';
import i18n from '../../locales/i18n';
import HeaderVue from '../components/HeaderVue.vue';
import ErrorPopup from '../components/Popup.vue'

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


import Action from '@/components/Actions.vue'
import Reaction from '@/components/Reactions.vue'

const router = useRouter();
const showPopup = ref(false);
const popupMessage = ref('');
const firstname = ref('');
const lastname = ref('');


const id = ref(0);


const actionStatus = ref(false);
const reactionStatus = ref(false);
const actionValue = ref('');
const reactionValue = ref('');
const actionName = ref('');
const reactionName = ref('');

const actionID = ref(0);
const reactionID = ref(0);

localStorage.setItem('actionID', actionID.value.toString());
localStorage.setItem('reactionID', reactionID.value.toString());

const returnValue = ref('');



enum CreateStatus {
  CREATE,
  ACTION,
  REACTION,
}
const showCreate = ref(CreateStatus.CREATE);


const setActionValues = (data: { value: string, name: string, id: number }) => {
  const { value, name, id } = data;
  actionValue.value = value;
  actionName.value = name;
  actionID.value = id;
  actionStatus.value = true;
}

const setReactionValues = (data: { value: string, name: string, id: number }) => {
  const { value, name, id } = data;
  reactionValue.value = value;
  reactionName.value = name;
  reactionID.value = id;
  reactionStatus.value = true;
}

const clearAction = () => {
  if (reactionValue.value === '') {
    actionValue.value = '';
    actionName.value = '';
    actionStatus.value = false;
    actionID.value = 0;
  }
  else
    alert('You cannot delete an action if a reaction has already been selected.')
};

const clearReactionValue = () => {
  reactionValue.value = '';
  reactionName.value = '';
  reactionStatus.value = false;
  reactionID.value = 0;
};




const callBackEnd = () => {

  console.log('\nName: [' + actionName.value + '] actionValue: [' + actionValue.value + ']');
  console.log('Name: [' + reactionName.value + '] reactionValue: [' + reactionValue.value + ']');
  console.log('action ID' + actionID.value)
  console.log('reaction ID' + reactionID.value)
  if (actionStatus.value && reactionStatus.value) {
    returnValue.value = createJsonData(actionName.value + '-' + reactionName.value, id.value, actionID.value, reactionID.value, actionValue.value, reactionValue.value);
    console.log(returnValue.value)
    const authToken = localStorage.getItem('authToken');
    fetch("http://localhost:8080/area/create", {
      method: "POST",
      headers: {
        Authorization: `Bearer ${authToken}`,
        "Content-Type": "application/json",
      },
      body: returnValue.value,
    })
    showPopup.value = true;
    clearReactionValue();
    clearAction();
    popupMessage.value = 'Area Créer';
  } else
      alert("An error has occurred, can't send this AREA")
}


function createJsonData(name: string, userId: number, actionId: number, reactionId: number, paramAction: any, paramReaction: any) {
  const jsonData = {
    name: name,
    userId: userId,
    actionId: actionId,
    reactionId: reactionId,
    paramAction: paramAction,
    paramReaction: paramReaction,
  };

  return JSON.stringify(jsonData);
}



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
  console.log('authToken : ' + authToken);

  axios.get('http://localhost:8080/user/me', {
    headers: {
      Authorization: `Bearer ${authToken}`,
    },
  })
  .then((response) => {
    if (response.data.admin === 1) {
      localStorage.setItem('AdminConnectedCheck', 'true');
    } else {
      localStorage.setItem('AdminConnectedCheck', 'false');
    }
    firstname.value = response.data.firstname;
    lastname.value = response.data.lastname;
    id.value = response.data.id;
  })
  .catch((error) => {
    console.error(error);
  });
});

const check_security = () => {
  const authToken = localStorage.getItem('authToken');
  if (!authToken) {
    router.push('/Login');
  }
}

onMounted(() => {
      check_security();
});

</script>



<style scoped>

.body {
  width: 80vw;
  height: 80vh;
  position: absolute;
  top: 100px;
  left: 10vw;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.button-valiated {
  background-color: #222222;
  color: white;
  border-radius: 50px;
  width: 300px;
  height: 100px;
  margin-top: 10px;
  font-weight: 800;
}

.button {
  width: 100%;
  color: white;
  border: none;
  cursor: pointer;
  background: transparent;
  font-size: 96px;
  font-family: 'Roboto', sans-serif;
  font-weight: 600;
  text-align: center;
  margin: 0 auto;
  line-height: 120px;
}

.button-container {
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  width: 656px;
  height: 120px;
  border-radius: 6px;
  border: none;
  margin-bottom: 60px;
  text-align: center;
}

.clear-button {
  font-size: 20px;
  color: white;
  position: absolute;
  top: 30%;
  right: 2%;
  text-decoration: none;
  background: transparent;
  border: none;
  text-decoration: none;
  font-weight: 500;
}

.clear-button.hover {
  cursor: pointer;
  border-radius: 6px;
  text-decoration: underline;
}

.button.to-choose {
  color: white;
  background: #222222;
  cursor: pointer;
  border-radius: 6px;
}

.button.disabled {
  color: white;
  background: #999999;
  cursor: default;
  pointer-events: none;
  border-radius: 6px;
}

.button.choosen {
  background: rgba(36, 104, 255, 1);
  cursor: default;
  pointer-events: none;
  border-radius: 6px;
}


.logo {
  border-radius: 50%;
  width: auto;
  height: 80px;
  transform: scale(1);
  margin-right: 20px;
}

.text-logo {
  width: auto;
  height: 70px;
  transform: scale(1);
}

.Link-Account {
  position: absolute;
  top: 1%;
  right: 1%;
  font-size: 20px;
  color: #000000;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 500;
  transition: 0.3s;
}

.NameContainer {
  position: absolute;
  top: 5%;
  right: 110px;
  font-size: 20px;
  color: #ffffff;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 800;
  transition: 0.3s;
  display: inline-block;
}

.First-Name, .Name {
  display: inline-block;
  margin: 0;
  margin-right: 10px;
  font-weight: 800;
}

</style>
