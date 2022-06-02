import { createContext, useContext } from "react";
import CellStore from "./cellStore";
import CommonStore from "./commonStore";
import GameStore from "./gameStore";
import ModalStore from "./modalStore";
import UserStore from "./userStore";

interface Store {
    gameStore: GameStore,
    userStore: UserStore,
    commonStore: CommonStore,
    cellStore: CellStore,
    modalStore: ModalStore
}

export const store: Store = {
    gameStore: new GameStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore(),
    cellStore: new CellStore(),
    modalStore: new ModalStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}