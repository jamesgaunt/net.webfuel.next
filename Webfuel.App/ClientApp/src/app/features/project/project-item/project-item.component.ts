import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { UserApi } from 'api/user.api';
import { Observable } from 'rxjs';
import { FormService } from '../../../core/form.service';
import { SelectDataSource } from '../../../shared/data-source/select-data-source';
import { FundingBody, Project } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { FundingBodyApi } from '../../../api/funding-body.api';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html'
})
export class ProjectItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectApi: ProjectApi,
    private formService: FormService,
    private fundingBodyApi: FundingBodyApi
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    fundingBodyId: new FormControl<string | null>(null),
    researchMethodologyId: new FormControl<string | null>(null),
  });

  save() {
    if (!this.form.valid)
      return;

    this.projectApi.updateProject(this.form.getRawValue(), { successGrowl: "Project Updated" }).subscribe((result) => {
      this.reset(result);
      this.router.navigate(['project/project-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

  // Data Sources

  fundingBodyDataSource = new SelectDataSource<FundingBody>({
    fetch: (query) => this.fundingBodyApi.queryFundingBody(query)
  });

}
