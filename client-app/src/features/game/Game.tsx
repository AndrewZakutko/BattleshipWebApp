import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Container } from "semantic-ui-react";
import { useStore } from "../../app/stores/store";
import PlayerField from "../field/PlayerField";
import OpponentField from "../field/OpponentField";
import ShootForm from "../form/ShootForm";
import LoadingComponent from "../../app/layout/LoadingComponent";

export default observer(function Game(){
    const { cellStore, userStore, gameStore } = useStore();

    if(cellStore.loadingInitial) return <LoadingComponent content="Loading app"/>

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