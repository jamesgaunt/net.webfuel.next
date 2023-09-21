import { Injectable } from '@angular/core';
import { GrowlService } from './growl.service';

@Injectable()
export class ErrorService {

  constructor(
    private growlService: GrowlService
  ) { }
}
