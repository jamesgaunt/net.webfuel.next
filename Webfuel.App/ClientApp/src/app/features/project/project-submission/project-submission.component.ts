import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSubmission, QueryProjectSubmission, User } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { FormService } from '../../../core/form.service';
import { ProjectSubmissionApi } from '../../../api/project-submission.api';
import { DataSourceLookup, IDataSource, IDataSourceWithGet } from '../../../shared/common/data-source';
import { UserApi } from '../../../api/user.api';
import { DialogService } from '../../../core/dialog.service';
import { ProjectSubmissionUpdateDialogComponent } from '../project-submission-update-dialog/project-submission-update-dialog.component';

@Component({
  selector: 'project-submission',
  templateUrl: './project-submission.component.html'
})
export class ProjectSubmissionComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private dialogService: DialogService,
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
    this.dialogService.open(ProjectSubmissionUpdateDialogComponent, {
      data: {
        projectSubmission: item
      }
    });
  }

  delete(item: ProjectSubmission) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.projectSubmissionApi.delete({ id: item.id }, { successGrowl: "Project Submission Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
