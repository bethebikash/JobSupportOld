import { all } from 'redux-saga/effects';

import { usersSaga } from './usersSaga';

export default function* RootSaga() {
  yield all([usersSaga()]);
}
