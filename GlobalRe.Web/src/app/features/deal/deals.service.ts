import { Injectable } from '@angular/core';
import {CoreService} from '../../shared/services/core.service';
import { Observable } from 'rxjs/Observable';
import { Store} from '@ngrx/store';
import * as fromDeals from '../../store/deal-status.reducer';
import * as collection from '../../actions/deals/deal-status.actions';
import * as fromRoot from '../../store/index';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class DealsService {
  
  constructor() {            
  }

  // dispatchGetDealStatusAction(url:string)
  // {
  //   this._store.dispatch(new collection.GetDealStatusAction(url));
  // } 
  // dispatchGetDealStatusCountAction(dealcountURL:any)
  // {
  //   this._store.dispatch(new collection.GetInprogresDealCountAction(dealcountURL));
  // }    
}
