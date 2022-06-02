import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { Container } from 'semantic-ui-react';
import LoadingComponent from '../../app/layout/LoadingComponent';
import { useStore } from '../../app/stores/store';
import PrepareField from '../field/PrepareField';
import AddShipForm from '../form/AddShipForm';

export default observer(function GamePrepare() {
    const { userStore, gameStore } = useStore();
  
    useEffect(() => {
      gameStore.loadFirstPlayerReady();
      gameStore.loadSecondPlayerReady();
      gameStore.loadGameStatus();
      userStore.loadLoadingGame();
      if(userStore.game!.secondPlayerName != null)
      {
        userStore.loadingGame = false;
      }
    });

    const handleClearField = () => {
      gameStore.clearField(userStore.fieldId!);
    }

    const handleReadyToGame = () => {
        userStore.readyToGame(userStore.fieldId!);
    }

    if(userStore.loadingGame) return <LoadingComponent content="Waiting for an oponent..."/>

    return (
      <Container>
          <div className='field'>
            <PrepareField />
          </div>
          <div className='addShipForm'>
            <AddShipForm />
            <button onClick={handleClearField} className="btn">Clear field</button>
            <button onClick={handleReadyToGame} className="btn">Ready to game</button>
          </div>
      </Container>
    )
})
