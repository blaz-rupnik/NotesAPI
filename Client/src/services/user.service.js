import config from '../../config';
import { requestOptions } from '@/helpers';
import { handleResponse } from '../helpers/handle-response';

export const userService = {
    getAll,
    getById
};

function getAll(){
    return fetch(`${config.dev.apiUrl}/users`, requestOptions.get())
    .then(handleResponse);
}

function getById(id){
    return fetch(`${config.dev.apiUrl}/users/${id}`, requestOptions.get())
    .then(handleResponse);
}