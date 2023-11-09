import { ChangeDetectionStrategy, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { FileStorageEntry } from '../../api/api.types';
import { FileStorageApi } from '../../api/file-storage.api';
import _ from 'shared/common/underscore';

@Component({
  selector: 'file-browser',
  templateUrl: './file-browser.component.html',
})
export class FileBrowserComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private fileStorageApi: FileStorageApi
  ) {
  }

  ngOnInit(): void {
    this.loadFiles();
  }

  // Inputs

  @Input({ required: true })
  fileStorageGroupId!: string;

  files: FileStorageEntry[] | null = null;

  loadFiles() {
    this.fileStorageApi.listFiles({ fileStorageGroupId: this.fileStorageGroupId }).subscribe((result) => this.files = result);
  }

  formatSize(file: FileStorageEntry) {
    return _.formatBytes(file.sizeBytes);
  }

}
