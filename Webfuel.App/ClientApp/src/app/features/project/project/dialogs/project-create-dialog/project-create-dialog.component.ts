import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { FormService } from 'core/form.service';
import { DialogService } from 'core/dialog.service';

@Injectable()
export class ProjectCreateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open() {
    return this.dialogService.openComponent<boolean>(ProjectCreateDialogComponent);
  }
}

@Component({
  templateUrl: './project-create-dialog.component.html'
})
export class ProjectCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<boolean>,
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

    this.projectApi.create(this.form.getRawValue(), { successGrowl: "Project Created" }).subscribe((result) => {
      this.dialogRef.close(true);
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
