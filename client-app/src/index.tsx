import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './app/layout/App';
import { Router } from 'react-router-dom';
import { store, StoreContext } from './app/stores/store';
import { createBrowserHistory } from 'history';

export const history = createBrowserHistory();

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <StoreContext.Provider value={store}>
    <Router history={history}>
      <App /> 
    </Router>
  </StoreContext.Provider>
);
