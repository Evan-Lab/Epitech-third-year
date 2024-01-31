<script setup lang="ts">
import { RouterLink } from 'vue-router'
import HeaderVue from '../components/HeaderVue.vue';
</script>

<template>
  <HeaderVue></HeaderVue>

  <div class="Contact">
    <img alt="Vue-logo" class="logo" src="@/assets/Logo.png"/>

    <span class="AreaCraft-Email">craft.area94@gmail.com</span>
    <span class="Numéro">+ 33 6 31 13 86 75</span>
    <span class="Credits">{{ $t('© 2023 AreaCraft. All right reserved') }}</span>

  </div>
</template>

<style scoped>

.AreaCraft-Email {
  font-size: 24px;
  color: #000000;
  font-weight: 800;
  position: absolute;
  top: 51%;
  left: 50%;
  transform: translateX(-50%);
}

.Numéro {
  font-size: 24px;
  color: #000000;
  font-weight: 800;
  position: absolute;
  top: 61%;
  left: 50%;
  transform: translateX(-50%);
}

.Tel-Logo {
  position:absolute;
  top: 61%;
  left: 39%;
  border-radius: 20px;
}

.Email-Logo {
  display: block;
  margin: 0 auto 2rem;
  position:absolute;
  top: 51%;
  left: 37%;
  border-radius: 20px;
}

.Credits {
  font-size: 18px;
  color: #000000;
  font-weight: 800;
  text-align: center;
  position: absolute;
  bottom: 30px;
  left: 50%;
  transform: translateX(-50%);
}


.logo {
  position:absolute;
  top: 20%;
  border-radius: 55%;
  width: auto;
  height: 25%;
  transform: scale(1);
  font-weight: 800;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
}

.text-logo {
  position: absolute;
  top: 20%;
  left: 35%;
  width: auto;
  height: 110px;
  transform: scale(1);
}


.link {
  color: #9C9C9C;
  font-size: 24px;
}

.link:hover {
  text-decoration: underline;
  background-color: transparent;
}


.link-home {
  position: absolute;
  top: 38px;
  left: 900px;
}

.link-contact {
  position: absolute;
  top: 38px;
  left: 1088px;
}

</style>