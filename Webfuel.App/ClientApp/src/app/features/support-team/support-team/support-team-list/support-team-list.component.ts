import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SupportTeamApi } from 'api/support-team.api';
import { SupportTeam } from '../../../../api/api.types';
import { CreateSupportTeamDialog } from '../dialogs/create-support-team/create-support-team.dialog';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';

@Component({
  selector: 'support-team-list',
  templateUrl: './support-team-list.component.html'
})
export class SupportTeamListComponent {
  constructor(
    private router: Router,
    private createSupportTeamDialog: CreateSupportTeamDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public supportTeamApi: SupportTeamApi,
  ) {
  }

  add() {
    this.createSupportTeamDialog.open();
  }

  edit(item: SupportTeam) {
    this.router.navigate(['support-team/support-team-item', item.id]);
  }

  delete(item: SupportTeam) {
    this.confirmDeleteDialog.open({ title: "Support Team" }).subscribe(() => {
      this.supportTeamApi.delete({ id: item.id }, { successGrowl: "Support Team Deleted" }).subscribe();
    });
  }
}
