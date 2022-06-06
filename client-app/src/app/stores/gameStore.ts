import { history } from "../..";
import agent from "../api/agent";
import AddShip from "../models/addShip";
import { Game } from "../models/game"
import { store } from "./store";
import { FinishGame } from "../models/game";

export default class GameStore {
    games: Game[] | null = null;
    historyGames: Game[] | null = null;
    loadingInitial = false;
    isReady = false;
    isFirstPlayerReady = false;
    isSecondPlayerReady = false;

    finishGame: FinishGame = {
        gameId: '',
        nameOfWinner: '',
        resultInfo: ''
    };


    loadGames = async () => {
        try {
            this.games = await agent.Games.list();
        } catch (error) {
            console.log(error);
        }
    }

    loadGameStatus = async () => {
        try {
            var game = await agent.Games.getByName(store.userStore.user!.name);
            if(store.userStore.game!.firstPlayerName != null)
            {
                this.isFirstPlayerReady = await agent.Account.checkStatusPrepare(store.userStore.game!.firstPlayerName!);
            }
            if(store.userStore.game!.secondPlayerName != null)
            {
                this.isSecondPlayerReady = await agent.Account.checkStatusPrepare(store.userStore.game!.secondPlayerName!);
            }
            if(this.isFirstPlayerReady && this.isSecondPlayerReady && game.status != 'Started')
            {
                await agent.Games.changeStatusToStarted(store.userStore.game!.id);
                history.push('/game');
            }
            if(this.isFirstPlayerReady && this.isSecondPlayerReady && game.status == 'Started') {
                history.push('/game');
            }
        } catch (error) {
            console.log(error);
        }
    }

    clearField = async (id: string) => {
        try {
            await agent.Fields.clear(id);
            store.cellStore.cells = await agent.Cells.list(store.userStore.fieldId!);
        } catch(error) {
            console.log(error);
        }
    }

    loadHistoryGames = async (name: string) => {
        this.loadingInitial = true;
        try {
            this.historyGames = await agent.Games.historyList(name);
            this.loadingInitial = false;
        } catch (error) {
            console.log(error);
            this.loadingInitial = false;
        }
    }

    checkCountOfShipsAliveOnField = async () => {
        try {
            var game = await agent.Games.getByName(store.userStore.user!.name);
            if(game.status == 'Finished')
            {
                history.push('/gamefinish');
            }

            var countOfFisrtPlayerShipsAlive = await agent.Fields.checkCountAlive(store.userStore.game!.firstPlayerFieldId!);
            var countOfSecondPlayerShipsAlive = await agent.Fields.checkCountAlive(store.userStore.game!.secondPlayerFieldId!);

            if(countOfFisrtPlayerShipsAlive == 0) {
                this.finishGame.gameId = store.userStore.game!.id;
                this.finishGame.nameOfWinner = store.userStore.game!.secondPlayerName;
                this.finishGame.resultInfo = await agent.Fields.info(store.userStore.game!.secondPlayerFieldId!);
                store.userStore.game!.nameOfWinner = store.userStore.game!.firstPlayerName;
                store.userStore.game!.moveCount = store.userStore.moveCount;
                store.userStore.game!.resultInfo = this.finishGame.resultInfo;

                await agent.Games.finish(this.finishGame);
                history.push('/gamefinish');
            }
            if(countOfSecondPlayerShipsAlive == 0) {
                this.finishGame.gameId = store.userStore.game!.id;
                this.finishGame.nameOfWinner = store.userStore.game!.firstPlayerName;
                this.finishGame.resultInfo = await agent.Fields.info(store.userStore.game!.firstPlayerFieldId!);
                store.userStore.game!.nameOfWinner = store.userStore.game!.firstPlayerName;
                store.userStore.game!.moveCount = store.userStore.moveCount;
                store.userStore.game!.resultInfo = this.finishGame.resultInfo;

                await agent.Games.finish(this.finishGame);
                history.push('/gamefinish');
            }
        } catch(error) {
            console.log(error);
        }
    }

    addShip = async (ship: AddShip) => {
        try {
            await agent.Ship.add(ship);
            store.cellStore.cells = await agent.Cells.list(store.userStore.fieldId!);
        } catch (error) {
            console.log(error);
        }
    }
}