import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportTeamApi } from 'api/support-team.api';
import { SupportTeam } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'support-team-item',
  templateUrl: './support-team-item.component.html'
})
export class SupportTeamItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public supportTeamApi: SupportTeamApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.supportTeam);
  }

  item!: SupportTeam;

  reset(item: SupportTeam) {
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

    this.supportTeamApi.update(this.form.getRawValue(), { successGrowl: "Support Team Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['support-team/support-team-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['support-team/support-team-list']);
  }
}
