import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import { Container } from 'semantic-ui-react';
import LoadingComponent from '../../app/layout/LoadingComponent';
import { useStore } from '../../app/stores/store';
import PrepareField from '../field/PrepareField';
import AddShipForm from '../form/AddShipForm';

export default observer(function GamePrepare() {
    const [loadingGame, setLoadingGame] = useState(true);
    const { userStore, gameStore, cellStore } = useStore();

    useEffect(() => {
      setInterval(() => {
        userStore.loadGame();
        gameStore.loadGameStatus();
        if(userStore.game!.secondPlayerName != null)
        {
          setLoadingGame(false);
        }
      }, 4500);
    }, []);

    useEffect(() => {
      cellStore.loadCells(userStore.fieldId!);
    })

    if(userStore.user!.name == userStore.game!.firstPlayerName && userStore.game!.secondPlayerName == null)
    {
      if(loadingGame) return <LoadingComponent content="Waiting for an oponent..."/>
    }
    else
    {
      if(loadingGame) return <LoadingComponent content="Loading..."/>
    }

    return (
      <Container>
          <div className='field'>
            <PrepareField />
          </div>
          <div className='addShipForm'>
            <AddShipForm />
            <button onClick={() => gameStore.clearField(userStore.fieldId!)} className="btn">Clear field</button>
            <button onClick={() => userStore.readyToGame(userStore.fieldId!)} className="btn">Ready to game</button>
            {userStore.user!.name == userStore.game!.firstPlayerName && gameStore.isFirstPlayerReady == true ? 
            (
              <p>You are ready to game, waiting for opponent...</p>
            )
            :
            (
              null
            )}
            {userStore.user!.name == userStore.game!.secondPlayerName && gameStore.isSecondPlayerReady == true ? 
            (
              <p>You are ready to game, waiting for opponent...</p>
            )
            :
            (
              null
            )}
          </div>
      </Container>
    )
})
