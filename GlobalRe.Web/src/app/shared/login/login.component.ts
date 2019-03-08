import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params, RoutesRecognized } from '@angular/router';
import { Store } from "@ngrx/store";
import * as fromRoot from '../../store/index';
import * as actions from '../../actions/auth.action';
import { Observable } from 'rxjs/Rx';
import { FilterType } from '../../app.config';
import { LoginUser } from '../models/login-user';
import { LoadUserViewsFail, LoadUserViewAgGridState } from '../../actions/deals/user-views.actions';
import { ClearActiveSaveSateExtendedSearchData } from '../../actions/deals/extended-search.action';
import { ClearDealStatusState } from '../../actions/deals/deal-status.actions';

@Component({
  selector: 'login',
  template: '<h3></h3>'
})
export class LoginComponent implements OnInit {
  IMPERSONATION: boolean;
  readonly loginUser$: Observable<LoginUser>;
  constructor(private _store: Store<fromRoot.AppState>,
    private _activatedRoute: ActivatedRoute) {
      this.loginUser$ = this._store.select<LoginUser>(fromRoot.getAuthenticatedUser);
      this.loginUser$.subscribe(val=>{
        this.IMPERSONATION=val.impersonation;
      })
  }

  ngOnInit() {
    // debugger

    if (this.IMPERSONATION) {
      this._activatedRoute.queryParams.subscribe(params => {
        //debugger
        let userName = params['userName'];
        if (userName == undefined)
          this._store.dispatch(new actions.AuthenticateAction());
        else {
          let user = new LoginUser({
            userName: userName
          });
          this._store.dispatch(new actions.AuthenticateSuccessAction({ user: user}));
          this._store.dispatch(new actions.LoginAction({ userName: userName }));
        }
        this._store.dispatch(new LoadUserViewsFail([]));
        this._store.dispatch(new ClearActiveSaveSateExtendedSearchData());
        this._store.dispatch(new ClearDealStatusState());
        let customViewConfigState = {
          userviewAgGridState: {
            colState: [],
            sortState: [],
            filterState: {}
          },
          inceptionFilterState: FilterType.All,
          customViewLoadMsg: 'success'
        };
        this._store.dispatch(new LoadUserViewAgGridState(customViewConfigState));
      });
      //this._route.navigate(['dashboard']);
    }
    else {
      console.log("Impersonation functionality is disabled.");
    }

  }
}
