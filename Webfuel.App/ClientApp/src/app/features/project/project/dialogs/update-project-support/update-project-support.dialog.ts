import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

export interface UpdateProjectSupportDialogData {
  projectSupport: ProjectSupport;
}

@Injectable()
export class UpdateProjectSupportDialog extends DialogBase<ProjectSupport, UpdateProjectSupportDialogData> {
  open(data: UpdateProjectSupportDialogData) {
    return this._open(UpdateProjectSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'update-project-support-dialog',
  templateUrl: './update-project-support.dialog.html'
})
export class UpdateProjectSupportDialogComponent extends DialogComponentBase<ProjectSupport, UpdateProjectSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectSupportApi: ProjectSupportApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue(this.data.projectSupport);
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
      this.close(result);
    });
  }

  cancel() {
    this.cancel();
  }
}
