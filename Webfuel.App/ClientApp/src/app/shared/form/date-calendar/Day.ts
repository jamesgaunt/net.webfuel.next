import { formatDate } from "@angular/common";
import _ from '../../underscore';

export class Day {

  private constructor(year: number, month: number, day: number) {
    this._year = year;
    this._month = month;
    this._dayOfMonth = day;
  }

  get year() {
    return this._year;
  }
  private _year: number;

  get month() {
    return this._month;
  }
  private _month: number;

  get dayOfMonth() {
    return this._dayOfMonth;
  }
  private _dayOfMonth: number;

  get dayOfWeek() {
    return this.toDate().getDay();
  }

  isSame(day: Day | null | undefined) {
    if (!_.isObject(day))
      return false;
    return day!.dayOfMonth === this.dayOfMonth && day!.month === this.month && day!.year === this.year;
  }

  format(pattern: string) {
    return formatDate(this.toDate(), pattern, "en-GB");
  }

  toDate() {
    return new Date(this.year, this.month - 1, this.dayOfMonth);
  }

  clone() {
    return Day.fromDate(this.toDate());
  }

  startOfMonth() {
    var date = this.toDate()
    var newDate = new Date(date.getFullYear(), date.getMonth(), 1);
    return Day.fromDate(newDate);
  }

  endOfMonth() {
    var date = this.toDate()
    var newDate = new Date(date.getFullYear(), date.getMonth() + 1, 0);
    return Day.fromDate(newDate);
  }

  addDays(offset: number) {
    var date = this.toDate()
    var newDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() + offset);
    return Day.fromDate(newDate);
  }

  addMonths(offset: number) {
    var date = this.toDate()
    var newDate = new Date(date.getFullYear(), date.getMonth() + offset, date.getDate());
    return Day.fromDate(newDate);
  }

  addYears(offset: number) {
    var date = this.toDate()
    var newDate = new Date(date.getFullYear() + offset, date.getMonth(), date.getDate());
    return Day.fromDate(newDate);
  }

  withMonth(month: number) {
    var offset = month - this.month;
    return this.addMonths(offset);
  }

  // Constructors

  static today() {
    return this.fromDate(new Date());
  }

  static fromDate(date: Date) {
    return new Day(date.getFullYear(), date.getMonth() + 1, date.getDate());
  }

  static parse(obj?: any): Day | null {
    if (!obj)
      return null;

    if (obj instanceof Date)
      return this.fromDate(obj);

    if (!_.isString(obj))
      return null;

    var text = "" + obj;

    if (text.indexOf('T') > 0)
      text = text.split('T')[0];

    var parts = text.split("-");
    if (parts.length != 3)
      return null;

    var y = _.parseNumber(parts[0], false);
    var m = _.parseNumber(parts[1], false);
    var d = _.parseNumber(parts[2], false);

    if (y === null || m === null || d === null)
      return null;

    return new Day(y, m, d);
  }
}
