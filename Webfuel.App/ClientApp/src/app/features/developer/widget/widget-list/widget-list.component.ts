import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';
import { DialogService } from 'core/dialog.service';
import { WidgetAddDialogComponent } from '../widget-add-dialog/widget-add-dialog.component';

@Component({
  templateUrl: './widget-list.component.html'
})
export class WidgetListComponent {
  constructor(
    private dialogService: DialogService
  ) {
  }

  add() {
    this.dialogService.open(WidgetAddDialogComponent);
  }
}
