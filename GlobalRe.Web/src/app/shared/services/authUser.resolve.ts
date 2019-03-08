import 'rxjs/add/operator/map';
import 'rxjs/add/operator/take';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { CoreService } from "../services/core.service";
import {
  Router, Resolve, RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { USERViEW_API_URL, USER_VIEW_SCREEN_NAME, USER_VIEW_ALL_SUBMISSIONS, USER_VIEW_MY_SUBMISSIONS, USERVIEW_ADD_API_URL } from '../../app.config';
import { catchError, map, delay, switchMap, tap } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Store } from '@ngrx/store';
import * as fromRoot from '../../store/index';
import { LoginUser } from '../models/login-user';
import { forkJoin } from 'rxjs/observable/forkJoin';
import { LoadUserViewsSuccess } from '../../actions/deals/user-views.actions';

@Injectable()
export class AuthenticationUserResolve implements Resolve<any> {

  constructor(private router: Router, private _coreService: CoreService, private _store: Store<fromRoot.AppState>) {
   
  }
  getPersonId(){
    let personId: number, userId: string = '';
    this._store.select<LoginUser>(fromRoot.getAuthenticatedUser).subscribe(user => {
        personId = user.personId;
    }).unsubscribe;
    if(personId){
        userId = personId + '';
    }
    return userId;
  }
  checkForKeyMember(personId){
    let isKeyMember: boolean = false;
    this._store.select(fromRoot.getLookupList).subscribe(val => {
        if(val && val.length > 0){
            for(let i=1;i<5;i++){
                if(val[i].results.find(item => item.value == personId)){
                    isKeyMember = true;
                }
            }
        }
    }).unsubscribe();
    return isKeyMember;
  }
  insertAllSubmissions(isDefaultFlag){
    let requestObj = {
        "screenName": USER_VIEW_SCREEN_NAME,
        "viewname": USER_VIEW_ALL_SUBMISSIONS,
        "default": isDefaultFlag,
        "layout": "all submissions",
        "customView": false,
        "sortOrder": 1
    };
    return this._coreService.invokePostEntityApi({data: requestObj}, USERVIEW_ADD_API_URL);
  }
  insertMySubmissions(isDefaultFlag){
    let requestObj = {
        "screenName": USER_VIEW_SCREEN_NAME,
        "viewname": USER_VIEW_MY_SUBMISSIONS,
        "default": isDefaultFlag,
        "layout": "my submissions",
        "customView": false,
        "sortOrder": 2
    };
    return this._coreService.invokePostEntityApi({data: requestObj}, USERVIEW_ADD_API_URL);
  }
  deleteMysubmissions(isDefaultFlag, isKeyMember, viewId){
    let requestObj = {
        "screenName": USER_VIEW_SCREEN_NAME,
        "default": isDefaultFlag,
        "keyMember": isKeyMember
    }
    return this._coreService.invokeDeleteEntityApi(USERVIEW_ADD_API_URL, viewId, requestObj);
  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<any> | Promise<any> | any {
      return this._coreService.invokeGetListResultApi(USERViEW_API_URL + USER_VIEW_SCREEN_NAME).pipe(
        catchError(err => {
            console.log('caught rethrown error, providing fallback value');
            return of({results: []});
        }),
        delay(1000),
        switchMap(res => {
            let personId: string = this.getPersonId()
            let isKeyMember: boolean = this.checkForKeyMember(personId);
            let insertArr: any = [];
            console.log('resolve', personId, isKeyMember, res);
            if(res && res.results && res.results.length > 0){
                let presentDefault = res.results.find(item => item.data.default);
                let presentAllSubmission = res.results.find(item => item.data.viewname == USER_VIEW_ALL_SUBMISSIONS);
                let presentMySubmission = res.results.find(item => item.data.viewname == USER_VIEW_MY_SUBMISSIONS);
                if(presentDefault){
                    if(!presentAllSubmission){
                        insertArr.push(this.insertAllSubmissions(false));
                    }
                    if(isKeyMember){
                        if(!presentMySubmission){
                            insertArr.push(this.insertMySubmissions(false));
                        }
                    }else{
                        if(presentMySubmission){
                            insertArr.push(this.deleteMysubmissions(presentMySubmission.data.default, false, presentMySubmission.data.viewId));
                        }
                    }
                    
                }else{
                    if(!presentAllSubmission){
                        insertArr.push(this.insertAllSubmissions(!isKeyMember));
                    }
                    if(isKeyMember){
                        if(!presentMySubmission){
                            insertArr.push(this.insertMySubmissions(isKeyMember));
                        }
                    }else{
                        if(presentMySubmission){
                            insertArr.push(this.deleteMysubmissions(false, false, presentMySubmission.data.viewId));
                        }
                    }
                }
            }else{
                insertArr.push(this.insertAllSubmissions(!isKeyMember));
                if(isKeyMember){
                    insertArr.push(this.insertMySubmissions(isKeyMember));
                }
            } 
            if(insertArr.length > 0){
                return forkJoin(insertArr)
            }else{
                return of([]);
            }
        })
      ).switchMap(res => {
        console.log('final switch map called');
        this._coreService.invokeGetListResultApi(USERViEW_API_URL + USER_VIEW_SCREEN_NAME).subscribe(userViewsResponse => {
            this._store.dispatch(new LoadUserViewsSuccess({data: userViewsResponse["results"], msg: 'first_time_data'}));
        });
        return res;
    })
  }
}
