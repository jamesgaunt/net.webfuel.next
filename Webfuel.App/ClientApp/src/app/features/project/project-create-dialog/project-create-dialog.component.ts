import { DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormService } from '../../../core/form.service';
import { SelectDataSource } from '../../../shared/data-source/select-data-source';
import { ProjectApi } from '../../../api/project.api';
import { Project } from '../../../api/api.types';

@Component({
  templateUrl: './project-create-dialog.component.html'
})
export class ProjectCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<Project>,
    private formService: FormService,
    private projectApi: ProjectApi
  ) {
  }

  form = new FormGroup({
    title: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.projectApi.createProject(this.form.getRawValue(), { successGrowl: "Project Created" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
