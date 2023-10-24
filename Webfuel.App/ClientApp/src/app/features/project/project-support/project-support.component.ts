import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, ProjectSupport, QueryProjectSupport, User } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { FormService } from '../../../core/form.service';
import { ProjectSupportApi } from '../../../api/project-support.api';
import { DataSourceLookup, IDataSource, IDataSourceWithGet } from '../../../shared/common/data-source';
import { UserApi } from '../../../api/user.api';

@Component({
  selector: 'project-support',
  templateUrl: './project-support.component.html'
})
export class ProjectSupportComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private userApi: UserApi,
    private projectSupportApi: ProjectSupportApi,
    public staticDataCache: StaticDataCache
  ) {
    this.userLookup = new DataSourceLookup(userApi);
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
    this.loadProjectSupport();
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

  // Project Support

  items: ProjectSupport[] = [];

  loadProjectSupport() {
    this.projectSupportApi.query({ projectId: this.item.id, skip: 0, take: 100 }).subscribe((result) => {
      this.items = result.items;
    })
  }

  userLookup: DataSourceLookup<User>;
}
