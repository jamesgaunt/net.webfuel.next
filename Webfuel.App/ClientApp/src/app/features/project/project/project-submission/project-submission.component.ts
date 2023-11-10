import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSubmission, QueryProjectSubmission } from 'api/api.types';
import { ProjectSubmissionApi } from 'api/project-submission.api';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { UpdateProjectSubmissionDialog } from '../project-submission/update-project-submission/update-project-submission.dialog';

@Component({
  selector: 'project-submission',
  templateUrl: './project-submission.component.html'
})
export class ProjectSubmissionComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private updateProjectSubmissionDialog: UpdateProjectSubmissionDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public projectSubmissionApi: ProjectSubmissionApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
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

  edit(item: ProjectSubmission) {
    this.updateProjectSubmissionDialog.open({ projectSubmission: item });
  }

  delete(item: ProjectSubmission) {
    this.confirmDeleteDialog.open({ title: "Project Submission" }).subscribe((result) => {
      this.projectSubmissionApi.delete({ id: item.id }, { successGrowl: "Project Submission Deleted" }).subscribe();
    });
  }
}
