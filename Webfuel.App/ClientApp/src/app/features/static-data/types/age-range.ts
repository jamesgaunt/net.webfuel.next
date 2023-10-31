import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateAgeRange, AgeRange, QueryAgeRange, UpdateAgeRange } from '../../../api/api.types';
import { AgeRangeApi } from '../../../api/age-range.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'age-range-list',
  templateUrl: '../shared/static-data.component.html'
})
export class AgeRangeComponent extends StaticDataComponent<AgeRange, QueryAgeRange, CreateAgeRange, UpdateAgeRange> {
  constructor(
    dataSource: AgeRangeApi,
  ) {
    super(dataSource);
    this.typeName = "Age Range";
  }
}
