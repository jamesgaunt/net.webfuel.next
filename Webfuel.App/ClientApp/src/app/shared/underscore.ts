
interface ILocalStorageItem {
  value: any;
  expiry: number | undefined;
}

export default class Underscore {

  // Is Operators

  static isString(value: any) {
    return (typeof value === 'string' || value instanceof String);
  }

  static isArray(value: any) {
    return !!(value && typeof value != 'function' && typeof value.length == 'number');
  }

  static isFunction(value: any) {
    return !!(value && value.constructor && value.call && value.apply);
  }

  static isNumber(value: any) {
    return (typeof value === 'number' && isFinite(value));
  }

  static isObject(value: any) {
    return (typeof value === 'object' && value !== null && !Underscore.isFunction(value));
  }

  // Format / Parse Number

  static formatNumber(n: number | null, dp: number): string {
    if (n == null || !Underscore.isNumber(n))
      return "";

    var c = dp,
      d = ".",
      t = ",",
      s = n < 0 ? "-" : "",
      i = parseInt(n = <any>Math.abs(+n || 0).toFixed(c)) + "",
      j: number = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n! - <any>i).toFixed(c).slice(2) : "");
  }

  static parseNumber(value: any, forceZero: boolean): number | null {
    if (Underscore.isString(value)) {
      var text = ("" + value).replace(/[^\d\.\-]/g, "");
      if (text == "")
        return forceZero ? 0 : null;

      var number = parseFloat(text);
      if (isNaN(number))
        return forceZero ? 0 : null;

      return number;
    }

    if (Underscore.isNumber(value))
      return <number>value;

    return forceZero ? 0 : null;
  }


  // Local Storage

  private static _localStoragePrefix = "20180921:";

  private static _generateLocalStorageKey(key: string) {
    return this._localStoragePrefix + key;
  }

  static getLocalStorage(key: string): any {
    if (!key)
      return;

    var jsonValue = localStorage.getItem(this._generateLocalStorageKey(key));
    if (jsonValue == undefined)
      return undefined;

    try {
      var value: ILocalStorageItem = JSON.parse(jsonValue);

      if (value.expiry && value.expiry < new Date().getTime()) {
        return undefined;
      }

      if (value && value.value != undefined)
        return value.value;

      return undefined;
    }
    catch (ex) {
      return undefined;
    }
  }

  static setLocalStorage(key: string, value: any, expirySeconds?: number) {
    if (!key)
      return;

    if (expirySeconds)
      expirySeconds = new Date().getTime() + expirySeconds * 1000.0;

    if (value === undefined) {
      localStorage.removeItem(this._generateLocalStorageKey(key));
    } else {
      var jsonValue = JSON.stringify({ value: value, expiry: expirySeconds });
      localStorage.setItem(this._generateLocalStorageKey(key), jsonValue);
    }
  }

}
