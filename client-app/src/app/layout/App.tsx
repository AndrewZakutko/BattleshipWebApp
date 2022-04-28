import React, { useEffect, useState } from 'react';
import './App.css';
import 'semantic-ui-css/semantic.min.css';
import { Route, Switch } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
import { Container } from 'semantic-ui-react';
import GameList from '../../features/game/GameList';
import LoginForm from '../../features/players/LoginForm';
import RegisterForm from '../../features/players/RegisterForm';
import Navbar from './Navbar';
import GameChoose from '../../features/game/GameChoose';
import PlayPrepare from '../../features/play/PlayPrepare';

function App() {
  return (
    <div>
      <Navbar />
      <Container>
        <Route exact path='/' component={HomePage}/>
        <Route 
          path={'/(.+)'}
          render={() => (
            <>
              <Container style={{marginTop: '7em'}}>
                <Switch>
                  <Route path='/gamelist' component={GameList}/>
                  <Route path={'/login'} component={LoginForm}/>
                  <Route path={'/register'} component={RegisterForm}/>
                  <Route path={'/gamechoose'} component={GameChoose}/>
                  <Route path={'/preparegamepage'} component={PlayPrepare}/>
                </Switch>
              </Container>
            </>
          )}
        />
      </Container>
    </div>
  );
}

export default App;

