import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../../../shared/common/dialog-base';
import { TriageTemplate } from '../../../../api/api.types';
import { TriageTemplateApi } from '../../../../api/triage-template.api';

@Injectable()
export class CreateTriageTemplateDialog extends DialogBase<TriageTemplate> {
  open() {
    return this._open(CreateTriageTemplateDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-triage-template-dialog',
  templateUrl: './create-triage-template.dialog.html'
})
export class CreateTriageTemplateDialogComponent extends DialogComponentBase<TriageTemplate> {

  constructor(
    private formService: FormService,
    private triageTemplateApi: TriageTemplateApi
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.triageTemplateApi.create(this.form.getRawValue()).subscribe((result) => {
      this._closeDialog(result);
    })
  }

  cancel() {
    this._cancelDialog();
  }

}
