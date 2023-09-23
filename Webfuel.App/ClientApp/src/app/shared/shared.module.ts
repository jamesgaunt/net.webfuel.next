import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { OverlayModule } from '@angular/cdk/overlay';
import { DialogModule } from '@angular/cdk/dialog';

import { ChromeComponent } from './chrome/chrome.component';

// Form
import { TextInputComponent } from './form/text-input/text-input.component';
import { NumberInputComponent } from './form/number-input/number-input.component';
import { DropDownSelectComponent } from './form/dropdown-select/dropdown-select.component';
import { DateCalendarComponent } from './form/date-calendar/date-calendar.component';
import { DatePickerComponent } from './form/date-picker/date-picker.component';

// Data Grid
import { DataGridComponent } from './data-grid/data-grid.component';
import { DataGridColumnComponent } from './data-grid/data-grid-column.component';
import { DataPagerComponent } from './data-pager/data-pager.component';
import { DataSorterComponent } from './data-sorter/data-sorter.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DialogModule,
  ],
  declarations: [

    ChromeComponent,

    // Form
    TextInputComponent,
    NumberInputComponent,
    DropDownSelectComponent,
    DateCalendarComponent,
    DatePickerComponent,

    // Data Grid
    DataGridComponent,
    DataGridColumnComponent,
    DataPagerComponent,
    DataSorterComponent,


  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DialogModule,

    ChromeComponent,

    // Form
    TextInputComponent,
    NumberInputComponent,
    DropDownSelectComponent,
    DateCalendarComponent,
    DatePickerComponent,

    // Data Grid
    DataGridComponent,
    DataGridColumnComponent,
    DataPagerComponent,
    DataSorterComponent,
  ]
})
export class SharedModule { }
