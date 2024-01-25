import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import _ from 'shared/common/underscore';
import { Day } from "../form/date-calendar/Day";

export class Validate {


  static minArrayLength(min: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (_.isArray(control.value) && control.value.length >= min)
        return null;
      return { minArrayLength: true };
    };
  }

  static dateMustBeInFuture(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      var day = Day.parse(control.value);
      console.log(day?.format('dd-MMM-yyyy'));
      if (!day || Day.today().ticks < day.ticks)
        return null;

      return { dateMustBeInFuture: true };
    };
  }

}


