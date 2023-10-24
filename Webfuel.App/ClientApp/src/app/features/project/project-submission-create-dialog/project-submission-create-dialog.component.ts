import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectSubmission } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { FormService } from '../../../core/form.service';
import { ProjectSubmissionApi } from '../../../api/project-submission.api';
import { UserApi } from '../../../api/user.api';
import { StaticDataCache } from '../../../api/static-data.cache';

export interface ProjectSubmissionCreateDialogOptions {
  projectId: string
}

@Component({
  selector: 'project-submission-create-dialog',
  templateUrl: './project-submission-create-dialog.component.html'
})
export class ProjectSubmissionCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<ProjectSubmission>,
    private formService: FormService,
    private projectSubmissionApi: ProjectSubmissionApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public options: ProjectSubmissionCreateDialogOptions,
  ) {
    this.form.patchValue({ projectId: options.projectId });
  }

  form = new FormGroup({
    projectId: new FormControl<string>('', { nonNullable: true }),
    submissionDate: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    nihrReference: new FormControl<string>('', { nonNullable: true }),
    submissionStageId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    submissionOutcomeId: new FormControl<string>(null!),
    fundingAmountOnSubmission: new FormControl<number>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.projectSubmissionApi.create(this.form.getRawValue(), { successGrowl: "Project Submission Added" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
