import { DialogModule } from '@angular/cdk/dialog';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { OverlayModule } from '@angular/cdk/overlay';
import { TextFieldModule } from '@angular/cdk/text-field';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgxTinymceModule } from 'ngx-tinymce';

// Chrome
import { ChromeComponent } from './chrome/chrome.component';

// Form
import { DateCalendarComponent } from './form/date-calendar/date-calendar.component';
import { DatePickerComponent } from './form/date-picker/date-picker.component';
import { DropDownMultiSelectComponent } from './form/dropdown/dropdown-multi-select/dropdown-multi-select.component';
import { DropDownSelectComponent } from './form/dropdown/dropdown-select/dropdown-select.component';
import { DropDownTextInputComponent } from './form/dropdown/dropdown-text-input/dropdown-text-input.component';
import { FileInputComponent } from './form/file-input/file-input.component';
import { NumberInputComponent } from './form/number-input/number-input.component';
import { TextAreaComponent } from './form/text-area/text-area.component';
import { TextInputComponent } from './form/text-input/text-input.component';
import { ToggleInputComponent } from './form/toggle-input/toggle-input.component';
import { HtmlEditorComponent } from './html-editor/html-editor.component';

// Grid
import { GridActionColumnComponent } from './grid/columns/grid-action-column.component';
import { GridBooleanColumnComponent } from './grid/columns/grid-boolean-column.component';
import { GridColumnComponent } from './grid/columns/grid-column.component';
import { GridDateColumnComponent } from './grid/columns/grid-date-column.component';
import { GridDateTimeColumnComponent } from './grid/columns/grid-datetime-column.component';
import { GridNumberColumnComponent } from './grid/columns/grid-number-column.component';
import { GridReferenceColumnComponent } from './grid/columns/grid-reference-column.component';
import { GridComponent } from './grid/grid.component';

// Directives
import { AutocompleteCodeDirective } from './directives/autocomplete-code.directive';

// Misc
import { FileBrowserComponent } from './file-browser/file-browser.component';

// Dialogs
import { AlertDialog, AlertDialogComponent } from './dialogs/alert/alert.dialog';
import {
  AttachProjectSupportFilesDialog,
  AttachProjectSupportFilesDialogComponent,
} from './dialogs/attach-project-support-files/attach-project-support-files.dialog';
import { ChangePasswordDialog, ChangePasswordDialogComponent } from './dialogs/change-password/change-password.dialog';
import { ConfirmDeactivateDialog, ConfirmDeactivateDialogComponent } from './dialogs/confirm-deactivate/confirm-deactivate.dialog';
import { ConfirmDeleteDialog, ConfirmDeleteDialogComponent } from './dialogs/confirm-delete/confirm-delete.dialog';
import { ConfirmDialog, ConfirmDialogComponent } from './dialogs/confirm/confirm.dialog';
import { CreateUserActivityDialog, CreateUserActivityDialogComponent } from './dialogs/create-user-activity/create-user-activity.dialog';
import { DatePickerDialog, DatePickerDialogComponent } from './dialogs/date-picker/date-picker.dialog';
import { FileViewerDialog, FileViewerDialogComponent } from './dialogs/file-viewer/file-viewer.dialog';
import { RunAnnualReportDialog, RunAnnualReportDialogComponent } from './dialogs/run-annual-report/run-annual-report.dialog';
import { SendEmailDialog, SendEmailDialogComponent } from './dialogs/send-email/send-email.dialog';
import { UpdateUserActivityDialog, UpdateUserActivityDialogComponent } from './dialogs/update-user-activity/update-user-activity.dialog';
import {
  UploadProjectSupportFilesDialog,
  UploadProjectSupportFilesDialogComponent,
} from './dialogs/upload-project-support-files/upload-project-support-files.dialog';
import { FileEditorDialogComponent, FileEditorDialog } from './dialogs/file-editor/file-editor.dialog';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DialogModule,
    DragDropModule,
    TextFieldModule,
    NgxTinymceModule.forRoot({
      baseURL: '//cdnjs.cloudflare.com/ajax/libs/tinymce/7.2.0/',
    }),
  ],
  declarations: [
    // Chrome
    ChromeComponent,

    // Form
    TextInputComponent,
    TextAreaComponent,
    NumberInputComponent,
    DropDownSelectComponent,
    DropDownMultiSelectComponent,
    DropDownTextInputComponent,
    DateCalendarComponent,
    DatePickerComponent,
    ToggleInputComponent,
    FileInputComponent,
    HtmlEditorComponent,

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

    // Misc
    FileBrowserComponent,

    // Dialogs
    ChangePasswordDialogComponent,
    ConfirmDialogComponent,
    ConfirmDeleteDialogComponent,
    ConfirmDeactivateDialogComponent,
    DatePickerDialogComponent,
    CreateUserActivityDialogComponent,
    UpdateUserActivityDialogComponent,
    FileViewerDialogComponent,
    FileEditorDialogComponent,
    AlertDialogComponent,
    RunAnnualReportDialogComponent,
    SendEmailDialogComponent,
    AttachProjectSupportFilesDialogComponent,
    UploadProjectSupportFilesDialogComponent,
  ],
  exports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    OverlayModule,
    DragDropModule,
    DialogModule,
    TextFieldModule,

    // Chrome
    ChromeComponent,

    // Form
    TextInputComponent,
    TextAreaComponent,
    NumberInputComponent,
    DropDownSelectComponent,
    DropDownMultiSelectComponent,
    DropDownTextInputComponent,
    DateCalendarComponent,
    DatePickerComponent,
    ToggleInputComponent,
    FileInputComponent,
    HtmlEditorComponent,

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

    // Misc
    FileBrowserComponent,

    // Dialogs
    ChangePasswordDialogComponent,
    ConfirmDialogComponent,
    ConfirmDeleteDialogComponent,
    ConfirmDeactivateDialogComponent,
    DatePickerDialogComponent,
    CreateUserActivityDialogComponent,
    UpdateUserActivityDialogComponent,
    FileViewerDialogComponent,
    FileEditorDialogComponent,
    AlertDialogComponent,
    RunAnnualReportDialogComponent,
    AttachProjectSupportFilesDialogComponent,
    UploadProjectSupportFilesDialogComponent,
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
    FileEditorDialog,
    AlertDialog,
    RunAnnualReportDialog,
    SendEmailDialog,
    AttachProjectSupportFilesDialog,
    UploadProjectSupportFilesDialog,
  ],
})
export class SharedModule {}
