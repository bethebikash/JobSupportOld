import 'regenerator-runtime/runtime';

import { applyMiddleware, compose, createStore } from 'redux';
import createSagaMiddleware from 'redux-saga';

import RootReducer from './reducers';
import RootSaga from './sagas/RootSaga';

const sagaMiddleware = createSagaMiddleware();

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const store = createStore(RootReducer, composeEnhancers(applyMiddleware(sagaMiddleware)));

export type RootState = ReturnType<typeof RootReducer>;

sagaMiddleware.run(RootSaga);

export default store;
