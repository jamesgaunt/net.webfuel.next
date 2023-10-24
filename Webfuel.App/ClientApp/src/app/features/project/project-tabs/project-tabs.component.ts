import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSubmission, ProjectSupport } from '../../../api/api.types';
import { DialogService } from '../../../core/dialog.service';
import { ProjectSupportCreateDialogComponent, ProjectSupportCreateDialogOptions } from '../project-support-create-dialog/project-support-create-dialog.component';
import { ProjectSubmissionCreateDialogComponent, ProjectSubmissionCreateDialogOptions } from '../project-submission-create-dialog/project-submission-create-dialog.component';

@Component({
  selector: 'project-tabs',
  templateUrl: './project-tabs.component.html'
})
export class ProjectTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialogService: DialogService
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
  }

  addSupport() {
    this.dialogService.open<ProjectSupport, ProjectSupportCreateDialogOptions>(ProjectSupportCreateDialogComponent, {
      data: {
        projectId: this.item.id
      },
      width: "1000px"
    });
  }

  addSubmission() {
    this.dialogService.open<ProjectSubmission, ProjectSubmissionCreateDialogOptions>(ProjectSubmissionCreateDialogComponent, {
      data: {
        projectId: this.item.id
      },
      width: "1000px"
    });
  }
}
