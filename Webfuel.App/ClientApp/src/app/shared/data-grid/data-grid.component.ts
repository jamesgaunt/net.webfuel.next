import { AfterContentInit, ChangeDetectorRef, Component, ContentChildren, EventEmitter, Input, OnDestroy, Output, QueryList, AfterViewInit, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { GridDataSource } from '../data-source/grid-data-source';
import _ from '../underscore';
import { DataGridColumnComponent } from './data-grid-column.component';
import { CdkDragDrop } from '@angular/cdk/drag-drop';

@Component({
  selector: 'data-grid',
  templateUrl: './data-grid.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataGridComponent<TItem> implements OnDestroy, AfterViewInit {

  constructor(
    private cd: ChangeDetectorRef
  ) {
  }

  // Data Source

  @Input({ required: true })
  get dataSource() {
    return this._dataSource;
  }
  set dataSource(value: GridDataSource<TItem>) {
    this._dataSource = value;
    this._dataSource.change.subscribe((response) => {
      this.reordering = false;
      this.cd.detectChanges();
    });
    this._dataSource.fetch();
  }
  private _dataSource!: GridDataSource<TItem>;

  // Columns

  @ContentChildren(DataGridColumnComponent<TItem>) columnQuery!: QueryList<DataGridColumnComponent<TItem>>;

  columns: DataGridColumnComponent<TItem>[] = [];

  ngAfterViewInit() {
    this.columns = this.columnQuery.toArray();
    _.forEach(this.columns, p => {
      p.grid = this;
      p.initialise();
    });
    this.cd.detectChanges();
  }

  get columnCount() {
    return this.columns.length + (this.dataSource.reorderable ? 1 : 0);
  }

  // Reorder

  reordering = false;

  drop(event: any) {
    if (this.reordering)
      return;
    this.reordering = true;
    this.dataSource.reorder(event.previousIndex, event.currentIndex);
  }

  // Destroy

  ngOnDestroy(): void {
  }
}

