import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { FormService } from 'core/form.service';
import { Validate } from '../../../../shared/common/validate';
import { ProjectComponentBase } from '../shared/project-component-base';
import { ConfigurationService } from '../../../../core/configuration.service';

@Component({
  selector: 'project-request',
  templateUrl: './project-request.component.html'
})
export class ProjectRequestComponent extends ProjectComponentBase {

  constructor(
    private formService: FormService,
    public configurationService: ConfigurationService
  ) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();
  }

  canUnlock() {
    return this.configurationService.hasClaim(p => p.claims.canUnlockProjects);
  }

  reset(item: Project) {
    super.reset(item);

    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  protected applyLock() {
    this.form.disable();
  }

  protected clearLock() {
    this.form.enable();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isFellowshipId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    nihrApplicationId: new FormControl<string>('', { nonNullable: true }),
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
    whoElseIsOnTheStudyTeam: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isCTUAlreadyInvolvedId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    isCTUAlreadyInvolvedFreeText: new FormControl<string>('', { nonNullable: true }),
    professionalBackgroundIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }), // 1.2 Development
    professionalBackgroundFreeText: new FormControl<string>('', { nonNullable: true }), // 1.2 Development
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.projectApi.updateRequest(this.form.getRawValue(), { successGrowl: "Project Updated" }).subscribe((result) => {
      this.reset(result);
      if (close)
        this.router.navigate(['project/project-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }
}
