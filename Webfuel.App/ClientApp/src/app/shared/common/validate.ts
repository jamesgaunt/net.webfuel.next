import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import _ from 'shared/common/underscore';

export class Validate {


  static minArrayLength(min: number): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (_.isArray(control.value) && control.value.length >= min)
        return null;
      return { minArrayLength: true };
    };
  }


}


