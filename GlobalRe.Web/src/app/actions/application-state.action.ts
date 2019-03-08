import { Action } from '@ngrx/store';
import { ApplicationState } from '../shared/models/application-state';


export const ApplicationStateActionTypes = {
  UPDATE_MULTISELECTION_MODE: '[ApplicationState] Update', 
}

export class UpdateMultiSelectionModeAction implements Action {
  readonly type = ApplicationStateActionTypes.UPDATE_MULTISELECTION_MODE;
  constructor(public payload: { applicationState: ApplicationState }) { }
}


export type ApplicationStateActions =
  | UpdateMultiSelectionModeAction;