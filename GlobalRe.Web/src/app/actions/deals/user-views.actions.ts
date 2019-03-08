
import { Action } from '@ngrx/store';

export const LOAD_USER_VIEWS = '[VIEW] Load user view';
export const LOAD_USER_VIEWS_FAIL = '[VIEW] Load user view fail';
export const LOAD_USER_VIEWS_SUCCESS = '[VIEW] Load user view success';

export const ADD_USER_VIEWS = '[VIEW] ADD user view';
export const ADD_USER_VIEWS_FAIL = '[VIEW] ADD user view fail';
export const ADD_USER_VIEWS_SUCCESS = '[VIEW] ADD user view success';

export const DELETE_USER_VIEWS = '[VIEW] DELETE user view';
export const DELETE_USER_VIEWS_FAIL = '[VIEW] DELETE user view fail';
export const DELETE_USER_VIEWS_SUCCESS = '[VIEW] DELETE user view success';

export const LOAD_USER_VIEW_AG_GRID_STATE = '[VIEW] AG GRID STATE';
//export const USER_VIEW_AG_GRID_STATE_FAIL = '[VIEW] AG GRID STATE FAIL';
//export const USER_VIEW_AG_GRID_STATE_SUCCESS = '[VIEW] AG GRID STATE SUCCESS';

export const LOAD_USER_VIEWS_STATE = '[VIEW] Load user view state';
export const LOAD_USER_VIEWS_STATE_FAIL = '[VIEW] Load user view state fail';
export const LOAD_USER_VIEWS_STATE_SUCCESS = '[VIEW] Load user view state success';

export const UPDATE_USER_VIEWS_STATE_MSG = '[VIEW] AG GRID STATE MSG UPDATE';
export const UPADTE_USER_VIEWS_MSG = '[VIEW] USER VIEW LIST MSG UPDATE'

export class LoadUserViews implements Action {
    readonly type = LOAD_USER_VIEWS;
    constructor(public payload: any) { }
}
export class LoadUserViewsFail implements Action {
    readonly type = LOAD_USER_VIEWS_FAIL;
    constructor(public payload: any) { }
}
export class LoadUserViewsSuccess implements Action {
    readonly type = LOAD_USER_VIEWS_SUCCESS;
    constructor(public payload: any) { }
}

export class AddUserViews implements Action {
    readonly type = ADD_USER_VIEWS;
    constructor(public payload: any) { }
}
export class AddUserViewsFail implements Action {
    readonly type = ADD_USER_VIEWS_FAIL;
    constructor(public payload: any) { }
}
export class AddUserViewsSuccess implements Action {
    readonly type = ADD_USER_VIEWS_SUCCESS;
    constructor(public payload: any) { }
}
export class DeleteUserViews implements Action {
    readonly type = DELETE_USER_VIEWS;
    constructor(public payload: any) { }
}
export class DeleteUserViewsFail implements Action {
    readonly type = DELETE_USER_VIEWS_FAIL;
    constructor(public payload: any) { }
}
export class DeleteUserViewsSuccess implements Action {
    readonly type = DELETE_USER_VIEWS_SUCCESS;
    constructor(public payload: any) { }
}

export class LoadUserViewAgGridState implements Action {
    readonly type = LOAD_USER_VIEW_AG_GRID_STATE;
    constructor(public payload: any) { }
}

// export class UserViewAgGridStateFail implements Action {
//     readonly type = USER_VIEW_AG_GRID_STATE_FAIL;
//     constructor(public payload: any) { }
// }

// export class UserViewAgGridStateSuccess implements Action {
//     readonly type = USER_VIEW_AG_GRID_STATE_SUCCESS;
//     constructor(public payload: any) { }
// }

export class LoadUserViewsSate implements Action {
    readonly type = LOAD_USER_VIEWS_STATE;
    constructor(public payload: any) { }
}
export class LoadUserViewsStateFail implements Action {
    readonly type = LOAD_USER_VIEWS_STATE_FAIL;
    constructor(public payload: any) { }
}
export class LoadUserViewsStateSuccess implements Action {
    readonly type = LOAD_USER_VIEWS_STATE_SUCCESS;
    constructor(public payload: any) { }
}
export class UpdateUserViewsStateMsg implements Action {
    readonly type = UPDATE_USER_VIEWS_STATE_MSG;
    constructor(public payload: any) { }
}
export class UpdateUserViewsListMsg implements Action {
    readonly type = UPADTE_USER_VIEWS_MSG;
    constructor(public payload: any) { }
}

export type UserViewAction = LoadUserViews 
    | LoadUserViewsFail 
    | LoadUserViewsSuccess 
    | AddUserViews 
    | AddUserViewsFail 
    | AddUserViewsSuccess
    | DeleteUserViews
    | DeleteUserViewsFail
    | DeleteUserViewsSuccess
    | LoadUserViewAgGridState
    | LoadUserViewsSate
    | LoadUserViewsStateFail
    | LoadUserViewsStateSuccess
    | UpdateUserViewsStateMsg
    | UpdateUserViewsListMsg;