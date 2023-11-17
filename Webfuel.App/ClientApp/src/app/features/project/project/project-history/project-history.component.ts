import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project, QueryProjectChangeLog } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { ProjectChangeLogApi } from '../../../../api/project-change-log.api';
import { UserService } from '../../../../core/user.service';

@Component({
  selector: 'project-history',
  templateUrl: './project-history.component.html'
})
export class ProjectHistoryComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public projectChangeLogApi: ProjectChangeLogApi,
    public staticDataCache: StaticDataCache,
    public userService: UserService,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
  }

  filter(query: QueryProjectChangeLog) {
    query.projectId = this.item.id;
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

}
