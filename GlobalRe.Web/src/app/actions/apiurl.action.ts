import { Action } from '@ngrx/store';
import { ApiUrl } from '../shared/models/api-url';


export const ApiUrlActionTypes = {
  ADD_APIURL: '[ApiUrl] Add',
  UPDATE_APIURL: '[ApiUrl] Update',
}

export class AddApiUrlAction implements Action {
  readonly type = ApiUrlActionTypes.ADD_APIURL;
  constructor(public payload: ApiUrl[]) { }
}

export class UpdateApiUrlAction implements Action {
  readonly type = ApiUrlActionTypes.UPDATE_APIURL;
  constructor(public payload: ApiUrl[]) { }
}


export type ApiUrlActions =
  | AddApiUrlAction
  | UpdateApiUrlAction;

