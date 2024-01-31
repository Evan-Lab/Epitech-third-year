<template>
  <div v-if="!isAreasParameter" class="page">
    <div class="header">
      <button @click="backToCreate" class="undescore-button">&lt;</button>
      <button @click="backToCreate" class="back-button">{{ $t('Back') }}</button>
      <span class="text">{{ $t('Choose an action') }}</span>
    </div>
    <div class="body">
      <div class="search-container">
        <input type="text" v-model="searchText"  :placeholder="$t('Search service')" class="search-bar" />
      </div>
      <div class="images-container">
        <div v-for="action in searchText === '' ? actions : filteredActions" :key="action.name" class="image-container">
          <img :src="action.image" :alt="action.name" class="action-image" @click="selectAction(action)" />
        </div>
      </div>
    </div>
  </div>
  <Home v-if="selectedTrigger" :selectedTrigger="selectedTrigger" @back-to-areas="resetSelectedComponent" @return-value="receiveValueFromComponent" />
</template>


<script setup lang="ts">

import WeatherImage from '@/assets/Weather.png'
import GithubImage from '@/assets/Github.png'
import YoutubeImage from '@/assets/Youtube.png'
import SpotifyImage from '@/assets/Spotify.png'
import worldtimeImage from '@/assets/Worldtime.png'
import Time from '@/assets/Time.png'




import WeatherTime from '@/components/Areas/Weather/Action/Time.vue'

import GithubCreate from '@/components/Areas/Github/Action/RepoCreate.vue'
import GithubPush from '@/components/Areas/Github/Action/RepoPush.vue'
import GithubPullRequest from '@/components/Areas/Github/Action/RepoPullRequest.vue'
import GithubNewCollaborator from '@/components/Areas/Github/Action/NewCollaborator.vue'
import GithubNewBranch from '@/components/Areas/Github/Action/NewBranch.vue'
import GithubNewPullRequestClosed from '@/components/Areas/Github/Action/NewPullRequestClosed.vue'
import GithubDeleteBranch from '@/components/Areas/Github/Action/DeleteBranch.vue'

import YoutubeCommentary from '@/components/Areas/Youtube/Action/Commentary.vue'
import YoutubeLike from '@/components/Areas/Youtube/Action/Like.vue'
import YoutubeChanelUrl from '@/components/Areas/Youtube/Action/ChanelUrl.vue'
import YoutubeChanel from '@/components/Areas/Youtube/Action/ChanelNewVideo.vue'
import YoutubeSubscriber from '@/components/Areas/Youtube/Action/Subscriber.vue'

import SpotifyCreatePlaylist from '@/components/Areas/Spotify/Action/CreatePlaylist.vue'
import SpotifyNewTrack from '@/components/Areas/Spotify/Action/NewTrack.vue'
import SpotifyNewFollower from '@/components/Areas/Spotify/Action/NewFollower.vue'

import WorldTimeTimer from '@/components/Areas/WorldTime/Action/Timer.vue'


import Home from '@/components/Home.vue'

import { ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { onMounted, computed } from 'vue';
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


const searchText = ref('');
const filteredActions = ref<{ name: string; image: string; trigger: any }[]>([]);

const router = useRouter();
const emit = defineEmits(['update-action-values', 'show-create']);
const selectedComponent = ref(null);
const isAreasParameter = ref(false);


let spotifyNeedConnection = localStorage.getItem('TestConnected') == 'true' ? true : false;
let gitHubNeedConnection = localStorage.getItem('TestConnectedGithub') == 'true' ? true : false;
let youtubeNeedConnection = localStorage.getItem('TestConnectedYoutube') == 'true' ? true : false;




const weatherActions = [
  { 
    name: 'Time',
    title:  t('Receive the current temperature'),
    description:  t('Set a time and a city and you will receive the current temperature of the city at the time you indicated'),
    show: true,
    component: WeatherTime },
];
const weatherTrigger = {
  background_color: "#4f5d73", areas: weatherActions
}

const githubActions = [
  { 
    name: 'Create',
    title:  t('Do something when you create a repository'),
    description:  t('When you create a repository you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubCreate },
  {
    name: 'Push',
    title:  t('Do something when you push'),
    description:  t('When you push on the repository you indicated you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubPush },
  { 
    name: 'Pull Request',
    title:  t('Do something when you create a pull request'),
    description:  t('When you create a pull request you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubPullRequest },
  { 
    name: 'New Collaborator',
    title:  t('Do something when you have a new collaborator'),
    description:  t('When you have a new collaborator you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubNewCollaborator },
  { 
    name: 'New Branch',
    title:  t('Do something when you create a new branch'),
    description:  t('When you create a new branch you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubNewBranch },
  { 
    name: 'New Pull Request Closed',
    title:  t('Do something when a pull request is closed'),
    description:  t('When you a pull request is closed you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubNewPullRequestClosed },
  { 
    name: 'Github Delete Branch',
    title:  t('Do something when you delete a branch'),
    description:  t('When you delete a branch you will do something or receive a notification'),
    show: gitHubNeedConnection,
    component: GithubDeleteBranch },
];
const githubTrigger = {
  background_color: "#333", areas: githubActions
}

const youtubeActions = [
  { 
    name: 'Chanel',
    title:  t('Do something when your channel publish a video'),
    description:  t('When your channel publish a video you will do something or receive a notification'),
    show: youtubeNeedConnection,
    component: YoutubeChanel },
  { 
    name: 'Subscriber',
    title:  t('Do something when you receive a subscriber'),
    description:  t('When you receive a subscriber you will do something or receive a notification'),
    show: youtubeNeedConnection,
    component: YoutubeSubscriber },
];
const youtubeTrigger = {
  background_color: "#ff0000", areas: youtubeActions
}

const spotifyActions = [
  { 
    name: 'Create Playlist',
    title:  t('Do something when you create a playlist'),
    description: t('When you create a playlist you will do something or receive a notification'),
    show: spotifyNeedConnection,
    component: SpotifyCreatePlaylist },
  {
    name: 'New Track',
    title:  t('Do something when you put a music in a playlist'),
    description:  t('When you put a music in a playlist you will do something or receive a notification'),
    show: spotifyNeedConnection,
    component: SpotifyNewTrack },
  { 
    name: 'New Follower',
    title:  t('Do something when you receive a new follower'),
    description:  t('When you receive a new follower you will do something or receive a notification'),
    show: spotifyNeedConnection,
    component: SpotifyNewFollower },
];
const spotifyTrigger = {
  background_color: "#1db954", areas: spotifyActions
}

const worldtimeActions = [
  { 
    name: 'Create Playlist',
    title:  t('Create a timer'),
    description: t('When you create a timer with date and time you will do something or receive a notification'),
    show: true,
    component: WorldTimeTimer },
];
const worldtimeTrigger = {
  background_color: "#4f5d73", areas: worldtimeActions
}



export type Trigger = {
  background_color: string;
  areas: Array<{
    name: string;
    title: string;
    description: string;
    show: boolean;
    component: any;
  }>;
};

const selectedTrigger = ref<Trigger | null>(null);


const actions = [
  { name: 'Weather', image: WeatherImage, trigger: weatherTrigger},
  { name: 'Github', image: GithubImage, trigger: githubTrigger},
  { name: 'Youtube', image: YoutubeImage, trigger: youtubeTrigger},
  { name: 'Spotify', image: SpotifyImage, trigger: spotifyTrigger},
  { name: 'WorldTime', image: worldtimeImage, trigger: worldtimeTrigger},
];


console.log(selectedTrigger);


const filterActions = () => {
  filteredActions.value = actions.filter(action =>
  action.name.toLowerCase().includes(searchText.value.toLowerCase())
  );
};
watch(searchText, filterActions);


const selectAction = (action: any) => {
  selectedComponent.value = action.component;
  selectedTrigger.value = action.trigger;
  isAreasParameter.value = true;
};

const resetSelectedComponent = () => {
  selectedComponent.value = null;
  selectedTrigger.value = null;
  isAreasParameter.value = false;
};

const receiveValueFromComponent = (data: { value: string, name: string }) => {
  emit('update-action-values', data);
  resetSelectedComponent();
  backToCreate();
};

const backToCreate = () => {
  emit('show-create');
}

</script>


<style scoped>

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

.search-container {
  width: 440px;
  height: 60px;
  display: flex;
  justify-content: center;
}

.search-bar {
  width: 100%;
  border-radius: 10px;
  background-color: #EEEEEE;
  border: none;
  font-weight:800;
  text-align: center;
}

.images-container {
  margin-top: 40px;
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
}

.image-container {
  width: 200px;
  height: 200px;
  margin: 10px;
  text-align: center;
  cursor: pointer;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.action-image {
  width: 170px;
  height: 170px;
  border-radius: 50%;
  object-fit: cover;
}

.back-button {
  background: transparent;
  border: 4px solid rgba(0, 0, 0, 1);
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
