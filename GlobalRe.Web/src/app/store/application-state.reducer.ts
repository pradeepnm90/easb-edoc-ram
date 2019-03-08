import { ApplicationStateActions, ApplicationStateActionTypes} from '../actions/application-state.action';
import { ApplicationState } from '../shared/models/application-state';

export interface AppState {
    applicationState: ApplicationState;
}

export const initialState: AppState = {
    applicationState: {
        multiSelectionMode: false
      },
};

export function reducer(state = initialState, action: ApplicationStateActions): AppState {
  switch (action.type) {     
    case ApplicationStateActionTypes.UPDATE_MULTISELECTION_MODE: {
     
        return {
          ...state,          
          applicationState: action['payload']['applicationState']
        };
      } 
    default: {
      return state;
    }
  }
}
export const getApplicationState= (state: AppState) => state.applicationState;
//export const isMultiSelectMode = (state: AppState) => state.applicationState.multiSelectionMode;
