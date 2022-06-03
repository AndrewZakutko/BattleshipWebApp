import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Container } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { useStore } from "../../app/stores/store";

export default observer(function(){
    const { userStore, gameStore } = useStore();

    useEffect(() => {
        gameStore.loadHistoryGames(userStore.user!.name);
    });

    if(gameStore.loadingInitial) return <LoadingComponent content="Loading history..."/>
    
    return(
        <Container>
            {gameStore.historyGames != null ? 
            (
                <>
                    <ol className="bullet">
                    {gameStore.historyGames!.map(game => (
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