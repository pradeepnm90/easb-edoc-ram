import { Component, HostListener } from '@angular/core';
import { Store} from '@ngrx/store';
import * as applicationStateActions from './actions/application-state.action';
import * as fromRoot from './store/index';
import { ApplicationState } from './shared/models/application-state';

@Component({
  selector: 'body',
  template: '<router-outlet ></router-outlet>'
})
export class AppComponent {
  constructor(private _store: Store<fromRoot.AppState>) {
  }

  @HostListener('window:keydown', ['$event'])
  keyDownEvent(event: KeyboardEvent) {
    console.log(event);
    if (event.keyCode == KEY_CODE.CTRL_KEY) {       
        this._store.dispatch(new applicationStateActions.UpdateMultiSelectionModeAction({applicationState: new ApplicationState({multiSelectionMode:true})}));
    }
  }

  @HostListener('window:keyup', ['$event'])
  keyUpEvent(event: KeyboardEvent) {
    console.log(event);
    if (event.keyCode == KEY_CODE.CTRL_KEY) {     
        this._store.dispatch(new applicationStateActions.UpdateMultiSelectionModeAction({applicationState: new ApplicationState({multiSelectionMode:false})}));
    }
  }
}

export enum KEY_CODE {
  CTRL_KEY = 17
}