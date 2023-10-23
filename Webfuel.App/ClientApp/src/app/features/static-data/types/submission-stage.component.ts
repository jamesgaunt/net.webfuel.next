import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { SubmissionStage, QuerySubmissionStage } from '../../../api/api.types';
import { SubmissionStageApi } from '../../../api/submission-stage.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'submission-stage-list',
  templateUrl: '../shared/static-data.component.html'
})
export class SubmissionStageComponent extends StaticDataComponent<SubmissionStage, QuerySubmissionStage, any, any> {
  constructor(
    dataSource: SubmissionStageApi,
  ) {
    super(dataSource);
    this.typeName = "Submission Stage";
  }
}
