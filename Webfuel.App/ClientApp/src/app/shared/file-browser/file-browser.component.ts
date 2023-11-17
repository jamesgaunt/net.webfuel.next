import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { debounceTime } from 'rxjs';
import _ from 'shared/common/underscore';
import { environment } from '../../../environments/environment';
import { FileStorageEntry, QueryFileStorageEntry } from '../../api/api.types';
import { FileStorageEntryApi } from '../../api/file-storage-entry.api';
import { UserService } from '../../core/user.service';
import { ConfirmDeleteDialog } from '../dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'file-browser',
  templateUrl: './file-browser.component.html',
})
export class FileBrowserComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private httpClient: HttpClient,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public fileStorageEntryApi: FileStorageEntryApi,
    public userService: UserService
  ) {
  }

  ngOnInit(): void {
    this.form.valueChanges.pipe(
      debounceTime(200),
      takeUntilDestroyed(this.destroyRef),
    ).subscribe(() => {
      this.uploadFiles();
    });
  }

  // Inputs

  @Input({ required: true })
  fileStorageGroupId!: string;

  @Input()
  locked = false;

  // Grid

  filter(query: QueryFileStorageEntry) {
    query.fileStorageGroupId = this.fileStorageGroupId;
  }

  formatSize(file: FileStorageEntry) {
    return _.formatBytes(file.sizeBytes);
  }

  sasRedirect(file: FileStorageEntry) {
    return environment.apiHost + "api/file-storage-entry/sas-redirect/" + file.id;
  }

  // Upload

  progress: number | null = null;

  form = new FormGroup({
    fileStorageGroupId: new FormControl('', { nonNullable: true }),
    files: new FormControl([])
  })

  uploadFiles() {
    if (this.locked)
      return;

    if (!this.form.value.files || this.form.value.files.length == 0)
      return;

    this.form.patchValue({ fileStorageGroupId: this.fileStorageGroupId }, { emitEvent: false });

    var formData = this.toFormData(this.form.value);

    this.httpClient.post(environment.apiHost + "api/file-storage-entry/upload", formData, {
      reportProgress: true,
      observe: 'events'
    }).subscribe({
      next:
        (event) => {
          if (event.type === HttpEventType.UploadProgress && event.total) {
            this.progress = Math.round((100 * event.loaded) / event.total);
            
          }
        },
      error: (err) => {
        console.log("Error: ", err);
      },
      complete: () => {
        this.progress = null;
        this.fileStorageEntryApi.changed.next(null);
        this.form.patchValue({ files: [] }, { emitEvent: false });
      }
    });
  }



  toFormData(formValue: any) {
    const formData = new FormData();
    var data: any = {};

    for (const key of Object.keys(formValue)) {
      if (key != "files") {
        data[key] = formValue[key];
        continue;
      }
      var files = formValue[key];
      if (files && files.length) {
        for (var i = 0; i < files.length; i++) {
          formData.append("file_" + i, files[i]);
        }
      }
    }
    formData.append("data", JSON.stringify(data));
    return formData;
  }

  deleteFile(file: FileStorageEntry) {
    if (this.locked)
      return;

    this.confirmDeleteDialog.open({ title: file.fileName }).subscribe((result) => {
      this.fileStorageEntryApi.delete({ id: file.id }, { successGrowl: "File Deleted" }).subscribe(() => {
      });
    });
  }
}
