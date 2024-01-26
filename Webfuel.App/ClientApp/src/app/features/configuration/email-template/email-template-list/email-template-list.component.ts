import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { EmailTemplate, Project } from 'api/api.types';
import { ProjectStatusEnum } from '../../../../api/api.enums';
import { EmailTemplateApi } from '../../../../api/email-template.api';
import { CreateEmailTemplateDialog } from '../create-email-template/create-email-template.dialog';

@Component({
  selector: 'email-template-list',
  templateUrl: './email-template-list.component.html'
})
export class EmailTemplateListComponent {
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public emailTemplateApi: EmailTemplateApi,
    private createEmailTemplateDialog: CreateEmailTemplateDialog
  ) {
  }


  add() {
    this.createEmailTemplateDialog.open();
  }

  edit(emailTemplate: EmailTemplate) {
    this.router.navigate(['configuration/email-template-item', emailTemplate.id]);
  }
}
