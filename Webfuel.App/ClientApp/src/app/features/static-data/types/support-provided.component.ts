import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateSupportProvided, SupportProvided, QuerySupportProvided, UpdateSupportProvided } from '../../../api/api.types';
import { SupportProvidedApi } from '../../../api/support-provided.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'support-provided-list',
  templateUrl: '../shared/static-data.component.html'
})
export class SupportProvidedComponent extends StaticDataComponent<SupportProvided, QuerySupportProvided, CreateSupportProvided, UpdateSupportProvided> {
  constructor(
    dataSource: SupportProvidedApi,
  ) {
    super(dataSource);
    this.typeName = "Support Provided";
  }
}
