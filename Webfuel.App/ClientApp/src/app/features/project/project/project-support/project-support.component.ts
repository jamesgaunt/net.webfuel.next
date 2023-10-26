import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSupport, User } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DataSourceLookup } from 'shared/common/data-source';
import { ConfirmDeleteDialogService } from 'shared/dialogs/confirm-delete/confirm-delete-dialog.component';
import { ProjectSupportUpdateDialogService } from '../dialogs/project-support-update-dialog/project-support-update-dialog.component';

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
    private confirmDeleteDialog: ConfirmDeleteDialogService,
    private updateProjectSupportDialog: ProjectSupportUpdateDialogService,
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
    this.updateProjectSupportDialog.open({ projectSupport: projectSupport });
  }

  deleteProjectSupport(projectSupport: ProjectSupport) {
    this.confirmDeleteDialog.open({ title: "Project Support" }).subscribe(() => {
      this.projectSupportApi.delete({ id: projectSupport.id }, { successGrowl: "Project Support Deleted" }).subscribe();
    });
  }
}
