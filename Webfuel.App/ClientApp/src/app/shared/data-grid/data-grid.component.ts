import { AfterContentInit, ChangeDetectorRef, Component, ContentChildren, EventEmitter, Input, OnDestroy, Output, QueryList, AfterViewInit, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { DataSource } from '../data-source';
import _ from '../underscore';
import { DataGridColumnComponent } from './data-grid-column.component';

@Component({
  selector: 'data-grid',
  templateUrl: './data-grid.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DataGridComponent implements OnDestroy, AfterContentInit {

  constructor(
    private cd: ChangeDetectorRef
  ) {
  }

  // Data Source

  @Input()
  get dataSource() {
    return this._dataSource;
  }
  set dataSource(value: DataSource<any, any>) {
    if (value.query.take == 0)
      value.query.take = 15;

    this._dataSource = value;
    this._dataSource.change.subscribe((response) => {
      this.cd.detectChanges();
    });
    this._dataSource.fetch();
  }
  private _dataSource!: DataSource<any, any>;

  // Columns

  @ContentChildren(DataGridColumnComponent) columnQuery!: QueryList<DataGridColumnComponent>;

  columns: DataGridColumnComponent[] = [];

  ngAfterContentInit() {
    this.columns = this.columnQuery.toArray();
    _.forEach(this.columns, p => {
      p.grid = this;
      setTimeout(() => p.initialise(), 0);
    });
  }

  get columnCount() {
    return this.columns.length; // + (this.dataSource.selectable ? 1 : 0);
  }

  // Destroy

  ngOnDestroy(): void {
  }
}

