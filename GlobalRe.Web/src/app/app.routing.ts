import { NgModule } from '@angular/core';
import { Routes, RouterModule, Resolve } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guards';
//import { ImpersonationGuard } from './shared/guards/impersonation.guards';
import { ShellContainerComponent } from './layout/shell.container';
import { LoginComponent } from './shared/login/login.component';
import { AuthenticationUserResolve } from './shared/services/authUser.resolve';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'    
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    component: ShellContainerComponent,     
    children: [
      {
        path: 'dashboard',
        loadChildren: './features/dashboard/dashboard.module#DashboardModule',
        canLoad:[AuthGuard],
        resolve: {viewlist: AuthenticationUserResolve}   
      }
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
