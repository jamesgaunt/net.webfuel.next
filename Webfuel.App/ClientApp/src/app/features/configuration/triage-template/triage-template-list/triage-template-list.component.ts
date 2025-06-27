import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TriageTemplate } from 'api/api.types';
import { TriageTemplateApi } from '../../../../api/triage-template.api';
import { CreateTriageTemplateDialog } from '../create-triage-template/create-triage-template.dialog';
import { ConfirmDeleteDialog } from 'shared/dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'triage-template-list',
  templateUrl: './triage-template-list.component.html',
})
export class TriageTemplateListComponent {
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public triageTemplateApi: TriageTemplateApi,
    private createTriageTemplateDialog: CreateTriageTemplateDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog
  ) {}

  add() {
    this.createTriageTemplateDialog.open();
  }

  edit(triageTemplate: TriageTemplate) {
    this.router.navigate(['configuration/triage-template-item', triageTemplate.id]);
  }

  delete(item: TriageTemplate) {
    this.confirmDeleteDialog.open({ title: 'Triage Tempplate' }).subscribe(() => {
      this.triageTemplateApi.delete({ id: item.id }, { successGrowl: 'Triage Template Deleted' }).subscribe();
    });
  }
}
