import {Action} from '@ngrx/store';

export const LOAD_DEAL_CHECKLIST = '[DEAL] Load Deal Checklist';
export const LOAD_DEAL_CHECKLIST_FAIL = '[DEAL] Load Deal Checklist Fail';
export const LOAD_DEAL_CHECKLIST_SUCCESS = '[DEAL] Load Deal Checklist Success';

export const CHECK_DEAL_CHECKLIST = '[DEAL] Check Deal Checklist';
export const CHECK_DEAL_CHECKLIST_FAIL = '[DEAL] Check Deal Checklist Fail';
export const CHECK_DEAL_CHECKLIST_SUCCESS = '[DEAL] Check Deal Checklist Success';

export class LoadDealChecklist implements Action{
    readonly type = LOAD_DEAL_CHECKLIST;
    constructor(public payload: any){}
}

export class LoadDealChecklistFail implements Action{
    readonly type = LOAD_DEAL_CHECKLIST_FAIL;
    constructor(public payload: any){}
}

export class LoadDealChecklistSuccess implements Action{
    readonly type = LOAD_DEAL_CHECKLIST_SUCCESS;
    constructor(public payload: any){}
}

export class CheckDealChecklist implements Action{
    readonly type = CHECK_DEAL_CHECKLIST;
    constructor(public payload: any){}
}

export class CheckDealChecklistFail implements Action{
    readonly type = CHECK_DEAL_CHECKLIST_FAIL;
    constructor(public payload: any){}
}

export class CheckDealChecklistSuccess implements Action{
    readonly type = CHECK_DEAL_CHECKLIST_SUCCESS;
    constructor(public payload: any){}
}

export type DealCheckListAction = LoadDealChecklist 
| LoadDealChecklistFail 
| LoadDealChecklistSuccess
| CheckDealChecklist
| CheckDealChecklistFail
| CheckDealChecklistSuccess;