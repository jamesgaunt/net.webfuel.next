import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { ConfigurationRoutingModule } from './configuration-routing.module';

import { ConfigurationMenuComponent } from './configuration-menu/configuration-menu.component';
import { CreateEmailTemplateDialog, CreateEmailTemplateDialogComponent } from './email-template/create-email-template/create-email-template.dialog';
import { EmailTemplateItemComponent } from './email-template/email-template-item/email-template-item.component';
import { EmailTemplateListComponent } from './email-template/email-template-list/email-template-list.component';
import { GlobalFileListComponent } from './global-files/global-file-list/global-file-list.component';
import {
  CreateTriageTemplateDialog,
  CreateTriageTemplateDialogComponent,
} from './triage-template/create-triage-template/create-triage-template.dialog';
import { TriageTemplateItemComponent } from './triage-template/triage-template-item/triage-template-item.component';
import { TriageTemplateListComponent } from './triage-template/triage-template-list/triage-template-list.component';

@NgModule({
  imports: [CommonModule, SharedModule, ConfigurationRoutingModule],
  declarations: [
    ConfigurationMenuComponent,

    EmailTemplateListComponent,
    EmailTemplateItemComponent,
    CreateEmailTemplateDialogComponent,

    TriageTemplateListComponent,
    TriageTemplateItemComponent,
    CreateTriageTemplateDialogComponent,

    GlobalFileListComponent,
  ],
  providers: [CreateEmailTemplateDialog, CreateTriageTemplateDialog],
})
export class ConfigurationModule {}
