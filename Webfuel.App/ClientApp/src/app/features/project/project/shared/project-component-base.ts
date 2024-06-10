import { Component, OnInit, inject } from "@angular/core";
import { ProjectApi } from "../../../../api/project.api";
import { ActivatedRoute, Router } from "@angular/router";
import { StaticDataCache } from "../../../../api/static-data.cache";
import { Project, ProjectStatus } from "../../../../api/api.types";
import { ProjectStatusEnum } from "../../../../api/api.enums";
import { ConfirmDialog } from "../../../../shared/dialogs/confirm/confirm.dialog";

@Component({
  template: ''
})
export abstract class ProjectComponentBase implements OnInit {

  // Dependency Injection

  protected route = inject(ActivatedRoute);
  protected router = inject(Router);
  protected projectApi = inject(ProjectApi);
  protected confirmDialog = inject(ConfirmDialog);
  staticDataCache = inject(StaticDataCache);

  constructor() {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
  }

  // Project

  item!: Project;

  reset(item: Project) {
    this.item = item;
    this.resetStatus();
  }

  // Project Status

  projectStatus: ProjectStatus | null = null;

  resetStatus() {
    this.staticDataCache.projectStatus.get({ id: this.item.statusId }).subscribe((result) => {
      this.projectStatus = result;
      this.locked ? this.applyLock() : this.clearLock();
    });
  }

  protected applyLock() { };

  protected clearLock() { };

  get locked() { return this.item.locked; }

  get discarded() { return this.item.discarded; }

  unlock() {
    this.confirmDialog.open({ title: "Unlock Project", message: "Are you sure you want to return this project to Active status?" }).subscribe(() => {
      this.projectApi.unlock({ id: this.item.id }, { successGrowl: "Project Unlocked" }).subscribe((result) => {
        this.reset(result);
      })
    });
  }
}
