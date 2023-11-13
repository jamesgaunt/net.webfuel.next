import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectTeamSupport } from 'api/api.types';
import { ProjectTeamSupportApi } from 'api/project-team-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { SupportTeamApi } from '../../../../../api/support-team.api';

export interface UpdateProjectTeamSupportDialogData {
  item: ProjectTeamSupport
}

@Injectable()
export class UpdateProjectTeamSupportDialog extends DialogBase<ProjectTeamSupport, UpdateProjectTeamSupportDialogData> {
  open(data: UpdateProjectTeamSupportDialogData) {
    return this._open(UpdateProjectTeamSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'update-project-team-support-dialog',
  templateUrl: './update-project-team-support.dialog.html'
})
export class UpdateProjectTeamSupportDialogComponent extends DialogComponentBase<ProjectTeamSupport, UpdateProjectTeamSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectTeamSupportApi: ProjectTeamSupportApi,
    public userApi: UserApi,
    public supportTeamApi: SupportTeamApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue(this.data.item);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    createdNotes: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    completedNotes: new FormControl<string>('', {  nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectTeamSupportApi.update(this.form.getRawValue(), { successGrowl: "Team Support Updated" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
