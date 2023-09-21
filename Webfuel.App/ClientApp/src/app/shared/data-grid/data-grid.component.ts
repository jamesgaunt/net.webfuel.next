import { AfterContentInit, ChangeDetectorRef, Component, ContentChildren, EventEmitter, Input, OnDestroy, Output, QueryList, AfterViewInit, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { GridDataSource } from '../data-source/grid-data-source';
import _ from '../underscore';
import { DataGridColumnComponent } from './data-grid-column.component';

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
    return this.columns.length; // + (this.dataSource.selectable ? 1 : 0);
  }

  // Destroy

  ngOnDestroy(): void {
  }
}

