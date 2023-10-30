import { Injectable } from '@angular/core';
import { GrowlService } from './growl.service';
import { FormGroup, AbstractControl } from "@angular/forms";
import _ from 'shared/common/underscore';

@Injectable()
export class FormService {

  constructor(
    private growlService: GrowlService
  ) { }

  // TODO: Track active form somehow?

  checkForErrors(form: FormGroup) {
    if (form.valid)
      return false;

    form.markAllAsTouched();
    this.growlErrors(form);
    return true;
  }

  growlErrors(form: FormGroup) {
    for (const field in form.controls) {
      const control = form.get(field);
      if (!control || control.valid)
        continue;

      const fieldName = _.camelCaseToLabel(field);

      for (const error in control.errors) {
        var validator = control.errors[error] || {};

        if (error == "required") {
          this.growlService.growlDanger(`${fieldName} is required`);
        }
        else if (error == "max") {
          this.growlService.growlDanger(`${fieldName} must be less than or equal to '${validator.max}'`)
        }
        else if (error == "min") {
          this.growlService.growlDanger(`${fieldName} must be greater than or equal to '${validator.min}'`)
        }
        else if (error.indexOf(fieldName) > 0) {
          this.growlService.growlDanger(`${error}`);
        }
        else {
          this.growlService.growlDanger(`${fieldName} ${error}`);
        }
      }
    }
  }
  /*
  applyValidationError(form: FormGroup, validationError: ErrorResponse) {
    _.forEach(validationError.validationErrors, (error: ValidationError) => {
      for (const field in form.controls) {
        if (_.compareInsensitive(field, error.property)) {
          const control = form.get(field);
          var errors: { [key: string]: any } = {};
          errors[error.message] = true;
          control!.setErrors(errors);
        }
      }
    });
    this.growlErrors(form);
  }
  */
}
