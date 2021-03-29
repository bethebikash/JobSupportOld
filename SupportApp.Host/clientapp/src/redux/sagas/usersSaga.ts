import { fetchDataError, fetchDataSuccess } from 'redux/actions/usersAction';
import types from 'redux/types';
import { call, put, takeEvery } from '@redux-saga/core/effects';
import instance from 'lib/axiosHelper/axiosInstance';

function* fetchUsers(action) {
  try {
    const response = yield call(() => instance.get(`${action.payload}`));
    yield put(fetchDataSuccess(response.data));
  } catch (error) {
    yield put(fetchDataError(error));
  }
}

export function* usersSaga() {
  yield takeEvery(types.SEND_REQUEST, fetchUsers);
}
