import { Observable } from "rxjs/Rx";
import { Injectable } from "@angular/core";
import { Effect, Actions } from "@ngrx/effects";
import { of } from "rxjs/observable/of";
import { CoreService } from "../shared/services/core.service";
import { LOAD_EXTENDED_SEARCH_DATA, LoadExtendedSearchDataSuccess, LoadExtendedSearchDataFail } from "../actions/deals/extended-search.action";


@Injectable()
export class ExtendedSearchEffects {
  constructor(
    private _actions: Actions,
    private _coreService: CoreService
  ) {
    console.log('ExtendedSearchEffects constructor')
  }

  @Effect()
  getExtendedSearchData$ = this._actions
  .ofType(LOAD_EXTENDED_SEARCH_DATA)
  .switchMap(payload => {
    console.log('getExtendedSearchData effect');
   return  this._coreService.invokeGetListResultApi(payload["payload"])
  .map(notes => {
    return new LoadExtendedSearchDataSuccess(notes["results"]);
  })
  .catch(err =>
  {
   return  of(new LoadExtendedSearchDataFail([])
   )})
  })
}
