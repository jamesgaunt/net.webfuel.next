import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Validate } from '../../../../../shared/common/validate';

export interface CompleteProjectSupportDialogData {
  projectSupport: ProjectSupport;
}

@Injectable()
export class CompleteProjectSupportDialog extends DialogBase<ProjectSupport, CompleteProjectSupportDialogData> {
  open(data: CompleteProjectSupportDialogData) {
    return this._open(CompleteProjectSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'complete-project-support-dialog',
  templateUrl: './complete-project-support.dialog.html'
})
export class CompleteProjectSupportDialogComponent extends DialogComponentBase<ProjectSupport, CompleteProjectSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectSupportApi: ProjectSupportApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue(this.data.projectSupport);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    supportRequestedCompletedNotes: new FormControl<string>('', { nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectSupportApi.complete(this.form.getRawValue(), { successGrowl: "Project Support Completed" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
