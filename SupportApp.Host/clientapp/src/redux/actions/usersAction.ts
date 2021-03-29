import types from 'redux/types';

export function fetchData(data) {
  return {
    type: types.SEND_REQUEST,
    payload: data,
  };
}

export function fetchDataSuccess(user) {
  return {
    type: types.SEND_REQUEST_SUCCESS,
    payload: user,
  };
}

export function fetchDataError(data) {
  return {
    type: types.SEND_REQUEST_ERROR,
    payload: data,
  };
}
