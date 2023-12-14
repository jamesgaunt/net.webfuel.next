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
import { FileInputComponent } from './form/file-input/file-input.component';

// Grid
import { GridComponent } from './grid/grid.component';
import { GridColumnComponent } from './grid/columns/grid-column.component';
import { GridActionColumnComponent } from './grid/columns/grid-action-column.component';
import { GridDateColumnComponent } from './grid/columns/grid-date-column.component';
import { GridNumberColumnComponent } from './grid/columns/grid-number-column.component';
import { GridBooleanColumnComponent } from './grid/columns/grid-boolean-column.component';
import { GridDateTimeColumnComponent } from './grid/columns/grid-datetime-column.component';
import { GridReferenceColumnComponent } from './grid/columns/grid-reference-column.component';

// Directives
import { AutocompleteCodeDirective } from './directives/autocomplete-code.directive';

// Dialogs
import { ChangePasswordDialog, ChangePasswordDialogComponent } from './dialogs/change-password/change-password.dialog';
import { ConfirmDialog, ConfirmDialogComponent } from './dialogs/confirm/confirm.dialog';
import { ConfirmDeleteDialog, ConfirmDeleteDialogComponent } from './dialogs/confirm-delete/confirm-delete.dialog';
import { ConfirmDeactivateDialog, ConfirmDeactivateDialogComponent } from './dialogs/confirm-deactivate/confirm-deactivate.dialog';
import { DatePickerDialog, DatePickerDialogComponent } from './dialogs/date-picker/date-picker.dialog';
import { CreateUserActivityDialog, CreateUserActivityDialogComponent } from './dialogs/create-user-activity/create-user-activity.dialog';
import { UpdateUserActivityDialog, UpdateUserActivityDialogComponent } from './dialogs/update-user-activity/update-user-activity.dialog';
import { FileViewerDialog, FileViewerDialogComponent } from './dialogs/file-viewer/file-viewer.dialog';
import { AlertDialog, AlertDialogComponent } from './dialogs/alert/alert.dialog';



// Misc
import { FileBrowserComponent } from './file-browser/file-browser.component';

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
    FileInputComponent,

    // Grid
    GridComponent,
    GridColumnComponent,
    GridActionColumnComponent,
    GridDateColumnComponent,
    GridNumberColumnComponent,
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
    CreateUserActivityDialogComponent,
    UpdateUserActivityDialogComponent,
    FileViewerDialogComponent,
    AlertDialogComponent,

    // Misc
    FileBrowserComponent
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DragDropModule,
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
    FileInputComponent,

    // Grid
    GridComponent,
    GridColumnComponent,
    GridActionColumnComponent,
    GridDateColumnComponent,
    GridNumberColumnComponent,
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
    CreateUserActivityDialogComponent,
    UpdateUserActivityDialogComponent,
    FileViewerDialogComponent,
    AlertDialogComponent,

    // Misc
    FileBrowserComponent
  ],
  providers: [
    ChangePasswordDialog,
    ConfirmDialog,
    ConfirmDeleteDialog,
    ConfirmDeactivateDialog,
    DatePickerDialog,
    CreateUserActivityDialog,
    UpdateUserActivityDialog,
    FileViewerDialog,
    AlertDialog,
  ]
})
export class SharedModule { }
