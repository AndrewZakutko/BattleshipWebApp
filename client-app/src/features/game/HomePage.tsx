import { observer } from "mobx-react-lite";
import React from "react";
import { Link } from "react-router-dom";
import { Button, Container } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import LoginForm from "../players/LoginForm";
import RegisterForm from "../players/RegisterForm";

export default observer(function HomePage() {
    const { userStore, modalStore } = useStore();
    return(
        <div className="home-page">
            <h1><span>Battleship Game</span></h1>
            {userStore.isLoggedIn ? (
                <>
                <h3>Hi! Let's go to game list</h3>
                <Button basic inverted color="red" as={Link} to='/gamelist'>Go to list of games</Button>
                </>
            )
            :
            (
                <>
                    <h3>Login or Register to play</h3>
                    <Button onClick={() => modalStore.openModal(<LoginForm />)} size="big" basic inverted color="red">Login</Button>
                    <Button onClick={() => modalStore.openModal(<RegisterForm />)} size="big" basic inverted color="red">Register</Button>
                </>
            )}
        </div>
    )
})
