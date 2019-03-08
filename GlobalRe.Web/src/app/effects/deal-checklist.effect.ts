import {Effect, Actions} from '@ngrx/effects';
import { of } from "rxjs/observable/of";
import { Injectable } from '@angular/core';

import {
    LOAD_DEAL_CHECKLIST,
    LoadDealChecklistSuccess,
    LoadDealChecklistFail,
    CHECK_DEAL_CHECKLIST,
    CheckDealChecklistSuccess,
    CheckDealChecklistFail,
    LoadDealChecklist

} from './../actions/deals/deal-checkList.actions';
import { CoreService } from "../shared/services/core.service";
import { DEAL_API_URL, CHECKLIST } from '../app.config';
// import {  } from "../app.config";



@Injectable()
export class DealChecklistEffects {
    constructor(
        private _actions: Actions,
        private _coreService: CoreService
    ) {}

    @Effect()
    getDealChecklist$ = this._actions
        .ofType(LOAD_DEAL_CHECKLIST)
        .switchMap(payload => {
            console.log(payload)
            return this._coreService.invokeGetListResultApi(payload["payload"])
                .map(DealCheckListresponse => {
                    console.log("User view response", DealCheckListresponse)
                    return new LoadDealChecklistSuccess(DealCheckListresponse["results"]);
                })
                .catch(err => {
                    return of(new LoadDealChecklistFail(err["error"])
                 )
                })
        });

        @Effect()
        checkDealChecklist$ = this._actions
            .ofType(CHECK_DEAL_CHECKLIST)
            .switchMap(payload => {
                console.log(payload)
                return this._coreService.invokeUpdateEntityApi(payload["payload"].requestObj, payload["payload"].url)
                    .map(checklist => {
                        console.log("Checlist -->", checklist)
                        let dealnum = checklist.url.split("/"); // from response
                        return new LoadDealChecklist(DEAL_API_URL+ '/'+ dealnum[3] +'/' + CHECKLIST)
                       
                    })
                    .catch(err => {
                        return of(new CheckDealChecklistFail(err["error"])
                     )
                    })
            });

        
}

