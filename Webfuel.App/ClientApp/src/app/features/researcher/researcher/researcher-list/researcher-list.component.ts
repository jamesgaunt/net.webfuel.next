import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ResearcherApi } from 'api/researcher.api';
import { DialogService } from 'core/dialog.service';
import { Researcher } from '../../../../api/api.types';
import { ResearcherCreateDialogComponent } from '../researcher-create-dialog/researcher-create-dialog.component';

@Component({
  selector: 'researcher-list',
  templateUrl: './researcher-list.component.html'
})
export class ResearcherListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    public researcherApi: ResearcherApi
  ) {
  }

  add() {
    this.dialogService.open(ResearcherCreateDialogComponent, {
    });
  }

  edit(item: Researcher) {
    this.router.navigate(['researcher/researcher-item', item.id]);
  }

  delete(item: Researcher) {
    this.dialogService.confirmDelete({
      title: item.email,
      confirmedCallback: () => {
        this.researcherApi.delete({ id: item.id }, { successGrowl: "Researcher Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
