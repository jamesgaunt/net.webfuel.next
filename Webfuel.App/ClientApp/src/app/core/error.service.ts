import { Injectable } from '@angular/core';
import { GrowlService } from './growl.service';
import _ from 'shared/common/underscore';

@Injectable()
export class ErrorService {

  constructor(
    private growlService: GrowlService
  ) { }

  interceptError(error: any) {
    this.growlService.growlDanger(error.title);
  }
}
