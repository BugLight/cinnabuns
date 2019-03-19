<template>
  <div class="app">
    <Header></Header>
    <router-view></router-view>
  </div>
</template>
<script>
import Header from './Header.vue';
export default {
  components: {
    Header
  }
}
</script>

<style lang="scss">
@import url('https://fonts.googleapis.com/css?family=Roboto');
.app {
  margin: 0;
  padding: 0;
  font-family: 'Roboto', sans-serif; 
}
</style>