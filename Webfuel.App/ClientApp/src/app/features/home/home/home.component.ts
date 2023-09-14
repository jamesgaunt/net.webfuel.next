import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { IWidget } from '../../../api/api.types';
import { WidgetApi } from '../../../api/widget.api';
import { DialogService } from '../../../core/dialog.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  constructor(
  ) {
  }

  isOpen = false;
}
