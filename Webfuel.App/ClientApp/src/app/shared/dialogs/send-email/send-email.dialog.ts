import { Component, Injectable } from '@angular/core';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { SendEmailRequest } from 'api/api.types';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { EmailApi } from 'api/email.api';
import { FormService } from 'core/form.service';
import _ from 'shared/common/underscore';

export interface SendEmailDialogData {
  request: SendEmailRequest
}

@Injectable()
export class SendEmailDialog extends DialogBase<SendEmailRequest, SendEmailDialogData> {
  open(data: SendEmailDialogData) {
    return this._open(SendEmailDialogComponent, data, { width: '1200px' });
  }
}

@Component({
  selector: 'send-email-dialog',
  templateUrl: './send-email.dialog.html'
})
export class SendEmailDialogComponent extends DialogComponentBase<SendEmailRequest, SendEmailDialogData> {

  constructor(
    private emailApi: EmailApi,
    private formService: FormService
  ) {
    super();
    if (this.data)
      this.form.patchValue(this.data.request);
  }

  form = new FormGroup({
    subject: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    htmlBody: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    sendTo: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    sendCc: new FormControl('', { nonNullable: true }),
  });

  cancel() {
    this._cancelDialog();
  }

  send() {
    if (this.formService.hasErrors(this.form))
      return;

    this.data.request.subject = this.form.getRawValue().subject;
    this.data.request.htmlBody = this.form.getRawValue().htmlBody;
    this.data.request.sendTo = this.form.getRawValue().sendTo;
    this.data.request.sendCc = this.form.getRawValue().sendCc;

    this.emailApi.send(this.data.request, { successGrowl: "Email Sent" }).subscribe((result) => {
      this._cancelDialog();
    })
  }
}
