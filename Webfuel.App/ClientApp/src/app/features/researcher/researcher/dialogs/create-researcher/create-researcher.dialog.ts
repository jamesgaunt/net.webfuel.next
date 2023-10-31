import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Researcher } from 'api/api.types';
import { ResearcherApi } from 'api/researcher.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

@Injectable()
export class CreateResearcherDialog extends DialogBase<Researcher> {
  open() {
    return this._open(CreateResearcherDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-researcher-dialog',
  templateUrl: './create-researcher.dialog.html'
})
export class CreateResearcherDialogComponent extends DialogComponentBase<Researcher> {

  constructor(
    private formService: FormService,
    private researcherApi: ResearcherApi,
  ) {
    super();
  }

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.researcherApi.create(this.form.getRawValue(), { successGrowl: "Researcher Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
