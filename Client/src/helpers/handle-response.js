import { authenticationService } from '../services/authentication.service';

export function handleResponse(response){
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if(!response.ok && !response.nocontent){
            if(response.status === 401){
                //automatic logout for unathourized actions
                authenticationService.logout();
                location.reload(true);
            }

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }
        
        return data;
    });
}