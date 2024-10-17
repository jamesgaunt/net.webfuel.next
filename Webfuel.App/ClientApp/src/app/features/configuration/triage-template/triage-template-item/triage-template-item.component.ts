import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TriageTemplate } from 'api/api.types';
import { TriageTemplateApi } from 'api/triage-template.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ReportService } from '../../../../core/report.service';
import { ConfirmDialog } from '../../../../shared/dialogs/confirm/confirm.dialog';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'triage-template-item',
  templateUrl: './triage-template-item.component.html'
})
export class TriageTemplateItemComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmDialog: ConfirmDialog,
    private confirmDeleteDialog: ConfirmDialog,
    private formService: FormService,
    public triageTemplateApi: TriageTemplateApi,
    public staticDataCache: StaticDataCache,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.triageTemplate);
  }

  // TriageTemplate

  item!: TriageTemplate;

  reset(item: TriageTemplate) {
    this.item = item;

    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    subject: new FormControl<string>('', { nonNullable: true }),
    htmlTemplate: new FormControl<string>('', { nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.triageTemplateApi.update(this.form.getRawValue(), { successGrowl: "Triage Template Updated" }).subscribe((result) => {
      this.reset(result);
      if (close)
        this.router.navigate(['configuration/triage-template-list']);
    });
  }
  
  cancel() {
    this.reset(this.item);
    this.router.navigate(['configuration/triage-template-list']);
  }
}
