import { Input, Output, Component, EventEmitter, HostListener, ChangeDetectorRef, Self, Optional, OnChanges, SimpleChanges, OnInit, forwardRef, ChangeDetectionStrategy } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, NgControl } from '@angular/forms';
import { Day } from './Day';
import { first } from 'rxjs';

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

    var date = firstDayOfMonth.addDays(-firstDayOfMonth.weekDay);
    for (var i = 0; i < 42; i++) {
      if (this.days[this.days.length - 1].length == 7)
        this.days.push([]);
      this.days[this.days.length - 1].push({
        value: date.clone(),
        selected: this.value !== null && date.isSame(this.value),
        text: "" + date.day,
        class: "day"
          + (this.value !== null && date.isSame(this.value) ? " selected" : "")
          + (date.month == this.period.month ? " active" : " inactive")
      });
      date = date.addDays(1);
    }
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

    var value = this.value ? this.value.toISOString() : null;
    this.onChange(value);
  }

  // ControlValueAccessor

  writeValue(obj: any): void {
    if (!obj) {
      this.value = null;
      this.period = Day.today().startOfMonth();
    } else {
      this.value = Day.fromObj(obj);
      this.period = this.value.startOfMonth();
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  private onChange: (_: any) => void = () => { };

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  private onTouched: () => void = () => { };

  setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
  isDisabled: boolean = false;
}

