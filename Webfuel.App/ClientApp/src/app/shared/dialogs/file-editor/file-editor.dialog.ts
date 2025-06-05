import { Component, inject, Injectable } from '@angular/core';
import { FileStorageEntry, FileTag } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { FileStorageEntryApi } from '../../../api/file-storage-entry.api';
import { DialogBase, DialogComponentBase } from '../../common/dialog-base';

export interface FileEditorDialogData {
  file: FileStorageEntry;
}

@Injectable()
export class FileEditorDialog extends DialogBase<boolean, FileEditorDialogData> {
  open(data: FileEditorDialogData) {
    return this._open(FileEditorDialogComponent, data);
  }
}

@Component({
  selector: 'file-editor-dialog',
  templateUrl: './file-editor.dialog.html',
})
export class FileEditorDialogComponent extends DialogComponentBase<boolean, FileEditorDialogData> {
  staticDataCache = inject(StaticDataCache);

  constructor(private fileStorageEntryApi: FileStorageEntryApi) {
    super();

    this.staticDataCache.fileTag.query({ skip: 0, take: 100 }).subscribe((result) => {
      this.fileTags = result.items;
    });
    this.fileTagIds = new Set<string>(this.data.file.fileTagIds);
  }

  fileTags: FileTag[] = [];

  fileTagIds = new Set<string>();

  isSelected(fileTagId: string) {
    return this.fileTagIds.has(fileTagId);
  }

  toggle(fileTagId: string) {
    if (this.fileTagIds.has(fileTagId)) {
      this.fileTagIds.delete(fileTagId);
    } else {
      this.fileTagIds.add(fileTagId);
    }
  }

  save() {
    if (!this.data.file) return;
    this.fileStorageEntryApi.update({ id: this.data.file.id, fileTagIds: Array.from(this.fileTagIds) }, { successGrowl: 'File Updated' }).subscribe({
      next: (result) => {
        this._closeDialog(true);
      },
    });
  }

  cancel() {
    this._cancelDialog();
  }
}
