import _ from '../../underscore'
import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';

@Component({
  selector: 'grid-column',
  templateUrl: './grid-column.component.html',
})
export class GridColumnComponent<TItem> {

  @Input()
  name: string = "";

  @Input()
  get label() {
    if (this._label == undefined)
      this._label = _.splitCamelCase(this.name);
    return this._label;
  }
  set label(value: string) {
    this._label = value;
  }
  private _label: string | undefined;

  @Input()
  justify: "left" | "right" | "center" = "left";

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
}
