import { Component, Injectable } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ProjectTeamSupport } from 'api/api.types';
import { ProjectTeamSupportApi } from 'api/project-team-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
import { SupportTeamApi } from '../../../../../api/support-team.api';

export interface CompleteProjectTeamSupportDialogData {
  id: string
}

@Injectable()
export class CompleteProjectTeamSupportDialog extends DialogBase<ProjectTeamSupport, CompleteProjectTeamSupportDialogData> {
  open(data: CompleteProjectTeamSupportDialogData) {
    return this._open(CompleteProjectTeamSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'complete-project-team-support-dialog',
  templateUrl: './complete-project-team-support.dialog.html'
})
export class CompleteProjectTeamSupportDialogComponent extends DialogComponentBase<ProjectTeamSupport, CompleteProjectTeamSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectTeamSupportApi: ProjectTeamSupportApi,
    public userApi: UserApi,
    public supportTeamApi: SupportTeamApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ id: this.data.id });
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    completedNotes: new FormControl<string>('', { nonNullable: true })
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectTeamSupportApi.complete(this.form.getRawValue(), { successGrowl: "Team Support Completed" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
