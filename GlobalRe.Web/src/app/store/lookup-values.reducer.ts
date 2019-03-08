  import {LookupValues} from "../shared/models/lookup-value";
  import {LookupsActionTypes,LookupsActions} from "../actions/look-up-values.action";
  import * as _ from 'lodash';
  import {NameValue} from "../shared/models/name-value";
  //import { lookup } from "dns";
  
  
  export interface LookupValueListState {
    lookupList:LookupValues[] | null;
  }
  export const initialState: LookupValueListState = {
    lookupList: [],
  };
  export function reducer(state = initialState, action: LookupsActions): LookupValueListState {    
    switch (action.type) {   
      case LookupsActionTypes.GET_LOOKUP_SUCCESS: { 
        let newState: LookupValues[] = [];
        if(action.payload.length>0)
        {                
          action.payload.map(lookupResult=>{          
            newState.push(lookupResult);                
          });
        }
        let lookupsdata= state.lookupList.map(
          oldLookup=>{
            let oldData=newState.find(lookup=>lookup.url==oldLookup.url);
            if(!(oldData))
            {
              newState.push(oldData);
            }
          }
        );
        return {lookupList:newState};
      }
      default: {
        return state;
      }
    }
  }

  export const getLookupList= (state: LookupValueListState) => state.lookupList;