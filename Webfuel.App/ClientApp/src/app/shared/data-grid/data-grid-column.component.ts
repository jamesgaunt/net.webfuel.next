import _ from '../underscore'
import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { DataGridComponent } from './data-grid.component';

@Component({
  selector: 'data-grid-column',
  templateUrl: './data-grid-column.component.html',
})
export class DataGridColumnComponent {

  @Input()
  name: string = "";

  @Input()
  label: string | null = null;

  @Input()
  justify: "left" | "right" | "center" = "left";

  @Input()
  template: string | null = null;

  // Not an input - set programatically
  grid!: DataGridComponent;

  // Head Template

  @Input()
  headTemplate: string | null = null;

  @ContentChild('headTemplate', { static: false }) customHeadTemplate: TemplateRef<any> | undefined; 
  @ViewChild('sortHeadTemplate', { static: false }) sortHeadTemplate!: TemplateRef<any>;
  @ViewChild('textHeadTemplate', { static: false }) textHeadTemplate!: TemplateRef<any>;

  headTemplateRef!: TemplateRef<any>;

  // Item Template

  @Input()
  itemTemplate: string | null = null;

  @ContentChild('itemTemplate', { static: true }) customItemTemplate: TemplateRef<any> | undefined;
  @ViewChild('textItemTemplate', { static: true }) textItemTemplate!: TemplateRef<any>;
  @ViewChild('boldItemTemplate', { static: true }) boldItemTemplate!: TemplateRef<any>;
  @ViewChild('flagItemTemplate', { static: true }) flagItemTemplate!: TemplateRef<any>;
  @ViewChild('dateItemTemplate', { static: true }) dateItemTemplate!: TemplateRef<any>;
  @ViewChild('utcItemTemplate', { static: true }) utcItemTemplate!: TemplateRef<any>;
  
  itemTemplateRef!: TemplateRef<any>;

  // Initialise (called by parent component)

  initialise() {
    if (!this.label)
      this.label = _.splitCamelCase(this.name);

    if (this.customItemTemplate && this.headTemplate === null)
      this.headTemplate = "text";

    this.headTemplateRef = this.getHeadTemplateRef();
    this.itemTemplateRef = this.getItemTemplateRef();
  }

  private getHeadTemplateRef() {
    if (this.customHeadTemplate)
      return this.customHeadTemplate;

    switch (this.headTemplate) {
      case "text": return this.textHeadTemplate;
      default:
      case "sort": return this.sortHeadTemplate;
    }
  }

  private getItemTemplateRef() {
    if (this.customItemTemplate)
      return this.customItemTemplate;

    switch (this.itemTemplate) {
      case "flag": return this.flagItemTemplate;
      case "bold": return this.boldItemTemplate;
      case "date": return this.dateItemTemplate;
      case "utc": return this.utcItemTemplate;
      default:
      case "text": return this.textItemTemplate;
    }
  }
}
