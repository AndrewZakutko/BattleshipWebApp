import axios, { AxiosResponse } from 'axios';
import { setTimeout } from 'timers';
import AddShip from '../models/addShip';
import { Cell } from '../models/cell';
import { FinishGame, Game } from '../models/game';
import { Shoot } from '../models/shoot';
import { User, UserConnectValues, UserFormValues } from '../models/user';
import { store } from '../stores/store';

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api';

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) config.headers!.Authorization = `Bearer ${token}`
    return config;
})

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
    list: () => request.get<Game[]>('/Game/list'),
    historyList: (name: string) => request.get<Game[]>(`/Game/historyList/${name}`),
    getByName: (name: string) => request.get<Game>(`/Game/${name}`),
    create: (user: User) => request.post<Game>('/Game/create', user),
    connect: (connectUser: UserConnectValues) => request.post<Game>('/Game/connect', connectUser),
    changeStatusToStarted: (gameId: string) => request.get(`/Game/changeToStarted/${gameId}`),
    finish: (finishGame: FinishGame) => request.post('/Game/finish', finishGame)
}

const Account = {
    current: () => request.get<User>('/Account'),
    login: (user: UserFormValues) => request.post<User>('/Account/login', user),
    register: (user: UserFormValues) => request.post<User>('/Account/register', user),
    changeToReady: (name: string) => request.get(`/Account/changeStatusPrepare/${name}`),
    checkStatusPrepare: (name: string) => request.get<boolean>(`/Account/checkStatusPrepare/${name}`),
    changeToReadyGoing: (name: string) => request.get(`/Account/changeStatusGoing/${name}`),
    checkStatusGoing: (name: string) => request.get<Boolean>(`/Account/checkStatusGoing/${name}`)
}

const Ship = {
    add: (ship: AddShip) => request.post('/Ship/add', ship)
}

const Fields = {
    clear: (id: string) => request.get(`/Field/clear/${id}`),
    checkCount: (id: string) => request.get<number>(`/Field/countShips/${id}`),
    checkCountAlive: (id: string) => request.get<number>(`/Field/countShipsAlive/${id}`),
    info: (id: string) => request.get<string>(`/Field/info/${id}`)
}

const Cells = {
    list: (id: string) => request.get<Cell[]>(`/Cell/list/${id}`),
}

const Shoots = {
    shoot: (shoot: Shoot) => request.post<Boolean>('/Shoots/takeAShoot', shoot)
}

const agent = {
    Games,
    Account,
    Ship,
    Fields,
    Cells,
    Shoots
}

export default agent;