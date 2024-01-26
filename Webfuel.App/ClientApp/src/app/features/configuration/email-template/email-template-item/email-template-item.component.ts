import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmailTemplate } from 'api/api.types';
import { EmailTemplateApi } from 'api/email-template.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ReportService } from '../../../../core/report.service';
import { ConfirmDialog } from '../../../../shared/dialogs/confirm/confirm.dialog';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'email-template-item',
  templateUrl: './email-template-item.component.html'
})
export class EmailTemplateItemComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmDialog: ConfirmDialog,
    private confirmDeleteDialog: ConfirmDialog,
    private formService: FormService,
    public emailTemplateApi: EmailTemplateApi,
    public staticDataCache: StaticDataCache,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.emailTemplate);
  }

  // EmailTemplate

  item!: EmailTemplate;

  reset(item: EmailTemplate) {
    this.item = item;

    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    sendTo: new FormControl<string>('', { nonNullable: true }),
    sendCc: new FormControl<string>('', { nonNullable: true }),
    sendBcc: new FormControl<string>('', { nonNullable: true }),
    sentBy: new FormControl<string>('', { nonNullable: true }),
    replyTo: new FormControl<string>('', { nonNullable: true }),
    subject: new FormControl<string>('', { nonNullable: true }),
    htmlTemplate: new FormControl<string>('', { nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.emailTemplateApi.update(this.form.getRawValue(), { successGrowl: "Email Template Updated" }).subscribe((result) => {
      this.reset(result);
      if (close)
        this.router.navigate(['configuration/email-template-list']);
    });
  }
  
  cancel() {
    this.reset(this.item);
    this.router.navigate(['configuration/email-template-list']);
  }
}
