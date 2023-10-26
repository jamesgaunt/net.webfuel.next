import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { OverlayModule } from '@angular/cdk/overlay';
import { DialogModule } from '@angular/cdk/dialog';
import { DragDropModule } from '@angular/cdk/drag-drop';

// Chrome
import { ChromeComponent } from './chrome/chrome.component';

// Form
import { TextInputComponent } from './form/text-input/text-input.component';
import { NumberInputComponent } from './form/number-input/number-input.component';
import { DropDownSelectComponent } from './form/dropdown-select/dropdown-select.component';
import { DropDownMultiSelectComponent } from './form/dropdown-multi-select/dropdown-multi-select.component';
import { DateCalendarComponent } from './form/date-calendar/date-calendar.component';
import { DatePickerComponent } from './form/date-picker/date-picker.component';
import { DropDownTextInputComponent } from './form/dropdown-text-input/dropdown-text-input.component';
import { ToggleInputComponent } from './form/toggle-input/toggle-input.component';

// Grid
import { GridComponent } from './grid/grid.component';
import { GridColumnComponent } from './grid/columns/grid-column.component';
import { GridActionColumnComponent } from './grid/columns/grid-action-column.component';
import { GridDateColumnComponent } from './grid/columns/grid-date-column.component';
import { GridBooleanColumnComponent } from './grid/columns/grid-boolean-column.component';
import { GridDateTimeColumnComponent } from './grid/columns/grid-datetime-column.component';
import { GridReferenceColumnComponent } from './grid/columns/grid-reference-column.component';

// Directives
import { AutocompleteCodeDirective } from './directives/autocomplete-code.directive';

// Dialogs
import { ChangePasswordDialogComponent, ChangePasswordDialogService } from './dialogs/change-password-dialog/change-password-dialog.component';
import { ConfirmDialogComponent, ConfirmDialogService } from './dialogs/confirm-dialog/confirm-dialog.component';
import { ConfirmDeleteDialogComponent, ConfirmDeleteDialogService } from './dialogs/confirm-delete/confirm-delete-dialog.component';
import { ConfirmDeactivateDialogComponent, ConfirmDeactivateDialogService } from './dialogs/confirm-deactivate/confirm-deactivate-dialog.component';
import { DatePickerDialogComponent, DatePickerDialogService } from './dialogs/date-picker-dialog/date-picker-dialog.component';
import { UserActivityCreateDialogComponent, UserActivityCreateDialogService } from './dialogs/user-activity-create-dialog/user-activity-create-dialog.component';
import { UserActivityUpdateDialogComponent, UserActivityUpdateDialogService } from './dialogs/user-activity-update-dialog/user-activity-update-dialog.component';


@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DialogModule,
    DragDropModule,
  ],
  declarations: [

    // Chrome
    ChromeComponent,

    // Form
    TextInputComponent,
    NumberInputComponent,
    DropDownSelectComponent,
    DropDownMultiSelectComponent,
    DropDownTextInputComponent,
    DateCalendarComponent,
    DatePickerComponent,
    ToggleInputComponent,

    // Grid
    GridComponent,
    GridColumnComponent,
    GridActionColumnComponent,
    GridDateColumnComponent,
    GridBooleanColumnComponent,
    GridDateTimeColumnComponent,
    GridReferenceColumnComponent,

    // Directives
    AutocompleteCodeDirective,

    // Dialogs
    ChangePasswordDialogComponent,
    ConfirmDialogComponent,
    ConfirmDeleteDialogComponent,
    ConfirmDeactivateDialogComponent,
    DatePickerDialogComponent,
    UserActivityCreateDialogComponent,
    UserActivityUpdateDialogComponent,
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DialogModule,

    // Chrome
    ChromeComponent,

    // Form
    TextInputComponent,
    NumberInputComponent,
    DropDownSelectComponent,
    DropDownMultiSelectComponent,
    DropDownTextInputComponent,
    DateCalendarComponent,
    DatePickerComponent,
    ToggleInputComponent,

    // Grid
    GridComponent,
    GridColumnComponent,
    GridActionColumnComponent,
    GridDateColumnComponent,
    GridBooleanColumnComponent,
    GridDateTimeColumnComponent,
    GridReferenceColumnComponent,

    // Directives
    AutocompleteCodeDirective,

    // Dialogs
    ChangePasswordDialogComponent,
    ConfirmDialogComponent,
    ConfirmDeleteDialogComponent,
    ConfirmDeactivateDialogComponent,
    DatePickerDialogComponent,
    UserActivityCreateDialogComponent,
    UserActivityUpdateDialogComponent,
  ],
  providers: [
    ChangePasswordDialogService,
    ConfirmDialogService,
    ConfirmDeleteDialogService,
    ConfirmDeactivateDialogService,
    DatePickerDialogService,
    UserActivityCreateDialogService,
    UserActivityUpdateDialogService,
  ]
})
export class SharedModule { }
