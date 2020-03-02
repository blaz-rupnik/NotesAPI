import config from 'config';
import { requestOptions } from '../helpers/request-options';
import { handleResponse } from '../helpers/handle-response';

export const folderService = {
    getAll,
    getById,
    deleteFolder
};

function getAll(){
    return fetch(`${config.externalApiUrl}/folders`, requestOptions.get())
    .then(handleResponse);
}

function getById(id){
    return fetch(`${config.externalApiUrl}/folders/${id}`, requestOptions.get())
    .then(handleResponse);
}

function deleteFolder(id){
    return fetch(`${config.externalApiUrl}/folders/${id}`, requestOptions.delete())
    .then(handleResponse);
}