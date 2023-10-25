import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { User, UserActivity } from '../../../../api/api.types';
import { UserGroupApi } from '../../../../api/user-group.api';
import { FormService } from '../../../../core/form.service';
import { TitleApi } from '../../../../api/title.api';
import { StaticDataCache } from '../../../../api/static-data.cache';
import { UserActivityApi } from '../../../../api/user-activity.api';
import { DialogService } from '../../../../core/dialog.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'user-activity',
  templateUrl: './user-activity.component.html'
})
export class UserActivityComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private dialogService: DialogService,
    public userApi: UserApi,
    public userActivityApi: UserActivityApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
    this.loadUserActivity();

    this.userActivityApi.changed.pipe(
      takeUntilDestroyed(this.destroyRef)
    )
    .subscribe(() => this.loadUserActivity());
  }

  item!: User;

  reset(item: User) {
    this.item = item;
    this.form.markAsPristine();
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['user/user-list']);
  }

  // User Activity

  items: UserActivity[] = [];

  loadUserActivity() {
    this.userActivityApi.query({ userId: this.item.id, skip: 0, take: 100 }).subscribe((result) => {
      this.items = result.items;
    })
  }

  editUserActivity(userActivity: UserActivity) {
    this.dialogService.updateUserActivity({ userActivity: userActivity });
  }

  deleteUserActivity(userActivity: UserActivity) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.userActivityApi.delete({ id: userActivity.id }, { successGrowl: "User Activity Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
