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
.app {
  margin: 0;
  padding: 0;
}
</style>