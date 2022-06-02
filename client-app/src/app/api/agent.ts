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
    list: () => request.get<Game[]>('/Games/list'),
    historyList: (name: string) => request.get<Game[]>(`/Games/historyList/${name}`),
    getByName: (name: string) => request.get<Game>(`/Games/${name}`),
    create: (user: User) => request.post<Game>('/Games/create', user),
    connect: (connectUser: UserConnectValues) => request.post<Game>('/Games/connect', connectUser),
    changeStatusToStarted: (gameId: string) => request.get(`/Games/changeStatus/${gameId}`),
    finish: (finishGame: FinishGame) => request.post('/Games/finish', finishGame)
}

const Account = {
    current: () => request.get<User>('/Account'),
    login: (user: UserFormValues) => request.post<User>('/Account/login', user),
    register: (user: UserFormValues) => request.post<User>('/Account/register', user),
    changeToReady: (name: string) => request.get(`/Account/changeStatus/${name}`),
    checkStatus: (name: string) => request.get<boolean>(`/Account/checkStatus/${name}`),
    changeToReadyGoing: (name: string) => request.get(`/Account/changeStatusGoing/${name}`),
    checkStatusGoing: (name: string) => request.get<Boolean>(`/Account/checkStatusGoing/${name}`)
}

const Ship = {
    add: (ship: AddShip) => request.post('/Ships/add', ship)
}

const Fields = {
    clear: (id: string) => request.get(`/Fields/${id}`),
    checkCountOfShips: (id: string) => request.get<number>(`/Fields/countShips/${id}`),
    checkCountOfShipsAlive: (id: string) => request.get<number>(`/Fields/countShipsAlive/${id}`),
    fieldInfo: (id: string) => request.get<string>(`/Fields/fieldInfo/${id}`)
}

const Cells = {
    getCells: (id: string) => request.get<Cell[]>(`/Cells/${id}`),
}

const Shoots = {
    takeAShoot: (shoot: Shoot) => request.post<Boolean>('/Shoots/takeAShoot', shoot)
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