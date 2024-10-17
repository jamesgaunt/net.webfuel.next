import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigurationMenuComponent } from './configuration-menu/configuration-menu.component';
import { EmailTemplateListComponent } from './email-template/email-template-list/email-template-list.component';
import { EmailTemplateItemComponent } from './email-template/email-template-item/email-template-item.component';
import { TriageTemplateItemComponent } from './triage-template/triage-template-item/triage-template-item.component';
import { TriageTemplateListComponent } from './triage-template/triage-template-list/triage-template-list.component';

import { EmailTemplateApi } from '../../api/email-template.api';
import { DeactivateService } from '../../core/deactivate.service';
import { TriageTemplateApi } from 'api/triage-template.api';

const routes: Routes = [
  {
    path: 'configuration-menu',
    component: ConfigurationMenuComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'email-template-list',
    component: EmailTemplateListComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'email-template-item/:id',
    component: EmailTemplateItemComponent,
    data: { activeSideMenu: 'Configuration' },
    resolve: { emailTemplate: EmailTemplateApi.emailTemplateResolver('id') },
    canDeactivate: [DeactivateService.isPristine<EmailTemplateItemComponent>()],
  },
  {
    path: 'triage-template-list',
    component: TriageTemplateListComponent,
    data: { activeSideMenu: 'Configuration' }
  },
  {
    path: 'triage-template-item/:id',
    component: TriageTemplateItemComponent,
    data: { activeSideMenu: 'Configuration' },
    resolve: { triageTemplate: TriageTemplateApi.triageTemplateResolver('id') },
    canDeactivate: [DeactivateService.isPristine<TriageTemplateItemComponent>()],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
