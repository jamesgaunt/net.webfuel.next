import { HttpClient, HttpEventType } from '@angular/common/http';
import { inject } from '@angular/core';
import { ProjectSupportFile } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { DialogComponentBase } from 'shared/common/dialog-base';
import { AttachProjectSupportFilesDialog } from 'shared/dialogs/attach-project-support-files/attach-project-support-files.dialog';
import { UploadProjectSupportFilesDialog } from 'shared/dialogs/upload-project-support-files/upload-project-support-files.dialog';
import { environment } from '../../../../../../environments/environment';

export interface ProjectSupportDialogData {
  projectSupportGroupId: string;
}

export abstract class ProjectSupportDialogComponentBase<TResult, TData extends ProjectSupportDialogData> extends DialogComponentBase<TResult, TData> {
  httpClient: HttpClient = inject(HttpClient);
  projectSupportApi: ProjectSupportApi = inject(ProjectSupportApi);
  uploadProjectSupportFilesDialog: UploadProjectSupportFilesDialog = inject(UploadProjectSupportFilesDialog);
  attachProjectSupportFilesDialog: AttachProjectSupportFilesDialog = inject(AttachProjectSupportFilesDialog);

  constructor() {
    super();
  }

  existingFiles: ProjectSupportFile[] = [];
  uploadedFiles: File[] = [];

  submitting = false;
  progress: number | null = null;

  abstract submitForm(): void;

  addUploadedFiles() {
    this.uploadProjectSupportFilesDialog.open({ projectSupportGroupId: this.data.projectSupportGroupId }).subscribe((result) => {
      result.uploadedFiles.forEach((file) => {
        var index = this.uploadedFiles.findIndex((p) => p.name == file.name);
        if (index >= 0) {
          this.uploadedFiles[index] = file;
        } else {
          this.uploadedFiles.push(file);
        }
      });
    });
  }

  addAttachedFiles() {
    this.projectSupportApi.selectFile({ projectSupportGroupId: this.data.projectSupportGroupId }).subscribe((result) => {
      this.attachProjectSupportFilesDialog
        .open({
          databaseFiles: result,
          existingFiles: this.existingFiles,
        })
        .subscribe((result) => {
          this.existingFiles = result.existingFiles;
        });
    });
  }

  removeUploadedFile(file: File, $event: MouseEvent) {
    $event.preventDefault();
    $event.stopPropagation();

    var index = this.uploadedFiles.indexOf(file);
    if (index !== -1) {
      this.uploadedFiles.splice(index, 1);
    }
  }

  removeExistingFile(file: ProjectSupportFile, $event: MouseEvent) {
    $event.preventDefault();
    $event.stopPropagation();

    var index = this.existingFiles.indexOf(file);
    if (index !== -1) {
      this.existingFiles.splice(index, 1);
    }
  }

  submitFiles() {
    var formData = this.toFormData();
    if (formData == null) {
      // no files to upload
      this.submitForm();
      return;
    }

    this.httpClient
      .post(environment.apiHost + 'api/project-support/upload-file', formData, {
        reportProgress: true,
        observe: 'events',
      })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress && event.total) {
            this.progress = Math.round((100 * event.loaded) / event.total);
          }
          if (event.type === HttpEventType.Response) {
            console.log(event);
            var newFiles = <ProjectSupportFile[]>(<any>event.body);
            this.uploadedFiles = [];
            this.existingFiles = this.existingFiles.concat(newFiles);
            this.submitForm();
          }
        },
        error: (err) => {
          this.fileSubmissionError(err);
        },
      });
  }

  toFormData(): FormData | null {
    if (!this.uploadedFiles || !this.uploadedFiles.length) return null;

    const formData = new FormData();
    for (var i = 0; i < this.uploadedFiles.length; i++) {
      formData.append('file_' + i, this.uploadedFiles[i]);
    }
    formData.append('data', JSON.stringify({ projectSupportGroupId: this.data.projectSupportGroupId }));
    return formData;
  }

  fileSubmissionError(err: any) {
    this.submitting = false;
    this.progress = null;
    console.log('File Submission Error: ', err);
    alert('There was an error uploading your files.\n\nPlease ensure all files are saved on your computer, and not open in another application.');
  }
}
