import { Injectable, inject } from '@angular/core';
import { GrowlService } from './growl.service';
import { DialogService } from './dialog.service';
import { CanDeactivateFn } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { ConfirmDeactivateDialogService } from '../shared/dialogs/confirm-deactivate/confirm-deactivate-dialog.component';

@Injectable()
export class DeactivateService {

  constructor(
    private confirmDeactivateDialogService: ConfirmDeactivateDialogService
  ) { }

  confirmDeactivate(dirty: boolean) {
    if (!dirty)
      return true;
    return new Promise<boolean>((resolver) => {
      this.confirmDeactivateDialogService.open().subscribe((result) => resolver(result));
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


