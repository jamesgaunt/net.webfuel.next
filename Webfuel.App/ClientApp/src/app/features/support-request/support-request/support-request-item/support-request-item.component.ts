import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { TriageSupportRequestDialog } from '../dialogs/triage-support-request/triage-support-request.dialog';
import { SupportRequestComponentBase } from '../shared/support-request-component-base';
import { AlertDialog } from '../../../../shared/dialogs/alert/alert.dialog';
import { Validate } from '../../../../shared/common/validate';

@Component({
  selector: 'support-request-item',
  templateUrl: './support-request-item.component.html'
})
export class SupportRequestItemComponent extends SupportRequestComponentBase {

  constructor(
    private formService: FormService,
    private alertDialog: AlertDialog,
    private triageSupportRequestDialog: TriageSupportRequestDialog,
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

    // Meta (not converted to project data)

    isThisRequestLinkedToAnExistingProject: new FormControl<boolean>(false, { nonNullable: true }), // 1.2 Development

    // Project Details

    title: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isFellowshipId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    nihrApplicationId: new FormControl<string>('', { nonNullable: true }),
    proposedFundingStreamName: new FormControl<string>('', { nonNullable: true }),
    targetSubmissionDate: new FormControl<string | null>(null),
    experienceOfResearchAwards: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isTeamMembersConsultedId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    isResubmissionId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    briefDescription: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    supportRequested: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    applicationStageId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    applicationStageFreeText: new FormControl<string>('', { nonNullable: true }),
    proposedFundingStreamId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    proposedFundingCallTypeId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    howDidYouFindUsFreeText: new FormControl<string>('', { nonNullable: true }),
    whoElseIsOnTheStudyTeam: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    isCTUAlreadyInvolvedId: new FormControl<string | null>(null!, { validators: [Validators.required], nonNullable: true }),
    isCTUAlreadyInvolvedFreeText: new FormControl<string>('', { nonNullable: true }),
    professionalBackgroundIds: new FormControl<string[]>([], { validators: [Validate.minArrayLength(1)], nonNullable: true }), // 1.2 Development
    professionalBackgroundFreeText: new FormControl<string>('', { nonNullable: true }),  // 1.2 Development
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
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

    if (!this.form.pristine) {
      this.alertDialog.open({ title: "Warning", message: "Please save unsaved changes before triaging" });
      return;
    }

    this.triageSupportRequestDialog.open({ id: this.item.id }).subscribe((result) => {
      this.reset(result);
    });
  }
}
