import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login/login.component';
import { isAuthenticated } from './core/guards/identity.guard';
import { ForgottenPasswordComponent } from './login/forgotten-password/forgotten-password.component';
import { ResetPasswordComponent } from './login/reset-password/reset-password.component';
import { SupportRequestFormComponent } from './external/support-request-form.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'external/support-request',
    component: SupportRequestFormComponent,
    data: { chrome: false }
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { chrome: false }
  },
  {
    path: 'forgotten-password',
    component: ForgottenPasswordComponent,
    data: { chrome: false }
  },
  {
    path: 'reset-password/:uid/:tid',
    component: ResetPasswordComponent,
    data: { chrome: false }
  },
  {
    path: 'home',
    loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'developer',
    loadChildren: () => import('./features/developer/developer.module').then(m => m.DeveloperModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'configuration',
    loadChildren: () => import('./features/configuration/configuration.module').then(m => m.ConfigurationModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'user',
    loadChildren: () => import('./features/user/user.module').then(m => m.UserModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'project',
    loadChildren: () => import('./features/project/project.module').then(m => m.ProjectModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'support-request',
    loadChildren: () => import('./features/support-request/support-request.module').then(m => m.SupportRequestModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'static-data',
    loadChildren: () => import('./features/static-data/static-data.module').then(m => m.StaticDataModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'reporting',
    loadChildren: () => import('./features/reporting/reporting.module').then(m => m.ReportingModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'report',
    loadChildren: () => import('./features/report/report.module').then(m => m.ReportModule),
    canActivate: [isAuthenticated]
  },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


