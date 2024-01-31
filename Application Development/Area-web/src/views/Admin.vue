<template>

    <HeaderVue></HeaderVue>
    <div class="background">
      <span class="Admin-Text">{{ $t('Admin Mode') }}</span>

      <div class="user-areas-container">
        <div class="buttons-container">
          <button class="ListAreas" @click="toggleAreaList" :class="{ 'selected': showAreaList, 'nothing': !showAreaList }">{{ $t('List of Areas') }}</button>
          <button class="ListUser" @click="toggleUserList" :class="{ 'selected': showUserList, 'nothing': !showUserList }">{{ $t('User list') }}</button>
          <button class="Delete-button" @click="showDeleteOptions = !showDeleteOptions" :class="{ 'selected': showDeleteOptions, 'nothing': !showDeleteOptions }">{{ $t('Delete user') }}</button>
          <button class="Delete-Area-button" @click="showAreaOption = !showAreaOption" :class="{ 'selected': showAreaOption, 'nothing': !showAreaOption }">{{ $t('Delete an Area') }}</button>
          <button class="Admin-button" @click="showAdminOptions = !showAdminOptions" :class="{ 'selected': showAdminOptions, 'nothing': !showAdminOptions }">{{ $t('Make a user admin') }}</button>
          <button class="Disconnect-button" @click="showDisconnectOptions = !showDisconnectOptions" :class="{ 'selected': showDisconnectOptions, 'nothing': !showDisconnectOptions }">{{ $t('Logout a user') }}</button>
          <button class="Status-button" @click="showStatusOption = !showStatusOption" :class="{ 'selected': showStatusOption, 'nothing': !showStatusOption }">{{ $t('Change user status') }}</button>
        </div>
      </div>

      <div class="delete" v-if="showDeleteOptions">
        <input class="palceholder" v-model="userIdToDelete" :placeholder="$t('ID of the user to delete')">
        <button class="confirm" @click="DeleteUser">{{ $t('Confirm delete') }}</button>
      </div>
      <div class="delete" v-if="showAdminOptions">
        <input class="palceholder" v-model="UsertoAdmin" :placeholder="$t('ID of the user to make Admin')">
      <button class="confirm" @click="MakeAdmin">{{ $t('Confirm promote') }}</button>
    </div>
    <div class="delete" v-if="showDisconnectOptions">
      <input class="palceholder" v-model="UserIdToDisconnect" :placeholder="$t('ID of the user to logout')">
      <button class="confirm" @click="MakeDisconnect">{{ $t('Confirm Logout') }}</button>
    </div>
    <div class="delete" v-if="showStatusOption">
      <input class="palceholder" v-model="UserIdToChangeStatus" :placeholder="$t('ID of the user to change the status')">
      <button class="confirm" @click="ChangeStatus">{{ $t('Confirm status change') }}</button>
    </div>
    <div class="delete" v-if="showAreaOption">
      <input class="palceholder" v-model="AreaIdToDelete" :placeholder="$t('ID of Area to delete')">
      <button class="confirm" @click="DeleteArea">{{ $t('Confirme delete of Area') }}</button>
    </div>

    <div v-if="showUserList && users.length > 0">
        <ul class="user-list">
          <h2>{{ $t('User list : ') }}</h2>
          <li v-for="user in users" :key="user.id" class="box">
            <p class="bold-text">ID : {{ user.id }}</p>
            <p class="bold-text">{{ $t('FirstName') }} : {{ user.firstname }}</p>
            <p class="bold-text">{{ $t('Name') }} : {{ user.lastname }}</p>
            <p class="bold-text">Email : {{ user.email }}</p>
            <p class="bold-text">{{ $t('Creation date') }} : {{ user.dateCreation }}</p>
        </li>
      </ul>
    </div>

    <div v-if="showAreaList && areas.length > 0">
      <h2>{{ $t('List of Areas : ') }}</h2>
      <ul class="areas-list">
        <li v-for="area in areas" :key="area.id" class="box">
          <p class="bold-text">{{ $t('ID of the Area : ') }} {{ area.id }}</p>
          <p class="bold-text">{{ $t('Name of the Area : ') }} {{ area.name }}</p>
          <p class="bold-text">{{ $t('Description : ') }} : {{ area.description }}</p>
        </li>
      </ul>
    </div>

  </div>
  
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
const firstname = ref('');
const lastname = ref('')
const users = ref<any[]>([]);
const areas = ref<any[]>([]);
const userIdToDelete = ref('');
const UsertoAdmin = ref('');
const AreaIdToDelete = ref('');
const UserIdToDisconnect = ref('');
const UserIdToChangeStatus = ref('');
const showDeleteOptions = ref(false);
const showAdminOptions = ref(false);
const showDisconnectOptions = ref(false);
const showStatusOption = ref(false);
const showAreaOption = ref(false);
const showUserList = ref(false);
const showAreaList = ref(false);
const showDeleteUser = ref(false);
const showMakeAdmin = ref(false);
const ShowLogout = ref(false);
const showChangeStatus = ref(false);
const showDeleteArea = ref(false);

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
    firstname.value = response.data.firstname;
    lastname.value = response.data.lastname;
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

const CheckAdminSecurity = () => {
  if (localStorage.getItem('AdminConnectedCheck') === 'true') {
    return true;
  } else {
    router.push('/Account');
    return false;
  }
}

const ListUser = () => {
  axios.get('http://localhost:8080/user/listUser', {
    headers: {
      Authorization: `Bearer ${localStorage.getItem('authToken')}`,
    },
  })
  .then((response) => {
    console.log(response.data);
    users.value = response.data;
    for (const user of users.value) {
      user.dateCreation = formatISODate(user.dateCreation);
    }
  })
  .catch((error) => {
    console.error(error);
  });
}

const formatISODate = (isoDate: string) => {
  const dateObj = new Date(isoDate);
  const year = dateObj.getFullYear();
  const month = String(dateObj.getMonth() + 1).padStart(2, '0');
  const day = String(dateObj.getDate()).padStart(2, '0');
  return `${day}-${month}-${year}`;
}

const DeleteUser = () => {
  const userId = userIdToDelete.value;
  if (userId) {
    axios.delete(`http://localhost:8080/user/deleteUserID?IdUser=${userId}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      },
    })
    .then((response) => {
      console.log(response);
      showDeleteOptions.value = false;
    })
    .catch((error) => {
      console.error(error);
    });
  } else {
    console.error("L'ID de l'utilisateur à supprimer est vide.");
  }
}

const MakeAdmin = () => {
  const userId = UsertoAdmin.value;
  if (userId) {
    axios.put('http://localhost:8080/user/set-admin', {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        idUser: userId,
      },
    })
    .then((response) => {
      console.log(response);
      showAdminOptions.value = false;
    })
    .catch((error) => {
      console.error(error);
    });
  } else {
    console.error("L'ID de l'utilisateur à promouvoir en tant qu'administrateur est vide.");
  }
}

const MakeDisconnect = () => {
  const userId = UserIdToDisconnect.value;
  if (userId) {
    axios.put(`http://localhost:8080/user/disconnect?IdUser=${userId}`, null, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      },
    })
    .then((response) => {
      console.log(response);
      showDisconnectOptions.value = false;
    })
    .catch((error) => {
      console.error(error);
    });
  } else {
    console.error("L'ID de l'utilisateur à déconnecter est vide.");
  }
}

const ChangeStatus = () => {
  const userId = UserIdToChangeStatus.value;
  if (userId) {
    axios.put(`http://localhost:8080/user/changeStatus?IdUser=${userId}`, null, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      },
    })
    .then((response) => {
      console.log(response);
      showStatusOption.value = false;
    })
    .catch((error) => {
      console.error(error);
    });
  } else {
    console.error("L'ID de l'utilisateur à changer le status est vide.");
  }
}

const DeleteArea = () => {
  const userId = AreaIdToDelete.value;
  if (userId) {
    axios.delete(`http://localhost:8080/area/deleteArea?IdArea=${userId}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem('authToken')}`,
      },
    })
    .then((response) => {
      console.log(response);
      showAreaOption.value = false;
    })
    .catch((error) => {
      console.error(error);
    });
  } else {
    console.error("L'ID de l'utilisateur à changer le status est vide.");
  }

}

const ListAreas = () => {
  axios.get('http://localhost:8080/area/listArea', {
    headers: {
      Authorization: `Bearer ${localStorage.getItem('authToken')}`,
    },
  })
  .then((response) => {
    console.log(response.data)
    areas.value = response.data;
  })
  .catch((error) => {
    console.error(error);
  });
}

const toggleUserList = () => {
  showUserList.value = !showUserList.value;
  if (showUserList.value) {
    ListUser();
  }
};

const toggleAreaList = () => {
  showAreaList.value = !showAreaList.value;
  if (showAreaList.value) {
    ListAreas();
  }
};

onMounted(() => {
   check_security();
   CheckAdminSecurity();
});

</script>

<style scoped>
.background {
  position: fixed;
  width: 100vw;
  height: calc(100vh - 100px);
  left: 0px;
  top: 100px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-start;
  overflow-y: auto;
  background-color: pink;
}

.Admin-Text {
  font-size: 32px;
  color: red;
  margin-bottom: 40px;
}

.selected {
  background-color: #ff0000;
}

.nothing {
  background-color: #007bff;
}

.buttons-container {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  background-color: aqua;
  flex-wrap: wrap;
}

.buttons-container button {
  color: white;
  padding: 10px 20px;
  border: none;
  cursor: pointer;
  border-radius: 5px;
  margin: 5px;
}


@media (max-width: 768px) {
  .buttons-container {
    flex-direction: column;
    text-align: center;
  }
}


.user-areas-container {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  width: 50%;
  padding: 10px;
}

.box {
  background-color: black;
  color: white;
  padding: 0px;
  margin-block: 20px;
  border-radius: 10px;
  font-family: 'Roboto', sans-serif;
}

.bold-text {
  font-weight: bold;
  margin: 3px 1px;
  color: #ffffff;
  text-align: center;
}

.delete {
  font-size: 20px;
  color: #ffffff;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 600;
  transition: 0.3s;
  display: inline-block;
}
.confirm {
  width: 400px;
  font-size: 20px;
  color: #ffffff;
  text-decoration: none;
  font-family: 'Roboto', sans-serif;
  font-weight: 600;
  transition: 0.3s;
  display: inline-block;
  background-color: #ff0000;
  border-radius: 5px;
  padding: 3px;
  border: none;
  cursor: pointer;
}

.confirm:hover {
  background-color: #ae0202af;
}

.palceholder {
  width: 400px;
}

</style>

