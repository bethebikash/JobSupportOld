import React from 'react';
import { Route, Switch } from 'react-router-dom';
import Home from 'page/Home';
import User from 'page/User';

function App() {
  return (
    <Switch>
      <Route exact path="/" component={Home} />
      <Route path="/user" component={User} />
    </Switch>
  );
}

export default App;
