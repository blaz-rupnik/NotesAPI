<template>
    <div>
        <nav class="navbar navbar-expand navbar-dark bg-dark">
            <div class="navbar-nav">
                <router-link v-if="currentUser" to="/" class="nav-item nav-link">Home</router-link>
                <router-link v-if="!currentUser" to="/register" class="nav-item nav-link">Register</router-link>
                <a v-if="currentUser" @click="logout" class="nav-item nav-link">Logout</a>
            </div>
        </nav>
        <div class="jumbotron">
            <div class="container">
                <router-view></router-view>
            </div>
        </div>
    </div>
</template>

<script>
import { authenticationService } from '../services/authentication.service';
import { router } from '../router/router';

export default {
    name: 'app',
    data () {
        return {
            currentUser: null
        };
    },
    created () {
        authenticationService.currentUser.subscribe(x => this.currentUser = x);
    },
    methods: {
        logout () {
            authenticationService.logout();
            router.push('/login');
        }
    }
};
</script>