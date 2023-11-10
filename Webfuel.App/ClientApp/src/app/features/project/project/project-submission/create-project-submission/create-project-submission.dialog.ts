import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSubmission } from 'api/api.types';
import { ProjectSubmissionApi } from 'api/project-submission.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

export interface CreateProjectSubmissionDialogData {
  projectId: string
}

@Injectable()
export class CreateProjectSubmissionDialog extends DialogBase<ProjectSubmission, CreateProjectSubmissionDialogData> {
  open(data: CreateProjectSubmissionDialogData) {
    return this._open(CreateProjectSubmissionDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'create-project-submission-dialog',
  templateUrl: './create-project-submission.dialog.html'
})
export class CreateProjectSubmissionDialogComponent extends DialogComponentBase<ProjectSubmission, CreateProjectSubmissionDialogData> {

  constructor(
    private formService: FormService,
    private projectSubmissionApi: ProjectSubmissionApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ projectId: this.data.projectId });
  }

  form = new FormGroup({
    projectId: new FormControl<string>('', { nonNullable: true }),
    submissionDate: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    nihrReference: new FormControl<string>('', { nonNullable: true }),
    submissionStageId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    submissionOutcomeId: new FormControl<string>(null!),
    fundingAmountOnSubmission: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(999999999)], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectSubmissionApi.create(this.form.getRawValue(), { successGrowl: "Project Submission Added" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
