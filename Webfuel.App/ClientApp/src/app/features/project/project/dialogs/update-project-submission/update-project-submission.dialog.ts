import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSubmission } from 'api/api.types';
import { ProjectSubmissionApi } from 'api/project-submission.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

export interface UpdateProjectSubmissionDialogData {
  projectSubmission: ProjectSubmission;
}

@Injectable()
export class UpdateProjectSubmissionDialog extends DialogBase<ProjectSubmission, UpdateProjectSubmissionDialogData> {
  open(data: UpdateProjectSubmissionDialogData) {
    return this._open(UpdateProjectSubmissionDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'update-project-submission-dialog',
  templateUrl: './update-project-submission.dialog.html'
})
export class UpdateProjectSubmissionDialogComponent extends DialogComponentBase<ProjectSubmission, UpdateProjectSubmissionDialogData> {

  constructor(
    private formService: FormService,
    private projectSubmissionApi: ProjectSubmissionApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue(this.data.projectSubmission);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    submissionDate: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    nihrReference: new FormControl<string>('', { nonNullable: true }),
    submissionStageId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    submissionOutcomeId: new FormControl<string>(null!),
    fundingAmountOnSubmission: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(999999999)], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectSubmissionApi.update(this.form.getRawValue(), { successGrowl: "Project Submission Updated" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
