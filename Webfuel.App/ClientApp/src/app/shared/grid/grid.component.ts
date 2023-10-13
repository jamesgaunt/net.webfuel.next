import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChildren, EventEmitter, Input, OnDestroy, Output, QueryList } from '@angular/core';
import { IDataSource } from '../common/data-source';
import { GridColumnComponent } from './columns/grid-column.component';
import { Query } from '../../api/api.types';
import _ from '../common/underscore'
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, debounceTime, tap } from 'rxjs';
import { QueryService } from '../../core/query.service';

@Component({
  selector: 'grid',
  templateUrl: './grid.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GridComponent<TItem> implements OnDestroy, AfterViewInit {

  constructor(
    private cd: ChangeDetectorRef,
    private queryService: QueryService
  ) {
  }

  @Input()
  dataSource: IDataSource<TItem> | undefined;

  @Input()
  filterForm: FormGroup | null = null;

  @Input()
  search = false;

  @Output()
  sort = new EventEmitter<TItem[]>();

  // Query

  searchForm = new FormGroup({
    search: new FormControl('', { nonNullable: true })
  });

  fetch() {
    if (!this.dataSource)
      return;

    this.dataSource.fetch(this.buildQuery()).subscribe((response) => {
      if (response.totalCount > 0 && response.items.length === 0 && this.query.skip! > 0) {
        this.query.skip = 0;
        this.fetch();
        return;
      }
      this.sorting = false;
      this.items = response.items;
      this.totalCount = response.totalCount;
      this.cd.detectChanges();
    });
  }

  buildQuery() {

    var query: any = this.query;

    if (this.filterForm)
      query = _.merge(query, this.filterForm.getRawValue());

    if (this.search)
      query = _.merge(query, this.searchForm.getRawValue());

    return query;
  }

  query: Query = {
    skip: 0, take: 10, sort: [], filters: [], projection: [], search: ''
  };

  // Items

  items: TItem[] = [];

  totalCount = -1; // -1 => Loading

  // Columns

  @ContentChildren(GridColumnComponent<TItem>) columnQuery!: QueryList<GridColumnComponent<TItem>>;

  columns: GridColumnComponent<TItem>[] = [];

  get columnCount() {
    return this.columns.length + (this.sortable ? 1 : 0);
  }

  // Page

  pageIndex: number = 0;

  pageCount: number = 0;

  range(pageCount: number) {
    return _.range(pageCount);
  }

  calculate() {
    if (this.query.take <= 0) {
      this.pageIndex = 0;
      this.pageCount = 1;
    } else {
      this.pageIndex = Math.floor(this.query.skip / this.query.take);
      this.pageCount = Math.ceil(this.totalCount / this.query.take);
    }
  }

  repage(pageIndex: number) {
    this.query.skip = this.query.take * pageIndex;
    this.fetch();
  }

  retake($event: any) {
    this.query.skip = 0;
    this.query.take = parseInt($event.target.value, 10);
    this.fetch();
  }

  // Drag & Drop Sort

  get sortable() { return this.sort.observed; }

  sorting = false;

  drop($event: any) {
    if (!this.sortable || this.sorting)
      return;
    this.sorting = true;

    var currentIndex = <number>$event.currentIndex;
    var previousIndex = <number>$event.previousIndex;

    // Client Side
    const item = this.items.splice(previousIndex, 1);
    this.items.splice(currentIndex, 0, item[0]);

    // Server Side
    this.sort.emit(this.items);
  }

  // Column Sort

  columnSort(column: GridColumnComponent<TItem>) {
    this.queryService.sortToggle(this.query, column.name);
    this.fetch();
  }

  columnDirection(column: GridColumnComponent<TItem>) {
    return this.queryService.sortDirection(this.query, column.name);
  }

  // Lifecycle

  ngAfterViewInit() {
    this.columns = this.columnQuery.toArray();

    _.forEach(this.columns, p => {
      p.grid = this;
    });

    if (this.dataSource) {
      this.dataSource.changed.pipe(
        debounceTime(200),
      )
      .subscribe(() => this.fetch());
    }

    if (this.filterForm) {
      this.filterForm.valueChanges
        .pipe(
          debounceTime(200),
        )
        .subscribe(() => this.fetch());
    }

    if (this.search) {
      this.searchForm.valueChanges
        .pipe(
          debounceTime(200),
        )
        .subscribe(() => this.fetch());
    }

    this.fetch();
    this.cd.detectChanges();
  }

  ngOnDestroy(): void {
  }
}

