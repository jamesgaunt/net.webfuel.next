import { Component, DestroyRef, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { ProjectSupport, ProjectSupportFile, SupportRequest } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { FormService } from 'core/form.service';
import { debounceTime } from 'rxjs';
import _ from 'shared/common/underscore';
import { environment } from '../../../../../environments/environment';
import { IsPrePostAwardEnum } from '../../../../api/api.enums';
import { UserService } from '../../../../core/user.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { SupportRequestComponentBase } from '../shared/support-request-component-base';
import { CreateSupportRequestSupportDialog } from './create-support-request-support/create-support-request-support.dialog';
import { UpdateSupportRequestSupportDialog } from './update-support-request-support/update-support-request-support.dialog';

@Component({
  selector: 'support-request-support',
  templateUrl: './support-request-support.component.html',
})
export class SupportRequestSupportComponent extends SupportRequestComponentBase {
  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private formService: FormService,
    public userService: UserService,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    private createSupportRequestSupportDialog: CreateSupportRequestSupportDialog,
    private updateSupportRequestSupportDialog: UpdateSupportRequestSupportDialog,
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

  protected applyLock() {}

  protected clearLock() {}

  reset(item: SupportRequest) {
    super.reset(item);
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
    this.createSupportRequestSupportDialog.open({ projectSupportGroupId: this.item.projectSupportGroupId });
  }

  editProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.updateSupportRequestSupportDialog.open({ projectSupportGroupId: projectSupport.projectSupportGroupId, projectSupport: projectSupport });
  }

  deleteProjectSupport(projectSupport: ProjectSupport) {
    if (this.locked) return;
    this.confirmDeleteDialog.open({ title: 'Project Support' }).subscribe(() => {
      this.projectSupportApi.delete({ id: projectSupport.id }, { successGrowl: 'Project Support Deleted' }).subscribe();
    });
  }

  summariseProjectSupport() {
    //  this.summariseProjectSupportDialog.open({ items: this.clientItems! });
  }

  // Files

  sasRedirect(file: ProjectSupportFile) {
    if (!file) return '';
    return environment.apiHost + 'api/file-storage-entry/sas-redirect/' + file.id;
  }
}
