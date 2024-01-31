import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue/dist/bootstrap-vue.css';

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import i18n from "../locales/i18n"

const app = createApp(App);

app.use(router).use(i18n).mount('#app');
