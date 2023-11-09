import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FileStorageEntry, UserActivity } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivityApi } from 'api/user-activity.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';
import { FileStorageApi } from '../../../api/file-storage.api';
import { DomSanitizer } from '@angular/platform-browser';

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
    private fileStorageApi: FileStorageApi,
    private domSanitizer: DomSanitizer
  ) {
    super();
    fileStorageApi.generateFileSasUri({ fileStorageEntryId: this.data.file.id }).subscribe((result) => {
      this.sasUri = result.value;
      this.safeSasUri = domSanitizer.bypassSecurityTrustResourceUrl(this.sasUri);
    });
  }

  sasUri = "";

  safeSasUri: any = null;

  cancel() {
    this._cancelDialog();
  }
}
