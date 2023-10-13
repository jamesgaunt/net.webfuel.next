import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit, EventEmitter, Output } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';

@Component({
  selector: 'grid-action-column',
  templateUrl: './grid-action-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridActionColumnComponent }]
})
export class GridActionColumnComponent<TItem> extends GridColumnComponent<TItem> {

  constructor() {
    super();
    this.justify = "right";
  }

  onAdd() {
    this.add.emit();
  }

  @Output()
  add = new EventEmitter();

  onEdit(item: TItem) {
    this.edit.emit(item);
  }

  @Output()
  edit = new EventEmitter<TItem>();

  onDelete(item: TItem) {
    this.delete.emit(item);
  }

  @Output()
  delete = new EventEmitter<TItem>();
}
