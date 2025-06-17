import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { Project, ProjectSupport, ProjectSupportFile } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { FormService } from 'core/form.service';
import { debounceTime } from 'rxjs';
import _ from 'shared/common/underscore';
import { environment } from '../../../../../environments/environment';
import { IsPrePostAwardEnum } from '../../../../api/api.enums';
import { UserService } from '../../../../core/user.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { UpdateProjectSupportDialog } from '../project-support/update-project-support/update-project-support.dialog';
import { ProjectComponentBase } from '../shared/project-component-base';
import { CompleteProjectSupportDialog } from './complete-project-support/complete-project-support.dialog';
import { CreateProjectSupportDialog } from './create-project-support/create-project-support.dialog';
import { SummariseProjectSupportDialog } from './summarise-project-support/summarise-project-support.dialog';
import { UpdateProjectSupportCompletionDialog } from './update-project-support-completion/update-project-support-completion.dialog';

@Component({
  selector: 'project-support',
  templateUrl: './project-support.component.html',
})
export class ProjectSupportComponent extends ProjectComponentBase {
  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private formService: FormService,
    public userService: UserService,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private createProjectSupportDialog: CreateProjectSupportDialog,
    private updateProjectSupportDialog: UpdateProjectSupportDialog,
    private updateProjectSupportCompletionDialog: UpdateProjectSupportCompletionDialog,
    private summariseProjectSupportDialog: SummariseProjectSupportDialog,
    private completeProjectSupportDialog: CompleteProjectSupportDialog,
    private projectSupportApi: ProjectSupportApi
  ) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();

    this.loadProjectSupport();

    this.projectSupportApi.changed.pipe(debounceTime(200), takeUntilDestroyed(this.destroyRef)).subscribe(() => this.loadProjectSupport());

    this.filterForm.valueChanges.pipe(debounceTime(200), takeUntilDestroyed(this.destroyRef)).subscribe(() => this.loadProjectSupport());

    this.clientFilterForm.valueChanges.pipe(debounceTime(200), takeUntilDestroyed(this.destroyRef)).subscribe(() => this.filterClientItems());

    this.form.valueChanges.pipe(debounceTime(200), takeUntilDestroyed(this.destroyRef)).subscribe(() => {
      this.projectApi.updateSupportSettings(this.form.getRawValue(), { successGrowl: 'Project Updated' }).subscribe((result) => {
        this.reset(result);
      });
    });

    var filename = _.getLocalStorage('SupportFileSearch');
    if (filename) {
      this.clientFilterForm.patchValue({ filename: filename });
      _.setLocalStorage('SupportFileSearch', null); // Clear the search after using it
    }
  }

  filterForm = new FormGroup({
    openTeamSupportOnly: new FormControl(false, { nonNullable: true }),
  });

  clientFilterForm = new FormGroup({
    filename: new FormControl<string>('', { nonNullable: true }),
  });

  form = new FormGroup({
    id: new FormControl<string>('', { nonNullable: true }),
    mockInterviews: new FormControl(false, { nonNullable: true }),
    grantsmanshipReview: new FormControl(false, { nonNullable: true }),
  });

  protected applyLock() {
    this.form.disable({ emitEvent: false });
  }

  protected clearLock() {
    this.form.enable({ emitEvent: false });
  }

  reset(item: Project) {
    super.reset(item);
    this.form.patchValue(item, { emitEvent: false });
    this.form.markAsPristine();
  }

  IsPrePostAwardEnum = IsPrePostAwardEnum;

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

  isOpen(item: ProjectSupport) {
    return item.supportRequestedTeamId !== null && item.supportRequestedCompletedAt === null;
  }

  isComplete(item: ProjectSupport) {
    return item.supportRequestedTeamId !== null && item.supportRequestedCompletedAt !== null;
  }

  passesClientFilter(item: ProjectSupport) {
    if (!this.clientFilterForm.value.filename) return true; // No filter applied
    return item.files.some((file) => file.fileName.toLowerCase().includes(this.clientFilterForm.getRawValue().filename.toLowerCase()));
  }

  clearFilename() {
    this.clientFilterForm.patchValue({ filename: '' });
  }

  // Project Support

  serverItems: ProjectSupport[] | null = null;
  clientItems: ProjectSupport[] | null = null;

  loadProjectSupport() {
    this.projectSupportApi
      .query({
        projectSupportGroupId: this.item.projectSupportGroupId,
        openTeamSupportOnly: this.filterForm.value.openTeamSupportOnly ?? false,
        skip: 0,
        take: 100,
      })
      .subscribe((result) => {
        this.serverItems = result.items;
        this.filterClientItems();
      });
  }

  filterClientItems() {
    if (this.serverItems === null) this.clientItems = null;
    else this.clientItems = this.serverItems.filter((item) => this.passesClientFilter(item));
  }

  addProjectSupport() {
    if (this.locked) return;
    this.createProjectSupportDialog.open({ projectSupportGroupId: this.item.projectSupportGroupId });
  }

  editProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.updateProjectSupportDialog.open({ projectSupportGroupId: projectSupport.projectSupportGroupId, projectSupport: projectSupport });
  }

  editProjectSupportCompletion(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.updateProjectSupportCompletionDialog.open({ projectSupport: projectSupport }).subscribe(() => {
      this.projectSupportApi.changed.next(null);
    });
  }

  deleteProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.confirmDeleteDialog.open({ title: 'Project Support' }).subscribe(() => {
      this.projectSupportApi.delete({ id: projectSupport.id }, { successGrowl: 'Project Support Deleted' }).subscribe();
    });
  }

  summariseProjectSupport() {
    this.summariseProjectSupportDialog.open({ items: this.clientItems! });
  }

  completeProjectSupport(projectSupport: ProjectSupport) {
    this.completeProjectSupportDialog.open({ projectSupport: projectSupport }).subscribe(() => {
      this.projectSupportApi.changed.next(null);
    });
  }

  requestTeamSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.updateProjectSupportDialog
      .open({ projectSupportGroupId: projectSupport.projectSupportGroupId, projectSupport: projectSupport, requestTeamSupport: true })
      .subscribe(() => {
        this.projectSupportApi.changed.next(null);
      });
  }

  uncompleteProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.confirmDialog
      .open({ title: 'Delete Completion Note', message: 'Are you sure you want to mark this project support as incomplete?' })
      .subscribe((result) => {
        this.projectSupportApi.uncomplete({ id: projectSupport.id }, { successGrowl: 'Project Support Marked as Incomplete' }).subscribe(() => {
          this.projectSupportApi.changed.next(null);
        });
      });
  }

  resendNotification(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.confirmDialog
      .open({ title: 'Resend Notification', message: 'Are you sure you want to resend the notification email to the referred team?' })
      .subscribe((result) => {
        this.projectSupportApi.resendNotification({ id: projectSupport.id }, { successGrowl: 'Notification email resent' }).subscribe(() => {
          this.projectSupportApi.changed.next(null);
        });
      });
  }

  // Files

  sasRedirect(file: ProjectSupportFile) {
    if (!file) return '';
    return environment.apiHost + 'api/file-storage-entry/sas-redirect/' + file.id;
  }
}
