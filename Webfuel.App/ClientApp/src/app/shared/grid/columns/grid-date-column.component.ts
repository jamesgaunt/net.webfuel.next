import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';

@Component({
  selector: 'grid-date-column',
  templateUrl: './grid-date-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridDateColumnComponent }]
})
export class GridDateColumnComponent<TItem> extends GridColumnComponent<TItem> {
}
