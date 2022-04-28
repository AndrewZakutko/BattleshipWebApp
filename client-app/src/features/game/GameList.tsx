import axios from "axios";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Button, Card, CardGroup, Container } from "semantic-ui-react";
import LoadingComponent from "../../app/layout/LoadingComponent";
import { Game } from "../../app/models/game";
import { useStore } from "../../app/stores/store";

export default observer(function () {
    const[games, setGames] = useState<Game[]>([]);
    const[loading, setLoading] = useState(true);
    const {gameStore, userStore} = useStore();

    useEffect(() => {
        axios.get<Game[]>('http://localhost:5000/api/games/list').then(response => {
            setGames(response.data);
            setLoading(false);
        })
    }, [])

    if(loading) return <LoadingComponent content="Loading app"/>

    return(
        <Container>
            <h1>Game List</h1>
            <CardGroup>
                {games.map((game) => 
                game.gameStatus != 'Started' && game.gameStatus != 'Finish' ? 
                (
                    <Card key={game.id}>
                        <Card.Content>
                            <Card.Header as='h3'>Owner: {game.firstPlayerName}</Card.Header>
                            <Card.Meta>Game status: {game.gameStatus}</Card.Meta>
                        </Card.Content>
                        <Button onClick={() => gameStore.connect(game.id, userStore.user!)} style={{marginTop: '10px'}}>Connect</Button>
                    </Card>
                )
                :
                null
                )}
            </CardGroup>
        </Container>
    )
})