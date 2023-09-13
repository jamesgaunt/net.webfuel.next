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
    name: new FormControl('', { nonNullable: true }),
    age: new FormControl(null),
  });

  doSomething() {

    this.widgetApi.doSomething().subscribe((result) => {

    });

  }

}
