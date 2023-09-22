import _ from '../underscore'
import { Component, Input, Output, EventEmitter, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { GridDataSource } from '../data-source/grid-data-source';


@Component({
  selector: 'data-pager',
  templateUrl: './data-pager.component.html',
  styleUrls: ['./data-pager.component.scss']
})
export class DataPagerComponent {
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
    this._dataSource.change.subscribe((response) => {
      this.calculate();
      this.cdr.detectChanges();
    });
  }
  _dataSource!: GridDataSource<any>;

  // Calculated

  pageIndex: number = 0;

  pageCount: number = 0;

  range(pageCount: number) {
    return _.range(pageCount);
  }

  calculate() {
    if (this.dataSource.query.take <= 0) {
      this.pageIndex = 0;
      this.pageCount = 1;
    } else {
      this.pageIndex = Math.floor(this.dataSource.query.skip / this.dataSource.query.take);
      this.pageCount = Math.ceil(this.dataSource.queryResult.totalCount / this.dataSource.query.take);
    }
  }

  repage(pageIndex: number) {
    if (!this.dataSource)
      return;
    this.dataSource.query.skip = this.dataSource.query.take * pageIndex;
    this.dataSource.fetch();
  }

  retake($event: any) {
    if (!this.dataSource)
      return;
    this.dataSource.query.skip = 0;
    this.dataSource.query.take = parseInt($event.target.value, 10);
    this.dataSource.fetch();
  }
}
