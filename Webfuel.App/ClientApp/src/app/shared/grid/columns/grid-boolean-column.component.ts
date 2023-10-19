import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';

@Component({
  selector: 'grid-boolean-column',
  templateUrl: './grid-boolean-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridBooleanColumnComponent }]
})
export class GridBooleanColumnComponent<TItem> extends GridColumnComponent<TItem> {

  constructor() {
    super();
    this.justify = "center";
  }

}
