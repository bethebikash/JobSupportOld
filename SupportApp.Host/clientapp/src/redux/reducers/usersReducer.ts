import types from 'redux/types';

const initialState = {
  loading: false,
  user: [],
  error: null,
};

const usersReducer = (state = initialState, action: any) => {
  switch (action.type) {
    case types.SEND_REQUEST:
      return {
        ...state,
        loading: true,
      };
    case types.SEND_REQUEST_SUCCESS:
      return {
        ...state,
        loading: false,
        user: action.payload,
        error: null,
      };
    case types.SEND_REQUEST_ERROR:
      return {
        ...state,
        loading: false,
        user: [],
        error: action.error,
      };
    default:
      return {
        ...state,
      };
  }
};

export default usersReducer;
