import { Injectable } from '@angular/core';
import { Route,Router, CanActivate,CanLoad, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Store } from "@ngrx/store";
import { AuthenticationService } from '../services/authentication.service';
import { Observable } from 'rxjs/Rx';
import * as fromRoot from '../../store/index';
import { LoginUser } from '../models/login-user';
import * as actions from '../../actions/auth.action';

@Injectable()
export class AuthGuard implements CanActivate,CanLoad{
  constructor(private _store: Store<fromRoot.AppState>,
    private _router: Router,
    private _authenticationService: AuthenticationService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    const observable = this._store.select(fromRoot.getAuthenticatedUser);
    return observable.map(user => {
      const authToken = this._authenticationService.getToken();
      const authenticated = user.isAuthenticated && authToken != null;
      if (!authenticated) {
        return false;
      }
      return true;
    }).take(1);

  }

  canLoad(route: Route): Observable<boolean>|Promise<boolean>|boolean {
    //debugger;
    this._store.select<LoginUser>(fromRoot.getAuthenticatedUser).subscribe(user => {
      //debugger
          if (user.userName == undefined || user.userName == '') {
            this._store.dispatch(new actions.AuthenticateAction());
          }
        });
    return true;
  }
}
