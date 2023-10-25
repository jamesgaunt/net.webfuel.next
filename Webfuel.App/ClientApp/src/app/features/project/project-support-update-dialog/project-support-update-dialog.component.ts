import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectSupport } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { FormService } from '../../../core/form.service';
import { ProjectSupportApi } from '../../../api/project-support.api';
import { UserApi } from '../../../api/user.api';
import { StaticDataCache } from '../../../api/static-data.cache';

export interface ProjectSupportUpdateDialogOptions {
  projectSupport: ProjectSupport;
}

@Component({
  selector: 'project-support-update-dialog',
  templateUrl: './project-support-update-dialog.component.html'
})
export class ProjectSupportUpdateDialogComponent {

  constructor(
    private dialogRef: DialogRef<ProjectSupport>,
    private formService: FormService,
    private projectSupportApi: ProjectSupportApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
    @Inject(DIALOG_DATA) public options: ProjectSupportUpdateDialogOptions,
  ) {
    this.form.patchValue(options.projectSupport);
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    adviserIds: new FormControl<string[]>([], { nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { nonNullable: true }),
    description: new FormControl<string>('', { nonNullable: true })
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.projectSupportApi.update(this.form.getRawValue(), { successGrowl: "Project Support Updated" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
