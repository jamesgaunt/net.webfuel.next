import { Component, Injectable, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport, ProjectSupportFile } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import { Validate } from '../../../../../shared/common/validate';
import { IsPrePostAwardEnum } from '../../../../../api/api.enums';
import { CompleteProjectSupportDialog } from '../complete-project-support/complete-project-support.dialog';
import { AttachProjectSupportFilesDialog } from 'shared/dialogs/attach-project-support-files/attach-project-support-files.dialog';
import { ProjectSupportDialogComponentBase } from '../common/ProjectSupportDialogComponentBase';
import { ConfigurationService } from 'core/configuration.service';

export interface CreateProjectSupportDialogData {
  projectId: string
}

@Injectable()
export class CreateProjectSupportDialog extends DialogBase<ProjectSupport, CreateProjectSupportDialogData> {
  open(data: CreateProjectSupportDialogData) {
    return this._open(CreateProjectSupportDialogComponent, data, {
      width: "1000px",
      disableClose: true
    });
  }
}

@Component({
  selector: 'create-project-support-dialog',
  templateUrl: './create-project-support.dialog.html'
})
export class CreateProjectSupportDialogComponent extends ProjectSupportDialogComponentBase<ProjectSupport, CreateProjectSupportDialogData> {

  constructor(
    private formService: FormService,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
    public configurationService: ConfigurationService
  ) {
    super();
    this.form.patchValue({ projectId: this.data.projectId });

    this.configurationService.configuration.subscribe((config) => {
      this.form.patchValue({
        adviserIds: config?.userId == null ? [] : [config.userId],
      });
    });

  }

  form = new FormGroup({
    projectId: new FormControl<string>('', { nonNullable: true }),
    date: new FormControl<string | null>(null),
    teamIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    adviserIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    supportProvidedIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }),
    description: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    workTimeInHours: new FormControl<number>(null!, { validators: [Validators.required, Validators.min(0), Validators.max(8)], nonNullable: true }),
    supportRequestedTeamId: new FormControl<string | null>(null),
    isPrePostAwardId: new FormControl<string>(IsPrePostAwardEnum.PreAward, { nonNullable: true }),

    files: new FormControl<ProjectSupportFile[]>([], { nonNullable: true }),
  });

  save() {
    if (this.submitting)
      return;
    this.submitting = true;

    if (this.form.valid === false) {
      this.deferredSubmitFiles();
    } else {
      this.submitFiles();
    }
  }

  deferredSubmitFiles() {
    // Sometimes there is a small delay in TinyMCE updating the validity of the form so we wait a second before trying again
    setTimeout(() => {
      if (this.formService.hasErrors(this.form)) {
        this.submitting = false;
        return;
      }
      this.submitFiles();
    }, 250);
  }

  cancel() {
    this._cancelDialog();
  }

  submitForm() {
    this.form.patchValue({ files: this.existingFiles });
    this.projectSupportApi.create(this.form.getRawValue(), { successGrowl: "Project Support Added" }).subscribe((result) => {
      this.submitting = false;
      this._closeDialog(result);
    },
    (error) => {
      this.submitting = false;
    });
  }
}
