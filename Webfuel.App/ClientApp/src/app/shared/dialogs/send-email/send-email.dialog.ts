import { Component, inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SendEmailAttachment, SendEmailRequest } from 'api/api.types';
import { EmailApi } from 'api/email.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { AttachEmailFilesDialog } from '../attach-email-files/attach-email-files.dialog';

export interface SendEmailDialogData {
  autoSend: boolean;
  request: SendEmailRequest;
}

@Injectable()
export class SendEmailDialog extends DialogBase<SendEmailRequest, SendEmailDialogData> {
  open(data: SendEmailDialogData) {
    return this._open(SendEmailDialogComponent, data, { width: '1200px' });
  }
}

@Component({
  selector: 'send-email-dialog',
  templateUrl: './send-email.dialog.html',
})
export class SendEmailDialogComponent extends DialogComponentBase<SendEmailRequest, SendEmailDialogData> {
  attachEmailFilesDialog = inject(AttachEmailFilesDialog);

  constructor(private emailApi: EmailApi, private formService: FormService) {
    super();
    if (this.data) {
      this.form.patchValue(this.data.request);
      this.attachments = this.data.request.attachments || [];
    }
  }

  form = new FormGroup({
    subject: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    htmlBody: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    sendTo: new FormControl('', { validators: [Validators.required], nonNullable: true }),
    sendCc: new FormControl('', { nonNullable: true }),
  });

  attachments: SendEmailAttachment[] = [];

  cancel() {
    this._cancelDialog();
  }

  send() {
    if (this.formService.hasErrors(this.form)) return;

    this.data.request.subject = this.form.getRawValue().subject;
    this.data.request.htmlBody = this.form.getRawValue().htmlBody;
    this.data.request.sendTo = this.form.getRawValue().sendTo;
    this.data.request.sendCc = this.form.getRawValue().sendCc;
    this.data.request.attachments = this.attachments;

    if (this.data.autoSend === true) {
      this.emailApi.send(this.data.request, { successGrowl: 'Email Sent' }).subscribe((result) => {
        this._closeDialog(result);
      });
    } else {
      this._closeDialog(this.data.request);
    }
  }

  attachFiles() {
    this.attachEmailFilesDialog
      .open({
        localFileStorageGroupId: this.data.request.localFileStorageGroupId,
        globalFileStorageGroupId: this.data.request.globalFileStorageGroupId,
        attachments: this.attachments,
      })
      .subscribe((result) => {
        this.attachments = result.attachments;
      });
  }

  removeAttachment(file: SendEmailAttachment, $event: MouseEvent) {
    $event.preventDefault();
    $event.stopPropagation();

    var index = this.attachments.indexOf(file);
    if (index !== -1) {
      this.attachments.splice(index, 1);
    }
  }
}
