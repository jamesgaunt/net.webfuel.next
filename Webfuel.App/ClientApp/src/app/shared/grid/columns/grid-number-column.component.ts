import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';

@Component({
  selector: 'grid-number-column',
  templateUrl: './grid-number-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridNumberColumnComponent }]
})
export class GridNumberColumnComponent<TItem> extends GridColumnComponent<TItem> {

  constructor() {
    super();
    this.justify = "right";
  }

  @Input()
  format = '1.0-0';

  @Input()
  prefix = '';

}
