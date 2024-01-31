<template>
  <div v-if="!isAreasParameter" class="page">
    <div class="header">
      <button @click="backToCreate" class="undescore-button">&lt;</button>
      <button @click="backToCreate" class="back-button">{{ $t('Back') }}</button>
      <span class="text">{{ $t('Choose a reaction') }}</span>
    </div>
    <div class="body">
      <div class="search-container">
        <input type="text" v-model="searchText" :placeholder="$t('Search service')" class="search-bar" />
      </div>
      <div class="images-container">
        <div v-for="reaction in searchText === '' ? reactions : filteredReactions" :key="reaction.name" class="image-container">
          <img :src="reaction.image" :alt="reaction.name" class="action-image" @click="selectReaction(reaction)" />
        </div>
      </div>
    </div>
  </div>
  <Home v-if="selectedTrigger" :selectedTrigger="selectedTrigger" @back-to-areas="resetSelectedComponent" @return-value="receiveValueFromComponent" />
</template>


<script setup lang="ts">
import OutlookImage from '@/assets/Outlook.png'
import YoutubeImage from '@/assets/Youtube.png'
import SpotifyImage from '@/assets/Spotify.png'


import OutlookSendMail from '@/components/Areas/Outlook/Reaction/SendMail.vue'

import YoutubeSubscribeToChanel from '@/components/Areas/Youtube/Reaction/SubscribeToChanel.vue'
import YoutubeVideoToPlaylist from '@/components/Areas/Youtube/Reaction/VideoToPlaylist.vue'

import SpotifyFollowPlaylist from '@/components/Areas/Spotify/Reaction/FollowPlaylist.vue'
import SpotifyFollowArtist from '@/components/Areas/Spotify/Reaction/FollowArtist.vue'
import SpotifyMusicToPlaylist from '@/components/Areas/Spotify/Reaction/MusicToPlaylist.vue'


import Home from '@/components/Home.vue'

import { ref, watch } from 'vue';
import { useRouter } from 'vue-router';

import { onMounted, computed } from 'vue';
import i18n from '../../locales/i18n';

const selectedLanguage = ref(localStorage.getItem('language') || 'en');

const isValidLanguage = (language : string) => ['English', 'French', 'Spanish'].includes(language);

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
const filteredReactions = ref<{ name: string; image: string; trigger: any }[]>([]);

const router = useRouter();
const emit = defineEmits(['update-reaction-values', 'show-create']);
const selectedComponent = ref(null);
const isAreasParameter = ref(false);


const outlookNeedConnection = ref(true);
const gitHubNeedConnection = ref(true);
const youtubeNeedConnection = localStorage.getItem('TestConnectedYoutube') == 'true' ? true : false;
let spotifyNeedConnection = localStorage.getItem('TestConnected') == 'true' ? true : false;
const facebookNeedConnection = ref(true);


const outlookReactions = [
  { 
    name: 'SendMail',
    title: t('Send me an email'),
    description: t('Send an email to the account you put in the trigger'),
    show: true,
    component: OutlookSendMail
  },
];
const outlookTrigger = {
  background_color: "#2468ff", areas: outlookReactions
}

const youtubeReactions = [
  { 
    name: 'SubscribeToChanel',
    title:  t('Subscribe to a Youtube channel'),
    description:  t('Subscribe to a Youtube channel when you put in the trigger'),
    show: youtubeNeedConnection,
    component: YoutubeSubscribeToChanel
  },
  { 
    name: 'VideoToPlaylist',
    title:  t('Video to Playlist'),
    description:  t('Put the Youtube video of your choice in the Youtube playlist of your choice'),
    show: youtubeNeedConnection,
    component: YoutubeVideoToPlaylist
  },
];
const youtubeTrigger = {
  background_color: "#ff0000", areas: youtubeReactions
}

const spotifyReactions = [
  { 
    name: 'FollowPlaylist',
    title:  t('Follow a Spotify playlist'),
    description:  t('Follow a Spotify playlist you put in the trigger'),
    show: spotifyNeedConnection,
    component: SpotifyFollowPlaylist
  },
  { 
    name: 'FollowArtist',
    title:  t('Follow a Spotify artist'),
    description:  t('Follow a Spotify artist you put in the trigger'),
    show: spotifyNeedConnection,
    component: SpotifyFollowArtist
  },
  { 
    name: 'MusicToPlaylist',
    title:  t('Music to Playlist'),
    description:  t('Put the Spotify music of your choice in the Spotify playlist of your choice'),
    show: spotifyNeedConnection,
    component: SpotifyMusicToPlaylist
  },
];
const spotifyTrigger = {
  background_color: "#1db954", areas: spotifyReactions
}




export type Trigger = {
  background_color: string;
  areas: Array<{
    name: string;
    title: string;
    description: string;
    component: any;
    show: boolean,
  }>;
};

const selectedTrigger = ref<Trigger | null>(null);


const reactions = [
  { name: 'Outlook', image: OutlookImage, trigger: outlookTrigger},
  { name: 'Youtube', image: YoutubeImage, trigger: youtubeTrigger},
  { name: 'Spotify', image: SpotifyImage, trigger: spotifyTrigger},
];


console.log(selectedTrigger);


const filterReactions = () => {
  filteredReactions.value = reactions.filter(reaction =>
  reaction.name.toLowerCase().includes(searchText.value.toLowerCase())
  );
};
watch(searchText, filterReactions);


const selectReaction = (reaction: any) => {
  selectedComponent.value = reaction.component;
  selectedTrigger.value = reaction.trigger;
  isAreasParameter.value = true;
};

const resetSelectedComponent = () => {
  selectedComponent.value = null;
  selectedTrigger.value = null;
  isAreasParameter.value = false;
};

const receiveValueFromComponent = (data: { value: string, name: string }) => {
  emit('update-reaction-values', data);
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
  text-align: center;
  font-weight:800;
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
