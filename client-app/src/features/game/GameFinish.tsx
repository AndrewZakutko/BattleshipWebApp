import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Container } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";

export default observer(function GameFinish(){
    const { userStore } = useStore();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setLoading(false);
        return () => {
            userStore.loadGame();
        }
    }, []);

    if(loading) return <LoadingComponent content="Loading result info..."/>

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