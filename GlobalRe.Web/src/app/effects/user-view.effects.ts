// import 'rxjs/add/operator';
// import 'rxjs/add/operator/map';
import { Observable } from "rxjs/Rx";
import { Injectable } from "@angular/core";
import { Effect, Actions } from "@ngrx/effects";
import { of } from "rxjs/observable/of";
import { Action } from "@ngrx/store";
//store
import { Store } from "@ngrx/store";
import {
    LoadUserViewsFail,
    LOAD_USER_VIEWS,
    LoadUserViewsSuccess,
    ADD_USER_VIEWS,
    AddUserViewsSuccess,
    ADD_USER_VIEWS_FAIL,
    LoadUserViews,
    DELETE_USER_VIEWS,
    DeleteUserViewsFail,
    // USER_VIEW_AG_GRID_STATE,
    // UserViewAgGridStateFail,
    // UserViewAgGridStateSuccess,
    LOAD_USER_VIEWS_STATE,
    LoadUserViewsStateSuccess,
    LoadUserViewsStateFail,
    LoadUserViewAgGridState,
    LOAD_USER_VIEW_AG_GRID_STATE,
} from "../actions/deals/user-views.actions";
import { CoreService } from "../shared/services/core.service";
//config
import { DEALBY_STATUS, USERViEW_API_URL, USER_VIEW_SCREEN_NAME } from "../app.config";
import * as fromDeals from "../store/deal-status.reducer";
import { debug } from "util";
import { DealStatus } from "../models/deal-status";
import { switchMap } from "rxjs/operator/switchMap";
import { map } from "rxjs/operator/map";
import { catchError, tap } from "rxjs/operators";
import { SaveActiveStateExtendedSearchData } from "../actions/deals/extended-search.action";
import { GetDealStatusSuccessAction, UpdateDealStatusState } from "../actions/deals/deal-status.actions";

// const defaultInprogressCountTotal= {"url": "/v1/dealstatussummaries",
// "results":[],"totalRecords": 0, "messages": []};

@Injectable()
export class UserViewsEffects {
    constructor(
        private _actions: Actions,
        //private _dealsService: DealsService,
        private _coreService: CoreService
    ) { }

    @Effect()
    getUserViews$ = this._actions
        .ofType(LOAD_USER_VIEWS)
        .switchMap(payload => {
            return this._coreService.invokeGetListResultApi(payload["payload"])
                .map(userViewsResponse => {
                    console.log("User view response", userViewsResponse)
                    return new LoadUserViewsSuccess(userViewsResponse["results"]);
                })
                .catch(err => {
                    return of(new LoadUserViewsFail(err["error"])
                    )
                })
        })


    @Effect() addUserView$ = this._actions
        .ofType(ADD_USER_VIEWS)
        .switchMap((payload, url) => {

            return this._coreService.invokePostEntityApi(payload["payload"].requestObj, payload["payload"].url)
        }).map(userViewsResponse => {
            console.log("User view response", userViewsResponse)
            return new LoadUserViews(USERViEW_API_URL + USER_VIEW_SCREEN_NAME)
        })
   
    @Effect()
    deleteUserViews$ = this._actions
        .ofType(DELETE_USER_VIEWS)
        .switchMap(payload => {
            return this._coreService.invokeDeleteEntityApi(payload["payload"].url, payload["payload"].id, payload["payload"].data)
                .map(userViewsResponse => {
                    console.log("User view response", userViewsResponse)
                    return new LoadUserViews(USERViEW_API_URL + USER_VIEW_SCREEN_NAME)// get api list
                })
                .catch(err => {
                    return of(new DeleteUserViewsFail(err["error"])
                    )
                })
        })

    // @Effect()
    // userViewAgGridState$ = this._actions
    //     .ofType(USER_VIEW_AG_GRID_STATE)
    //     .switchMap(payload => {
    //         console.log("Payload state", payload)
    //         return [payload]
    //             .map(userViewsResponse => {
    //                 console.log("User view response", userViewsResponse)
    //             })
    //     })
    // let screenState = {
    //     extendedSearch: this.extendedSearchSaveState,
    //     dealStatus: dealStatus,
    //     inceptionFilter: inceptionState,
    //     agGrid: agGridState
    //   };

        @Effect()
        getUserViewsState$ = this._actions
        .ofType(LOAD_USER_VIEWS_STATE)
        .switchMap(payload => {
            console.log("Payload--->", payload)
            return this._coreService.invokeGetListResultApi(payload["payload"])
        }).switchMap(res =>{
            console.log("State", res["data"])
            let response = JSON.parse(res["data"].layout);
            let customViewConfigState = {
                userviewAgGridState: response.agGrid,
                inceptionFilterState: response.inceptionFilter,
                customViewLoadMsg: 'success'
            };
                   return [
                    new SaveActiveStateExtendedSearchData(response.extendedSearch),
                    new GetDealStatusSuccessAction(response.dealStatus),
                    new LoadUserViewAgGridState(customViewConfigState)
                ] 
        });

}
