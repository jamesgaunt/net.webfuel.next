import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Validate } from '../../../../../shared/common/validate';

export interface CreateProjectSupportDialogData {
  projectId: string
}

@Injectable()
export class CreateProjectSupportDialog extends DialogBase<ProjectSupport, CreateProjectSupportDialogData> {
  open(data: CreateProjectSupportDialogData) {
    return this._open(CreateProjectSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'create-project-support-dialog',
  templateUrl: './create-project-support.dialog.html'
})
export class CreateProjectSupportDialogComponent extends DialogComponentBase<ProjectSupport, CreateProjectSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectSupportApi: ProjectSupportApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue({ projectId: this.data.projectId });
  }


  form = new FormGroup({
    projectId: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string | null>(null),
    teamIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    adviserIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true })
  });

  save() {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectSupportApi.create(this.form.getRawValue(), { successGrowl: "Project Support Added" }).subscribe((result) => {
      this._closeDialog(result);
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
