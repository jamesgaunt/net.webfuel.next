import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  formData = new FormGroup({
    email: new FormControl('', { nonNullable: true }),
    name: new FormControl('', { nonNullable: true })
  });

  reset() {
  }

}
