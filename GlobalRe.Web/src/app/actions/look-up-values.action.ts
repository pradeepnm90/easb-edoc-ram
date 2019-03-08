import {Injectable} from '@angular/core';
import {Action} from '@ngrx/store';
import {LookupValues} from "../shared/models/lookup-value";

export enum LookupsActionTypes {
    GET_LOOKUP= '[LookupValues] Get Lookup Values',
    GET_LOOKUP_SUCCESS= '[LookupValues] Get Lookup Values Success'
  }

  export class getLookupAction implements Action {    
    readonly type = LookupsActionTypes.GET_LOOKUP;
    constructor(public payload:  string[]) { }
  }
  
  export class getLookupSuccessAction implements Action {    
    readonly type = LookupsActionTypes.GET_LOOKUP_SUCCESS;
    constructor(public payload: any[]) { }
  }
  
  
  export type LookupsActions =
    | getLookupAction
    | getLookupSuccessAction;


