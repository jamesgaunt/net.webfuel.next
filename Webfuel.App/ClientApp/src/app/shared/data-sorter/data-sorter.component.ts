import _ from '../underscore'
import { Component, Input, Output, EventEmitter, ChangeDetectorRef, OnDestroy, OnChanges, SimpleChanges, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { GridDataSource } from '../data-source/grid-data-source';

@Component({
  selector: 'data-sorter',
  templateUrl: './data-sorter.component.html',
  styleUrls: ['./data-sorter.component.scss']
})
export class DataSorterComponent {

  constructor(
    private cdr: ChangeDetectorRef
  ) {
    cdr.detach();
  }

  @Input({ required: true })
  get dataSource() {
    return this._dataSource;
  }
  set dataSource(value: GridDataSource<any>) {
    this._dataSource = value;
    this._dataSource.change.subscribe(() => {
      this.cdr.detectChanges();
    });
  }
  _dataSource!: GridDataSource<any>;

  @Input()
  reverseToggle = false;

  @Input()
  column: string = "";

  @Input()
  label: string | null = null;

  get direction() {
    return this.dataSource.sortDirection(this.column);
  }

  toggle() {
    this.dataSource.sortToggle(this.column, this.reverseToggle);
    this.dataSource.fetch();
  }
}
