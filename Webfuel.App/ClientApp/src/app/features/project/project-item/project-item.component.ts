import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { FormService } from '../../../core/form.service';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html'
})
export class ProjectItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    public projectApi: ProjectApi,
    private formService: FormService,
    public staticDataCache: StaticDataCache
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

  save(close: boolean) {
    if (!this.form.valid)
      return;

    this.projectApi.update(this.form.getRawValue(), { successGrowl: "Project Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['project/project-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }
}
