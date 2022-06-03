import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { Container } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";

export default observer(function GameFinish(){
    const { userStore } = useStore();

    useEffect(() => {
        return () => {
            userStore.loadGame();
        }
    }, []);

    return(
        <Container>
            <h2>Game finish!</h2>
            <p>First player: {userStore.game!.firstPlayerName}</p>
            <p>Second player: {userStore.game!.secondPlayerName}</p>
            <p>Name of winner: {userStore.game!.nameOfWinner}</p>
            <p>Move count: {userStore.game!.moveCount}</p>
            <p>Result info: {userStore.game!.resultInfo}</p>
            <Link to={'/gamelist'}>Go to game list</Link>
        </Container>
    )
})