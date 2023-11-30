import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, forwardRef, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../core/form.service';
import { IReportSchema, ReportDesign } from '../../../api/api.types';
import { AddReportColumnDialog } from './dialogs/add-report-column/add-report-column.dialog';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'report-designer',
  templateUrl: './report-designer.component.html',
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
    private addReportColumnDialog: AddReportColumnDialog
  ) {
  }

  ngOnInit() {
  }

  @Input({ required: true })
  schema!: IReportSchema;

  // Design

  design!: ReportDesign;

  addColumn() {
    this.addReportColumnDialog.open({ schema: this.schema, design: this.design }).subscribe(() => {
      this.emitChanges();
    });
  }

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.design));
  }

  onChange: (value: ReportDesign) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportDesign | null): void {
    if (value == null) {
      this.design = {
        columns: [],
      }
    } else {
      this.design = _.deepClone(value);
    }
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
