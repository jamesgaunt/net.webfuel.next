import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserApi } from 'api/user.api';
import { SupportTeam, SupportTeamUser, User } from 'api/api.types';
import { UserGroupApi } from 'api/user-group.api';
import { FormService } from 'core/form.service';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportTeamUserApi } from '../../../../api/support-team-user.api';
import { SupportTeamApi } from '../../../../api/support-team.api';
import _ from 'shared/common/underscore';

interface SupportTeamEx extends SupportTeam {
  _processing?: boolean;
}

@Component({
  selector: 'user-support-team',
  templateUrl: './user-support-team.component.html'
})
export class UserSupportTeamComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public userApi: UserApi,
    public supportTeamApi: SupportTeamApi,
    public supportTeamUserApi: SupportTeamUserApi,
    public staticDataCache: StaticDataCache,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
    this.load();
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

  // Implementation

  load() {
    this.supportTeamApi.query({ skip: 0, take: 100 }).subscribe((result) => this.supportTeams = result.items);
    this.supportTeamUserApi.query({ skip: 0, take: 100, userId: this.item.id, supportTeamId: null }).subscribe((result) => this.supportTeamUsers = result.items);
  }

  supportTeams: SupportTeamEx[] = [];

  supportTeamUsers: SupportTeamUser[] = [];

  isSupportTeamSelected(supportTeam: SupportTeam) {
    return _.some(this.supportTeamUsers, (p) => p.supportTeamId == supportTeam.id);
  }

  toggleSupportTeam(supportTeam: SupportTeamEx) {
    supportTeam._processing = true;
    if (this.isSupportTeamSelected(supportTeam)) {
      this.supportTeamUserApi.delete({ userId: this.item.id, supportTeamId: supportTeam.id }, { successGrowl: "Removed from support team" }).subscribe(() => {
        this.load();
      });
    } else {
      this.supportTeamUserApi.insert({ userId: this.item.id, supportTeamId: supportTeam.id }, { successGrowl: "Added to support team" }).subscribe(() => {
        this.load();
      });
    }
  }
}
