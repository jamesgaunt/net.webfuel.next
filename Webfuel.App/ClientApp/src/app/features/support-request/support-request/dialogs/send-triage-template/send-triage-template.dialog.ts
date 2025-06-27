import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { TriageTemplateApi } from 'api/triage-template.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { SendEmailDialog } from 'shared/dialogs/send-email/send-email.dialog';

export interface SendTriageTemplateDialogData {
  supportRequest: SupportRequest;
}

@Injectable()
export class SendTriageTemplateDialog extends DialogBase<SupportRequest, SendTriageTemplateDialogData> {
  open(data: SendTriageTemplateDialogData) {
    return this._open(SendTriageTemplateDialogComponent, data, {
      width: '800px',
    });
  }
}

@Component({
  selector: 'send-triage-template-dialog',
  templateUrl: './send-triage-template.dialog.html',
})
export class SendTriageTemplateDialogComponent extends DialogComponentBase<SupportRequest, SendTriageTemplateDialogData> {
  constructor(
    private router: Router,
    private formService: FormService,
    private sendEmailDialog: SendEmailDialog,
    public triageTemplateApi: TriageTemplateApi
  ) {
    super();
  }

  form = new FormGroup({
    triageTemplateId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form)) return;

    this.triageTemplateApi
      .generateEmail({
        triageTemplateId: this.form.getRawValue().triageTemplateId,
        supportRequestId: this.data.supportRequest.id,
      })
      .subscribe((result) => {
        this.sendEmailDialog.open({ autoSend: false, request: result }).subscribe((request) => {
          this.triageTemplateApi
            .sendEmail({ supportRequestId: this.data.supportRequest.id, sendEmailRequest: request }, { successGrowl: 'Triage Email Sent' })
            .subscribe(() => {});
        });
        this._cancelDialog();
      });
  }

  cancel() {
    this._cancelDialog();
  }
}
