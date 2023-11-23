import { Component, OnInit } from '@angular/core';
import { DashboardApi } from '../../../api/dashboard.api';
import { DashboardModel, ProjectTeamSupport } from '../../../api/api.types';
import { StaticDataCache } from '../../../api/static-data.cache';
import { UserService } from '../../../core/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  constructor(
    private router: Router,
    private dashboardApi: DashboardApi,
    public userService: UserService,
    public staticDataCache: StaticDataCache,
  ) {
  }

  ngOnInit(): void {
    this.dashboardApi.getDashboardModel().subscribe((result) => this.model = result);
  }

  model: DashboardModel | null = null;

  viewTeamSupport(teamSupport: ProjectTeamSupport) {
    this.router.navigateByUrl("/project/project-team-support/" + teamSupport.projectId);
  }
}
