import { Input, Output, Component, EventEmitter, HostListener, ChangeDetectorRef, Self, Optional, OnChanges, SimpleChanges, OnInit, forwardRef, ChangeDetectionStrategy } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, NgControl } from '@angular/forms';
import { Day } from './Day';
import { first, noop } from 'rxjs';

interface IDay {
  text: string;
  class: string;
  selected: boolean;
  value: Day;
}

@Component({
  selector: 'date-calendar',
  templateUrl: './date-calendar.component.html',
  styleUrls: ['./date-calendar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DateCalendarComponent),
      multi: true
    }
  ]
})
export class DateCalendarComponent implements ControlValueAccessor, OnInit {

  constructor(
    private cd: ChangeDetectorRef
  ) {
  }

  // Lifecycle

  ngOnInit() {
    this.populateDays();
  }

  // Properties

  mode: "day" | "month" | "year" = "day";

  period: Day = Day.today().startOfMonth();

  value?: Day | null = null;

  // Day Mode

  days: Array<Array<IDay>> | null = null;

  populateDays() {
    this.days = [[]];
    var firstDayOfMonth = this.period.startOfMonth();

    var date = firstDayOfMonth.addDays(-firstDayOfMonth.dayOfWeek);
    for (var i = 0; i < 42; i++) {
      if (this.days[this.days.length - 1].length == 7)
        this.days.push([]);
      this.days[this.days.length - 1].push({
        value: date.clone(),
        selected: date.isSame(this.value),
        text: "" + date.dayOfMonth,
        class: "day"
          + (date.isSame(this.value) ? " selected" : "")
          + (date.month == this.period.month ? " active" : " inactive")
      });
      date = date.addDays(1);
    }
    this.cd.detectChanges();
  }

  // Month Mode

  // UI

  setMode(mode: "day" | "month" | "year") {
    this.mode = mode;
  }

  dayTitle() {
    return this.period.format('MMM YYYY');
  }

  monthTitle() {
    return this.period.format('YYYY');
  }

  nextMonth() {
    this.period = this.period.addMonths(1);
    this.populateDays();
  }

  priorMonth() {
    this.period = this.period.addMonths(-1);
    this.populateDays();
  }

  nextYear() {
    this.period = this.period.addYears(1);
    this.populateDays();
  }

  priorYear() {
    this.period = this.period.addYears(-1);
    this.populateDays();
  }

  nextDecade() {
    this.period = this.period.addYears(10);
    this.populateDays();
  }

  priorDecade() {
    this.period = this.period.addYears(-10);
    this.populateDays();
  }

  pickMonth(month: number) {
    this.period = this.period.withMonth(month);
    this.mode = 'day';
    this.populateDays();
  }

  pickDay(day: IDay) {
    this.value = day.value;
    this.populateDays();
    var value = this.value ? this.value.format("YYYY-MM-dd") : null;
    this.onChange(value);
  }

  // ControlValueAccessor

  writeValue(obj: any): void {
    this.value = Day.parse(obj);
    if (this.value == null) {
      this.period = Day.today().startOfMonth();
    } else {
      this.period = this.value.startOfMonth();
    }
    this.populateDays();
  }

  public registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }
  onChange: (value: string | null) => void = noop;

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  onTouched: () => void = noop;

  public setDisabledState?(isDisabled: boolean): void {
    this._isDisabled = isDisabled;
  }
  private _isDisabled = false;
}

