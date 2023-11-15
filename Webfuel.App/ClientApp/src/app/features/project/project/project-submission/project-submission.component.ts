import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSubmission, QueryProjectSubmission } from 'api/api.types';
import { ProjectSubmissionApi } from 'api/project-submission.api';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { UpdateProjectSubmissionDialog } from '../project-submission/update-project-submission/update-project-submission.dialog';
import { ProjectComponentBase } from '../shared/project-component-base';
import { CreateProjectSubmissionDialog } from './create-project-submission/create-project-submission.dialog';

@Component({
  selector: 'project-submission',
  templateUrl: './project-submission.component.html'
})
export class ProjectSubmissionComponent extends ProjectComponentBase {

  constructor(
    private formService: FormService,
    private createProjectSubmissionDialog: CreateProjectSubmissionDialog,
    private updateProjectSubmissionDialog: UpdateProjectSubmissionDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public projectSubmissionApi: ProjectSubmissionApi,
  ) {
    super();
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

  filter(query: QueryProjectSubmission) {
    query.projectId = this.item.id;
  }

  add() {
    if (this.locked) return;
    this.createProjectSubmissionDialog.open({ projectId: this.item.id });
  }


  edit(item: ProjectSubmission) {
    if (this.locked) return;
    this.updateProjectSubmissionDialog.open({ projectSubmission: item });
  }

  delete(item: ProjectSubmission) {
    if (this.locked) return;
    this.confirmDeleteDialog.open({ title: "Project Submission" }).subscribe((result) => {
      this.projectSubmissionApi.delete({ id: item.id }, { successGrowl: "Project Submission Deleted" }).subscribe();
    });
  }
}
