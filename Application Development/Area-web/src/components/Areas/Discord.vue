<template>
  <div>
    <span>Discord Page</span>
    <button @click="choiceOne">Choice 1</button>
    <button @click="choiceTwo">Choice 2</button>
    <button @click="backToAreas">Back to Areas</button>
  </div>
</template>


<script setup>

const emit = defineEmits();

const choiceOne = () => {
  emit('return-value', 'Discord choice 1');
  emit('back-to-areas');
};

const choiceTwo = () => {
  emit('return-value', 'Discord choice 2');
  emit('back-to-areas');
};

const backToAreas = () => {
  emit('back-to-areas');
};
</script>