import { makeAutoObservable } from "mobx";
import agent from "../api/agent";
import { Cell } from "../models/cell";

export default class CellStore {
    cells: Cell[] | null = null;
    playerCells: Cell[] | null = null;
    opponentCells: Cell[] | null = null;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }

    loadCells = async (id: string) => {
        try {
            this.cells = await agent.Cells.getCells(id);
        } catch(error) {
            console.log(error);
        }
    }

    loadPlayerCells = async (id: string) => {
        try {
            this.playerCells = await agent.Cells.getCells(id);
        } catch(error) {
            console.log(error);
        }
    }

    loadOpponentCells = async (id: string) => {
        try {
            this.opponentCells = await agent.Cells.getCells(id);
        } catch(error) {
            console.log(error);
        }
    }
}