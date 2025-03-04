import { Injectable } from '@angular/core';
import { GrowlService } from './growl.service';
import _ from 'shared/common/underscore';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ValidationError, ValidationProblemDetails } from '../api/api.types';
import { environment } from '../../environments/environment';

interface IError {
  type: string;
  title: string;
  detail: string;
  status: number;
}

@Injectable()
export class ErrorService {

  constructor(
    private growlService: GrowlService,
    private router: Router
  ) { }

  interceptError(err: HttpErrorResponse) {

    if (err.status == 0) {
      this.growlService.growlDanger("Status 0. API is probably down! API Host: " + environment.apiHost);
      return;
    }

    var error = this._extractError(err);

    if (!error) {
      this.growlService.growlDanger("Unable to extract error from HTTP response");
      return;
    }

    if (error.type == "/not-authorized") {
      this.router.navigateByUrl("/login");
      return;
    }

    if (error.type == "/validation-errors") {
      const e = <ValidationProblemDetails>error;
      if (e.validationErrors.length > 0) {
        _.forEach(e.validationErrors, (v) => {
          this.growlService.growlDanger(v.message);
        });
        return;
      }
    }

    this.growlService.growlDanger(error.title);
  }

  public extractErrorMessage(err: HttpErrorResponse) {
    var error = this._extractError(err);
    if (!error)
      return "Unrecognised error";
    return error.title;
  }

  private _extractError(err: HttpErrorResponse): IError | undefined {
    if (this._testForError(err.error))
      return <IError>(err.error);
    return undefined;
  }

  private _testForError(a: any) {
    if (!a)
      return false;
    return _.isString(a.type) && _.isString(a.title) && _.isNumber(a.status);
  }

}
