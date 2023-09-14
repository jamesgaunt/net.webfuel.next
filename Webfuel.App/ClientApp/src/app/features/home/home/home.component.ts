import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IWidget } from '../../../api/api.types';
import { WidgetApi } from '../../../api/widget.api';
import { DialogService } from '../../../core/dialog.service';
import { AddWidgetDialogComponent } from '../widget/add-widget-dialog.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {

  constructor(
    public widgetApi: WidgetApi,
    public dialogService: DialogService 
  ) {
  }

  formData = new FormGroup({
    name: new FormControl('', { nonNullable: true }),
    age: new FormControl(null),
  });

  doSomething() {

    this.widgetApi.doSomething().subscribe((result) => {

    });

  }

  isOpen = false;

  // Add

  openDialog() {
    this.dialogService.open<IWidget>(AddWidgetDialogComponent, {
    });
  }
}
