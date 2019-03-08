import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppComponent } from './app.component';
import { LoginComponent } from './shared/login/login.component';
import { ShellContainerComponent } from './layout/shell.container';
import { SharedModule } from './shared/shared.module';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { metaReducer } from "./store/index";
import { CustomRouterStateSerializer } from './shared/utils';
import {
  StoreRouterConnectingModule,
  RouterStateSerializer,
} from '@ngrx/router-store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { MaterialModule } from '../app/shared/material/material.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { HttpExInterceptor } from './shared/intercepters/http-intercepter';
import { AuthGuard } from './shared/guards/auth.guards';
import { AppRoutingModule } from './app.routing';
import { AuthEffects } from '../app/effects/auth.effect';
import {ReactiveFormsModule, FormsModule} from "@angular/forms";
import { AuthenticationUserResolve } from './shared/services/authUser.resolve';
import { LookupValueEffects } from './effects/lookup-value.effect';
import { UserViewsEffects } from './effects/user-view.effects';
import { ExtendedSearchEffects } from './effects/extended-search.effect';
@NgModule({
  declarations: [
    AppComponent,
    ShellContainerComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,BrowserAnimationsModule,
    MaterialModule,
    SharedModule,
    HttpClientModule,
    StoreDevtoolsModule,
    StoreModule.forRoot({ reducer: metaReducer }),
    EffectsModule.forRoot([AuthEffects, LookupValueEffects, UserViewsEffects, ExtendedSearchEffects]),
    StoreRouterConnectingModule,
    StoreDevtoolsModule.instrument({ maxAge: 50 }),
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule, FormsModule
  ],
  providers: [
    {
      provide: RouterStateSerializer,
      useClass: CustomRouterStateSerializer
    },
    AuthGuard,
    AuthenticationUserResolve,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpExInterceptor,
      multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
