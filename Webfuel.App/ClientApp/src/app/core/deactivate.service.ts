import { Injectable, inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { CanDeactivateFn } from '@angular/router';
import { ConfirmDeactivateDialog } from '../shared/dialogs/confirm-deactivate/confirm-deactivate.dialog';

@Injectable()
export class DeactivateService {

  constructor(
    private confirmDeactivateDialog: ConfirmDeactivateDialog
  ) { }

  confirmDeactivate(dirty: boolean) {
    if (!dirty)
      return true;
    return new Promise<boolean>((resolver) => {
      this.confirmDeactivateDialog.open().subscribe((result) => resolver(result));
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


