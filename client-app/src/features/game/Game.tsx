import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { Container } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import PlayerField from "../field/PlayerField";
import OpponentField from "../field/OpponentField";
import ShootForm from "../form/ShootForm";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default observer(function Game(){
    const [loadingGame, setLoadingGame] = useState(true);
    const { cellStore, userStore } = useStore();

    useEffect(() => {
        userStore.loadInitialGoingStatus();
    }, []);

    useEffect(() => {
        setInterval(() => {
            if(userStore.user!.name == userStore.game!.firstPlayerName)
            {
                cellStore.loadPlayerCells(userStore.game!.firstPlayerFieldId!);
                cellStore.loadOpponentCells(userStore.game!.secondPlayerFieldId!);
                userStore.loadGoingStatus();
                setLoadingGame(false);
            }
            else
            {
                cellStore.loadPlayerCells(userStore.game!.secondPlayerFieldId!);
                cellStore.loadOpponentCells(userStore.game!.firstPlayerFieldId!);
                userStore.loadGoingStatus();
                setLoadingGame(false);
            }
        }, 4500);
    }, []);

    if(loadingGame) return <LoadingComponent content="Loading game..."/>

    return(
        <Container>
            <>
                {userStore.isGoing ? 
                (
                    <h4>You are shooting now!</h4>
                )
                :
                (
                    <h4>Your opponent is shooting now!</h4>
                )
                }
                <div className='field'>
                    <h3>Your field</h3>
                    <PlayerField />
                </div>
                <div style={{marginLeft: '20px'}} className='field'>
                    <h3>Opponent field</h3>
                    <OpponentField />
                </div>
                <div className="shoot-form">
                    <ShootForm />
                </div>
            </>
        </Container>
    )
})