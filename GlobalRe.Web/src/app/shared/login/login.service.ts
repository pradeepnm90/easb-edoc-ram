import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/exhaustMap';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';
import { LoginUser } from '../../shared/models/login-user';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Store } from "@ngrx/store";
import * as collection from '../../actions/auth.action';
import * as fromRoot from '../../store/index';

@Injectable()
export class LoginService {
  loginUser: LoginUser;
  constructor(private _http: HttpClient, private _store: Store<fromRoot.AppState>,
    private _router: Router) {

  }

  login() {    
    this._store.dispatch(new collection.AuthenticateAction());
  }

}
