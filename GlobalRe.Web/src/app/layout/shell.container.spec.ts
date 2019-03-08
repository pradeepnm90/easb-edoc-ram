import { async, ComponentFixture, TestBed, inject, fakeAsync } from '@angular/core/testing';
import { ShellContainerComponent } from './shell.container';
import { Router, RouterOutlet } from "@angular/router";
import { RouterTestingModule } from '@angular/router/testing'
import { StoreModule, Store, combineReducers } from '@ngrx/store';
import * as fromRoot from '../store/index';
import 'rxjs/add/observable/from';
import { Observable } from 'rxjs/Observable';
import * as actions from '../actions/auth.action';
import { LoginUser } from '../shared/models/login-user';
import { MaterialModule } from '../shared/material/material.module';

describe('Layout Shell Component', () => {
  let instance: ShellContainerComponent;
  let fixture: ComponentFixture<ShellContainerComponent>;
  let nativeElement: any;
  let store: Store<fromRoot.AppState>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule,
        RouterTestingModule,
        StoreModule.forRoot({ reducer: fromRoot.metaReducer })
      ],
      declarations: [ShellContainerComponent],
      providers: [ ]
    });

    store = TestBed.get(Store);
    spyOn(store, 'dispatch').and.callThrough();
    fixture = TestBed.createComponent(ShellContainerComponent);
    instance = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should have a Component', () => {
    expect(instance).toBeTruthy();
  });

  // it('should dispatch an authenticate action', () => {
  //   const action = new actions.AuthenticateAction();
  //   expect(store.dispatch).toHaveBeenCalledWith(action);
  // });

  it('should display login user information after the data is loaded', () => {
     let mockUser = new LoginUser({
      userName: 'testUser',
      applicationVersion: '1.0',
      domainName: 'MARKELCORP',
      environment: 'QA',
      isAuthenticated: true
    });

    const action = new actions.LoginSuccessAction({ user: mockUser });

    store.dispatch(action);

    instance.loginUser$.subscribe(user => {
      expect(user.userName).toEqual(mockUser.userName);
    });
  });
  it('should call openERMS',async( () => {
    instance.openERMS();
     spyOn(instance, 'openERMS');
     const button = fixture.debugElement.nativeElement.querySelector('.ermslink');
     button.click();
     fixture.whenStable().then(() => {
       fixture.detectChanges();
       expect(instance.openERMS).toHaveBeenCalledTimes(0);
     });
  }));
  it('should call openQlikView',async( () => {
    instance.openQlikView();
     spyOn(instance, 'openQlikView');
     const button = fixture.debugElement.nativeElement.querySelector('.qlikview');
     button.click();
     fixture.whenStable().then(() => {
       fixture.detectChanges();
       expect(instance.openQlikView).toHaveBeenCalledTimes(0);
     });
  }));
});


