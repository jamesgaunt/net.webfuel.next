import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ResearcherApi } from 'api/researcher.api';
import { DialogService } from 'core/dialog.service';
import { Researcher } from 'api/api.types';
import { ResearcherCreateDialogComponent, ResearcherCreateDialogService } from '../dialogs/researcher-create-dialog/researcher-create-dialog.component';
import { ConfirmDeleteDialogService } from 'shared/dialogs/confirm-delete/confirm-delete-dialog.component';

@Component({
  selector: 'researcher-list',
  templateUrl: './researcher-list.component.html'
})
export class ResearcherListComponent {
  constructor(
    private router: Router,
    private createResearcherDialog: ResearcherCreateDialogService,
    private confirmDeleteDialog: ConfirmDeleteDialogService,
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
