import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnInit, forwardRef, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../core/form.service';
import { ReportSchema, ReportDesign, ReportColumn, ReportFilter } from '../../../api/api.types';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';
import { ReportDesignService } from '../report-design-service/report-design.service';
import { ReportFilterType } from '../../../api/api.enums';

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

  ReportFilterType = ReportFilterType;

  @Input({ required: true })
  schema!: ReportSchema;

  // Design

  design!: ReportDesign;

  // Columns

  addColumn() {
    this.reportDesignService.insertColumn(this.schema, this.design).subscribe((design) => {
      this.design = design;
      this.emitChanges();
    });
  }

  editColumn(column: ReportColumn) {
    this.reportDesignService.updateColumn(this.schema, this.design, column).subscribe((design) => {
      this.design = design;
      this.emitChanges();
    });
  }

  deleteColumn(column: ReportColumn) {
    this.reportDesignService.deleteColumn(this.schema, this.design, column).subscribe((design) => {
      this.design = design;
      this.emitChanges();
    });
  }

  dropColumn($event: any) {
    var currentIndex = <number>$event.currentIndex;
    var previousIndex = <number>$event.previousIndex;

    // Client side only, reordring stuff can't really break anything
    const item = this.design.columns.splice(previousIndex, 1);
    this.design.columns.splice(currentIndex, 0, item[0]);

    this.emitChanges();
  }

  // Filters

  addFilter(parentId?: string) {
    this.reportDesignService.insertFilter(this.schema, this.design, parentId || null).subscribe((design) => {
      this.design = design;
      this.emitChanges();
   });
  }

  editFilter(filter: ReportFilter) {
    this.reportDesignService.updateFilter(this.schema, this.design, filter).subscribe((design) => {
      this.design = design;
      this.emitChanges();
    });
  }

  deleteFilter(filter: ReportFilter) {
    this.reportDesignService.deleteFilter(this.schema, this.design, filter).subscribe((design) => {
      this.design = design;
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
