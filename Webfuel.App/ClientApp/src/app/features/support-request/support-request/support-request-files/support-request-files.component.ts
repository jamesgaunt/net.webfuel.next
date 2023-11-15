import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FileStorageEntry, SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import { FormService } from 'core/form.service';
import { TriageSupportRequestDialog } from '../dialogs/triage-support-request/triage-support-request.dialog';
import { SupportRequestComponentBase } from '../shared/support-request-component-base';

@Component({
  selector: 'support-request-files',
  templateUrl: './support-request-files.component.html'
})
export class SupportRequestFilesComponent extends SupportRequestComponentBase {

  constructor(
    private formService: FormService,
    public supportRequestApi: SupportRequestApi
  ) {
    super();
  }

  form = new FormGroup({
  });

  cancel() {
    this.reset(this.item);
    this.router.navigate(['support-request/support-request-list']);
  }
}
