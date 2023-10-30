import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { StaticDataCache } from '../api/static-data.cache';
import { PingApi } from '../api/ping.api';

@Component({
  selector: 'support-request-form',
  templateUrl: './support-request-form.component.html'
})
export class SupportRequestFormComponent {

  constructor(
    private router: Router,
    public pingApi: PingApi,
    public staticDataCache: StaticDataCache,
  ) {
    this.pingApi.ping().subscribe({
      next: (result) => {
        this.ping = result.timestamp;
      },
      error: (error) => {
        this.ping = JSON.stringify(error);
      }
    })
  }

  ping = "PING";

  form = new FormGroup({
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
}
