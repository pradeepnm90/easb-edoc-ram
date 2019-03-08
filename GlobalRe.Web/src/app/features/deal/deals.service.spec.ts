import { TestBed, inject } from '@angular/core/testing';
import { DealsService } from './deals.service';
import { CoreService } from '../../shared/services/core.service';
import {HttpClient,HttpHandler} from "@angular/common/http";
import { StoreModule, Store, combineReducers,StateObservable } from '@ngrx/store';
import * as fromRoot from '../../store/index';

let coreService:CoreService;

describe('DealsServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [        
        StoreModule.forRoot({
          auth: combineReducers(fromRoot.reducers),
        }),       
      ],
      providers: [
        DealsService,
        CoreService,
        HttpClient,
        HttpHandler,
        Store
      ],
      
    }).compileComponents();;

  });

  it('should be created', inject([DealsService], (service: DealsService) => {
    expect(service).toBeTruthy();
  }));


});
