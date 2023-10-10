import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LoginComponent } from './login/login/login.component';
import { isAuthenticated } from './core/guards/identity.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
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
    path: 'user',
    loadChildren: () => import('./features/user/user.module').then(m => m.UserModule),
    canActivate: [isAuthenticated]
  },
  {
    path: 'static-data',
    loadChildren: () => import('./features/static-data/static-data.module').then(m => m.StaticDataModule),
    canActivate: [isAuthenticated]
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }


