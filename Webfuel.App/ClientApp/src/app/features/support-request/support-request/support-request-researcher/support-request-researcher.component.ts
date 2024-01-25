import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { TriageSupportRequestDialog } from '../dialogs/triage-support-request/triage-support-request.dialog';
import { SupportRequestComponentBase } from '../shared/support-request-component-base';

@Component({
  selector: 'support-request-researcher',
  templateUrl: './support-request-researcher.component.html'
})
export class SupportRequestResearcherComponent extends SupportRequestComponentBase {

  constructor(
    private formService: FormService,
    public supportRequestApi: SupportRequestApi,
   
  ) {
    super();
  }

  reset(item: SupportRequest) {
    super.reset(item);
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  applyLock() {
    this.form.disable();
  }

  clearLock() {
    this.form.enable();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    // Team Contact Details

    teamContactTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactEmail: new FormControl<string>('', { validators: [Validators.required, Validators.email], nonNullable: true }),
    teamContactRoleId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    teamContactRoleFreeText: new FormControl<string>('', { nonNullable: true }),
    teamContactMailingPermission: new FormControl<boolean>(false, { validators: [Validators.requiredTrue], nonNullable: true }),
    teamContactPrivacyStatementRead: new FormControl<boolean>(false, { validators: [Validators.requiredTrue], nonNullable: true }),

    // Lead Applicant Details

    leadApplicantTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantEmail: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantJobRole: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisationTypeId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisation: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantDepartment: new FormControl<string>('', { nonNullable: true }),

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
    if (this.formService.hasErrors(this.form))
      return;

    this.supportRequestApi.updateResearcher(this.form.getRawValue(), { successGrowl: "Support Request Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['support-request/support-request-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['support-request/support-request-list']);
  }
}
