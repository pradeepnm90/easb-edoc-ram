// import 'rxjs/add/operator';
// import 'rxjs/add/operator/map';
import {Observable} from 'rxjs/Rx';
import { Injectable } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { of } from 'rxjs/observable/of';
import { Action } from '@ngrx/store';
//store
import { Store} from '@ngrx/store';
import * as dealStatusCollection from '../actions/deals/deal-status.actions';
import {
  DealStatusActionTypes,
  DealStatusActions,
  GetDealStatusSuccessAction,GetInprogresDealCountSuccessAction
} from '../actions/deals/deal-status.actions';
//Services
import { DealsService } from '../../app/features/deal/deals.service';
import { CoreService } from '../shared/services/core.service';
//config
import {DEALBY_STATUS} from '../app.config';
import * as fromDeals from '../store/deal-status.reducer';
import { debug } from 'util';
import { DealStatus } from '../models/deal-status';

// const defaultInprogressCountTotal= {"url": "/v1/dealstatussummaries",
// "results":[],"totalRecords": 0, "messages": []};


@Injectable()
export class DealEffects {

  constructor(private _actions: Actions,
              //private _dealsService: DealsService,
              private _coreService: CoreService,
              private store: Store<fromDeals.DealStatusState>) {
  }

  @Effect()
  getDealStatus$ = this._actions
    .ofType(DealStatusActionTypes.GET_DEAL_STATUS)
    .switchMap((payload) => {
      return this._coreService.invokeGetListResultApi(payload['payload']);
    })
    .map(dealstatus => {
        return new GetDealStatusSuccessAction(dealstatus['results']);
      }
    );

  // @Effect()
  // getInprgressDealsCount$ = this._actions
  //   .ofType(DealStatusActionTypes.GET_INPROGRESS_DEAL_COUNT)
  //   .switchMap((payload) => {
  //     if (payload['payload'].split('=').length > 1 && payload['payload'].split('=')[1] !== "")
  //       return this._coreService.invokeGetListResultApi(payload['payload']);
  //     else
  //       return Observable.of(defaultInprogressCountTotal);
  //   })
  //   .map(dealStatus => {
  //       return new GetInprogresDealCountSuccessAction(dealStatus['totalRecords']);
  //     }
  //   );
}
