import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { TriageSupportRequestDialog } from '../dialogs/triage-support-request/triage-support-request.dialog';

@Component({
  selector: 'support-request-item',
  templateUrl: './support-request-item.component.html'
})
export class SupportRequestItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    private triageSupportRequestDialog: TriageSupportRequestDialog,
    public staticDataCache: StaticDataCache,
    public supportRequestApi: SupportRequestApi,
   
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.supportRequest);
  }

  item!: SupportRequest;

  reset(item: SupportRequest) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isFellowshipId: new FormControl<string | null>(null),
    dateOfRequest: new FormControl<string | null>(null),
    fundingStreamName: new FormControl<string>('', { nonNullable: true }),
    targetSubmissionDate: new FormControl<string | null>(null),
    experienceOfResearchAwards: new FormControl<string>('', { nonNullable: true }),
    isTeamMembersConsultedId: new FormControl<string | null>(null),
    isResubmissionId: new FormControl<string | null>(null),
    briefDescription: new FormControl<string>('', { nonNullable: true }),
    supportRequested: new FormControl<string>('', { nonNullable: true }),
    isLeadApplicantNHSId: new FormControl<string | null>(null),
    applicationStageId: new FormControl<string | null>(null),
    fundingStreamId: new FormControl<string | null>(null),
    fundingCallTypeId: new FormControl<string | null>(null),
    howDidYouFindUsId: new FormControl<string | null>(null),
  });

  save(close: boolean) {
    if (!this.form.valid)
      return;

    this.supportRequestApi.update(this.form.getRawValue(), { successGrowl: "Support Request Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['support-request/support-request-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['support-request/support-request-list']);
  }

  triage() {
    this.triageSupportRequestDialog.open({ id: this.item.id });
  }
}
