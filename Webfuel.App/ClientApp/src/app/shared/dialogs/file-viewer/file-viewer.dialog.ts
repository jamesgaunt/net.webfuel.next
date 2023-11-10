import { Component, Injectable } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { FileStorageEntry, UserActivity } from 'api/api.types';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';
import { FileStorageEntryApi } from '../../../api/file-storage-entry.api';

export interface FileViewerDialogData {
  file: FileStorageEntry
}

@Injectable()
export class FileViewerDialog extends DialogBase<UserActivity, FileViewerDialogData> {
  open(data: FileViewerDialogData) {
    return this._open(FileViewerDialogComponent, data);
  }
}

@Component({
  selector: 'file-viewer-dialog',
  templateUrl: './file-viewer.dialog.html'
})
export class FileViewerDialogComponent extends DialogComponentBase<UserActivity, FileViewerDialogData>  {

  constructor(
    private fileStorageEntryApi: FileStorageEntryApi,
    private domSanitizer: DomSanitizer
  ) {
    super();

  }

  sasUri = "";

  safeSasUri: any = null;

  cancel() {
    this._cancelDialog();
  }
}
