import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupportFile } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';

export interface UploadProjectSupportFilesDialogData {
}

export interface UploadProjectSupportFilesDialogResult {
  uploadedFiles: File[];
}

@Injectable()
export class UploadProjectSupportFilesDialog extends DialogBase<UploadProjectSupportFilesDialogResult, UploadProjectSupportFilesDialogData> {
  open(init: UploadProjectSupportFilesDialogData) {
    return this._open(UploadProjectSupportFilesDialogComponent, init, { width: "800px" });
  }
}

@Component({
  selector: 'upload-project-support-files-dialog',
  templateUrl: './upload-project-support-files.dialog.html'
})
export class UploadProjectSupportFilesDialogComponent extends DialogComponentBase<UploadProjectSupportFilesDialogResult, UploadProjectSupportFilesDialogData> {

  constructor(
  ) {
    super();
  }

  form = new FormGroup({
    files: new FormControl<File[]>([], { nonNullable: true }),
  });

  save() {
    this._closeDialog({
      uploadedFiles: this.form.getRawValue().files
    });
  }

  cancel() {
    this._cancelDialog();
  }

  uploadFiles() {
  }

  formatSize(file: ProjectSupportFile) {
    return _.formatBytes(file.sizeBytes);
  }
}
