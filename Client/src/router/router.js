import Vue from 'vue';
import Router from 'vue-router';

import LoginPage from '../login/LoginPage';
import RegisterPage from '../register/RegisterPage';
import HomePage from '../home/HomePage';

import { authenticationService } from '../services/authentication.service';

Vue.use(Router);

export const router = new Router({
    mode: 'history',
    routes: [
        {
            path: '/',
            component: HomePage,
            meta: { authorize: [] }
        },
        {
            path: '/login',
            component: LoginPage
        },
        {
            path: '/register',
            component: RegisterPage
        },
        { path: '*', redirect: '/' }
    ]
});

router.beforeEach((to, from, next) => {
    const { authorize } = to.meta;
    const currentUser = authenticationService.currentUserValue;

    if (authorize){
        if (!currentUser){
            return next({ path: '/login', query: { returnUrl: to.path } });
        }
    }

    next();
});