import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

@Injectable()
export class CreateProjectDialog extends DialogBase<Project> {
  open() {
    return this._open(CreateProjectDialogComponent, undefined);
  }
}

@Component({
  selector: 'create-project-dialog',
  templateUrl: './create-project.dialog.html'
})
export class CreateProjectDialogComponent extends DialogComponentBase<Project> {

  constructor(
    private formService: FormService,
    private projectApi: ProjectApi
  ) {
    super();
  }

  form = new FormGroup({
    title: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;
    this.projectApi.create(this.form.getRawValue(), { successGrowl: "Project Created" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
