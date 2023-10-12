import { AfterViewInit, ChangeDetectorRef, Component, ContentChildren, Input, OnDestroy, QueryList } from '@angular/core';
import { IDataSource } from '../data-source/data-source';
import { GridColumnComponent } from './columns/grid-column.component';
import { Query } from '../../api/api.types';
import _ from '../underscore'
import { FormControl, FormGroup } from '@angular/forms';
import { debounceTime, tap } from 'rxjs';

@Component({
  selector: 'grid',
  templateUrl: './grid.component.html',
  //changeDetection: ChangeDetectionStrategy.OnPush
})
export class GridComponent<TItem> implements OnDestroy, AfterViewInit {

  constructor(
    private cd: ChangeDetectorRef
  ) {
  }

  @Input()
  dataSource: IDataSource<TItem> | undefined;

  @Input()
  filterForm: FormGroup | null = null;

  @Input()
  search = false;

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
      this.items = response.items;
      this.totalCount = response.totalCount;
      this.cd.detectChanges();
    });
  }

  buildQuery() {

    var query:any = this.query;

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

  totalCount = 0;

  // Columns

  @ContentChildren(GridColumnComponent<TItem>) columnQuery!: QueryList<GridColumnComponent<TItem>>;

  columns: GridColumnComponent<TItem>[] = [];

  ngAfterViewInit() {
    this.columns = this.columnQuery.toArray();

    if (this.dataSource) {
      this.dataSource.changed.pipe(
        debounceTime(200),
        tap(value => this.fetch()),
      )
      .subscribe();
    }

    if (this.filterForm) {
      this.filterForm.valueChanges
        .pipe(
          debounceTime(200),
          tap(value => this.fetch()),
        )
        .subscribe();
    }

    if (this.search) {
      this.searchForm.valueChanges
        .pipe(
          debounceTime(200),
          tap(value => this.fetch()),
        )
        .subscribe();
    }

    this.fetch();
    this.cd.detectChanges();
  }

  ngOnDestroy(): void {
  }

  get columnCount() {
    return this.columns.length;
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
}

