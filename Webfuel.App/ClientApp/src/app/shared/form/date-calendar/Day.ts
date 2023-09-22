import { formatDate } from "@angular/common";

export class Day {

  private constructor(year: number, month: number, day: number) {
    this._year = year;
    this._month = month;
    this._day = day;
  }

  get year() {
    return this._year;
  }
  private _year: number;

  get month() {
    return this._month;
  }
  private _month: number;

  get day() {
    return this._day;
  }
  private _day: number;

  get weekDay() {
    return this.toDate().getDay();
  }

  isSame(day: Day | null | undefined) {
    if (!day)
      return false;
    return day.day === this.day && day.month === this.month && day.year === this.year;
  }

  format(pattern: string) {
    return formatDate(this.toDate(), pattern, "en-GB");
  }

  toISOString() {
    return "!!!";
  }

  toDate() {
    return new Date(this.year, this.month - 1, this.day);
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
    return this.addMonths(this.month - month);
  }

  // Constructors

  static today() {
    return this.fromDate(new Date());
  }

  static fromDate(date: Date) {
    return new Day(date.getFullYear(), date.getMonth() + 1, date.getDate());
  }

  static fromObj(obj: any) {
    return this.today();
  }
}
