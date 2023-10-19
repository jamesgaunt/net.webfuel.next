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

  // Add

  @Input()
  canAdd = true;

  @Output()
  add = new EventEmitter();

  onAdd() {
    this.add.emit();
  }

  // Edit

  @Input()
  canEdit = true;

  @Output()
  edit = new EventEmitter<TItem>();

  onEdit(item: TItem) {
    this.edit.emit(item);
  }

  // Delete

  @Input()
  canDelete = true;

  @Output()
  delete = new EventEmitter<TItem>();

  onDelete(item: TItem) {
    this.delete.emit(item);
  }
}
