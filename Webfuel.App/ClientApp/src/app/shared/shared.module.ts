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

// Data Grid
import { DataGridComponent } from './data-grid/data-grid.component';
import { DataGridColumnComponent } from './data-grid/data-grid-column.component';

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

    // Data Grid
    DataGridComponent,
    DataGridColumnComponent,
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

    // Data Grid
    DataGridComponent,
    DataGridColumnComponent,
  ]
})
export class SharedModule { }
