import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Button, Container } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { User } from "../../app/models/user";
import { useStore } from "../../app/stores/store";

export default observer(function GameList() {
    const {gameStore, userStore} = useStore();

    useEffect(() => {
        gameStore.loadGames();
    });

    const handleConnect = (id: string, user: User) => {
        userStore.connectToGame(id, user);   
    }

    if(gameStore.loadingInitial) return <LoadingComponent content="Loading app"/>

    return(
        <>
            <Container>
                <h1>List of games:</h1>
                {gameStore.games != null ?
                (
                    <>
                        <ol className="bullet">
                            {gameStore.games!.map((game) => 
                            (
                                <>
                                    {game.gameStatus == 'NotReady' && game.firstPlayerName != userStore.user!.name ? 
                                    (
                                        <li key={game.id}>
                                            <p>Owner: {game.firstPlayerName}</p>
                                            <p>Game status: {game.gameStatus}</p>
                                            <Button onClick={() => handleConnect(game.id, userStore.user!)} style={{marginTop: '10px'}} size="small" color="blue">Connect</Button>
                                        </li>
                                    )
                                    :
                                    (
                                        null
                                    )}
                                </>
                            )
                            )}
                        </ol> 
                    </>   
                )
                :
                (
                    <>
                        <h3>List of games is empty</h3>
                    </>
                )
                }
            </Container>
        </>
    )
})