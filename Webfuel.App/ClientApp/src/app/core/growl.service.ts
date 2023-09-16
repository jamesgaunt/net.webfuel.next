import _ from '../shared/underscore';
import { Injectable, Injector } from '@angular/core';
import { BehaviorSubject, timer } from 'rxjs';

export interface IGrowl {
  class?: string;
  message: string;
}

@Injectable()
export class GrowlService {

  constructor(
  ) {
  }

  growlDanger(message: string) {
    this.growl({ message: message, class: "is-danger" });
  }

  growlWarning(message: string) {
    this.growl({ message: message, class: "is-warning" });
  }

  growlSuccess(message: string) {
    this.growl({ message: message, class: "is-success" });
  }

  growls = new BehaviorSubject<IGrowl[]>([]);

  private growl(growl: IGrowl) {
    this.growls.value.push(growl)
    this.growls.next(this.growls.value);

    var sub = timer(4000).subscribe(() => {
      this.dismiss(growl);
      sub.unsubscribe();
    });
  }

  dismiss(growl: IGrowl) {
    var index = this.growls.value.indexOf(growl);
    if (index >= 0)
      this.growls.value.splice(index, 1);
    this.growls.next(this.growls.value);
  }
}
