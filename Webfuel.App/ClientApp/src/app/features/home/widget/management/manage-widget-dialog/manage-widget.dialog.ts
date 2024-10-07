import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Widget } from 'api/api.types';
import { UserLoginApi } from 'api/user-login.api';
import { WidgetApi } from 'api/widget.api';
import { ConfigurationService } from 'core/configuration.service';
import { FormService } from 'core/form.service';
import { GrowlService } from 'core/growl.service';
import { IdentityService } from 'core/identity.service';
import { IDataSource } from 'shared/common/data-source';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';

@Injectable()
export class ManageWidgetDialog extends DialogBase<true> {
  open() {
    return this._open(ManageWidgetDialogComponent, undefined);
  }
}

@Component({
  selector: 'manage-widget-dialog',
  templateUrl: './manage-widget.dialog.html'
})
export class ManageWidgetDialogComponent extends DialogComponentBase<true> {

  constructor(
    private formService: FormService,
    private growlService: GrowlService,
    private widgetApi: WidgetApi,
    private configurationService: ConfigurationService,
  ) {
    super();
  }

  widgets: Widget[] | null = null;




  close() {
    this._cancelDialog();
  }

  addWidget() {

  }

  deleteWidget(widget: Widget) {

  }
}
