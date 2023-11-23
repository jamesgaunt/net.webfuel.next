import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectTeamSupport } from 'api/api.types';
import { ProjectTeamSupportApi } from 'api/project-team-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
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

  form = new FormGroup({
    projectId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    supportTeamId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    createdNotes: new FormControl<string>('', { validators: [Validators.required], nonNullable: true })
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
