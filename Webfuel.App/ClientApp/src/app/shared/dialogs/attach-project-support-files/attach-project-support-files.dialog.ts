import { DialogRef } from '@angular/cdk/dialog';
import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupportFile } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';

export interface AttachProjectSupportFilesDialogData {
  databaseFiles: ProjectSupportFile[],
  existingFiles: ProjectSupportFile[],
}

export interface AttachProjectSupportFilesDialogResult {
  existingFiles: ProjectSupportFile[];
}

@Injectable()
export class AttachProjectSupportFilesDialog extends DialogBase<AttachProjectSupportFilesDialogResult, AttachProjectSupportFilesDialogData> {
  open(init: AttachProjectSupportFilesDialogData) {
    return this._open(AttachProjectSupportFilesDialogComponent, init, { width: "800px" });
  }
}

@Component({
  selector: 'attach-project-support-files-dialog',
  templateUrl: './attach-project-support-files.dialog.html'
})
export class AttachProjectSupportFilesDialogComponent extends DialogComponentBase<AttachProjectSupportFilesDialogResult, AttachProjectSupportFilesDialogData> {

  constructor(
  ) {
    super();
    this.existingFiles = _.deepClone(this.data.existingFiles);
  }

  form = new FormGroup({
  });

  save() {
    this._closeDialog({
      existingFiles: this.existingFiles,
    });
  }

  existingFiles: ProjectSupportFile[] = [];

  isExistingFile(file: ProjectSupportFile) {
    return this.existingFiles.some(f => f.id === file.id);
  }

  toggleExistingFile(file: ProjectSupportFile) {
    if (this.isExistingFile(file)) {
      this.existingFiles = _.remove(this.existingFiles, f => f.id === file.id);
    } else {
      this.existingFiles.push(file);
    }
  }

  iconClass(file: ProjectSupportFile) {
    if(this.isExistingFile(file)) {
      return "far fa-check-square";
    }
    return "far fa-square";
  }

  cancel() {
    this._cancelDialog();
  }

  uploadFiles() {
    this._closeDialog({ existingFiles: this.existingFiles })
  }

  formatSize(file: ProjectSupportFile) {
    return _.formatBytes(file.sizeBytes);
  }
}
