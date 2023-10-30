import { Component, DestroyRef, inject } from '@angular/core';
import { UpdateUserActivityDialog } from '../../../shared/dialogs/update-user-activity/update-user-activity.dialog';
import { ConfirmDeleteDialog } from '../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { UserApi } from '../../../api/user.api';
import { UserActivityApi } from '../../../api/user-activity.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { CreateUserActivityDialog } from '../../../shared/dialogs/create-user-activity/create-user-activity.dialog';
import { UserActivity } from '../../../api/api.types';
import { FormGroup } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

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
    public staticDataCache: StaticDataCache
  ) {
  }

  ngOnInit() {
    this.loadUserActivity();

    this.userActivityApi.changed.pipe(
      takeUntilDestroyed(this.destroyRef)
    )
      .subscribe(() => this.loadUserActivity());
  }

  form = new FormGroup({
  });

  // User Activity

  items: UserActivity[] | null = null;

  loadUserActivity() {
    this.userActivityApi.query({ userId: null, skip: 0, take: 100 }).subscribe((result) => {
      this.items = result.items;
    })
  }

  createUserActivity() {
    this.createUserActivityDialog.open();
  }

  editUserActivity(userActivity: UserActivity) {
    this.updateUserActivityDialog.open({ userActivity: userActivity });
  }

  deleteUserActivity(userActivity: UserActivity) {
    this.confirmDeleteDialog.open({ title: "User Activity" }).subscribe(() => {
      this.userActivityApi.delete({ id: userActivity.id }, { successGrowl: "User Activity Deleted" }).subscribe();
    });
  }
}
