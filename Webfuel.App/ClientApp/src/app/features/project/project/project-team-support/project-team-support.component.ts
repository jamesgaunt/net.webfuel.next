import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormGroup } from '@angular/forms';
import { ProjectTeamSupport } from 'api/api.types';
import { ProjectTeamSupportApi } from 'api/project-team-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { UserService } from '../../../../core/user.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { ProjectComponentBase } from '../shared/project-component-base';
import { CompleteProjectTeamSupportDialog } from './complete-project-team-support/complete-project-team-support.dialog';
import { CreateProjectTeamSupportDialog } from './create-project-team-support/create-project-team-support.dialog';
import { UpdateProjectTeamSupportDialog } from './update-project-team-support/update-project-team-support.dialog';

@Component({
  selector: 'project-team-support',
  templateUrl: './project-team-support.component.html'
})
export class ProjectTeamSupportComponent extends ProjectComponentBase {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private formService: FormService,
    public userService: UserService,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private projectTeamSupportApi: ProjectTeamSupportApi,
    private createProjectTeamSupportDialog: CreateProjectTeamSupportDialog,
    private updateProjectTeamSupportDialog: UpdateProjectTeamSupportDialog,
    private completeProjectTeamSupportDialog: CompleteProjectTeamSupportDialog,
    public staticDataCache: StaticDataCache,
  ) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();

    this.loadProjectTeamSupport();
    this.projectTeamSupportApi.changed.pipe(
      takeUntilDestroyed(this.destroyRef)
    )
    .subscribe(() => this.loadProjectTeamSupport());
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

  addTeamSupport() {
    if (this.locked) return;
    this.createProjectTeamSupportDialog.open({ projectId: this.item.id });
  }

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
