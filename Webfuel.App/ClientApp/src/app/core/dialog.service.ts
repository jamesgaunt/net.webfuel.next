import { Dialog } from "@angular/cdk/dialog";
import { ComponentType } from "@angular/cdk/portal";
import { Injectable, TemplateRef } from "@angular/core";

export interface IDialogOptions<TResult, TData> {
  data?: TData;
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
    });
  }
}

