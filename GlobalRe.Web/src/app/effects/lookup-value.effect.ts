import {Injectable} from '@angular/core';
import {Effect, Actions, toPayload} from '@ngrx/effects';
import {LookupsActionTypes,getLookupSuccessAction} from "../actions/look-up-values.action";
import { forkJoin } from 'rxjs/observable/forkJoin';
import {CoreService} from "../shared/services/core.service";
// import {BASE_API_URL, EntityType} from "../app.config";
import {Observable} from "rxjs/Observable";
import {LookupValues} from "../shared/models/lookup-value";
import {HttpClient} from "@angular/common/http";
import { LoadExtendedSearchKeyMemberNameData } from '../actions/deals/extended-search.action';

@Injectable()
export class LookupValueEffects{
    constructor(private _updates$: Actions, private _coreService: CoreService){}

    @Effect() entityOptions$ = this._updates$
        .ofType(LookupsActionTypes.GET_LOOKUP)        
        .switchMap((payload) => 
        {                    
            return forkJoin(                
                 payload['payload'].map(apiUrls=> {
                        return this._coreService.invokeGetListResultApi(apiUrls);
                 }));            
        })        
        .switchMap(result => {          
          return [new  getLookupSuccessAction(result), new LoadExtendedSearchKeyMemberNameData(result)]; 
        });           

}
