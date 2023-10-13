import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { CreateResearchMethodology, ResearchMethodology, UpdateResearchMethodology } from '../../../api/api.types';
import { ResearchMethodologyApi } from '../../../api/research-methodology.api';
import _ from '../../../shared/common/underscore';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';

@Component({
  selector: 'research-methodology',
  templateUrl: '../shared/standard.component.html'
})
export class ResearchMethodologyComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private researchMethodologyApi: ResearchMethodologyApi,
  ) {
  }

  typeName = "Research Methodology";

  staticDataSource = this.researchMethodologyApi.researchMethodologyDataSource;

  add() {
    this.dialogService.open<CreateResearchMethodology, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.researchMethodologyApi.createResearchMethodology(command).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    });
  }

  edit(item: ResearchMethodology) {
    this.dialogService.open<UpdateResearchMethodology, StaticDataUpdateOptions>(StaticDataUpdateDialogComponent, {
      data: {
        data: item,
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.researchMethodologyApi.updateResearchMethodology(command).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    });
  }

  delete(item: ResearchMethodology) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.researchMethodologyApi.deleteResearchMethodology({ id: item.id }).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    })
  }

  sort(items: ResearchMethodology[]) {
    this.researchMethodologyApi.sortResearchMethodology({ ids: _.map(items, p => p.id) }).subscribe((result) => {
      this.staticDataSource.changed?.emit();
    })
  }
}
