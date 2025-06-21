import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User, UserActivity } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { UserActivityApi } from 'api/user-activity.api';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import _ from 'shared/common/underscore';
import { ReportService } from '../../../../core/report.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { UpdateUserActivityDialog } from '../../../../shared/dialogs/update-user-activity/update-user-activity.dialog';

@Component({
  selector: 'user-activity',
  templateUrl: './user-activity.component.html',
})
export class UserActivityComponent implements OnInit {
  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private updateUserActivityDialog: UpdateUserActivityDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public userApi: UserApi,
    public userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache,
    public reportService: ReportService
  ) {}

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
  }

  item!: User;

  reset(item: User) {
    this.item = item;
    this.filterForm.patchValue({ userId: item.id });
  }

  filterForm = new FormGroup({
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    description: new FormControl<string>('', { nonNullable: true }),
    userId: new FormControl<string | null>(null),
  });

  resetFilterForm() {
    this.filterForm.patchValue({ fromDate: null, toDate: null, description: '' });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-list']);
  }

  // User Activity

  edit(userActivity: UserActivity) {
    this.updateUserActivityDialog.open({ userActivity: userActivity });
  }

  delete(userActivity: UserActivity) {
    this.confirmDeleteDialog.open({ title: 'User Activity' }).subscribe(() => {
      this.userActivityApi.delete({ id: userActivity.id }, { successGrowl: 'User Activity Deleted' }).subscribe();
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
