import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ConfigurationRoutingModule } from './configuration-routing.module';

import { ConfigurationMenuComponent } from './configuration-menu/configuration-menu.component';
import { EmailTemplateListComponent } from './email-template/email-template-list/email-template-list.component';
import { CreateEmailTemplateDialog, CreateEmailTemplateDialogComponent } from './email-template/create-email-template/create-email-template.dialog';
import { EmailTemplateItemComponent } from './email-template/email-template-item/email-template-item.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ConfigurationRoutingModule
  ],
  declarations: [
    ConfigurationMenuComponent,
    EmailTemplateListComponent,
    EmailTemplateItemComponent,

    CreateEmailTemplateDialogComponent
  ],
  providers: [
    CreateEmailTemplateDialog
  ]
})
export class ConfigurationModule { }
