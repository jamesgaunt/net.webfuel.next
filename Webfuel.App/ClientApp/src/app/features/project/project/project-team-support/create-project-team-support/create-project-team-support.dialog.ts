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

export interface CreateProjectTeamSupportDialogData {
  projectId: string
}

@Injectable()
export class CreateProjectTeamSupportDialog extends DialogBase<ProjectTeamSupport, CreateProjectTeamSupportDialogData> {
  open(data: CreateProjectTeamSupportDialogData) {
    return this._open(CreateProjectTeamSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'create-project-team-support-dialog',
  templateUrl: './create-project-team-support.dialog.html'
})
export class CreateProjectTeamSupportDialogComponent extends DialogComponentBase<ProjectTeamSupport, CreateProjectTeamSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectTeamSupportApi: ProjectTeamSupportApi,
    public userApi: UserApi,
    public supportTeamApi: SupportTeamApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ projectId: this.data.projectId });
  }

  minArrayLength(min: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (_.isArray(control.value) && control.value.length >= min)
        return null;
      return { minArrayLength: true };
    };
  }

  form = new FormGroup({
    projectId: new FormControl<string>('', { nonNullable: true }),
    supportTeamId: new FormControl<string>('', { nonNullable: true }),
    notes: new FormControl<string>('', { validators: [Validators.required], nonNullable: true })
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectTeamSupportApi.create(this.form.getRawValue(), { successGrowl: "Team Support Requested" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
