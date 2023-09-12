import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

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

    // Form
    TextInputComponent,
    NumberInputComponent,
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,

    // Form
    TextInputComponent,
    NumberInputComponent,
  ]
})
export class SharedModule { }
