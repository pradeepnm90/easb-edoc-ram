import {Action} from '@ngrx/store';

export const LOAD_EXTENDED_SEARCH_DATA = '[Extended Search] Load extended search data';
export const LOAD_EXTENDED_SEARCH_DATA_FAIL = '[Extended Search] Load extended search data fail';
export const LOAD_EXTENDED_SEARCH_DATA_SUCCESS = '[Extended Search] Load extended search data success';
export const CLEAR_EXTENDED_SEARCH_DATA = '[Extended Search] Load extended search data Clear';
export const UPDATE_ACTIVE_EXTENDED_SEARCH_STATE = '[Extended Search] Update active extended search data';
export const SAVE_ACTIVE_EXTENDED_SEARCH_STATE = '[Extended Search] Save active extended search data';
export const LOAD_EXTENDED_SERACH_KEY_MEMBER_NAME_DATE = '[Extended Search] Load extended search key member name data';
export const CLEAR_ACTIVE_EXTENDED_SEARCH_STATE = '[Extended Search] Clear active extended search data';
export const CLEAR_ACTIVE_N_SAVE_EXTENDED_SEARCH_STATE = '[Extended Search] Clear active and save extended search data'
export class LoadExtendedSearchData implements Action {
    readonly type  = LOAD_EXTENDED_SEARCH_DATA;
    constructor(public payload: any) {}
}
export class LoadExtendedSearchDataFail implements Action {
    readonly type  = LOAD_EXTENDED_SEARCH_DATA_FAIL;
    constructor(public payload: any){}
}
export class LoadExtendedSearchDataSuccess implements Action {
    readonly type  = LOAD_EXTENDED_SEARCH_DATA_SUCCESS;
    constructor(public payload: any){}
}
export class ClearExtendedSearchData implements Action {
    readonly type  = CLEAR_EXTENDED_SEARCH_DATA;
    constructor(){}
}
export class UpdateActiveSateExtendedSearchData implements Action {
    readonly type  = UPDATE_ACTIVE_EXTENDED_SEARCH_STATE;
    constructor(public payload: any){}
}
export class SaveActiveStateExtendedSearchData implements Action {
    readonly type  = SAVE_ACTIVE_EXTENDED_SEARCH_STATE;
    constructor(public payload: any){}
}
export class LoadExtendedSearchKeyMemberNameData implements Action {
    readonly type  = LOAD_EXTENDED_SERACH_KEY_MEMBER_NAME_DATE;
    constructor(public payload: any){}
}
export class ClearActiveSateExtendedSearchData implements Action {
    readonly type  = CLEAR_ACTIVE_EXTENDED_SEARCH_STATE;
    constructor(){}
}
export class ClearActiveSaveSateExtendedSearchData implements Action {
    readonly type  = CLEAR_ACTIVE_N_SAVE_EXTENDED_SEARCH_STATE;
    constructor(){}
}

export type ExtendedSearchActions = LoadExtendedSearchData
                        | LoadExtendedSearchDataFail
                        | LoadExtendedSearchDataSuccess 
                        | ClearExtendedSearchData
                        | UpdateActiveSateExtendedSearchData
                        | SaveActiveStateExtendedSearchData
                        | LoadExtendedSearchKeyMemberNameData
                        | ClearActiveSateExtendedSearchData
                        | ClearActiveSaveSateExtendedSearchData;