<template>
  <HeaderVue></HeaderVue>

  <div class="background">
    <div class="logos-container">
      <div class="logo-item">
        <img alt="Spotify-logo" class="logo" src="@/assets/Spotify.png" />
        <button v-if="!SpotifyConnected" type="submit" class="button spotify" @click="login_Spotify">{{ $t('Login with Spotify') }}</button>
        <button v-if="SpotifyConnected" type="submit" class="button spotify">{{ $t('Connected') }}</button>
      </div>
      <div class="logo-item">
        <img alt="Github-logo" class="logo" src="@/assets/Github.png" width="100" height="100" />
        <button v-if="!GithubConnected" type="submit" class="button github" @click="login_Github">{{ $t('Login with Github') }}</button>
        <button v-if="GithubConnected" type="submit" class="button github">{{ $t('Connected') }}</button>
      </div>
      <div class="logo-item">
        <img alt="Vue-logo" class="logo" src="@/assets/Youtube.png" width="100" height="75" />
        <button v-if="!GoogleConnected" type="submit" class="button youtube" @click="login_Google">{{ $t('Login with Youtube') }}</button>
        <button v-if="GoogleConnected" type="submit" class="button youtube">{{ $t('Connected') }}</button>
      </div>


    </div>
  </div>
</template>

<script setup lang="ts">
import { RouterLink, useRoute, useRouter } from 'vue-router'
import axios from 'axios'
import { ref, onMounted } from 'vue';
import i18n from '../../locales/i18n';
import HeaderVue from '../components/HeaderVue.vue';

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

const route = useRoute()
const router = useRouter();
const firstname = ref('');
const lastname = ref('')
const email = ref('')
const creation_date = ref('')
let SpotifyConnected = false
let GithubConnected = false
let FacebookConnected = false
let GoogleConnected = false

if (localStorage.getItem('SpotifyConnectedCheck') != 'true')
  localStorage.setItem('SpotifyConnectedCheck', 'false')
if (localStorage.getItem('GithubConnectedCheck') != 'true')
  localStorage.setItem('GithubConnectedCheck', 'false')
if (localStorage.getItem('FacebookConnectedCheck') != 'true')
  localStorage.setItem('FacebookConnectedCheck', 'false')
if (localStorage.getItem('GoogleConnectedCheck') != 'true')
  localStorage.setItem('GoogleConnectedCheck', 'false')

if (localStorage.getItem('SpotifyConnectedCheck') == 'true')
  SpotifyConnected = true
if (localStorage.getItem('GithubConnectedCheck') == 'true')
  GithubConnected = true
if (localStorage.getItem('FacebookConnectedCheck') == 'true')
  FacebookConnected = true
if (localStorage.getItem('GoogleConnectedCheck') == 'true')
  GoogleConnected = true

if (localStorage.getItem('SpotifyConnected') != 'true')
  localStorage.setItem('SpotifyConnected', 'false')
if (localStorage.getItem('GithubConnected') != 'true')
  localStorage.setItem('GithubConnected', 'false')
if (localStorage.getItem('FacebookConnected') != 'true')
  localStorage.setItem('FacebookConnected', 'false')
if (localStorage.getItem('GoogleConnected') != 'true')
  localStorage.setItem('GoogleConnected', 'false')

const authTokenForrequest = localStorage.getItem('authToken');

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
      email.value = response.data.email;

      const fullDate = response.data.dateCreation;
      const dateObj = new Date(fullDate);
      const formattedDate = dateObj.toISOString().split('T')[0];
      creation_date.value = formattedDate;
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

async function login_Spotify() {
    let spotify_url = '';

    fetch("http://localhost:8080/spotify/link", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then(async (response) => {
        if (response.status === 200) {
            response.text().then((data) => {
                console.log(data);
                spotify_url = data;
                open(spotify_url, '_self');
                localStorage.setItem('SpotifyConnected', 'true')
            });
        } else {
            console.error('Erreur lors de la connexion');
        }
    })
    .catch((error) => {
        console.error('Erreur lors de la connexion :', error);
    });
}

function spotify_2() {

  const interval = setInterval(() => {
    const params = new URLSearchParams(window.location.search);
    const _code = params.get('code');

    if (_code) {
      clearInterval(interval);

      fetch("http://localhost:8080/spotify/token", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${authTokenForrequest}`,
        },
        body: JSON.stringify({
          code: _code,
        }),
      })
        .then(async (response) => {
          if (response.status === 200) {
            console.log('Response : ' + response)
            localStorage.setItem('SpotifyConnected', 'false')
            localStorage.setItem('SpotifyConnectedCheck', 'true')
          } else {
            console.error('Erreur lors de la connexion');
          }
        })
        .catch((error) => {
          console.error('Erreur lors de la connexion :', error);
        });
    }
  }, 1000);
}

async function login_Github() {
    let github_url = '';

    fetch("http://localhost:8080/github/link", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then(async (response) => {
        if (response.status === 200) {
            response.text().then((data) => {
                console.log(data);
                github_url = data;
                open(github_url, '_self');
                localStorage.setItem('GithubConnected', 'true')
            });
        } else {
            console.error('Erreur lors de la connexion');
        }
    })
    .catch((error) => {
        console.error('Erreur lors de la connexion :', error);
    });
}

function Github2() {
  const interval = setInterval(() => {
    const params = new URLSearchParams(window.location.search);
    const _code = params.get('code');
    if (_code) {
      clearInterval(interval);

      fetch("http://localhost:8080/github/token", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${authTokenForrequest}`,
        },
        body: JSON.stringify({
          code: _code,
        }),
      })
        .then(async (response) => {
          if (response.status === 200) {
            console.log('Response : ' + response)
            localStorage.setItem('GithubConnected', 'false')
            localStorage.setItem('GithubConnectedCheck', 'true')
          } else {
            console.error('Erreur lors de la connexion');
          }
        })
        .catch((error) => {
          console.error('Erreur lors de la connexion :', error);
        });
    }
  }, 1000);
}

async function login_Facebook() {
    let facebook_url = '';

    fetch("http://localhost:8080/facebook/link", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then(async (response) => {
        if (response.status === 200) {
            response.text().then((data) => {
                console.log(data);
                facebook_url = data;
                open(facebook_url, '_self');
                localStorage.setItem('FacebookConnected', 'true')
            });
        } else {
            console.error('Erreur lors de la connexion');
        }
    })
    .catch((error) => {
        console.error('Erreur lors de la connexion :', error);
    });
}

function Facebook_2() {
  const interval = setInterval(() => {
    const params = new URLSearchParams(window.location.search);
    const _code = params.get('code');
    if (_code) {
      clearInterval(interval);

      fetch("http://localhost:8080/facebook/token", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${authTokenForrequest}`,
        },
        body: JSON.stringify({
          code: _code,
        }),
      })
        .then(async (response) => {
          if (response.status === 200) {
            console.log('Response : ' + response)
            localStorage.setItem('FacebookConnected', 'false')
            localStorage.setItem('FacebookConnectedCheck', 'true')
            console.log("FacebookConnectedCheck: ", localStorage.getItem('FacebookConnectedCheck'))
          } else {
            console.error('Erreur lors de la connexion');
          }
        })
        .catch((error) => {
          console.error('Erreur lors de la connexion :', error);
        });
    }
  }, 1000);
}

async function login_Discord() {
    let discord_url = '';

    fetch("http://localhost:8080/discord/link", {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    })
    .then(async (response) => {
        if (response.status === 200) {
            response.text().then((data) => {
                console.log(data);
                discord_url = data;
                open(discord_url, '_self');
            });
        } else {
            console.error('Erreur lors de la connexion');
        }
    })
    .catch((error) => {
        console.error('Erreur lors de la connexion :', error);
    });
}

function Discord_2() {
  const interval = setInterval(() => {
    const params = new URLSearchParams(window.location.search);
    const _code = params.get('code');
    if (_code) {
      clearInterval(interval);

      fetch("http://localhost:8080/discord/token", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          code: _code,
        }),
      })
        .then(async (response) => {
          if (response.status === 200) {
            console.log('Response : ' + response)
          } else {
            console.error('Erreur lors de la connexion');
          }
        })
        .catch((error) => {
          console.error('Erreur lors de la connexion :', error);
        });
    }
  }, 1000);
}

async function login_Google() {
    let google_url = '';

    fetch("http://localhost:8080/google/link", {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
        pageName: 'account',
        }),
    })
    .then(async (response) => {
        if (response.status === 200) {
            response.text().then((data) => {
                console.log(data);
                google_url = data;
                open(google_url, '_self');
                localStorage.setItem('GoogleConnected', 'true')
            });
        } else {
            console.error('Erreur lors de la connexion');
        }
    })
    .catch((error) => {
        console.error('Erreur lors de la connexion :', error);
    });
}

function login_Google_2() {
    const interval = setInterval(() => {
    const params = new URLSearchParams(window.location.search);
    const _code = params.get('code');
    if (_code) {
      clearInterval(interval);

      fetch("http://localhost:8080/google/token", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${authTokenForrequest}`,
        },
        body: JSON.stringify({
          code: _code,
          pageName: 'account',
        }),
      })
        .then(async (response) => {
          if (response.status === 200) {
            console.log('Response : ' + response)
            localStorage.setItem('GoogleConnected', 'false')
            localStorage.setItem('GoogleConnectedCheck', 'true')
          } else {
            console.error('Erreur lors de la connexion');
          }
        })
        .catch((error) => {
          console.error('Erreur lors de la connexion :', error);
        });
    }
  }, 1000);
}

function checkCodeAndPerformAction() {

  const params = new URLSearchParams(window.location.search);
  const _code = params.get('code');

  if (_code) {
    console.log('Code détecté : ' + _code);
    let SpotifyConnectedCheck = localStorage.getItem('SpotifyConnected')
    let GithubConnectedCheck = localStorage.getItem('GithubConnected')
    let FacebookConnectedCheck = localStorage.getItem('FacebookConnected')
    let GoogleConnectedCheck = localStorage.getItem('GoogleConnected')
    console.log("Spotify : ", localStorage.getItem('SpotifyConnectedCheck'))
    console.log("Github : ", localStorage.getItem('GithubConnectedCheck'))
    console.log("Facebook : ", localStorage.getItem('FacebookConnectedCheck'))
    console.log("Google : ", localStorage.getItem('GoogleConnectedCheck'))

    if (SpotifyConnectedCheck == 'true') {
      spotify_2();
      localStorage.setItem('TestConnected', 'true')
      localStorage.setItem('SpotifyConnected', 'false')
      setTimeout(() => {
      location.reload();
      }, 2000);
    }
    if (GithubConnectedCheck == 'true') {
      Github2();
      localStorage.setItem('TestConnectedGithub', 'true')
      localStorage.setItem('GithubConnected', 'false')
      setTimeout(() => {
      location.reload();
      }, 2000);
    }
    if (FacebookConnectedCheck == 'true') {
      Facebook_2();
      localStorage.setItem('FacebookConnected', 'false')
      setTimeout(() => {
      location.reload();
      }, 2000);
    }
    if (GoogleConnectedCheck == 'true') {
      login_Google_2();
      localStorage.setItem('TestConnectedYoutube', 'true')
      localStorage.setItem('GoogleConnected', 'false')
      setTimeout(() => {
      location.reload();
      }, 2000);
    }
  }
}

checkCodeAndPerformAction();

</script>

<style scoped>
.background {
  width: 100vw;
  min-height: calc(100vh - 100px);
  position: absolute;
  top: 100px;
  left: 0;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  overflow-y: auto;
}

.logos-container {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  flex-wrap: wrap;
  gap: 200px;
}

.logo-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  text-align: center;
}

.logo {
  width: 100px;
  height: 100px;
  border-radius: 100px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

.spotify {
  background-color: #62b350;
  color: #fff;
}

.github {
  background-color: #7289DA;
  color: #fff;
}

.facebook {
  background-color: #3B5998;
  color: #fff;
}

.youtube {
  background-color: #FF0000;
  color: #fff;
}

.button {
  margin-top: 10px;
  margin: 0 auto 2rem;
  border-radius: 100px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
  border: none;
  padding: 10px 20px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: 0.3s;
  margin-top: 20px;
}
</style>