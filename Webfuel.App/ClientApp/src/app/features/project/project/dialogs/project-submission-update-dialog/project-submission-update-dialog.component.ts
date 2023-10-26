import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectSubmission } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { FormService } from 'core/form.service';
import { ProjectSubmissionApi } from 'api/project-submission.api';
import { UserApi } from 'api/user.api';
import { StaticDataCache } from 'api/static-data.cache';
import { DialogService } from '../../../../../core/dialog.service';

export interface ProjectSubmissionUpdateDialogData {
  projectSubmission: ProjectSubmission;
}

@Injectable()
export class  ProjectSubmissionUpdateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: ProjectSubmissionUpdateDialogData) {
    return this.dialogService.openComponent<ProjectSubmission, ProjectSubmissionUpdateDialogData>(ProjectSubmissionUpdateDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'project-submission-update-dialog',
  templateUrl: './project-submission-update-dialog.component.html'
})
export class ProjectSubmissionUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<ProjectSubmission>,
    private formService: FormService,
    private projectSubmissionApi: ProjectSubmissionApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public data: ProjectSubmissionUpdateDialogData,
  ) {
    this.form.patchValue(data.projectSubmission);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    submissionDate: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    nihrReference: new FormControl<string>('', { nonNullable: true }),
    submissionStageId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    submissionOutcomeId: new FormControl<string>(null!),
    fundingAmountOnSubmission: new FormControl<number>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.projectSubmissionApi.update(this.form.getRawValue(), { successGrowl: "Project Submission Updated" }).subscribe((result) => {
      this.dialogRef.close(result);
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
