import axios, { AxiosResponse } from 'axios';
import { setTimeout } from 'timers';
import { Game } from '../models/game';
import { User, UserConnectValues, UserFormValues } from '../models/user';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.response.use(async response => {
    try {
        await sleep(1000);
        return response;
    } catch (error) {
        console.log(error);
        return await Promise.reject(error);
    }
})

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

const request = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T>(url).then(responseBody),
}

const Games = {
    list: () => request.get<[]>('/games/list'),
    game: (id: string) => request.get<Game>(`/games/${id}`),
    create: (user: User) => request.post<void>('/games/create', user),
    connect: (connectUser: UserConnectValues) => request.post<void>('/games/connect', connectUser),
}

const Account = {
    current: () => request.get<User>('/account/current'),
    login: (user: UserFormValues) => request.post<User>('/account/login', user),
    register: (user: UserFormValues) => request.post<User>('/account/register', user)
}

const agent = {
    Games,
    Account
}

export default agent;