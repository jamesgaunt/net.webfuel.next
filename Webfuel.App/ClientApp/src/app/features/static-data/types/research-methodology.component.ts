import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { TitleApi } from 'api/title.api';
import { DialogService } from 'core/dialog.service';
import { GridDataSource } from '../../../shared/data-source/grid-data-source';
import { Router } from '@angular/router';
import _ from '../../../shared/underscore';
import { BehaviorSubject } from 'rxjs';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';
import { CreateResearchMethodology, UpdateResearchMethodology, ResearchMethodology } from '../../../api/api.types';
import { ResearchMethodologyApi } from '../../../api/research-methodology.api';

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

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true }),
  });

  dataSource = new GridDataSource<ResearchMethodology>({
    fetch: (query) => this.researchMethodologyApi.queryResearchMethodology(_.merge(query, this.filterForm.getRawValue())),
    reorder: (items) => this.researchMethodologyApi.sortResearchMethodology({ ids: _.map(items, p => p.id) }),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open<CreateResearchMethodology, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.researchMethodologyApi.createResearchMethodology(command).subscribe((result) => {
          this.dataSource.fetch();
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
          this.dataSource.fetch();
        });
      }
    });
  }

  delete(item: ResearchMethodology) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.researchMethodologyApi.deleteResearchMethodology({ id: item.id }).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    })
  }
}
