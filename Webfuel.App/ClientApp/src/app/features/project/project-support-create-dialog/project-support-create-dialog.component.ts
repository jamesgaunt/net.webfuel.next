import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectSupport } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { FormService } from '../../../core/form.service';
import { ProjectSupportApi } from '../../../api/project-support.api';
import { UserApi } from '../../../api/user.api';
import { StaticDataCache } from '../../../api/static-data.cache';

export interface ProjectSupportCreateDialogOptions {
  projectId: string
}

@Component({
  selector: 'project-support-create-dialog',
  templateUrl: './project-support-create-dialog.component.html'
})
export class ProjectSupportCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<ProjectSupport>,
    private formService: FormService,
    private projectSupportApi: ProjectSupportApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public options: ProjectSupportCreateDialogOptions,
  ) {
    this.form.patchValue({ projectId: options.projectId });
  }

  form = new FormGroup({
    projectId: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string | null>(null),
    adviserIds: new FormControl<string[]>([], { nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { nonNullable: true }),
    description: new FormControl<string>('', { nonNullable: true })
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.projectSupportApi.create(this.form.getRawValue(), { successGrowl: "Project Support Added" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
