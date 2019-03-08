import { Action } from '@ngrx/store';
import { Authenticate } from '../models/user';
import { LoginUser } from '../shared/models/login-user';


export const AuthActionTypes = {
  AUTHENTICATE: '[Auth] Authenticate',
  AUTHENTICATE_SUCCESS: '[Auth] Authenticate Success',
  LOGIN: '[Auth] Login',
  LOGOUT: '[Auth] Logout',
  LOGIN_SUCCESS: '[Auth] Login Success',
  LOGIN_FAILURE: '[Auth] Login Failure',
  UPDATE_USER_DISPLAY_NAME:'[Auth] update user display name',
  GET_USER_DISPLAY_NAME:'[Auth] get user display name',
  ADD_BASEAPI: '[Auth] Set base api'

}
export class AddBaseApiURL implements Action {
  readonly type = AuthActionTypes.ADD_BASEAPI;
  constructor(public payload: {user:LoginUser}) { }
}
export class AuthenticateAction implements Action {
  readonly type = AuthActionTypes.AUTHENTICATE;
  constructor() { }
}

export class AuthenticateSuccessAction implements Action {
  readonly type = AuthActionTypes.AUTHENTICATE_SUCCESS;
  constructor(public payload: { user: LoginUser }) { }
}
export class GetUserDisplayNameAction implements Action {
  readonly type = AuthActionTypes.GET_USER_DISPLAY_NAME;
  constructor() { }
}
export class UpdateUserDisplayNameAction implements Action {
  readonly type = AuthActionTypes.UPDATE_USER_DISPLAY_NAME;
  constructor(public payload: any) { }
}
export class LoginAction implements Action {
  readonly type = AuthActionTypes.LOGIN;
  constructor(public payload: { userName: string }) { }
}

export class LoginSuccessAction implements Action {
  readonly type = AuthActionTypes.LOGIN_SUCCESS;

  constructor(public payload: { user: LoginUser}) { }
}

export class LoginFailureAction implements Action {
  readonly type = AuthActionTypes.LOGIN_FAILURE;
  constructor() { }
  //constructor(public payload: { error: string }) { }
}


export type AuthActions =
  | LoginAction
  | LoginSuccessAction
  | AuthenticateAction
  | LoginFailureAction
  | GetUserDisplayNameAction
  | UpdateUserDisplayNameAction
  | AddBaseApiURL;

