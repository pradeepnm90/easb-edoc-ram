import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardContainer } from './dashboard.container';
import { AuthGuard } from '../../shared/guards/auth.guards';


const routes: Routes = [
  {
    path: '',
    component: DashboardContainer,
    canActivate: [AuthGuard],
    resolve:{}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule {}
export const routingComponents = [DashboardContainer];
