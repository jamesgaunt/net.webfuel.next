import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';
import { BehaviorSubject } from 'rxjs';
import { IDataSource } from 'shared/common/data-source';
import { Query } from '../../../api/api.types';

@Component({
  selector: 'grid-reference-column',
  templateUrl: './grid-reference-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridReferenceColumnComponent }]
})
export class GridReferenceColumnComponent<TItem> extends GridColumnComponent<TItem> {

  @Input()
  dataSource: IDataSource<TItem, Query, any, any> | undefined;

  lookup(id: string): BehaviorSubject<TItem | null> {

    if (this._cache[id] == undefined) {
      this._cache[id] = new BehaviorSubject<TItem | null>(null);

      if (!this.dataSource)
        return new BehaviorSubject<TItem | null>(null);

      this.dataSource.query({
        skip: 0, take: 1, filters: [
          { field: "id", op: "eq", value: id }
        ]
      }).subscribe((result) => {
        if (result.items.length == 1)
          this._cache[id].next(result.items[0]);
      });
    }
    return this._cache[id];
  }

  format(item: any) {
    if (!item)
      return "";
    return item['name'];
  }

  private _cache: { [key: string]: BehaviorSubject<TItem | null> } = {};
}
