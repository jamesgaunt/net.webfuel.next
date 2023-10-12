import { Component, Input, OnInit, ContentChildren, TemplateRef, QueryList, ContentChild, ViewChild, AfterContentInit, AfterViewInit } from '@angular/core';
import { GridColumnComponent } from './grid-column.component';

@Component({
  selector: 'grid-datetime-column',
  templateUrl: './grid-datetime-column.component.html',
  providers: [{ provide: GridColumnComponent, useExisting: GridDateTimeColumnComponent }]
})
export class GridDateTimeColumnComponent<TItem> extends GridColumnComponent<TItem> {
}
