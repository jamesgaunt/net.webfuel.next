import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectSubmissionCreateDialogService } from '../dialogs/project-submission-create-dialog/project-submission-create-dialog.component';
import { ProjectSupportCreateDialogService } from '../dialogs/project-support-create-dialog/project-support-create-dialog.component';

@Component({
  selector: 'project-tabs',
  templateUrl: './project-tabs.component.html'
})
export class ProjectTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private createProjectSupportDialog: ProjectSupportCreateDialogService,
    private createProjectSubmissionDialog: ProjectSubmissionCreateDialogService
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
    this.createProjectSupportDialog.open({ projectId: this.item.id });
  }

  addSubmission() {
    this.createProjectSubmissionDialog.open({ projectId: this.item.id });
  }
}
