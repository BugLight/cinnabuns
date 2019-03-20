import Vue from 'vue';
import Resource from 'vue-resource';
import Vuex from 'vuex';
import { library } from '@fortawesome/fontawesome-svg-core';
import { faCreditCard, faShoppingBasket, faTimes, faEdit } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { polyfill } from 'es6-object-assign';


polyfill();

library.add(faCreditCard, faShoppingBasket, faTimes, faEdit);


Vue.component('font-awesome-icon', FontAwesomeIcon);

Vue.config.productionTip = false;

import App from './components/App.vue';
import router from './router';

Vue.use(Resource);
Vue.use(Vuex);

/**
 * Vuex - global state storage
 */

const store = new Vuex.Store({
  state: {
    activeCanteen: null,
    canteens: null,
    accessToken: localStorage.getItem('accessToken') || ''
  },
  mutations: {
    setCanteens (state, canteens) {
      state.canteens = canteens;
    },
    setActiveCanteen (state, canteen) {
      state.activeCanteen = canteen;
    },
    setAccessToken (state, token) {
      state.accessToken = token;
      localStorage.setItem('accessToken', token);
    },
    clearAccessToken (state) {
      state.accessToken = '';
      localStorage.removeItem('accessToken');
    }
  },
  getters: {
    isAuthenticated (state) {
      return !!state.accessToken;
    },
    accessToken (state) {
      return state.accessToken;
    }
  },
  actions: {
    auth (context, login) {
      return new Promise((res, rej) => {
        Vue.http.post(BACK_URL + '/api/auth', login).then(response => {
          if (response.status != 200) {
            context.commit('clearAccessToken');
            rej();
          } else {
            let token = response.body;
            context.commit('setAccessToken', token)
            res(token);
          }
        }).catch(e => {
          context.commit('clearAccessToken');
          rej(e);
        });
      });
    }
  }
})

const app = new Vue({
  components: {
    App
  },
  router,
  store,
  render: h => h(App)
}).$mount('#app');