import { Component, DestroyRef, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ClientConfiguration, Project } from 'api/api.types';
import { FormService } from 'core/form.service';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ProjectComponentBase } from '../shared/project-component-base';

@Component({
  selector: 'project-item',
  templateUrl: './project-item.component.html'
})
export class ProjectItemComponent extends ProjectComponentBase {

  destroyRef: DestroyRef = inject(DestroyRef);

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
