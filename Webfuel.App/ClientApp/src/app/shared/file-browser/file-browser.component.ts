import { ChangeDetectionStrategy, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { FileStorageEntry } from '../../api/api.types';
import { FileStorageApi } from '../../api/file-storage.api';

@Component({
  selector: 'file-browser',
  templateUrl: './file-browser.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class FileBrowserComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    fileStorageApi: FileStorageApi
  ) {
  }

  ngOnInit(): void {
  }

  // Inputs

  @Input({ required: true })
  fileStorageGroupId!: string;

  files: FileStorageEntry[] | null = null;



}
