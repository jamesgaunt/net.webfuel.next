import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { ChromeComponent } from './chrome/chrome.component';

// Form
import { TextInputComponent } from './form/text-input/text-input.component';
import { NumberInputComponent } from './form/number-input/number-input.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  declarations: [

    ChromeComponent,

    // Form
    TextInputComponent,
    NumberInputComponent,
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,

    ChromeComponent,

    // Form
    TextInputComponent,
    NumberInputComponent,
  ]
})
export class SharedModule { }
