import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { ProjectStatus, QueryProjectStatus } from '../../../api/api.types';
import { ProjectStatusApi } from '../../../api/project-status.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'project-status-list',
  templateUrl: '../shared/static-data.component.html'
})
export class ProjectStatusComponent extends StaticDataComponent<ProjectStatus, QueryProjectStatus, any, any> {
  constructor(
    dataSource: ProjectStatusApi,
  ) {
    super(dataSource);
    this.typeName = "Project Status";
  }
}
