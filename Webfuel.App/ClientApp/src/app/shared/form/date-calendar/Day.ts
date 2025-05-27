import { formatDate } from "@angular/common";
import _ from 'shared/common/underscore';

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

  get ticks() {
    return this.toDate().getTime();
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
    var d = new Date(this.year, this.month - 1, this.dayOfMonth);
    return d;
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

static parseUKDate(input: string): string | null {
  // Split by space, slash, or hyphen and filter out empty parts
  const parts = input.trim().split(/[\s\/\-]+/).filter(part => part.length > 0);

  if (parts.length !== 3) {
    return null;
  }

  const [dayStr, monthStr, yearStr] = parts;

  // Parse day
  const day = parseInt(dayStr, 10);
  if (isNaN(day) || day < 1 || day > 31) {
    return null;
  }

  // Parse month
  let month: number;
  const monthLower = monthStr.toLowerCase();

  // Check if it's a number
  const monthNum = parseInt(monthStr, 10);
  if (!isNaN(monthNum)) {
    if (monthNum < 1 || monthNum > 12) {
      return null;
    }
    month = monthNum;
  } else {
    // Month name mapping
    const monthNames: { [key: string]: number } = {
      'jan': 1, 'january': 1,
      'feb': 2, 'february': 2,
      'mar': 3, 'march': 3,
      'apr': 4, 'april': 4,
      'may': 5,
      'jun': 6, 'june': 6,
      'jul': 7, 'july': 7,
      'aug': 8, 'august': 8,
      'sep': 9, 'september': 9,
      'oct': 10, 'october': 10,
      'nov': 11, 'november': 11,
      'dec': 12, 'december': 12
    };

    month = monthNames[monthLower];
    if (!month) {
      return null;
    }
  }

  // Parse year
  let year = parseInt(yearStr, 10);
  if (isNaN(year)) {
    return null;
  }

  // Handle 2-digit years (assume 21st century)
  if (year >= 0 && year <= 99) {
    year += 2000;
  }

  // Basic validation
  if (year < 1000 || year > 9999) {
    return null;
  }

  // Create date to validate day/month combination
  const date = new Date(year, month - 1, day);
  if (date.getFullYear() !== year ||
    date.getMonth() !== month - 1 ||
    date.getDate() !== day) {
    return null;
  }

  // Format as yyyy-mm-dd
  const formattedMonth = month.toString().padStart(2, '0');
  const formattedDay = day.toString().padStart(2, '0');

  return `${year}-${formattedMonth}-${formattedDay}`;
}

}
