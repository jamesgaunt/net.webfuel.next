import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Heartbeat } from 'api/api.types';
import { HeartbeatApi } from 'api/heartbeat.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ReportService } from '../../../../core/report.service';
import { ConfirmDialog } from '../../../../shared/dialogs/confirm/confirm.dialog';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'heartbeat-item',
  templateUrl: './heartbeat-item.component.html'
})
export class HeartbeatItemComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmDialog: ConfirmDialog,
    private confirmDeleteDialog: ConfirmDialog,
    private formService: FormService,
    public heartbeatApi: HeartbeatApi,
    public staticDataCache: StaticDataCache,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.heartbeat);
  }

  // Heartbeat

  item!: Heartbeat;

  reset(item: Heartbeat) {
    this.item = item;

    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    providerName: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    providerParameter: new FormControl<string>('', { nonNullable: true }),
    live: new FormControl<boolean>(true, { nonNullable: true }),

    minTime: new FormControl<string>('', { nonNullable: true }),
    maxTime: new FormControl<string>('', { nonNullable: true }),
    schedule: new FormControl<string>('', { nonNullable: true }),
    logSuccessfulExecutions: new FormControl<boolean>(true, { nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.heartbeatApi.update(this.form.getRawValue(), { successGrowl: "Heartbeat Updated" }).subscribe((result) => {
      this.reset(result);
      if (close)
        this.router.navigate(['developer/heartbeat-list']);
    });
  }
  
  cancel() {
    this.reset(this.item);
    this.router.navigate(['developer/heartbeat-list']);
  }
}
