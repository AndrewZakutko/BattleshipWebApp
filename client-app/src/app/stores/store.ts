import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import GameStore from "./gameStore";
import UserStore from "./userStore";

interface Store {
    gameStore: GameStore,
    userStore: UserStore,
    commonStore: CommonStore
}

export const store: Store = {
    gameStore: new GameStore(),
    userStore: new UserStore(),
    commonStore: new CommonStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}