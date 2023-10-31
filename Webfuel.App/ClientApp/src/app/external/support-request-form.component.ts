import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StaticDataCache } from '../api/static-data.cache';
import { PingApi } from '../api/ping.api';
import { SupportRequestApi } from '../api/support-request.api';
import { FormService } from '../core/form.service';
import { debounceTime, takeUntil } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'support-request-form',
  templateUrl: './support-request-form.component.html'
})
export class SupportRequestFormComponent {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private router: Router,
    private formService: FormService,
    private supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache,
  ) {
    this.form.valueChanges.pipe(
      takeUntilDestroyed(this.destroyRef),
      debounceTime(200)
    ).subscribe((result) => {
      if (this.form.valid)
        this.errorMessage = "";
    })
  }

  stage: 'input' | 'submitted' | 'error' = 'input';

  form = new FormGroup({

    // Project Details

    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isFellowshipId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    fundingStreamName: new FormControl<string>('', { nonNullable: true }),
    targetSubmissionDate: new FormControl<string | null>(null),
    experienceOfResearchAwards: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isTeamMembersConsultedId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    isResubmissionId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    briefDescription: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    supportRequested: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    applicationStageId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    fundingStreamId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    fundingCallTypeId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),

    // Team Contact Details

    teamContactTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactEmail: new FormControl<string>('', { validators: [Validators.required, Validators.email], nonNullable: true }),
    teamContactRoleId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    teamContactMailingPermission: new FormControl<boolean>(false, { validators: [Validators.requiredTrue], nonNullable: true }),
    teamContactPrivacyStatementRead: new FormControl<boolean>(false, { validators: [Validators.requiredTrue], nonNullable: true }),

    // Lead Applicant Details

    leadApplicantTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantJobRole: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisationTypeId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisation: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantDepartment: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantAddressLine1: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressLine2: new FormControl<string>('', { nonNullable: true }),
    leadApplicantAddressTown: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressCounty: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressCountry: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressPostcode: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantORCID: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isLeadApplicantNHSId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),

    leadApplicantAgeRangeId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantDisabilityId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantGenderId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantEthnicityId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  submitting = false;

  errorMessage = "";

  submit() {
    if (this.formService.hasErrors(this.form)) {
      this.errorMessage = "Please complete all required fields";
      return;
    }

    this.submitting = true;
    this.supportRequestApi.create(this.form.getRawValue()).subscribe({
      next: (result) => {
        this.stage = 'submitted';
      },
      error: (err) => {
        this.stage = 'error';
      },
      complete: () => {
        setTimeout(() => this.submitting = false, 1000);
      }
    });
  }
}
