import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';
import { IDataSource } from '../../data-source/data-source';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'grid-reference-column',
  templateUrl: './grid-reference-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridReferenceColumnComponent }]
})
export class GridReferenceColumnComponent<TItem> extends GridColumnComponent<TItem> {

  @Input()
  dataSource: IDataSource<TItem> | undefined;

  cache: { [key: string]: BehaviorSubject<TItem | null> } = {};

  lookup(id: string): BehaviorSubject<TItem | null> {

    if (this.cache[id] == undefined) {
      this.cache[id] = new BehaviorSubject<TItem | null>(null);

      if (!this.dataSource)
        return new BehaviorSubject<TItem | null>(null);

      this.dataSource.fetch({
        skip: 0, take: 1, filters: [
          { field: "id", op: "eq", value: id }
        ]
      }).subscribe((result) => {
        if (result.items.length == 1)
          this.cache[id].next(result.items[0]);
      });
    }
    return this.cache[id];
  }

  format(item: any) {
    if (!item)
      return "";
    return item['name'];
  }
}
