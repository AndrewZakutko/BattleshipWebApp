import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Button, Container } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { User } from "../../app/models/user";
import { useStore } from "../../app/stores/store";

export default observer(function GameList() {
    const {gameStore, userStore} = useStore();
    const [games, setGames] = useState(gameStore.games);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        gameStore.loadGames();
    }, []);

    useEffect(() => {
        setInterval(() => {
            gameStore.loadGames();
            setGames(gameStore.games);
            setLoading(false);
        }, 4500);
    }, []);

    const handleConnect = (id: string, user: User) => {
        userStore.connectToGame(id, user);   
    }

    if(loading) return <LoadingComponent content="Loading list of games..."/>

    return(
        <>
            <Container>
                <h1>List of games:</h1>
                {games != null ?
                (
                    <>
                        <ol className="bullet">
                            {games!.map((game) => 
                            (
                                <>
                                    {game.status == 'NotReady' && game.firstPlayerName != userStore.user!.name && game.secondPlayerName == null ? 
                                    (
                                        <li key={game.id}>
                                            <p>Owner: {game.firstPlayerName}</p>
                                            <p>Game status: {game.status}</p>
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
                        <p>Wait for someone to create or create yourself</p>
                    </>
                )
                }
            </Container>
        </>
    )
})