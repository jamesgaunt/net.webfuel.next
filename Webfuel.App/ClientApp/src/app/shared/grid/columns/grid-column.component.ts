import _ from '../../common/underscore'
import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridComponent } from '../grid.component';

@Component({
  selector: 'grid-column',
  templateUrl: './grid-column.component.html',
})
export class GridColumnComponent<TItem> {

  @Input()
  name: string = "";

  @Input()
  get label() {
    if (this._label == undefined) {
      this._label = _.splitCamelCase(this.name);
      if (this._label.endsWith(" Id"))
        this._label = this._label.substring(0, this._label.length - 3);
    }
    return this._label;
  }
  set label(value: string) {
    this._label = value;
  }
  private _label: string | undefined;

  @Input()
  justify: "left" | "right" | "center" = "left";

  @Input()
  get sortable() {
    if (!this.grid || this.grid.sortable)
      return false; // We can't enable sorting on both the grid and individual columns
    return this._sortable;
  }
  set sortable(value) {
    this._sortable = value;
  }
  _sortable = true;

  // Injected Grid Reference

  grid: GridComponent<TItem, any, any, any> | null = null;

  // Head Template

  @ContentChild('headTemplate', { static: false }) customHeadTemplate: TemplateRef<any> | undefined;
  @ViewChild('headTemplate', { static: false }) textHeadTemplate!: TemplateRef<any>;

  get headTemplateRef() {
    if (this.customHeadTemplate)
      return this.customHeadTemplate;
    return this.textHeadTemplate;
  }

  // Item Template

  @ContentChild('itemTemplate', { static: true }) customItemTemplate: TemplateRef<any> | undefined;
  @ViewChild('itemTemplate', { static: true }) textItemTemplate!: TemplateRef<any>;

  get itemTemplateRef() {
    if (this.customItemTemplate)
      return this.customItemTemplate;
    return this.textItemTemplate;
  }

  // Column Sort

  get direction() {
    if (!this.grid)
      return 0;
    return this.grid.columnDirection(this);
  }
}
