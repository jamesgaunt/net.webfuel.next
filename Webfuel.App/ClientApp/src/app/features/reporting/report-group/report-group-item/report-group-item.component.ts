import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportGroupApi } from 'api/report-group.api';
import { ReportGroup } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'report-group-item',
  templateUrl: './report-group-item.component.html'
})
export class ReportGroupItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public reportGroupApi: ReportGroupApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.reportGroup);
  }

  item!: ReportGroup;

  reset(item: ReportGroup) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportGroupApi.update(this.form.getRawValue(), { successGrowl: "Report Group Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['reporting/report-group-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['reporting/report-group-list']);
  }
}
