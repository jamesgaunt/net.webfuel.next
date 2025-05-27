import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Validate } from '../../../../../shared/common/validate';
import { IsPrePostAwardEnum } from '../../../../../api/api.enums';

export interface UpdateProjectSupportCompletionDialogData {
  projectSupport: ProjectSupport;
}

@Injectable()
export class UpdateProjectSupportCompletionDialog extends DialogBase<ProjectSupport, UpdateProjectSupportCompletionDialogData> {
  open(data: UpdateProjectSupportCompletionDialogData) {
    return this._open(UpdateProjectSupportCompletionDialogComponent, data, {
      width: "1000px",
      disableClose: true
    });
  }
}

@Component({
  selector: 'update-project-support-completion-dialog',
  templateUrl: './update-project-support-completion.dialog.html'
})
export class UpdateProjectSupportCompletionDialogComponent extends DialogComponentBase<ProjectSupport, UpdateProjectSupportCompletionDialogData> {

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
    supportRequestedCompletedDate: new FormControl<string | null>(null),
    supportRequestedCompletedNotes: new FormControl<string>('', { nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectSupportApi.updateCompletion(this.form.getRawValue(), { successGrowl: "Project Support Updated" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
