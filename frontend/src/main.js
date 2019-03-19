import Vue from 'vue';
import Resource from 'vue-resource';

import App from './components/App.vue';
import router from './router';

Vue.use(Resource);

const app = new Vue({
  components: {
    App
  },
  router,
  render: h => h(App)
}).$mount('#app')