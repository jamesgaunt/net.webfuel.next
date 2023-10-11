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

  // Collection Operators

  static find<T>(arr: Array<T>, iter: (item: T) => boolean): T | undefined {
    if (!arr)
      return undefined;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === true)
        return arr[i];
    }
    return undefined;
  }

  static findIndex<T>(arr: Array<T>, iter: (item: T) => boolean) {
    if (!arr)
      return -1;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === true)
        return i;
    }
    return -1;
  }

  static some<T>(arr: Array<T>, iter: (item: T) => boolean) {
    if (!arr)
      return false;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === true)
        return true;
    }
    return false;
  }

  static includes<T>(arr: Array<T>, item: T) {
    return Underscore.some(arr, (p) => p == item);
  }

  static none<T>(arr: Array<T>, iter: (item: T) => boolean) {
    if (!arr)
      return true;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === true)
        return false;
    }
    return true;
  }

  static all<T>(arr: Array<T>, iter: (item: T) => boolean) {
    if (!arr)
      return false;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === false)
        return false;
    }
    return true;
  }

  static forEach<T>(arr: Array<T>, iter: (item: T) => any) {
    if (!arr)
      return;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === false)
        break;
    }
  }

  static filter<T>(arr: Array<T>, iter: (item: T) => boolean) {
    var res: Array<T> = [];
    if (!arr)
      return res;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) === true)
        res.push(arr[i]);
    }
    return res;
  }

  static splice<T>(arr: Array<T>, iter: (item: T) => boolean) {
    var index = this.findIndex(arr, iter);
    if (index >= 0)
      arr.splice(index, 1);
    return arr;
  }

  static remove<T>(arr: Array<T>, iter: (item: T) => boolean) {
    var res: Array<T> = [];
    if (!arr)
      return res;
    for (var i = 0; i < arr.length; i++) {
      if (iter(arr[i]) !== true)
        res.push(arr[i]);
    }
    return res;
  }

  static removeAt<T>(arr: Array<T>, index: number) {
    var res: Array<T> = [];
    if (!arr)
      return res;
    for (var i = 0; i < arr.length; i++) {
      if (i !== index)
        res.push(arr[i]);
    }
    return res;
  }

  static sortBy<T>(arr: Array<T>, iter: (item: T) => any) {
    if (!arr)
      return [];
    return arr.sort((a, b) => {
      var ai = iter(a);
      var bi = iter(b);
      if (ai > bi)
        return 1;
      if (bi > ai)
        return -1;
      return 0;
    });
  }

  static map<T, A>(arr: Array<T>, iter: (item: T) => A) {
    if (!arr)
      return [];
    var result: Array<A> = new Array(arr.length);
    for (var i = 0; i < arr.length; i++) {
      result[i] = iter(arr[i]);
    }
    return result;
  }

  static reduce<T, A>(arr: Array<T>, func: (item: T, acc: A) => A, init: A) {
    if (!arr)
      return init;
    for (var i = 0; i < arr.length; i++) {
      init = func(arr[i], init);
    }
    return init;
  }

  // Merge

  static merge<T0, T1>(_0: T0, _1: T1): T0 & T1 {
    return { ..._0, ..._1 };
  }

  // String

  static compareInsensitive(string1: string, string2: string) {
    if (string1 === string2)
      return true;
    if (!string1 || !string2)
      return false;
    return string1.toUpperCase() === string2.toUpperCase();
  }

  static capitalise(input: string) {
    if (!input)
      return input;
    return input[0].toUpperCase() + input.slice(1);
  }

  static camelCaseToLabel(input: string) {
    return this.capitalise(this.splitCamelCase(input));
  }

  static splitCamelCase(input: string) {
    var output = [], i, l, capRe = /[A-Z]/;
    for (i = 0, l = input.length; i < l; i += 1) {
      if (i === 0) {
        output.push(input[i].toUpperCase());
      }
      else {
        if (i > 0 && capRe.test(input[i])) {
          output.push(" ");
        }
        output.push(input[i]);
      }
    }
    return output.join("");
  }

  static nameToCode(name: string) {
    if (!name)
      return name;

    var code = "";
    var inWord = false;

    for (var i = 0; i < name.length; i++) {
      var c = name[i];

      if (c.match(/[a-zA-Z0-9]/)) {
        if (!inWord) {
          c = c.toUpperCase();
          if (code.length > 0)
            code += "_";
        }

        code += c;
        inWord = true;

      } else {
        inWord = false;
      }
    }
    return code;
  }

  static nameToPath(name: string) {
    if (!name)
      return name;
    var path = "/";
    var valid = false;
    var inTag = false;
    for (var i = 0; i < name.length; i++) {
      var c = name[i];
      if (inTag) {
        if (c == '>') {
          inTag = false;
        }
      } else {
        if (c == '<') {
          inTag = true;
        } else if (c.match(/[a-zA-Z0-9]/)) {
          path += c.toLowerCase();
          valid = true;
        } else if (valid) {
          path += "-";
          valid = false;
        }
      }
    }
    if (path[path.length - 1] == "-")
      path = path.substring(0, path.length - 1);
    return path;
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

  // Miscellaneous 

  static lastName(name: string) {
    if (!name)
      return "";
    var parts = name.split(' ');
    if (parts.length == 0)
      return "";
    return parts[parts.length - 1];
  }

  static range(size: number): Array<number> {
    if (size <= 0)
      return [];
    var result = Array(size);
    for (var i = 0; i < size; i++)
      result[i] = i;
    return result;
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
