import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Researcher } from 'api/api.types';
import { ResearcherApi } from 'api/researcher.api';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { CreateResearcherDialog } from '../dialogs/create-researcher/create-researcher.dialog';

@Component({
  selector: 'researcher-list',
  templateUrl: './researcher-list.component.html'
})
export class ResearcherListComponent {
  constructor(
    private router: Router,
    private createResearcherDialog: CreateResearcherDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public researcherApi: ResearcherApi
  ) {
  }

  add() {
    this.createResearcherDialog.open();
  }

  edit(item: Researcher) {
    this.router.navigate(['researcher/researcher-item', item.id]);
  }

  delete(item: Researcher) {
    this.confirmDeleteDialog.open({ title: "Researcher" }).subscribe(() => {
      this.researcherApi.delete({ id: item.id }, { successGrowl: "Researcher Deleted" }).subscribe();
    });
  }
}
