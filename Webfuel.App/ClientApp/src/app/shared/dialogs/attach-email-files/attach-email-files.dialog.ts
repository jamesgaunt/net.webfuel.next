import { Component, inject, Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FileStorageEntry, ProjectSupportFile, SendEmailAttachment } from 'api/api.types';
import { FileStorageEntryApi } from 'api/file-storage-entry.api';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';

export interface AttachEmailFilesDialogData {
  localFileStorageGroupId: string | null;
  globalFileStorageGroupId: string | null;

  attachments: SendEmailAttachment[];
}

export interface AttachEmailFilesDialogResult {
  attachments: SendEmailAttachment[];
}

@Injectable()
export class AttachEmailFilesDialog extends DialogBase<AttachEmailFilesDialogResult, AttachEmailFilesDialogData> {
  open(init: AttachEmailFilesDialogData) {
    return this._open(AttachEmailFilesDialogComponent, init, { width: '800px' });
  }
}

@Component({
  selector: 'attach-email-files-dialog',
  templateUrl: './attach-email-files.dialog.html',
})
export class AttachEmailFilesDialogComponent extends DialogComponentBase<AttachEmailFilesDialogResult, AttachEmailFilesDialogData> {
  fileStorageEntryApi = inject(FileStorageEntryApi);

  constructor() {
    super();

    this.attachments = _.deepClone(this.data.attachments) || [];

    if (this.data.localFileStorageGroupId !== null) {
      this.fileStorageEntryApi
        .query({ fileStorageGroupId: this.data.localFileStorageGroupId, skip: 0, take: 1000, fileTagId: null })
        .subscribe((result) => {
          this.localFiles = result.items;
        });
    }

    if (this.data.globalFileStorageGroupId !== null) {
      this.fileStorageEntryApi
        .query({ fileStorageGroupId: this.data.globalFileStorageGroupId, skip: 0, take: 1000, fileTagId: null })
        .subscribe((result) => {
          this.globalFiles = result.items;
        });
    }
  }

  form = new FormGroup({});

  save() {
    this._closeDialog({
      attachments: this.attachments,
    });
  }

  localFiles: FileStorageEntry[] | null = null;
  globalFiles: FileStorageEntry[] | null = null;

  view: 'local' | 'global' = 'global';
  attachments: SendEmailAttachment[] = [];

  switchView(view: 'local' | 'global') {
    this.view = view;
  }

  isAttachment(file: FileStorageEntry) {
    return this.attachments.some((f) => f.fileStorageEntryId === file.id);
  }

  toggleAttachment(file: FileStorageEntry) {
    if (this.isAttachment(file)) {
      this.attachments = _.remove(this.attachments, (f) => f.fileStorageEntryId === file.id);
    } else {
      this.attachments.push({
        fileStorageEntryId: file.id,
        fileName: file.fileName,
        sizeBytes: file.sizeBytes,
      });
    }
  }

  iconClass(file: FileStorageEntry) {
    if (this.isAttachment(file)) {
      return 'far fa-check-square';
    }
    return 'far fa-square';
  }

  cancel() {
    this._cancelDialog();
  }

  formatSize(file: ProjectSupportFile) {
    return _.formatBytes(file.sizeBytes);
  }
}
