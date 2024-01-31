<template>
    <div v-if="isVisible" class="popup">
      <div class="popup-content">
        <p class="message-text">{{ message }}</p>
        <button class="button_exit" @click="closePopup">OK</button>
      </div>
    </div>
</template>

<script lang="ts">
import { ref } from 'vue';

export default {
  props: {
    isVisible: Boolean,
    message: String,
  },
  setup(props, { emit }) {
    const closePopup = () => {
      emit('close');
    };
    return {
     closePopup,
    };
  },
};
</script>

<style scoped>
.popup {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  background: none;
  z-index: 999;
}

.popup-content {
  background: #fff;
  padding: 30px;
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
  text-align: center;
}

.message-text {
  font-size: 1.2rem;
  font-weight: 600;
  margin-bottom: 20px;
  color: #333;
}

.button_exit {
  padding: 10px 20px;
  font-weight: 600;
  text-decoration: none;
  border-radius: 20px;
  transition: background-color 0.3s, color 0.3s;
  background: #f6805b;
  color: #fff;
  border: none;
  cursor: pointer;
}

.button_exit:hover {
  background-color: #c9522e;
}
</style>
