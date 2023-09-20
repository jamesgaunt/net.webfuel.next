import _ from '../underscore';
import { FormGroup, AbstractControl } from "@angular/forms";
import { ErrorType, IError } from "../../api/api.types";
import { IApiErrorHandler } from '../../core/api.service';
import { GrowlService } from '../../core/growl.service';

export interface IFormManagerOptions {
}

export class FormManager<TControl extends {
  [K in keyof TControl]: AbstractControl<any>;
}> implements IApiErrorHandler {

  constructor(
    controls: TControl,
    private growlService: GrowlService,
    private options: IFormManagerOptions) {
    this.formGroup = new FormGroup(controls);
    options = options || {};
  }

  formGroup: FormGroup<TControl>;

  get valid() {
    return this.formGroup.valid;
  }

  markAllAsTouched() {
    this.formGroup.markAllAsTouched();
  }

  getRawValue() {
    return this.formGroup.getRawValue();
  }

  patchValue(value: any) {
    this.formGroup.patchValue(value);
  }

  hasErrors() {
    this.formGroup.markAllAsTouched();

    if (this.formGroup.valid)
      return false;

    this.growlErrors();

    return true;
  }

  // Error Handling

  growlErrors() {
    for (const field in this.formGroup.controls) {
      const control = this.formGroup.get(field);
      if (!control || control.valid)
        continue;

      const fieldName = "'" + _.camelCaseToLabel(field) + "'";

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

  handleError(err: any): void {
    if (err.error && err.error.errorType)
      return this.handleErrorWithType(err.error);
  }

  handleErrorWithType(error: IError): void {
    switch (error.errorType) {
      case ErrorType.ValidationError:
        return this.handleValidationError(error);
    }
  }

  handleValidationError(validationError: IError) {
    _.forEach(validationError.validationErrors, (error) => {
      for (const field in this.formGroup.controls) {
        if (_.compareInsensitive(field, error.property)) {
          const control = this.formGroup.get(field);
          var errors: { [key: string]: any } = {};
          errors[error.message] = true;
          control!.setErrors(errors);
        }
      }
    });
    this.growlErrors();
  }
}
