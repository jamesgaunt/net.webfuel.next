import { Component, DestroyRef, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectStatus } from 'api/api.types';
import { ProjectStatusEnum } from 'api/api.enums';
import { FormService } from 'core/form.service';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ProjectComponentBase } from '../shared/project-component-base';
import { UserApi } from '../../../../api/user.api';
import { ProjectAdviserApi } from '../../../../api/project-adviser.api';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html'
})
export class ProjectItemComponent extends ProjectComponentBase {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private formService: FormService,
    public userApi: UserApi,
    private projectAdviserApi: ProjectAdviserApi,
    public configurationService: ConfigurationService
  ) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();

    this.projectAdviserApi.selectUserIdsByProjectId({ projectId: this.item.id }).subscribe((result) => {
      this.form.patchValue({
        projectAdviserUserIds: result
      })
    });
  }

  canUnlock() {
    return this.configurationService.hasClaim(p => p.claims.canUnlockProjects);
  }

  isAdministrator() {
    return this.configurationService.hasClaim(p => p.claims.administrator);
  }

  isDeveloper() {
    return this.configurationService.hasClaim(p => p.claims.developer);
  }

  filterStatus = (status: ProjectStatus) => {
    if (status.locked && !this.canUnlock())
      return false;
    return true;
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
    if (!this.isAdministrator()) {
      this.form.controls.administratorComments.disable();
    }
  }

  isRoundRobinEnquiry() {
    return this.form.getRawValue().isRoundRobinEnquiry;
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    statusId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    closureDate: new FormControl<string | null>(null),
    administratorComments: new FormControl<string>('', { nonNullable: true }),
    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    willStudyUseCTUId: new FormControl<string | null>(null),
    isPaidRSSAdviserLeadId: new FormControl<string | null>(null),
    isPaidRSSAdviserCoapplicantId: new FormControl<string | null>(null),
    rssHubProvidingAdviceIds: new FormControl<string[]>([], { nonNullable: true }),
    monetaryValueOfFundingApplication: new FormControl<number | null>(null),

    submittedFundingStreamId: new FormControl<string | null>(null),
    submittedFundingStreamFreeText: new FormControl<string>('', { nonNullable: true }),
    submittedFundingStreamName: new FormControl<string>('', { nonNullable: true }),

    leadAdviserUserId: new FormControl<string | null>(null),

    outlineSubmissionDeadline: new FormControl<string | null>(null),
    outlineSubmissionStatusId: new FormControl<string | null>(null),
    outlineOutcomeExpectedDate: new FormControl<string | null>(null),
    outlineOutcomeId: new FormControl<string | null>(null),

    fullSubmissionDeadline: new FormControl<string | null>(null),
    fullSubmissionStatusId: new FormControl<string | null>(null),
    fullOutcomeExpectedDate: new FormControl<string | null>(null),
    fullOutcomeId: new FormControl<string | null>(null),

    projectStartDate: new FormControl<string | null>(null),
    recruitmentTarget: new FormControl<number | null>(null),
    numberOfProjectSites: new FormControl<number | null>(null),
    isInternationalMultiSiteStudyId: new FormControl<string | null>(null),

    socialCare: new FormControl<boolean>(false, { nonNullable: true }),
    publicHealth: new FormControl<boolean>(false, { nonNullable: true }),

    projectAdviserUserIds: new FormControl<string[]>([], { nonNullable: true }),

    // Misc

    isRoundRobinEnquiry: new FormControl<boolean>(false, { nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    if (this.form.value.statusId != this.item.statusId) {

      if ((this.form.value.statusId == ProjectStatusEnum.Closed || this.form.value.statusId == ProjectStatusEnum.ClosedNotSubmitted) && this.item.diagnosticCount > 0) {
        this.confirmDialog.open({ title: "Closing project with open diagnostics", message: "Are you sure you want to close this project with open diagnostic issues?", style: "danger" }).subscribe(() => {
          this._save(close);
        });
      } else {
        this.confirmDialog.open({ title: "Status changing", message: "Are you sure you want to change the status of this project?" }).subscribe(() => {
          this._save(close);
        });
      }
    }
    else { 
      this._save(close);
    }
  }

  private _save(close: boolean) {
    this.projectApi.update(this.form.getRawValue(), { successGrowl: "Project Updated" }).subscribe((result) => {
      this.reset(result);
      if (close)
        this.router.navigate(['project/project-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }

  enrich() {
    this.projectApi.enrich({ id: this.item.id }).subscribe((result) => {
      this.reset(result);
    })
  }
}
