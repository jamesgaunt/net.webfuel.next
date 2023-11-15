import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';

@Component({
  selector: 'project-request',
  templateUrl: './project-request.component.html'
})
export class ProjectRequestComponent implements OnInit {

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
    this.form.disable();
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
    isFellowshipId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    proposedFundingStreamName: new FormControl<string>('', { nonNullable: true }),
    targetSubmissionDate: new FormControl<string | null>(null),
    experienceOfResearchAwards: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isTeamMembersConsultedId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    isResubmissionId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    briefDescription: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    supportRequested: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    applicationStageId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    applicationStageFreeText: new FormControl<string>('', { nonNullable: true }),
    proposedFundingStreamId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    proposedFundingCallTypeId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsFreeText: new FormControl<string>('', { nonNullable: true }),
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }
}
