import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateDisability, Disability, QueryDisability, UpdateDisability } from '../../../api/api.types';
import { DisabilityApi } from '../../../api/disability.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'disability-list',
  templateUrl: '../shared/static-data.component.html'
})
export class DisabilityComponent extends StaticDataComponent<Disability, QueryDisability, CreateDisability, UpdateDisability> {
  constructor(
    dataSource: DisabilityApi,
  ) {
    super(dataSource);
    this.typeName = "Disability";
  }
}
