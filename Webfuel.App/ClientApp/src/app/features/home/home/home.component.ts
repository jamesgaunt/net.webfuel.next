import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { WidgetApi } from '../../../api/widget.api';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  constructor(
    public widgetApi: WidgetApi
  ) {
  }

  formData = new FormGroup({
    email: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { nonNullable: true })
  });

  doSomething() {

    this.widgetApi.doSomething().subscribe((result) => {

    });

  }

}
