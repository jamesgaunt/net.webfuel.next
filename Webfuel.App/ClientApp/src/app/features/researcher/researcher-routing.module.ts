import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ResearcherListComponent } from './researcher/researcher-list/researcher-list.component';
import { ResearcherItemComponent } from './researcher/researcher-item/researcher-item.component';

import { ResearcherApi } from '../../api/researcher.api';
import { DeactivateService } from '../../core/deactivate.service';

const routes: Routes = [
  {
    path: 'researcher-list',
    component: ResearcherListComponent,
    data: { activeSideMenu: 'Researchers' }
  },
  {
    path: 'researcher-item/:id',
    component: ResearcherItemComponent,
    resolve: { researcher: ResearcherApi.researcherResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ResearcherItemComponent>()],
    data: { activeSideMenu: 'Researchers' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ResearcherRoutingModule { }
