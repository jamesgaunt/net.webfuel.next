import { Component, DestroyRef, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import _ from 'shared/common/underscore';
import { UserActivity } from '../../../api/api.types';
import { StaticDataCache } from '../../../api/static-data.cache';
import { UserActivityApi } from '../../../api/user-activity.api';
import { UserApi } from '../../../api/user.api';
import { ReportService } from '../../../core/report.service';
import { ConfirmDeleteDialog } from '../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { CreateUserActivityDialog } from '../../../shared/dialogs/create-user-activity/create-user-activity.dialog';
import { UpdateUserActivityDialog } from '../../../shared/dialogs/update-user-activity/update-user-activity.dialog';

@Component({
  selector: 'my-activity',
  templateUrl: './my-activity.component.html'
})
export class MyActivityComponent {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private createUserActivityDialog: CreateUserActivityDialog,
    private updateUserActivityDialog: UpdateUserActivityDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public userApi: UserApi,
    public userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache,
    public reportService: ReportService,
  ) {
  }

  ngOnInit() {
  }

  filterForm = new FormGroup({
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    description: new FormControl<string>('', { nonNullable: true }),
    userId: new FormControl<string | null>(null)
  });

  resetFilterForm() {
    this.filterForm.patchValue({ fromDate: null, toDate: null, description: '' });
  }

  // User Activity

  createUserActivity() {
    this.createUserActivityDialog.open();
  }

  edit(userActivity: UserActivity) {
    this.updateUserActivityDialog.open({ userActivity: userActivity });
  }

  delete(userActivity: UserActivity) {
    this.confirmDeleteDialog.open({ title: "User Activity" }).subscribe(() => {
      this.userActivityApi.delete({ id: userActivity.id }, { successGrowl: "User Activity Deleted" }).subscribe();
    });
  }

  export() {
    this.userActivityApi.export(_.merge(this.filterForm.getRawValue(), { skip: 0, take: 0 })).subscribe((result) => {
      this.reportService.runReport(result);
    });
  }

  // Project Activity

  isProjectActivity(userActivity: UserActivity): boolean {
    return userActivity.projectSupportId !== null;
  }
}
