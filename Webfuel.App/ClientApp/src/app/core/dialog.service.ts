import { Dialog } from "@angular/cdk/dialog";
import { ComponentType } from "@angular/cdk/portal";
import { Injectable, TemplateRef } from "@angular/core";
import { noop } from "rxjs";
import { ConfirmDeleteDialogComponent, IConfirmDeleteDialogOptions } from "./dialogs/confirm-delete-dialog.component";

export interface IDialogOptions<TResult, TData> {
  data?: TData;
  callback?: (result: TResult | undefined) => void;
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

    if (options.callback) {
      dialogRef.closed.subscribe((result) => options!.callback!(result))
    }
   
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
}

