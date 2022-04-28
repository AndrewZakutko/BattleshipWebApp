import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import { history } from "../..";
import { User, UserConnectValues } from "../models/user";
import { Game } from "../models/game";

export default class GameStore {
    game: Game | null = null;
    connectUser: UserConnectValues = { 
        gameId: '', 
        name: ''
    }

    constructor() {
        makeAutoObservable(this)
    }

    create = async (user: User) => {
        try {
            await agent.Games.create(user);
            history.push('/preparegamepage');
        }
        catch (error) {
            console.log(error);
        }
    }

    connect = async (id: string, user: User) => {
        try {
            this.connectUser.gameId = id;
            this.connectUser.name = user.name;
            await agent.Games.connect(this.connectUser);
            history.push('/preparegamepage');
        }
        catch (error) {
            console.log(error);
        }
    }
}