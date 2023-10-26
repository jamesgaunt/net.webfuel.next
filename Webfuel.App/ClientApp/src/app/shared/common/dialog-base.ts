import { inject } from "@angular/core";
import { ComponentType } from "@angular/cdk/portal";
import { DialogHandle, DialogService, DialogSettings } from "core/dialog.service";
import { DIALOG_DATA, DialogRef } from "@angular/cdk/dialog";
import { TitleComponent } from "../../features/static-data/types/title.component";


export abstract class DialogBase<TResult, TData = unknown>{

  dialogService: DialogService = inject(DialogService);

  protected _open<TComponent>(component: ComponentType<TComponent>, data: TData, settings?: DialogSettings) {
    return this.dialogService.openComponent<TResult, TData>(component, data, settings);
  }
}

export abstract class DialogComponentBase<TResult, TData = unknown> {

  private _dialogRef: DialogRef<TResult> = inject(DialogRef<TResult>);

  data: TData = inject(DIALOG_DATA);

  close(result: TResult) {
    this._dialogRef.close(result);
  }

  cancel() {
    this._dialogRef.close();
  }
}
