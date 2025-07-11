import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project } from 'api/api.types';
import { FormService } from 'core/form.service';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ProjectComponentBase } from '../shared/project-component-base';

@Component({
  selector: 'project-researcher',
  templateUrl: './project-researcher.component.html',
})
export class ProjectResearcherComponent extends ProjectComponentBase {
  constructor(private formService: FormService, public configurationService: ConfigurationService) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();
  }

  canUnlock() {
    return this.configurationService.hasClaim((p) => p.claims.canUnlockProjects);
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

    // Team Contact Details

    teamContactTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactEmail: new FormControl<string>('', { validators: [Validators.required, Validators.email], nonNullable: true }),
    teamContactAltEmail: new FormControl<string>('', { validators: [Validators.email], nonNullable: true }),
    teamContactRoleId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    teamContactRoleFreeText: new FormControl<string>('', { nonNullable: true }),
    teamContactMailingPermission: new FormControl<boolean>(false, { nonNullable: true }),
    teamContactPrivacyStatementRead: new FormControl<boolean>(false, { nonNullable: true }),
    teamContactServiceAgreementRead: new FormControl<boolean>(false, { nonNullable: true }),

    // Lead Applicant Details

    leadApplicantTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantEmail: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantJobRole: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantCareerStage: new FormControl<string>('', { nonNullable: true }),
    leadApplicantCareerStageId: new FormControl<string | null>(null, {}),
    leadApplicantOrganisationTypeId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisation: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantDepartment: new FormControl<string>('', { nonNullable: true }),
    leadApplicantLocationId: new FormControl<string | null>(null, {}),

    leadApplicantAddressLine1: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressLine2: new FormControl<string>('', { nonNullable: true }),
    leadApplicantAddressTown: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressCounty: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressCountry: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressPostcode: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantORCID: new FormControl<string>('', { nonNullable: true }),
    isLeadApplicantNHSId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),

    leadApplicantAgeRangeId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantGenderId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantEthnicityId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form)) return;

    this.projectApi.updateResearcher(this.form.getRawValue(), { successGrowl: 'Project Updated' }).subscribe((result) => {
      this.reset(result);
      if (close) this.router.navigate(['project/project-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }
}
