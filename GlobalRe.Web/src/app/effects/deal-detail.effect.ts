
import { Injectable } from '@angular/core';
import { Effect, Actions } from '@ngrx/effects';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { Store} from '@ngrx/store';
import * as _ from "lodash";
import {  CurrentDealActionTypes, UpdateDealSuccess,LoadSuccess } from '../actions/deals/deal-details.actions';
import { CoreService } from '../shared/services/core.service';
import { SharedEventService } from '../shared/services/shared-event.service';
import { BroadcastEvent } from '../shared/models/broadcast-event';
import * as fromRoot from "../store";
import {EntityType,GlobalEventType} from "../app.config";

@Injectable()
export class DealDetailEffects {

  constructor(private _actions: Actions,private _sharedEventService: SharedEventService,
              private _coreService: CoreService, private _store: Store<fromRoot.AppState>) {
  }

  @Effect() LoadDealDetail$ = this._actions
    .ofType(CurrentDealActionTypes.LOAD_DEAL)
    .switchMap((payload) => {
      return this._coreService.invokeGetEntityApi(payload['payload']);
    })
    .map(dealDetails => {
        return new LoadSuccess(dealDetails['results']);
      }
    );
  @Effect() updateDealDetail$ = this._actions
    .ofType(CurrentDealActionTypes.UPDATE_DEAL)
    .switchMap((payload) => {
      return this._coreService.invokeUpdateEntityApi(payload['payload'], '/v1/deals/' + payload['payload']['data']['dealNumber']);
    })
    .map(dealDetails => {
      this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Deal_saved,
        EntityType.Deals,
        "Deal Saved Successfully",dealDetails));
      return new UpdateDealSuccess(dealDetails['results']);
      }
    );
}
