import Vue from 'vue';
import App from './App.vue';
import router from './router';
import axios from 'axios';
import VueAxios from 'vue-axios';
import VueRouter from 'vue-router';
import VueMoment from 'vue-moment';

Vue.config.productionTip = false;

Vue.use(VueRouter)
Vue.use(VueAxios, axios);
Vue.use(VueMoment);

new Vue({
  router,
  //store,
  render: h => h(App),
}).$mount('#app');
