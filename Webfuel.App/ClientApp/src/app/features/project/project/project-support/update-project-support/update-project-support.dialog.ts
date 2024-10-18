import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport, ProjectSupportFile } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Validate } from '../../../../../shared/common/validate';
import { IsPrePostAwardEnum } from '../../../../../api/api.enums';
import { AttachProjectSupportFilesDialog } from 'shared/dialogs/attach-project-support-files/attach-project-support-files.dialog';
import { ProjectSupportDialogComponentBase } from '../common/ProjectSupportDialogComponentBase';

export interface UpdateProjectSupportDialogData {
  projectId: string;
  projectSupport: ProjectSupport;
  requestTeamSupport?: boolean;
}

@Injectable()
export class UpdateProjectSupportDialog extends DialogBase<ProjectSupport, UpdateProjectSupportDialogData> {
  open(data: UpdateProjectSupportDialogData) {
    return this._open(UpdateProjectSupportDialogComponent, data, {
      width: "1000px",
      disableClose: true
    });
  }
}

@Component({
  selector: 'update-project-support-dialog',
  templateUrl: './update-project-support.dialog.html'
})
export class UpdateProjectSupportDialogComponent extends ProjectSupportDialogComponentBase<ProjectSupport, UpdateProjectSupportDialogData> {

  constructor(
    private formService: FormService,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.form.patchValue(this.data.projectSupport);
    this.existingFiles = this.data.projectSupport.files;
  }

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    teamIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    adviserIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { nonNullable: true }),
    description: new FormControl<string>('', { nonNullable: true }),
    workTimeInHours: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(8)], nonNullable: true }),
    supportRequestedTeamId: new FormControl<string | null>(null),
    isPrePostAwardId: new FormControl<string>(IsPrePostAwardEnum.PreAward, { nonNullable: true }),

    files: new FormControl<ProjectSupportFile[]>([], { nonNullable: true }),
  });

  save() {
    if (this.submitting || this.formService.hasErrors(this.form))
      return;

    this.submitting = true;
    this.submitFiles();
  }

  cancel() {
    this._cancelDialog();
  }

  submitForm() {
    this.form.patchValue({ files: this.existingFiles });
    this.projectSupportApi.update(this.form.getRawValue(), { successGrowl: "Project Support Updated" }).subscribe((result) => {
      this.submitting = false;
      this._closeDialog(result);
    });
  }
}
