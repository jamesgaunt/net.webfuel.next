import { DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { SupportRequest } from '../../../api/api.types';
import { SupportRequestApi } from '../../../api/support-request.api';
import { FormService } from '../../../core/form.service';

@Component({
  templateUrl: './support-request-create-dialog.component.html'
})
export class SupportRequestCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<SupportRequest>,
    private formService: FormService,
    private supportRequestApi: SupportRequestApi
  ) {
  }

  form = new FormGroup({
    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isFellowshipId: new FormControl<string | null>(null),
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

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.supportRequestApi.create(this.form.getRawValue(), { successGrowl: "Support Request Created" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
