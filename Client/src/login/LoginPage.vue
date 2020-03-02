<template>
    <div>        
        <h2>Login</h2>
        <form @submit.prevent="onSubmit">
            <!-- Username -->
            <div class="form-group">
                <label for="username">Username</label>
                <input type="text" v-model.trim="$v.username.$model" name="username" class="form-control"
                :class="{ 'is-invalid': submitted && $v.username.$error }" />
                <div v-if="submitted && !$v.username.required" class="invalid-feedback">Username is required</div>
            </div>
            <!-- Password -->
            <div class="form-group">
                <label htmlFor="password">Password</label>
                <input type="password" v-model.trim="$v.password.$model" name="password" class="form-control"
                :class="{ 'is-invalid': submitted && $v.password.$error }" />
                <div v-if="submitted && !$v.password.required" class="invalid-feedback">Password is required</div>
            </div>
            <div class="form-group">
                <button class="btn btn-primary" :disabled="loading">
                    <span class="spinner-border spinner-border-sm" v-show="loading"></span>
                    <span>Login</span>
                </button>
            </div>
        </form>
    </div>
</template>

<script>
import { required } from 'vuelidate/lib/validators';
import { authenticationService } from '../services/authentication.service';
import { router } from '../router/router';

export default {
    data () {
        return {
            username: '',
            password: '',
            submitted: false,
            loading: false,
            returnUrl: '',
            error: ''
        };
    },
    validations: {
        username: { required },
        password: { required }
    },
    created () {
        if (authenticationService.currentUserValue){
            return router.push('/');
        }

        this.returnUrl = this.$route.query.returnUrl || '/';
    },
    methods: {
        onSubmit (){
            this.submitted = true;

            this.$v.$touch();
            if (this.$v.$invalid){
                return;
            }

            this.loading = true;
            authenticationService.login(this.username, this.password)
                .then(
                    user => {
                        router.push(this.returnUrl);
                        console.log(user);
                    },
                    error => {
                        this.error = error;
                        this.loading = false;
                        console.log(error);
                    }
                );
        }
    }
};
</script>