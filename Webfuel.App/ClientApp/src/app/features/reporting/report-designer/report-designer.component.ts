import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, forwardRef, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../core/form.service';
import { ReportSchema, ReportDesign, ReportColumn, ReportFilter } from '../../../api/api.types';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';
import { ReportDesignService } from '../report-design-service/report-design.service';

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
    private reportDesignService: ReportDesignService
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
    this.reportDesignService.addColumn(this.schema, this.design).subscribe(() => {
      this.emitChanges();
    });
  }

  editColumn(column: ReportColumn) {
    this.reportDesignService.editColumn(this.schema, this.design, column).subscribe(() => {
      this.emitChanges();
    });
  }

  deleteColumn(column: ReportColumn) {
    this.reportDesignService.deleteColumn(this.schema, this.design, column).subscribe(() => {
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

  // Filters

  addFilter() {
    this.reportDesignService.addFilter(this.schema, this.design, this.design.filters).subscribe((design) => {
      this.design = design;
      this.emitChanges();
    });
  }

  editFilter(filter: ReportFilter) {
  }

  deleteFilter(filter: ReportFilter) {
    this.reportDesignService.deleteFilter(this.schema, this.design, filter).subscribe(() => {
      this.emitChanges();
    });
  }

  dropFilter($event: any) {
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
