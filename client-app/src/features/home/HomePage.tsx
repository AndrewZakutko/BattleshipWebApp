import { observer } from "mobx-react-lite";
import React from "react";
import { Link } from "react-router-dom";
import { Button, Container } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";

export default observer(function HomePage() {
    const { userStore } = useStore();

    return(
        <Container text className="home-page">
            <h1>Battleship Game</h1>
            {userStore.isLoggedIn ? (
                <>
                <h2>Hi! Let's go to game list</h2>
                <Button as={Link} to='/gamechoose'>Go to choose game</Button>
                </>
            )
            :
            (
                <>
                <h2>Login or Register to play</h2>
                <Button as={Link} to='/login'>Login</Button>
                <Button as={Link} to='/register'>Register</Button>
            </>
            )}
        </Container>
    )
})
