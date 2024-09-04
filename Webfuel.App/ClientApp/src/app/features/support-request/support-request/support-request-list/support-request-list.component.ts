import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SupportRequest } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { SupportRequestApi } from 'api/support-request.api';
import _ from 'shared/common/underscore';
import { ReportService } from '../../../../core/report.service';

@Component({
  selector: 'support-request-list',
  templateUrl: './support-request-list.component.html'
})
export class SupportRequestListComponent {
  constructor(
    private router: Router,
    public supportRequestApi: SupportRequestApi,
    public staticDataCache: StaticDataCache,
    private reportService: ReportService
  ) {
  }

  filterForm = new FormGroup({
    number: new FormControl<string>('', { nonNullable: true }),
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    statusId: new FormControl<string | null>(null),
    title: new FormControl<string>('', { nonNullable: true }),
    teamContactFullName: new FormControl<string>('', { nonNullable: true }),
    proposedFundingStreamId: new FormControl<string | null>(null),
  });

  resetFilterForm() {
    this.filterForm.patchValue({
      number: '',
      fromDate: null,
      toDate: null,
      statusId: null,
      title: ''
    })
  }

  add() {
    window.open("/external/support-request", "_blank");
  }

  edit(item: SupportRequest) {
    this.router.navigate(['support-request/support-request-item', item.id]);
  }

  export() {
    this.supportRequestApi.export(_.merge(this.filterForm.getRawValue(), { skip: 0, take: 0 })).subscribe((result) => {
      this.reportService.runReport(result);
    });
  }
}
