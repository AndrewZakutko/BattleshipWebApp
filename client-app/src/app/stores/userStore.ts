import { makeAutoObservable, runInAction } from "mobx";
import { history } from "../..";
import agent from "../api/agent";
import { Game } from "../models/game";
import { User, UserConnectValues, UserFormValues } from "../models/user";
import { store } from "./store";
import { Shoot } from "../models/shoot";

export default class UserStore {
    user: User | null = null;
    game: Game | null = null;
    isReadyToGame: Boolean = false;
    isGoing: Boolean = false;
    fieldId: string | null = null;
    loadingInitial = false;
    connectUser: UserConnectValues = { 
        gameId: '', 
        name: ''
    }
    
    constructor() {
        makeAutoObservable(this);
    }

    get isLoggedIn() {
        return !! this.user;
    }

    login = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.login(creds);
            store.commonStore.setToken(user.token);
            const game = await agent.Games.getByName(user.name);
            runInAction(() => {
                this.user = user;
                if(game != null)
                {
                    this.game = game;
                }
            })
            if(game.firstPlayerName == null || game.secondPlayerName == null)
            {
                history.push('/gamelist');
                store.modalStore.closeModal();
            }
            if(game.gameStatus == 'Started')
            {
                var isFirstPlayerGoing = await agent.Account.checkStatusGoing(this.game!.firstPlayerName!);
                var isSecondPlayerGoing = await agent.Account.checkStatusGoing(this.game!.secondPlayerName!);
                if(isFirstPlayerGoing == false && isSecondPlayerGoing == false)
                {
                    await agent.Account.changeToReadyGoing(this.game!.firstPlayerName!);
                }
                this.isGoing = await agent.Account.checkStatusGoing(this.user!.name);
                history.push('/game');
                store.modalStore.closeModal();
            }
            if(game.gameStatus == 'NotReady' && game.firstPlayerName == user.name)
            {
                this.fieldId = game.firstPlayerFieldId as string;
                history.push('/gameprepare');
                store.modalStore.closeModal();
            }
            if(game.gameStatus == 'NotReady' && game.secondPlayerName == user.name)
            {
                this.fieldId = game.secondPlayerFieldId as string;
                history.push('/gameprepare');
                store.modalStore.closeModal();
            }
            store.modalStore.closeModal();
        }
        catch (error) {
            throw error;
        }
    }

    logout = () => {
        store.commonStore.setToken(null);
        window.localStorage.removeItem('jwt');
        this.user = null;
        this.game = null;
        history.push('/');
    }

    register = async (creds: UserFormValues) => {
        try {
            const user = await agent.Account.register(creds);
            store.commonStore.setToken(user.token);
            const game = await agent.Games.getByName(user.name);
            runInAction(() => {
                this.user = user;
                if(game != null)
                {
                    this.game = game;
                }
            })
            history.push('/gamelist');
            store.modalStore.closeModal();
        }
        catch (error) {
            throw error;
        }
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current();
            const game = await agent.Games.getByName(user.name);
            runInAction(() => {
                this.user = user;
                if(game != null)
                {
                    this.game = game;
                }
            })
            if(game.firstPlayerName == null || game.secondPlayerName == null)
            {
                history.push('/gamelist');
            }
            if(game.gameStatus == 'Started')
            {
                var isFirstPlayerGoing = await agent.Account.checkStatusGoing(this.game!.firstPlayerName!);
                var isSecondPlayerGoing = await agent.Account.checkStatusGoing(this.game!.secondPlayerName!);
                if(isFirstPlayerGoing == false && isSecondPlayerGoing == false)
                {
                    await agent.Account.changeToReadyGoing(this.game!.firstPlayerName!);
                }
                this.isGoing = await agent.Account.checkStatusGoing(this.user!.name);
                history.push('/game');
            }
            if(game.firstPlayerName == user.name)
            {
                this.fieldId = game.firstPlayerFieldId as string;
                history.push('/gameprepare');
            }
            if(game.secondPlayerName == user.name)
            {
                this.fieldId = game.secondPlayerFieldId as string;
                history.push('/gameprepare');
            }
        } catch (error) {
            console.log(error);
        }
    }

    createGame = async (user: User) => {
        try {
            console.log(user);
            this.game = await agent.Games.create(user);
            this.fieldId = this.game.firstPlayerFieldId!;
            history.push('/gameprepare');
        }
        catch (error) {
            console.log(error);
        }
    }

    connectToGame = async (id: string, user: User) => {
        try {
            this.connectUser.gameId = id;
            this.connectUser.name = user.name;
            this.game = await agent.Games.connect(this.connectUser);
            this.fieldId = this.game.firstPlayerFieldId!;
            history.push('/gameprepare');
        }
        catch (error) {
            console.log(error);
        }
    }

    readyToGame = async (id: string) => {
        try {
            var countShips = await agent.Fields.checkCountOfShips(id);
            if(countShips == 10) {
                agent.Account.changeToReady(this.user!.name);
                store.gameStore.isReady = true;
            }
            else{
                console.log("Count of ship cells is not 20!");
            }
        }
        catch (error) {
            console.log(error);
        }
    }

    loadGame = async () => {
        try {
            this.game = await agent.Games.getByName(this.user!.name);
        } catch (error) {
            console.log(error);
        }
    }

    loadInitialGoingStatus = async () => {
        try {
            var firstPlayerStatusGoing = await agent.Account.checkStatusGoing(this.game!.firstPlayerName!);
            var secondPlayerStatusGoing = await agent.Account.checkStatusGoing(this.game!.secondPlayerName!);
            if(firstPlayerStatusGoing == false && secondPlayerStatusGoing == false)
            {
                await agent.Account.changeToReadyGoing(this.game!.firstPlayerName!);
            }
        } catch (error) {
            console.log(error);
        }
    }

    loadGoingStatus = async () => {
        try{
            this.isGoing = await agent.Account.checkStatusGoing(this.user!.name);
        } catch(error){
            console.log(error);
        }
    }

    shoot = async (shoot: Shoot) => {
        try {
            if(this.isGoing)
            {
                await agent.Shoots.takeAShoot(shoot);
                this.isGoing = await agent.Account.checkStatusGoing(this.user!.name);
                if(this.game!.firstPlayerName == this.user!.name)
                {
                    store.cellStore.playerCells = await agent.Cells.getCells(this.game!.firstPlayerFieldId!);
                    store.cellStore.opponentCells = await agent.Cells.getCells(this.game!.secondPlayerFieldId!);
                }
                if(this.game!.secondPlayerName == this.user!.name)
                {
                    store.cellStore.playerCells = await agent.Cells.getCells(this.game!.secondPlayerFieldId!);
                    store.cellStore.opponentCells = await agent.Cells.getCells(this.game!.firstPlayerFieldId!);
                }
                store.gameStore.checkCountOfShipsAliveOnField();
            }
            else{
                console.log("Your opponent is going now!");
            }
        } catch(error) {
            console.log(error);
        }
    }
}