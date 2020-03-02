import { BehaviorSubject } from 'rxjs';

import config from 'config';
import { requestOptions } from '../helpers/request-options';
import { handleResponse } from '../helpers/handle-response';

const currentUserSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('currentUser')));

export const authenticationService = {
    login,
    logout,
    register,
    currentUser: currentUserSubject.asObservable(),
    get currentUserValue() { return currentUserSubject.value }
};

function login(username, password){
    return fetch(`${config.externalApiUrl}/users/authenticate`, requestOptions.post({ username, password }))
    .then(handleResponse)
        .then(user => {
            localStorage.setItem('currentUser', JSON.stringify(user));
            currentUserSubject.next(user);

            return user;
        });
}

function register(username, password){
    return fetch(`${config.externalApiUrl}/users/register`, requestOptions.post({ username, password }))
        .then(handleResponse);
}

function logout(){
    localStorage.removeItem('currentUser');
    currentUserSubject.next(null);
}