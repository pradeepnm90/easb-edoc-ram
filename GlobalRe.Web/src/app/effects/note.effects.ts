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
  LoadNotesSuccess,
  LOAD_NOTES,
  LoadNotesFail
} from "../actions/deals/deal-notes.actions";
import { CoreService } from "../shared/services/core.service";
//config
import { DEALBY_STATUS } from "../app.config";
import * as fromDeals from "../store/deal-status.reducer";
import { debug } from "util";
import { DealStatus } from "../models/deal-status";
import { switchMap } from "rxjs/operator/switchMap";
import { map } from "rxjs/operator/map";
import { catchError, tap } from "rxjs/operators";

// const defaultInprogressCountTotal= {"url": "/v1/dealstatussummaries",
// "results":[],"totalRecords": 0, "messages": []};

@Injectable()
export class NoteEffects {
  constructor(
    private _actions: Actions,
    //private _dealsService: DealsService,
    private _coreService: CoreService
  ) {}

  @Effect()
  getNotes$ = this._actions
  .ofType(LOAD_NOTES)
  .switchMap(payload => {
   return  this._coreService.invokeGetListResultApi(payload["payload"])
  .map(notes => {
    return new LoadNotesSuccess(notes["results"]);
  })
  .catch(err =>
  {
   return  of(new LoadNotesFail(err["error"])
   )})
  })
}
