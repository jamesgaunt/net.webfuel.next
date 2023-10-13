import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ResearcherApi } from 'api/researcher.api';
import { Researcher } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';
import { TitleApi } from '../../../../api/title.api';

@Component({
  selector: 'researcher-item',
  templateUrl: './researcher-item.component.html'
})
export class ResearcherItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public researcherApi: ResearcherApi,
    public titleApi: TitleApi,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.researcher);
  }

  item!: Researcher;

  reset(item: Researcher) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    email: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (!this.form.valid)
      return;

    this.researcherApi.updateResearcher(this.form.getRawValue(), { successGrowl: "Researcher Updated" }).subscribe((result) => {
      this.reset(result);
      this.router.navigate(['researcher/researcher-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['researcher/researcher-list']);
  }
}
