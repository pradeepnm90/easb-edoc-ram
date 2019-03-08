import { createSelector, createFeatureSelector } from "@ngrx/store";
//rootreducer
import * as fromAuth from "./auth.reducer";
import { storeLogger } from "ngrx-store-logger";
import { compose } from "@ngrx/core";
import { combineReducers } from "@ngrx/store";
import * as fromDealStatus from "./deal-status.reducer";
import * as fromApiReducer from "./apiurl.reducer";
import * as fromApplicationState from "./application-state.reducer";
import * as fromLookupReducer from "./lookup-values.reducer";
import * as fromDealDetailReducer from "./deal-details.reducer";
import * as fromNoteReducer from "./note.reducer";
import * as fromDocumentReducer from "./document.reducer";
import * as fromKeyNonKeyDocumentReducer from "./key-non-key-documents.reducer";
import * as fromExtendedSearchReducer from "./extended-search.reducer";
import * as fromUserViewreducer from "./user-view.reducer";
import * as fromcustomViewconfigState from "./userViewState.reducer"
import * as fromDealChecklistState from "./deal-checklist.reducer";
import * as fromUserNotificationState from "./user-notification.reducer";
export interface AppState {
  reducer: {
    authentication: fromAuth.AuthState;
    dealStatus: fromDealStatus.DealStatusState;
    apiList: fromApiReducer.ApiUrlState;
    lookupList: fromLookupReducer.LookupValueListState;
    currentDealDetail: fromDealDetailReducer.DealDetailsState;
    applicationState: fromApplicationState.AppState;
    note: fromNoteReducer.NoteState;
    document: fromDocumentReducer.DocumentState;
    keyNonKeyDocument: fromKeyNonKeyDocumentReducer.KeyNonKeyDocumentState;
    extendedSearchData: fromExtendedSearchReducer.ExtendedSearchState;
    userView: fromUserViewreducer.UserViewState;
    customViewconfig: fromcustomViewconfigState.customViewState;
    dealChecklist: fromDealChecklistState.DealChecklist;
    userNotificationList: fromUserNotificationState.UserNotificationState;
  };
}
export const reducers = {
  authentication: fromAuth.reducer,
  dealStatus: fromDealStatus.reducer,
  lookupList: fromLookupReducer.reducer,
  currentDealDetail: fromDealDetailReducer.reducer,
  apiList: fromApiReducer.reducer,
  applicationState: fromApplicationState.reducer,
  note: fromNoteReducer.reducer,
  document: fromDocumentReducer.reducer,
  keyNonKeyDocument: fromKeyNonKeyDocumentReducer.reducer,
  extendedSearchData: fromExtendedSearchReducer.reducer,
   userView: fromUserViewreducer.reducer,
   customViewconfig: fromcustomViewconfigState.reducer,
   dealChecklist: fromDealChecklistState.reducer,
   userNotificationList: fromUserNotificationState.reducer
};

const developmentReducer: Function = compose(
  storeLogger(),
  combineReducers
)(reducers);
export function metaReducer(state: any, action: any) {
  return developmentReducer(state, action);
}


export const getAuthenticationState = (state: AppState) =>
  state.reducer ? state.reducer.authentication : null;
export const getAuthenticatedUser = createSelector(
  getAuthenticationState,
  fromAuth.getUser
);
export const getBaseApiUrl = (state: AppState)=>
  state.reducer? state.reducer.authentication : null;
export const getBaseApiUrlValue = createSelector(
  getBaseApiUrl,
  fromAuth.getUserApi
)

export const getDealStatusState = (state: AppState) =>
  state.reducer ? state.reducer.dealStatus : [];
export const getDealStatus = createSelector(
  getDealStatusState,
  fromDealStatus.getDealStatus
);


export const getApiListState = (state: AppState) =>
  state.reducer ? state.reducer.apiList : [];
export const getApiList = createSelector(
  getApiListState,
  fromApiReducer.getApiUrls
);

export const getLookupListState = (state: AppState) =>
  state.reducer ? state.reducer.lookupList : [];
export const getLookupList = createSelector(
  getLookupListState,
  fromLookupReducer.getLookupList
);

export const getResponseDealDetailUpdateState = (state: AppState) =>
  state.reducer ? state.reducer.currentDealDetail : [];
export const getResponseUpdateDealDetail = createSelector(
  getResponseDealDetailUpdateState,
  fromDealDetailReducer.updateDealDetail
);

export const isUserAuthenticated = createSelector(
  getAuthenticationState,
  fromAuth.isAuthenticated
);

export const getNoteState = (state: AppState) =>
  state.reducer ? state.reducer.note : [];
export const getNoteList = createSelector(
  getNoteState,
  fromNoteReducer.getNoteList
);

export const getDocumentState = (state: AppState) =>
  state.reducer ? state.reducer.document : [];
export const getDocumentList = createSelector(
  getDocumentState,
  fromDocumentReducer.getDocumentList
);
export const getKeyNonKeyDocumentState = (state: AppState) =>
  state.reducer ? state.reducer.keyNonKeyDocument : [];
export const getKeyNonKeyDocumentList = createSelector(
  getKeyNonKeyDocumentState,
  fromKeyNonKeyDocumentReducer.getKeyNonKeyDocumentList
);
export const getAllPageDetail = createSelector(
  getKeyNonKeyDocumentState,
  fromKeyNonKeyDocumentReducer.getAllPageDetail
);

export const getAppState = (state: AppState) =>
  state.reducer ? state.reducer.applicationState : [];
export const getApplicationState = createSelector(
  getAppState,
  fromApplicationState.getApplicationState
);


// extended search related store
export const getExtendedSearchDataState = (state: AppState) =>
  state.reducer ? state.reducer.extendedSearchData : null;
export const getExtendedSearchMainData = createSelector(
  getExtendedSearchDataState,
  fromExtendedSearchReducer.extendedSearchMainData
);
export const getExtendedSearchSaveState = createSelector(
  getExtendedSearchDataState,
  fromExtendedSearchReducer.extendedSearchSaveData
);
export const getExtendedSearchActiveState = createSelector(
  getExtendedSearchDataState,
  fromExtendedSearchReducer.extendedSearchActiveData
);

export const getUserViewState = (state: AppState) =>
  state.reducer ? state.reducer.userView : [];
export const getUserViewList = createSelector(
  getUserViewState,
  fromUserViewreducer.getUserViewList
);
export const getUserViewListMsg = createSelector(
  getUserViewState,
  fromUserViewreducer.getUserViewListMsg
);

export const getUserViewGridState = (state: AppState) =>
  state.reducer ? state.reducer.customViewconfig : [];
export const agGridState = createSelector(
  getUserViewGridState,
  fromcustomViewconfigState.getCustomUserViewState
);

export const getDealChecklistState = (state: AppState) =>
  state.reducer ? state.reducer.dealChecklist : [];
export const getDealCheckList = createSelector(
  getDealChecklistState,
  fromDealChecklistState.getDealChecklist
);

export const getUserNotificationState = (state: AppState) =>
  state.reducer ? state.reducer.userNotificationList : [];
export const getUserNotificationList = createSelector(
  getUserNotificationState,
  fromUserNotificationState.getUserNotificationList
);


//export const isMultiSelectionMode = createSelector(getApplicationState,fromApplicationStateReducer.isMultiSelectMode);
