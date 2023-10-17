import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateGender, Gender, QueryGender, UpdateGender } from '../../../api/api.types';
import { GenderApi } from '../../../api/gender.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'gender-list',
  templateUrl: '../shared/static-data.component.html'
})
export class GenderComponent extends StaticDataComponent<Gender, QueryGender, CreateGender, UpdateGender> {
  constructor(
    dataSource: GenderApi,
  ) {
    super(dataSource);
    this.typeName = "Gender";
  }
}
