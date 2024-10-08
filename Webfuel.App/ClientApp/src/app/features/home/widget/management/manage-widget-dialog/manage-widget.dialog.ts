import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Widget, WidgetType } from 'api/api.types';
import { UserLoginApi } from 'api/user-login.api';
import { WidgetApi } from 'api/widget.api';
import { ConfigurationService } from 'core/configuration.service';
import { FormService } from 'core/form.service';
import { GrowlService } from 'core/growl.service';
import { IdentityService } from 'core/identity.service';
import { WidgetService } from 'core/widget.service';
import { IDataSource } from 'shared/common/data-source';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';

export interface ManageWidgetDialogData {
  userId: string;
}

@Injectable()
export class ManageWidgetDialog extends DialogBase<true, ManageWidgetDialogData> {
  open(data: ManageWidgetDialogData) {
    return this._open(ManageWidgetDialogComponent, data, { disableClose: true });
  }
}

@Component({
  selector: 'manage-widget-dialog',
  templateUrl: './manage-widget.dialog.html'
})
export class ManageWidgetDialogComponent extends DialogComponentBase<true, ManageWidgetDialogData> {

  constructor(
    private formService: FormService,
    private growlService: GrowlService,
    private widgetApi: WidgetApi,
    public widgetService: WidgetService,
  ) {
    super();
    this.setMode("manage");
    this.widgetApi.selectType({ userId: this.data.userId }).subscribe(result => this.widgetTypes = result);
  }

  widgets: Widget[] | null = null;

  widgetTypes: WidgetType[] = [];

  loadWidgets() {
    this.widgetApi.select({ userId: this.data.userId }).subscribe(result => this.widgets = result);
  }

  close() {
    this._closeDialog(true);
  }

  deleteWidget(widget: Widget) {
    this.widgetApi.delete({ id: widget.id }, { successGrowl: "Changes saved" }).subscribe((result) => {
      this.loadWidgets();
    })
  }

  dropWidget($event: any) {
    var currentIndex = <number>$event.currentIndex;
    var previousIndex = <number>$event.previousIndex;

    // Client side only, reordering stuff can't really break anything
    const item = this.widgets!.splice(previousIndex, 1);
    this.widgets!.splice(currentIndex, 0, item[0]);

    // Server side
    this.widgetApi.sort({ ids: _.map(this.widgets!, p => p.id), userId: this.data.userId }, { successGrowl: "Changes saved" }).subscribe(() => {
      this.loadWidgets();
    });
  }

  // Add

  addWidget() {
    this.setMode("add");
  }

  cancelAdd() {
    this.setMode("manage");
  }

  addWidgetType(widgetType: WidgetType) {
    this.widgetApi.create({ userId: this.data.userId, widgetTypeId: widgetType.id }, { successGrowl: "Changes saved" }).subscribe(() => {
      this.setMode("manage");
    })
  }

  // Edit

  editWidget(widget: Widget) {
  }

  // Mode

  mode: "manage" | "add" | "edit" = "manage";

  setMode(mode: "manage" | "add" | "edit") {
    this.mode = mode;
    this.loadWidgets();
  }
}
