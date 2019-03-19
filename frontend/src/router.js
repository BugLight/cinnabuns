import Vue from 'vue';
import Router from 'vue-router';

import Home from './components/Home.vue';
import Canteen from './components/Canteen.vue';

Vue.use(Router);

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/canteen/:id',
      name: 'canteen',
      component: Canteen
    }
  ]
})