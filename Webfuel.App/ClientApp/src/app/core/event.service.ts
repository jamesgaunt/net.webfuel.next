import { EventEmitter, Injectable } from '@angular/core';
import _ from '../shared/underscore';

@Injectable()
export class EventService {

  constructor(
  ) { }

  identityChanged = new EventEmitter<any>();

  onIdentityChanged() {
    this.identityChanged.emit();
  }
}
