import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, forwardRef, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../core/form.service';
import { ReportSchema, ReportDesign, ReportColumn } from '../../../api/api.types';
import { AddReportColumnDialog } from './dialogs/add-report-column/add-report-column.dialog';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';
import { ConfirmDeleteDialog } from '../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { EditReportColumnDialog } from './dialogs/edit-report-column/edit-report-column';

@Component({
  selector: 'report-designer',
  templateUrl: './report-designer.component.html',
  styleUrls: [ './report-designer.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ReportDesignerComponent),
      multi: true
    }
  ]
})
export class ReportDesignerComponent implements ControlValueAccessor, OnInit {

  cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private addReportColumnDialog: AddReportColumnDialog,
    private editReportColumnDialog: EditReportColumnDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
  ) {
  }

  ngOnInit() {
  }

  @Input({ required: true })
  schema!: ReportSchema;

  // Design

  design!: ReportDesign;

  // Columns

  addColumn() {
    this.addReportColumnDialog.open({ schema: this.schema, design: this.design }).subscribe(() => {
      this.emitChanges();
    });
  }

  editColumn(column: ReportColumn) {
    this.editReportColumnDialog.open({ column: column }).subscribe((result) => {
      this.design.columns[this.design.columns.findIndex(p => p.fieldId == column.fieldId)] = result;
      this.emitChanges();
    });
  }

  deleteColumn(column: ReportColumn) {
    this.confirmDeleteDialog.open({ title: "Delete Column" }).subscribe(() => {
      this.design.columns = this.design.columns.filter(p => p.fieldId != column.fieldId);
      this.emitChanges();
    });
  }

  dropColumn($event: any) {
    var currentIndex = <number>$event.currentIndex;
    var previousIndex = <number>$event.previousIndex;

    // Client Side
    const item = this.design.columns.splice(previousIndex, 1);
    this.design.columns.splice(currentIndex, 0, item[0]);

    this.emitChanges();
  }

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.design));
  }

  onChange: (value: ReportDesign) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportDesign): void {
    this.design = _.deepClone(value);
    this.cd.detectChanges();
  }

  public registerOnChange(fn: (value: ReportDesign) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
    this.cd.detectChanges();
  }
  isDisabled = false;
}
