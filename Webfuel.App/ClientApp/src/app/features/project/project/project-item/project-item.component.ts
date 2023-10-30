import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { Observable, debounceTime, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html'
})
export class ProjectItemComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

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
    this.form.valueChanges.pipe(
      debounceTime(200),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe(() => this.toggleFreeText());
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
    this.toggleFreeText()
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    statusId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    isQuantativeTeamContributionId: new FormControl<string | null>(null),
    isCTUTeamContributionId: new FormControl<string | null>(null),
    isPPIEAndEDIContributionId: new FormControl<string | null>(null),

    submittedFundingStreamId: new FormControl<string | null>(null),
    submittedFundingStreamFreeText: new FormControl<string>('', { nonNullable: true }),
    submittedFundingStreamName: new FormControl<string>('', { nonNullable: true }),

    projectStartDate: new FormControl<string | null>(null),
    recruitmentTarget: new FormControl<number | null>(null),
    numberOfProjectSites: new FormControl<number | null>(null),
    isInternationalMultiSiteStudyId: new FormControl<string | null>(null),
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

  // Free Text Toggles

  toggleFreeText() {
    this.staticDataCache.fundingStream.get({ id: this.form.value.submittedFundingStreamId || '' })
      .subscribe((result) => this.showSubmittedFundingStreamFreeText = (result?.freeText ?? false));
  }

  showSubmittedFundingStreamFreeText = false;
}
