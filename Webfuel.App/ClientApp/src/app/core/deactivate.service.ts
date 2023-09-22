import { Injectable, inject } from '@angular/core';
import { GrowlService } from './growl.service';
import { DialogService } from './dialog.service';
import { CanDeactivateFn } from '@angular/router';
import { FormGroup } from '@angular/forms';

@Injectable()
export class DeactivateService {

  constructor(
    private dialogService: DialogService
  ) { }

  confirmDeactivate(dirty: boolean) {
    if (!dirty)
      return true;
    return new Promise<boolean>((resolver) => {
      this.dialogService.confirmDeactivate({
        callback: (result) => resolver(result)
      });
    });
  }

  static isTrue<T>(dirty: (component: T) => boolean): CanDeactivateFn<T> {
    return (component: T) => {
      return inject(DeactivateService).confirmDeactivate(dirty(component));
    };
  }

  static isPristine<T extends { form: FormGroup<any>}>(): CanDeactivateFn<T> {
    return (component: T) => {
      return inject(DeactivateService).confirmDeactivate(!component.form.pristine);
    };
  }
}


