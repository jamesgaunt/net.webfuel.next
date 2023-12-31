import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormGroup } from '@angular/forms';
import { ProjectSupport, SupportProvided } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { FormService } from 'core/form.service';
import _ from 'shared/common/underscore';
import { UserService } from '../../../../core/user.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { UpdateProjectSupportDialog } from '../project-support/update-project-support/update-project-support.dialog';
import { ProjectComponentBase } from '../shared/project-component-base';
import { CreateProjectSupportDialog } from './create-project-support/create-project-support.dialog';

@Component({
  selector: 'project-support',
  templateUrl: './project-support.component.html'
})
export class ProjectSupportComponent extends ProjectComponentBase {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private formService: FormService,
    public userService: UserService,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private createProjectSupportDialog: CreateProjectSupportDialog,
    private updateProjectSupportDialog: UpdateProjectSupportDialog,
    private projectSupportApi: ProjectSupportApi,
  ) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();

    this.loadProjectSupport();
    this.projectSupportApi.changed.pipe(
      takeUntilDestroyed(this.destroyRef)
    )
   .subscribe(() => this.loadProjectSupport());
    this.staticDataCache.supportProvided.query({ skip: 0, take: 100 }).subscribe((result) => this.categories = result.items);
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

  // Project Support

  items: ProjectSupport[] | null = null;

  loadProjectSupport() {
    this.projectSupportApi.query({ projectId: this.item.id, skip: 0, take: 100 }).subscribe((result) => {
      this.items = result.items;
    })
  }

  addProjectSupport() {
    if (this.locked) return;
    this.createProjectSupportDialog.open({ projectId: this.item.id });
  }

  editProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.updateProjectSupportDialog.open({ projectSupport: projectSupport });
  }

  deleteProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.confirmDeleteDialog.open({ title: "Project Support" }).subscribe(() => {
      this.projectSupportApi.delete({ id: projectSupport.id }, { successGrowl: "Project Support Deleted" }).subscribe();
    });
  }

  // Summary

  categories: SupportProvided[] = [];

  containsCategory(category: SupportProvided) {
    return _.some(this.items || [], (p) => _.some(p.supportProvidedIds, (s) => s == category.id));
  }
}
