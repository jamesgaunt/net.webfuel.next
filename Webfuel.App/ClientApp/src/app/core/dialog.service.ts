import { Dialog, DialogRef } from "@angular/cdk/dialog";
import { ComponentType } from "@angular/cdk/portal";
import { Injectable, TemplateRef } from "@angular/core";
import { Observable } from "rxjs/internal/Observable";
import { Subscriber, TeardownLogic, noop } from "rxjs";

export interface DialogSettings {
  width?: string;
  disableClose?: boolean;
}

export class DialogHandle<TResult> extends Observable<TResult> {
  constructor(
    private dialogRef: DialogRef<TResult>,
    subscribe: (subscriber: Subscriber<TResult>) => TeardownLogic
  ) {
    super(subscribe);
  }
}

@Injectable()
export class DialogService {

  constructor(
    private dialog: Dialog) {
  }

  openComponent<TResult = unknown, TData = unknown>(
    component: ComponentType<unknown>,
    data?: TData,
    settings?: DialogSettings
  ) {
    settings = settings || {};
    const dialogRef = this.dialog.open<TResult, TData, unknown>(component, {
      data: data,
      width: settings.width || 'auto',
      disableClose: settings.disableClose || false,
    });

    return new DialogHandle<TResult>(dialogRef, (subscriber) => {
      dialogRef.closed.subscribe({
        next: (result) => result === undefined ? noop : subscriber.next(result),
        complete: () => subscriber.complete()
      });
    });
  }
}

