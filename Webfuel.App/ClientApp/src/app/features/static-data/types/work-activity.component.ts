import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateWorkActivity, WorkActivity, QueryWorkActivity, UpdateWorkActivity } from '../../../api/api.types';
import { WorkActivityApi } from '../../../api/work-activity.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'work-activity-list',
  templateUrl: '../shared/static-data.component.html'
})
export class WorkActivityComponent extends StaticDataComponent<WorkActivity, QueryWorkActivity, CreateWorkActivity, UpdateWorkActivity> {
  constructor(
    dataSource: WorkActivityApi,
  ) {
    super(dataSource);
    this.typeName = "Work Activity";
    this.enableHidden = true;
    this.enableFreeText = true;
  }
}
