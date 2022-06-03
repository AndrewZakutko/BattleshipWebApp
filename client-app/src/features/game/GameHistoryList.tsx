import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Container } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";

export default observer(function(){
    const { userStore, gameStore } = useStore();
    const [historyGames, setHistoryGames] = useState(gameStore.historyGames);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        gameStore.loadHistoryGames(userStore.user!.name);
    }, []);

    useEffect(() => {
        setInterval(() => {
            gameStore.loadHistoryGames(userStore.user!.name);
            setHistoryGames(gameStore.historyGames);
            setLoading(false);
        }, 4500);
    }, []);
    
    if(loading) return <LoadingComponent content="Loading history of games..."/>

    return(
        <Container>
            {historyGames != null ? 
            (
                <>
                    <ol className="bullet">
                    {historyGames!.map(game => (
                        <li key={game.id}>
                            <p>First player: {game.firstPlayerName}</p>
                            <p>Second player: {game.secondPlayerName}</p>
                            <p>Name of winner: {game.nameOfWinner}</p>
                            <p>Count of moves: {game.moveCount}</p>
                            <p>Result info: {game.resultInfo}</p>
                        </li>
                    ))}
                    </ol>
                </>
            )
            :
            (
                <>
                    <h3>List of history games is empty</h3>
                </>
            )}
        </Container>
    )
})