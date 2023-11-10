import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { CreateProjectSubmissionDialog } from '../project-submission/create-project-submission/create-project-submission.dialog';
import { CreateProjectSupportDialog } from '../project-support/create-project-support/create-project-support.dialog';
import { CreateProjectTeamSupportDialog } from '../project-team-support/create-project-team-support/create-project-team-support.dialog';

@Component({
  selector: 'project-tabs',
  templateUrl: './project-tabs.component.html'
})
export class ProjectTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private createProjectSupportDialog: CreateProjectSupportDialog,
    private createProjectSubmissionDialog: CreateProjectSubmissionDialog,
    private createProjectTeamSupportDialog: CreateProjectTeamSupportDialog
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

  addTeamSupport() {
    this.createProjectTeamSupportDialog.open({ projectId: this.item.id });
  }
}
