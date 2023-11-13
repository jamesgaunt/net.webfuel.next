import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectTeamSupport, SupportProvided, SupportTeam, User } from 'api/api.types';
import { ProjectTeamSupportApi } from 'api/project-team-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DataSourceLookup } from 'shared/common/data-source';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import _ from 'shared/common/underscore';
import { SupportTeamApi } from '../../../../api/support-team.api';
import { CompleteProjectTeamSupportDialog } from './complete-project-team-support/complete-project-team-support.dialog';
import { UpdateProjectTeamSupportDialog } from './update-project-team-support/update-project-team-support.dialog';

@Component({
  selector: 'project-team-support',
  templateUrl: './project-team-support.component.html'
})
export class ProjectTeamSupportComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private userApi: UserApi,
    private supportTeamApi: SupportTeamApi,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private projectTeamSupportApi: ProjectTeamSupportApi,
    private updateProjectTeamSupportDialog: UpdateProjectTeamSupportDialog,
    private completeProjectTeamSupportDialog: CompleteProjectTeamSupportDialog,
    public staticDataCache: StaticDataCache
  ) {
    this.userLookup = new DataSourceLookup(userApi);
    this.teamLookup = new DataSourceLookup(supportTeamApi)
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
    this.loadProjectTeamSupport();

    this.projectTeamSupportApi.changed.pipe(
      takeUntilDestroyed(this.destroyRef)
    )
      .subscribe(() => this.loadProjectTeamSupport());
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

  items: ProjectTeamSupport[] | null = null;

  loadProjectTeamSupport() {
    this.projectTeamSupportApi.query({ projectId: this.item.id, skip: 0, take: 100 }).subscribe((result) => {
      this.items = result.items;
    })
  }

  userLookup: DataSourceLookup<User>;

  teamLookup: DataSourceLookup<SupportTeam>;

  completeProjectTeamSupport(projectTeamSupport: ProjectTeamSupport) {
    this.completeProjectTeamSupportDialog.open({ id: projectTeamSupport.id }).subscribe(() => {
      this.projectTeamSupportApi.changed.next(null);
    })
  }

  editProjectTeamSupport(projectTeamSupport: ProjectTeamSupport) {
    this.updateProjectTeamSupportDialog.open({ item: projectTeamSupport }).subscribe(() => {
    });
  }

  deleteProjectTeamSupport(projectTeamSupport: ProjectTeamSupport) {
    this.confirmDeleteDialog.open({ title: "Project Team Support" }).subscribe(() => {
      this.projectTeamSupportApi.delete({ id: projectTeamSupport.id }, { successGrowl: "Team Support Deleted" }).subscribe();
    });
  }
}
