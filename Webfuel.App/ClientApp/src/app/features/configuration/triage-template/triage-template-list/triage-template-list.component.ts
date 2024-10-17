import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { TriageTemplate, Project } from 'api/api.types';
import { ProjectStatusEnum } from '../../../../api/api.enums';
import { TriageTemplateApi } from '../../../../api/triage-template.api';
import { CreateTriageTemplateDialog } from '../create-triage-template/create-triage-template.dialog';

@Component({
  selector: 'triage-template-list',
  templateUrl: './triage-template-list.component.html'
})
export class TriageTemplateListComponent {
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public triageTemplateApi: TriageTemplateApi,
    private createTriageTemplateDialog: CreateTriageTemplateDialog
  ) {
  }


  add() {
    this.createTriageTemplateDialog.open();
  }

  edit(triageTemplate: TriageTemplate) {
    this.router.navigate(['configuration/triage-template-item', triageTemplate.id]);
  }
}
