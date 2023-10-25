import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSupport, QueryProjectSupport, User } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { FormService } from '../../../core/form.service';
import { ProjectSupportApi } from '../../../api/project-support.api';
import { DataSourceLookup, IDataSource, IDataSourceWithGet } from '../../../shared/common/data-source';
import { UserApi } from '../../../api/user.api';
import { DialogService } from '../../../core/dialog.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ProjectSupportUpdateDialogComponent } from '../project-support-update-dialog/project-support-update-dialog.component';

@Component({
  selector: 'project-support',
  templateUrl: './project-support.component.html'
})
export class ProjectSupportComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private userApi: UserApi,
    private dialogService: DialogService,
    private projectSupportApi: ProjectSupportApi,
    public staticDataCache: StaticDataCache
  ) {
    this.userLookup = new DataSourceLookup(userApi);
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
    this.loadProjectSupport();

    this.projectSupportApi.changed.pipe(
      takeUntilDestroyed(this.destroyRef)
    )
      .subscribe(() => this.loadProjectSupport());
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

  // Project Support

  items: ProjectSupport[] = [];

  loadProjectSupport() {
    this.projectSupportApi.query({ projectId: this.item.id, skip: 0, take: 100 }).subscribe((result) => {
      this.items = result.items;
    })
  }

  userLookup: DataSourceLookup<User>;

  editProjectSupport(projectSupport: ProjectSupport) {
    this.dialogService.open(ProjectSupportUpdateDialogComponent, {
      data: {
        projectSupport: projectSupport
      }
    });
  }

  deleteProjectSupport(projectSupport: ProjectSupport) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.projectSupportApi.delete({ id: projectSupport.id }, { successGrowl: "Project Support Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
