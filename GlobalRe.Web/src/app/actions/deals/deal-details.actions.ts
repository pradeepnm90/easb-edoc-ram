import { Action } from '@ngrx/store';
import {Deal} from "../../models/deal";
import {EntityApiData} from "../../shared/models/entity-api-data";
export const CurrentDealActionTypes =
  {
    LOAD_DEAL :'[Deal] Load',
  LOAD_DEAL_SUCCESS : '[Deal] Load Success',
  UPDATE_DEAL :'[Deal] Update Deal',
  UPDATE_DEAL_SUCCESS : '[Deal] Update Deal Success',
}

export class LoadDealAction implements Action
{
  readonly type = CurrentDealActionTypes.LOAD_DEAL;
  constructor(public payload:any) {}
}
export class LoadSuccess implements Action {
  readonly type = CurrentDealActionTypes.LOAD_DEAL_SUCCESS;
  constructor(public payload: EntityApiData<Deal>) {}
}
export class UpdateDealsAction implements Action
{
  type = CurrentDealActionTypes.UPDATE_DEAL;
  constructor(public payload:  EntityApiData<Deal>) { }
}
export class UpdateDealSuccess implements Action {
  readonly type = CurrentDealActionTypes.UPDATE_DEAL_SUCCESS;
  constructor(public payload: EntityApiData<Deal>) {}
}

export type CurrentDealActions =
   LoadDealAction
  | LoadSuccess
  | UpdateDealsAction
  | UpdateDealSuccess;
