import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { ApplicationStage, CreateApplicationStage, QueryApplicationStage, UpdateApplicationStage } from '../../../api/api.types';
import { ApplicationStageApi } from '../../../api/application-stage.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'application-stage-list',
  templateUrl: '../shared/static-data.component.html'
})
export class ApplicationStageComponent extends StaticDataComponent<ApplicationStage, QueryApplicationStage, CreateApplicationStage, UpdateApplicationStage> {
  constructor(
    dataSource: ApplicationStageApi,
  ) {
    super(dataSource);
    this.typeName = "Application Stage";
  }
}
