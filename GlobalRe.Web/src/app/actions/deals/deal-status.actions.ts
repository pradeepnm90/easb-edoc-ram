import { Action } from '@ngrx/store';
import { DealStatus } from '../../models/deal-status';
import {EntityApiData} from '../../shared/models/entity-api-data';
export enum DealStatusActionTypes {
  GET_DEAL_STATUS = '[DealStatus] Get Deal Status',
  GET_DEAL_STATUS_SUCCESS = '[DealStatus] Get Deal Status Success',
  GET_INPROGRESS_DEAL_COUNT = '[DealStatus] Get Deal Status Count',
  GET_INPROGRESS_DEAL_COUNT_SUCCESS = '[DealStatus] Get Deal Status Count Success',
  UPDATE_DEAL_STATUS_ACTIONS = '[DealStatus] Update deal Status',
  UPDATE_SUB_DEAL_STATUS_ACTIONS = '[DealStatus] Update Sub deal Status',
  UPDATE_DEAL_STATUS_STATE_ACTION = '[DealStatus] Update Deal Status State',
  CLEAR_DEAL_STATUS_STATE_ACTION = '[DealStatus] Clear Deal Status State'
}

export class GetDealStatusAction implements Action {
  readonly type = DealStatusActionTypes.GET_DEAL_STATUS;
  constructor(public payload: string) {}
}

export class GetDealStatusSuccessAction implements Action {
  readonly type = DealStatusActionTypes.GET_DEAL_STATUS_SUCCESS;
  constructor(public payload:  EntityApiData<DealStatus>[]) {}
}
export class GetInprogresDealCountAction implements Action {
  readonly type = DealStatusActionTypes.GET_INPROGRESS_DEAL_COUNT;
  constructor(public payload:  string) {}
}

export class UpdateDealStatusAction implements Action {
  readonly type = DealStatusActionTypes.UPDATE_DEAL_STATUS_ACTIONS;
  constructor(public payload:  EntityApiData<DealStatus>) {}
}
export class UpdateSubDealStatusAction implements Action {
  readonly type = DealStatusActionTypes.UPDATE_SUB_DEAL_STATUS_ACTIONS;
  constructor(public payload:  DealStatus) {}
}


export class GetInprogresDealCountSuccessAction implements Action {
  readonly type = DealStatusActionTypes.GET_INPROGRESS_DEAL_COUNT_SUCCESS;
  constructor(public payload:  any) {}
}

export class UpdateDealStatusState implements Action {
  readonly type = DealStatusActionTypes.UPDATE_DEAL_STATUS_STATE_ACTION;
  constructor(public payload:  any) {}
}
export class ClearDealStatusState implements Action {
  readonly type = DealStatusActionTypes.CLEAR_DEAL_STATUS_STATE_ACTION;
  constructor() {}
}

export type DealStatusActions =
   GetDealStatusAction
  | GetDealStatusSuccessAction
  | GetInprogresDealCountAction
  | GetInprogresDealCountSuccessAction
  | UpdateDealStatusAction
  | UpdateSubDealStatusAction
  | UpdateDealStatusState
  | ClearDealStatusState;
