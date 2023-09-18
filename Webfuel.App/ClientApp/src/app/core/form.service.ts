import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { GrowlService } from './growl.service';
import { FormGroup, AbstractControl } from "@angular/forms";
import { FormManager, IFormManagerOptions } from '../shared/form/form-manager';

@Injectable()
export class FormService {

  constructor(
    private growlService: GrowlService
  ) { }

  buildManager<TControl extends {
    [K in keyof TControl]: AbstractControl<any>;
  }>(controls: TControl, options?: IFormManagerOptions) {
    return new FormManager(controls, this.growlService, options || {});
  }
}
