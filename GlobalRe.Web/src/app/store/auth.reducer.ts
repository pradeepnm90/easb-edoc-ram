import { AuthActions, AuthActionTypes } from '../actions/auth.action';
import * as auth from '../actions/auth.action';
import { LoginUser } from '../shared/models/login-user';

export interface AuthState {
  baseAPI: string;
  loggedIn: boolean;
  user: LoginUser | null;
}

export const initialState: AuthState = {
  baseAPI: '',
  loggedIn: false,
  user: {
    userName: '',
    personId: 0,
    applicationVersion: '',
    domainName: '',
    environment: '',
    isAuthenticated: false,
    tokenProxyRootUrl: '',
    authToken: '',
    access_token: '',
     displayName:'',
    canEditDeal:false,
    apiUrl:'',
    LINK_AMBEST_URL:'',
    LINK_AON_URL: '',
    LINK_ERMRCM_URL: '',
    LINK_ERMS_DEALEDIT_API: '',
    LINK_ERMS_URL: '',
    LINK_EXPENSES_URL: '',
    LINK_GUYCARPENTER_URL: '',
    LINK_IMAGE_RIGHT: '',
    LINK_MARKELEXTERNAL_URL: '',
    LINK_MYMARKEL_URL: '',
    LINK_QLIKVIEW_URL: '',
    LINK_SERVICEDESK_EMAIL: '',
    LINK_SNL_URL: '',
    LINK_WORKDAY_URL: '',
    production: false,
    impersonation: true
  },
};

export function reducer(state = initialState, action: auth.AuthActions): AuthState {
  switch (action.type) {
    case AuthActionTypes.AUTHENTICATE_SUCCESS: {
      return {
        ...state,
        loggedIn: true,
        user: action['payload']['user']
      };
    }
    case AuthActionTypes.ADD_BASEAPI: {
      // if(action['payload']&&action['payload']!=null)
      // state.user['personId']=action['payload']['user'].personId;
      return {
        ...state,
        user: action['payload']['user']
        
      };
    }
    case AuthActionTypes.LOGIN_SUCCESS: {
      return {
        ...state,
        loggedIn: true,
        user: action['payload']['user']
      };
    }
    case AuthActionTypes.UPDATE_USER_DISPLAY_NAME: {
      if(action['payload']&&action['payload']!=null){
        state.user['displayName']=action['payload'].userName;
      }
      return {
        ...state
      };
    }
    default: {
      return state;
    }
  }
}

//export const getLoggedIn = (state: AuthState) => state.loggedIn;
export const getUser = (state: AuthState) => state.user;
export const getUserApi = (state: AuthState) => state.user.apiUrl;
export const isAuthenticated = (state: AuthState) => state.user.isAuthenticated;
