import Vue from 'vue';
import App from './app/App';
import { router } from './router/router';
import Vuelidate from 'vuelidate';

Vue.use(Vuelidate);

new Vue({
  el: '#app',
  router,
  render: h => h(App)
});
