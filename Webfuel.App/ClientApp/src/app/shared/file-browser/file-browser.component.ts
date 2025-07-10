import { HttpClient, HttpEventType } from '@angular/common/http';
import { Component, DestroyRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { StaticDataCache } from 'api/static-data.cache';
import { debounceTime } from 'rxjs';
import _ from 'shared/common/underscore';
import { FileEditorDialog } from 'shared/dialogs/file-editor/file-editor.dialog';
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

  globalFileStorageGroupId = '17c28098-375c-4a1a-bc41-43813786ab84'; // This is the ID for the Global File Storage Group

  constructor(
    private router: Router,
    private httpClient: HttpClient,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private fileEditorDialog: FileEditorDialog,
    public fileStorageEntryApi: FileStorageEntryApi,
    public userService: UserService,
    public staticDataCache: StaticDataCache
  ) {}

  ngOnInit(): void {
    this.form.valueChanges.pipe(debounceTime(200), takeUntilDestroyed(this.destroyRef)).subscribe(() => {
      this.uploadFiles();
    });
  }

  isGlobalFiles() {
    return this.fileStorageGroupId == this.globalFileStorageGroupId;
  }

  // Inputs

  @Input({ required: true })
  fileStorageGroupId!: string;

  @Input()
  projectId: string | null = null;

  @Input()
  locked = false;

  // Grid

  filterForm = new FormGroup({
    search: new FormControl<string>('', { nonNullable: true }),
    fileTagId: new FormControl<string | null>(null),
  });

  resetFilterForm() {
    this.filterForm.patchValue({
      search: '',
      fileTagId: null,
    });
  }

  filter(query: QueryFileStorageEntry) {
    query.fileStorageGroupId = this.fileStorageGroupId;
  }

  formatSize(file: FileStorageEntry) {
    return _.formatBytes(file.sizeBytes);
  }

  sasRedirect(file: FileStorageEntry) {
    return environment.apiHost + 'api/file-storage-entry/sas-redirect/' + file.id;
  }

  // Upload

  progress: number | null = null;

  form = new FormGroup({
    fileStorageGroupId: new FormControl('', { nonNullable: true }),
    files: new FormControl([]),
  });

  uploadFiles() {
    if (this.locked) return;

    if (!this.form.value.files || this.form.value.files.length == 0) return;

    this.form.patchValue({ fileStorageGroupId: this.fileStorageGroupId }, { emitEvent: false });

    var formData = this.toFormData(this.form.value);

    this.httpClient
      .post(environment.apiHost + 'api/file-storage-entry/upload', formData, {
        reportProgress: true,
        observe: 'events',
      })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress && event.total) {
            this.progress = Math.round((100 * event.loaded) / event.total);
          }
        },
        error: (err) => {
          console.log('Error: ', err);
        },
        complete: () => {
          this.progress = null;
          this.fileStorageEntryApi.changed.next(null);
          this.form.patchValue({ files: [] }, { emitEvent: false });
        },
      });
  }

  toFormData(formValue: any) {
    const formData = new FormData();
    var data: any = {};

    for (const key of Object.keys(formValue)) {
      if (key != 'files') {
        data[key] = formValue[key];
        continue;
      }
      var files = formValue[key];
      if (files && files.length) {
        for (var i = 0; i < files.length; i++) {
          formData.append('file_' + i, files[i]);
        }
      }
    }
    formData.append('data', JSON.stringify(data));
    return formData;
  }

  deleteFile(file: FileStorageEntry) {
    if (this.locked) return;

    this.confirmDeleteDialog.open({ title: file.fileName }).subscribe((result) => {
      this.fileStorageEntryApi.delete({ id: file.id }, { successGrowl: 'File Deleted' }).subscribe(() => {});
    });
  }

  editFile(file: FileStorageEntry) {
    if (this.locked) return;
    this.fileEditorDialog.open({ file: file }).subscribe(() => {});
  }

  searchSupport(file: FileStorageEntry) {
    _.setLocalStorage('SupportFileSearch', file.fileName);
    if (this.projectId !== null) {
      this.router.navigateByUrl(`/project/project-support/${this.projectId}`);
    }
  }
}
