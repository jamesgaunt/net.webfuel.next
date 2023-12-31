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
  selector: 'project-researcher',
  templateUrl: './project-researcher.component.html'
})
export class ProjectResearcherComponent implements OnInit {

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

  cancel() {
    this.reset(this.item);
    this.router.navigate(['project/project-list']);
  }
}
