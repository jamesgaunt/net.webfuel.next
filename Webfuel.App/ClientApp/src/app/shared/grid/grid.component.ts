import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChildren, DestroyRef, EventEmitter, Input, OnDestroy, Output, QueryList, inject } from '@angular/core';
import { IDataSource } from '../common/data-source';
import { GridColumnComponent } from './columns/grid-column.component';
import { Query } from '../../api/api.types';
import _ from '../common/underscore'
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, debounceTime, tap } from 'rxjs';
import { QueryService } from '../../core/query.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'grid',
  templateUrl: './grid.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class GridComponent<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> implements AfterViewInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private cd: ChangeDetectorRef,
    private queryService: QueryService
  ) {
  }

  @Input()
  dataSource: IDataSource<TItem, TQuery, TCreate, TUpdate> | undefined;

  @Input()
  filterForm: FormGroup | null = null;

  @Output()
  filter = new EventEmitter<TQuery>();

  @Input()
  search = false;

  @Input()
  stateKey = '';

  // Query

  searchForm = new FormGroup({
    search: new FormControl('', { nonNullable: true })
  });

  fetch() {
    if (!this.dataSource)
      return;

    if (this.sortable) {
      this.query.skip = 0;
      this.query.take = 100;
    }

    var query = this.buildQuery();

    this.dataSource.query(query).subscribe((response) => {
      if (response.totalCount > 0 && response.items.length === 0 && this.query.skip! > 0) {
        this.query.skip = 0;
        this.fetch();
        return;
      }
      this.saveState();
      this.sorting = false;
      this.items = response.items;
      this.totalCount = response.totalCount;
      this.calculatePage();
      this.cd.detectChanges();
    });
  }

  buildQuery() {

    var query: any = this.query;

    if (this.filterForm)
      query = _.merge(query, this.filterForm.getRawValue());

    if (this.search)
      query = _.merge(query, this.searchForm.getRawValue());

    if (this.filter)
      this.filter.emit(query);

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

  // State

  saveState() {
    if (!this.stateKey)
      return;

    var state = <any>{ query: this.query }

    if (this.filterForm)
      state.filterForm = this.filterForm.getRawValue();

    console.log("Saving Grid State: " + this.stateKey, state);

    _.setLocalStorage("GridState:" + this.stateKey, state, 60 * 60 * 24);
  }

  loadState() {
    if (!this.stateKey)
      return;

    var state = _.getLocalStorage("GridState:" + this.stateKey);
    if (!state || !state.query)
      return;

    console.log("Loaded Grid State: " + this.stateKey, state);

    this.query = state.query;

    if (state.filterForm || this.filterForm)
      this.filterForm?.patchValue(state.filterForm, { emitEvent: false });
  }

  // Page

  pageIndex: number = 0;

  pageCount: number = 0;

  range(pageCount: number) {
    return _.range(pageCount);
  }

  calculatePage() {
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

  get sortable() { return this.dataSource && this.dataSource.sort; }

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
    this.dataSource!.sort!({ ids: _.map(this.items, p => (<any>p).id) }).subscribe();
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
    this.loadState();

    this.columns = this.columnQuery.toArray();

    _.forEach(this.columns, p => {
      p.grid = this;
    });

    if (this.dataSource && this.dataSource.changed) {
      this.dataSource.changed.pipe(
        debounceTime(250),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe(() => this.fetch());
    }

    if (this.filterForm) {
      this.filterForm.valueChanges
        .pipe(
          debounceTime(200),
          takeUntilDestroyed(this.destroyRef)
        )
        .subscribe(() => this.fetch());
    }

    if (this.search) {
      this.searchForm.valueChanges
        .pipe(
          debounceTime(250),
          takeUntilDestroyed(this.destroyRef)
        )
        .subscribe(() => this.fetch());
    }

    this.fetch();
    this.cd.detectChanges();
  }
}

