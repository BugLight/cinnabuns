import Vue from 'vue';

import App from './components/App.vue';
import router from './router';

const app = new Vue({
  components: {
    App
  },
  router,
  render: h => h(App)
}).$mount('#app')