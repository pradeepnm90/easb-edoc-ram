import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/exhaustMap';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';
import { Observable } from 'rxjs/Rx';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Effect, Actions } from '@ngrx/effects';
import { of } from 'rxjs/observable/of';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Store } from "@ngrx/store";
import * as actions from '../actions/auth.action';
import * as fromRoot from '../store/index';
import 'rxjs/add/operator/map';
import * as lookup from '../actions/look-up-values.action';

import {
  LoginSuccessAction,
  LoginAction,
  AuthActionTypes,
} from '../actions/auth.action';
import { STATUSNAME_API_URL, UNDERWRITERS_API_URL, TAs_UAs_API_URL, ACTUARIES_API_URL, MODELERES_API_URL, NOTE_TYPE_URL, EXTENDED_SEARCH_URL } from '../app.config';
import { LoadExtendedSearchData } from '../actions/deals/extended-search.action';
import { CoreService } from '../shared/services/core.service';
import { observeOn } from 'rxjs/operators';

@Injectable()
export class AuthEffects {
  constructor(
    private _router: Router,
    private _actions: Actions,
    private _authenticationService: AuthenticationService,
    private _coreService: CoreService
  ) { }

  @Effect()
  login$ = this._actions.ofType(AuthActionTypes.LOGIN)
    .switchMap((payload) => {
      return this._authenticationService.authenticateUser(payload['payload'].userName);
    }).map((user) => {
       return new actions.AddBaseApiURL({user: user})
    });
  @Effect()
  baseApiUrl$ = this._actions.ofType(AuthActionTypes.ADD_BASEAPI)
  .switchMap((payload) =>{
   return [
        new actions.GetUserDisplayNameAction(),
        new actions.LoginSuccessAction(payload['payload']),
        new lookup.getLookupAction([STATUSNAME_API_URL, UNDERWRITERS_API_URL, TAs_UAs_API_URL, ACTUARIES_API_URL, MODELERES_API_URL, NOTE_TYPE_URL]),
        new LoadExtendedSearchData(EXTENDED_SEARCH_URL),
        
      ]   
  })
  @Effect({ dispatch: false })
  loginSuccess$ = this._actions.ofType(AuthActionTypes.LOGIN_SUCCESS)
    .do(() => {
      this._router.navigate(['/dashboard']);
    });
  @Effect()
  updateUserDisplayName = this._actions.ofType(AuthActionTypes.GET_USER_DISPLAY_NAME)
    .switchMap(() => {
      console.log("DISPLAY GET NAME")
      return this._authenticationService.getUserDisplayName();
    }).map(userDisplayName =>  {
      console.log(userDisplayName)
      if(userDisplayName&&userDisplayName.length&&userDisplayName[0]['data'])
      return new actions.UpdateUserDisplayNameAction({ 'userName': userDisplayName[0]['data']['firstName']+' '+userDisplayName[0]['data']['lastName']});
      else
        return new actions.UpdateUserDisplayNameAction(null);
    });
  @Effect()
  authenticate$ = this._actions.ofType(AuthActionTypes.AUTHENTICATE)
    .switchMap(() => {
      return this._authenticationService.getUserName();
    }).map(user => {
      const userName = user["value"].userName;
      return new actions.LoginAction({ userName: userName });
    });

  @Effect()
  loginFailed$ = this._actions.ofType(AuthActionTypes.LOGIN_FAILURE)
    .do(() => {
      this._router.navigate(['/login']);
    });
}
