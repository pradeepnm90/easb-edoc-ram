// import { Injectable } from '@angular/core';
// import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
// import { Store } from "@ngrx/store";
// import { AuthenticationService } from '../services/authentication.service';
// import { CoreService } from '../services/core.service';
// import { Observable } from 'rxjs/Rx';
// import * as collection from '../../actions/auth.action';
// import * as fromRoot from '../../store/index';
// import { LoginUser } from '../models/login-user';

// @Injectable()
// export class ImpersonationGuard implements CanActivate {
//   constructor(private _store: Store<fromRoot.AppState>,
//     private _router: Router,
//     private _authenticationService: AuthenticationService) { }

//   canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
//     debugger
//     let user = route.params['id'];    
//     if (true) {     
//       route.queryParams.subscribe(params => {
//         debugger
//         let userName = params['userName'];
//         // if (userName == undefined)
//         //   this._store.dispatch(new actions.AuthenticateAction());
//         // else
//         //   this._store.dispatch(new actions.LoginAction({ userName: userName }));
//       });
//     }
//     else {
//      // this._store.dispatch(new actions.AuthenticateAction());
//     }
//     return true;
//   }
// }
