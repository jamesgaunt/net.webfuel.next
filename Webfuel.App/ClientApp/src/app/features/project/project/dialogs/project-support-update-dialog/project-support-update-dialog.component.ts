import { DIALOG_DATA, DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectSupport } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { FormService } from 'core/form.service';
import { ProjectSupportApi } from 'api/project-support.api';
import { UserApi } from 'api/user.api';
import { StaticDataCache } from 'api/static-data.cache';
import { DialogService } from '../../../../../core/dialog.service';

export interface ProjectSupportUpdateDialogData {
  projectSupport: ProjectSupport;
}

@Injectable()
export class ProjectSupportUpdateDialogService {
  constructor(
    private dialogService: DialogService
  ) { }

  open(data: ProjectSupportUpdateDialogData) {
    return this.dialogService.openComponent<ProjectSupport, ProjectSupportUpdateDialogData>(ProjectSupportUpdateDialogComponent, data, {
      width: "1000px"
    });
  }
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
    @Inject(DIALOG_DATA) public data: ProjectSupportUpdateDialogData,
  ) {
    this.form.patchValue(data.projectSupport);
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
      this.dialogRef.close(result);
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
