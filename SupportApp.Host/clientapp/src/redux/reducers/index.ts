import { combineReducers } from 'redux';

import usersReducer from './usersReducer';

const RootReducer = combineReducers({
  users: usersReducer,
});

export default RootReducer;
