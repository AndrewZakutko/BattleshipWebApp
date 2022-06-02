import React, { useEffect, useState } from 'react';
import './App.css';
import 'semantic-ui-css/semantic.min.css';
import { Route, Switch } from 'react-router-dom';
import HomePage from '../../features/game/HomePage';
import { Container } from 'semantic-ui-react';
import LoginForm from '../../features/players/LoginForm';
import RegisterForm from '../../features/players/RegisterForm';
import Navbar from './Navbar';
import { useStore } from '../stores/store';
import LoadingComponent from './LoadingComponent';
import GameList from '../../features/game/GameList';
import GamePrepare from '../../features/game/GamePrepare';
import Game from '../../features/game/Game';
import GameFinish from '../../features/game/GameFinish';
import GameHistoryList from '../../features/game/GameHistoryList';
import ModalContainer from '../common/modals/ModalContainer';

export default function App() {
  const { commonStore, userStore } = useStore();

  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore])

  if (!commonStore.appLoaded) return <LoadingComponent content='Loading...' />

  return (
    <>
      <ModalContainer /> 
      <Route exact path='/' component={HomePage}/>
      <Route 
        path={'/(.+)'}
        render={() => (
          <>
            <Navbar />
            <div className='content'>
              <Switch>
                <Route path='/gamelist' component={GameList}/>
                <Route path={'/login'} component={LoginForm}/>
                <Route path={'/register'} component={RegisterForm}/>
                <Route path={'/gameprepare'} component={GamePrepare}/>
                <Route path={'/game'} component={Game}/>
                <Route path={'/gamefinish'} component={GameFinish}/>
                <Route path={'/gamehistory'} component={GameHistoryList}/>
              </Switch>
            </div>
          </>
        )}
      />
    </>
  );
}

