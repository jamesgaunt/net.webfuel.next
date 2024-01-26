import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../../../shared/common/dialog-base';
import { EmailTemplate } from '../../../../api/api.types';
import { EmailTemplateApi } from '../../../../api/email-template.api';

@Injectable()
export class CreateEmailTemplateDialog extends DialogBase<EmailTemplate> {
  open() {
    return this._open(CreateEmailTemplateDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-email-template-dialog',
  templateUrl: './create-email-template.dialog.html'
})
export class CreateEmailTemplateDialogComponent extends DialogComponentBase<EmailTemplate> {

  constructor(
    private formService: FormService,
    private emailTemplateApi: EmailTemplateApi
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.emailTemplateApi.create(this.form.getRawValue()).subscribe((result) => {
      this._closeDialog(result);
    })
  }

  cancel() {
    this._cancelDialog();
  }

}
