import { Dialog } from "@angular/cdk/dialog";
import { ComponentType } from "@angular/cdk/portal";
import { Injectable, TemplateRef } from "@angular/core";
import { noop } from "rxjs";
import { ConfirmDeleteDialogComponent, IConfirmDeleteDialogOptions } from "./dialogs/confirm-delete-dialog.component";
import { ConfirmDeactivateDialogComponent, IConfirmDeactivateDialogOptions } from "./dialogs/confirm-deactivate-dialog.component";
import { DatePickerDialogComponent, IDatePickerDialogOptions } from "./dialogs/date-picker-dialog.component";
import { ConfirmDialogComponent, IConfirmDialogOptions } from "./dialogs/confirm-dialog.component";
import { IUserActivityCreateDialogOptions, UserActivityCreateDialogComponent } from "./dialogs/user-activity-create-dialog.component";

export interface IDialogOptions<TResult, TData> {
  data?: TData;

  callback?: (result: TResult | undefined) => void;
  successCallback?: (result: TResult) => void;
  cancelledCallback?: () => void;

  width?: string;
}

@Injectable()
export class DialogService {

  constructor(
    private dialog: Dialog) {
  }

  open<TResult, TData = unknown>(
    component: ComponentType<unknown> | TemplateRef<unknown>,
    options?: IDialogOptions<TResult, TData>
  ) {
    options = options || {};

    const dialogRef = this.dialog.open<TResult, TData, unknown>(component, {
      data: options.data,
      width: options.width
    });

    dialogRef.closed.subscribe((result) => {
      if (options!.callback)
        options!.callback(result)

      if (result !== undefined && options!.successCallback)
        options!.successCallback(result);

      if (result === undefined && options!.cancelledCallback)
        options!.cancelledCallback();
    });

    return dialogRef;
  }

  confirm(options?: IConfirmDialogOptions) {
    this.open(ConfirmDialogComponent, {
      data: options,
      callback: (result) => {
        if (options && options.callback)
          options.callback(!!result);
        if (result === true && options && options.confirmedCallback)
          options.confirmedCallback();
      },
      width: '500px'
    })
  }

  confirmDelete(options?: IConfirmDeleteDialogOptions) {
    this.open(ConfirmDeleteDialogComponent, {
      data: options,
      callback: (result) => {
        if (result === true && options && options.confirmedCallback)
          options.confirmedCallback();
      },
      width: '500px'
    })
  }

  confirmDeactivate(options?: IConfirmDeactivateDialogOptions) {
    this.open(ConfirmDeactivateDialogComponent, {
      data: options,
      callback: (result) => {
        if (options && options.callback)
          options.callback(!!result);
      },
      width: '500px'
    })
  }

  pickDate(options?: IDatePickerDialogOptions) {
    this.open(DatePickerDialogComponent, {
      data: options,
      callback: (result) => {
        if (result !== undefined && options && options.callback)
          options.callback(<any>result);
      },
      width: 'auto'
    })
  }

  addUserActivity(options?: IUserActivityCreateDialogOptions) {
    this.open(UserActivityCreateDialogComponent, {
      data: options,
      callback: (result) => {
        if (result !== undefined && options && options.callback)
          options.callback(<any>result);
      },
      width: 'auto'
    })
  }
}

