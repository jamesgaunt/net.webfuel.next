import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserActivity } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivityApi } from 'api/user-activity.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

export interface FileViewerDialogData {
  fileStorageEntryId: string
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
  ) {
    super();
  }

  close() {
    this._cancelDialog();
  }
}
