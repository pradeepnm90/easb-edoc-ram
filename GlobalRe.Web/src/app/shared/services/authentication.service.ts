import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/exhaustMap';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';
import { LoginUser } from '../../shared/models/login-user';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import * as fromRoot from '../../store/index';
import {Store} from "@ngrx/store";
import { CoreService } from './core.service';
import { USER_DISPLAYNAME_URL } from '../../app.config';
@Injectable()
export class AuthenticationService {
  BASE_API_URL ="";
  public baseApiUrl$: Observable<string>;
  constructor(private _http: HttpClient,private _store: Store<fromRoot.AppState>) {
    this.baseApiUrl$ = this._store.select(fromRoot.getBaseApiUrlValue);
          this.baseApiUrl$.subscribe(dataApi =>{
          this.BASE_API_URL = dataApi +'/'; 
        });
  }

  getToken() {
    return sessionStorage.getItem('authToken');
  }

  authenticateUser(impersonatedUserName: string) {
    var tokenUrl = window.location.origin.indexOf("localhost") == -1? window.location.origin + '/token' : "http://d-rdc-core01:50092/token";
    var params = JSON.stringify({impersonatedUser: impersonatedUserName});

    return this._http.post(tokenUrl, params,
    {
        headers: new HttpHeaders()
            .set('Accept', '*/*')
            .set('Content-Type', 'application/x-www-form-urlencoded'),
        withCredentials: true
    })
    .map((authResponse: LoginUser) => {
  
      let canEdit = authResponse["Underwriting DealEdit EditDeal"];
      console.log("Auth service Can Edit:"+canEdit)
      authResponse.canEditDeal = canEdit && canEdit.toUpperCase() == 'TRUE' ? true:false;
      let loginUserDetail=new LoginUser(authResponse);
      if (authResponse && authResponse.isAuthenticated) {
        sessionStorage.setItem('authToken', authResponse.access_token);
      } else {
        loginUserDetail = new LoginUser();
      }
      return loginUserDetail;
    });
  }

  getUserDisplayName() {
    return this._http.get(this.BASE_API_URL + USER_DISPLAYNAME_URL)
      .map(userDisplayNameResponse => { 
        console.log("User Dssisplay Name:"+userDisplayNameResponse)
        return userDisplayNameResponse['results'];
      });

  }
  getUserName() {
    var user_name_url = window.location.origin.indexOf("localhost") == -1? window.location.origin + '/whoami' : "http://d-rdc-core01:50092/whoami";
    return this._http.get(user_name_url, { withCredentials: true })
      .map(userNameResponse => {
        let userName = '';
        if (userNameResponse['UserName'] != null) {
          const domainNameIndex = userNameResponse['UserName'].indexOf('\\');
          userName = userNameResponse['UserName'].substring(domainNameIndex + 1, userNameResponse['UserName'].length);
        }
       return Observable.of({ userName: userName });
      });
  }
}
