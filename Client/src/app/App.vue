<template>
    <div>
        <div v-if="currentUser">
            <h2>{{ currentUser }}</h2>
        </div>
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