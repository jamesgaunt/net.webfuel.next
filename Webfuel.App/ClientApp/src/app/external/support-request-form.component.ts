import { Component, DestroyRef, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StaticDataCache } from '../api/static-data.cache';
import { PingApi } from '../api/ping.api';
import { SupportRequestApi } from '../api/support-request.api';
import { FormService } from '../core/form.service';
import { debounceTime, takeUntil } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ResearcherRoleEnum } from '../api/api.enums';
import { Validate } from '../shared/common/validate';

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
    private httpClient: HttpClient,
  ) {

    this.form.valueChanges.pipe(
      takeUntilDestroyed(this.destroyRef),
      debounceTime(200)
    ).subscribe((result) => {
      if (this.form.valid)
        this.errorMessage = "";
    });
  }

  isTeamContactAlsoChiefInvestigator() {
    var isChief =
      this.form.value.teamContactRoleId == ResearcherRoleEnum.ChiefInvestigator ||
      this.form.value.teamContactRoleId == ResearcherRoleEnum.FellowshipApplicant;
    if (isChief)
      this.syncTeamContactDetails();
    return isChief;
  }

  syncTeamContactDetails() {
    this.form.patchValue({ leadApplicantTitle: this.form.value.teamContactTitle });
    this.form.patchValue({ leadApplicantFirstName: this.form.value.teamContactFirstName });
    this.form.patchValue({ leadApplicantLastName: this.form.value.teamContactLastName });
    this.form.patchValue({ leadApplicantEmail: this.form.value.teamContactEmail });
  }

  stage: 'input' | 'submitted' | 'error' = 'input';

  form = new FormGroup({

    // Project Details

    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isFellowshipId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    proposedFundingStreamName: new FormControl<string>('', { nonNullable: true }),
    targetSubmissionDate: new FormControl<string | null>(null, { validators: [Validate.dateMustBeInFuture()] }),
    experienceOfResearchAwards: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isTeamMembersConsultedId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    isResubmissionId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    briefDescription: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    supportRequested: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    applicationStageId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    applicationStageFreeText: new FormControl<string>('', { nonNullable: true }),
    proposedFundingStreamId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    proposedFundingCallTypeId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsFreeText: new FormControl<string>('', { nonNullable: true }),
    whoElseIsOnTheStudyTeam: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isCTUAlreadyInvolvedId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    isCTUAlreadyInvolvedFreeText: new FormControl<string>('', { nonNullable: true }),

    // Team Contact Details

    teamContactTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    teamContactEmail: new FormControl<string>('', { validators: [Validators.required, Validators.email], nonNullable: true }),
    teamContactRoleId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    teamContactRoleFreeText: new FormControl<string>('', { nonNullable: true }),
    teamContactMailingPermission: new FormControl<boolean>(false, { validators: [Validators.requiredTrue], nonNullable: true }),
    teamContactPrivacyStatementRead: new FormControl<boolean>(false, { validators: [Validators.requiredTrue], nonNullable: true }),

    // Lead Applicant Details

    leadApplicantTitle: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantFirstName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantLastName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantEmail: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantJobRole: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisationTypeId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantOrganisation: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantDepartment: new FormControl<string>('', { nonNullable: true }),

    leadApplicantAddressLine1: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressLine2: new FormControl<string>('', { nonNullable: true }),
    leadApplicantAddressTown: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressCounty: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressCountry: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    leadApplicantAddressPostcode: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    leadApplicantORCID: new FormControl<string>('', { nonNullable: true }),
    isLeadApplicantNHSId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),

    leadApplicantAgeRangeId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantGenderId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    leadApplicantEthnicityId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),

    // Files
    files: new FormControl(null),
    fileStorageGroupId: new FormControl<string | null>(null),
  });

  submitting = false;

  progress: number | null = null;

  errorMessage = "";

  submit() {
    // Check title/name are syncronised
    this.isTeamContactAlsoChiefInvestigator();

    if (this.formService.hasErrors(this.form)) {
      this.errorMessage = "Please complete all required fields";
      return;
    }

    this.submitting = true;
    this.submitFiles();
  }

  submitFiles() {
    var fileData = this.buildFileData();
    if (fileData == null) {
      // no files to upload
      this.submitForm(null);
      return;
    }

    this.httpClient.post(environment.apiHost + "api/support-request/files", fileData, {
      reportProgress: true,
      observe: 'events'
    }).subscribe({
      next: (event) => {
        if (event.type === HttpEventType.UploadProgress && event.total) {
          this.progress = Math.round((100 * event.loaded) / event.total);
        }
        if (event.type === HttpEventType.Response) {
          // We are done submiting files
          console.log(event);
          var fileStorageGroupId = (<any>event.body)?.fileStorageGroupId;
          if (!fileStorageGroupId) {
            this.fileSubmissionError("FileStorageGroupId not returned by server");
          } else {
            this.submitForm(fileStorageGroupId);
          }
        }
      },
      error: (err) => {
        this.fileSubmissionError(err);
      }
    });
  }

  submitForm(fileStorageGroupId: string | null) {
    this.form.patchValue({ fileStorageGroupId: fileStorageGroupId });
    this.supportRequestApi.submitForm(this.form.getRawValue()).subscribe({
      next: (result) => {
        this.submitting = false;
        this.progress = null;
        this.stage = 'submitted';
      },
      error: (err) => {
        this.submitting = false;
        this.progress = null;
        this.formSubmissionError(err);
      }
    });
  }

  fileSubmissionError(err: any) {
    this.submitting = false;
    this.progress = null;
    console.log("File Submission Error: ", err);

    alert("There was an error uploading your files.\n\nPlease ensure all files are saved on your computer, and not open in another application.");
  }

  formSubmissionError(err: any) {
    this.submitting = false;
    this.progress = null;
    console.log("Form Submission Error: ", err);

    this.stage = 'error';
  }

  buildFileData(): FormData | null {
    var files = <any>this.form.value.files;
    if (!files || !files.length)
      return null;
    const formData = new FormData();
    for (var i = 0; i < files.length; i++) {
      formData.append("file_" + i, files[i]);
    }
    return formData;
  }
}
