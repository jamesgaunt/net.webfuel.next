import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SupportTeamApi } from 'api/support-team.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { SupportTeam } from '../../../../../api/api.types';

@Injectable()
export class CreateSupportTeamDialog extends DialogBase<SupportTeam> {
  open() {
    return this._open(CreateSupportTeamDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-support-team-dialog',
  templateUrl: './create-support-team.dialog.html'
})
export class CreateSupportTeamDialogComponent extends DialogComponentBase<SupportTeam> {

  constructor(
    private formService: FormService,
    private supportTeamApi: SupportTeamApi,
  ) {
    super();
  }

  form = new FormGroup({
    name: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.supportTeamApi.create(this.form.getRawValue(), { successGrowl: "Support Team Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
