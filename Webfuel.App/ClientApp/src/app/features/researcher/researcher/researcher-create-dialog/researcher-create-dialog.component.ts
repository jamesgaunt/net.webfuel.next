import { DialogRef } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ResearcherApi } from 'api/researcher.api';
import { Researcher } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';

@Component({
  selector: 'researcher-create-dialog-component',
  templateUrl: './researcher-create-dialog.component.html'
})
export class ResearcherCreateDialogComponent {

  constructor(
    private dialogRef: DialogRef<Researcher>,
    private formService: FormService,
    private researcherApi: ResearcherApi,
  ) {
  }

  form = new FormGroup({
    email: new FormControl('', { validators: [Validators.required], nonNullable: true }),
  });

  save() {
    if (this.formService.checkForErrors(this.form))
      return;

    this.researcherApi.create(this.form.getRawValue(), { successGrowl: "Researcher Created" }).subscribe((result) => {
      this.dialogRef.close();
    });
  }

  cancel() {
    this.dialogRef.close();
  }
}
