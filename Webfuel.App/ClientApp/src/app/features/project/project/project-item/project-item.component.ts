import { Component, DestroyRef, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project, ProjectStatus } from 'api/api.types';
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
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    statusId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    closureDate: new FormControl<string | null>(null),

    willStudyUseCTUId: new FormControl<string | null>(null),
    isPaidRSSAdviserLeadId: new FormControl<string | null>(null),
    isPaidRSSAdviserCoapplicantId: new FormControl<string | null>(null),
    rssHubProvidingAdviceIds: new FormControl<string[]>([], { nonNullable: true }),
    monetaryValueOfFundingApplication: new FormControl<number | null>(null),

    submittedFundingStreamId: new FormControl<string | null>(null),
    submittedFundingStreamFreeText: new FormControl<string>('', { nonNullable: true }),
    submittedFundingStreamName: new FormControl<string>('', { nonNullable: true }),

    leadAdviserUserId: new FormControl<string | null>(null),

    projectStartDate: new FormControl<string | null>(null),
    recruitmentTarget: new FormControl<number | null>(null),
    numberOfProjectSites: new FormControl<number | null>(null),
    isInternationalMultiSiteStudyId: new FormControl<string | null>(null),

    socialCare: new FormControl<boolean>(false, { nonNullable: true }),
    publicHealth: new FormControl<boolean>(false, { nonNullable: true }),

    projectAdviserUserIds: new FormControl<string[]>([], { nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    if (this.form.value.statusId != this.item.statusId) {
      this.confirmDialog.open({ title: "Status Changing", message: "Are you sure you want to change the status of this project?" }).subscribe(() => {
        this._save(close);
      });
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
}
