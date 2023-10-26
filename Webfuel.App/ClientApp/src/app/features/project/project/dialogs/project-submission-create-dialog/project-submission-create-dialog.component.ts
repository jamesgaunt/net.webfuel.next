import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSubmission } from 'api/api.types';
import { ProjectSubmissionApi } from 'api/project-submission.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogService } from '../../../../../core/dialog.service';

export interface ProjectSubmissionCreateDialogData {
  projectId: string
}

@Injectable()
export class ProjectSubmissionCreateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: ProjectSubmissionCreateDialogData) {
    return this.dialogService.openComponent<boolean, ProjectSubmissionCreateDialogData>(ProjectSubmissionCreateDialogComponent, data, {
      width: "1000px"
    });
  }
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
    @Inject(DIALOG_DATA) public data: ProjectSubmissionCreateDialogData,
  ) {
    this.form.patchValue({ projectId: data.projectId });
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
      this.dialogRef.close(result);
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
