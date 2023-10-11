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
import { CreateFundingBody, UpdateFundingBody, FundingBody } from '../../../api/api.types';
import { FundingBodyApi } from '../../../api/funding-body.api';

@Component({
  selector: 'funding-body',
  templateUrl: '../shared/standard.component.html'
})
export class FundingBodyComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private fundingBodyApi: FundingBodyApi,
  ) {
  }

  typeName = "Funding Body";

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true }),
  });

  dataSource = new GridDataSource<FundingBody>({
    fetch: (query) => this.fundingBodyApi.queryFundingBody(_.merge(query, this.filterForm.getRawValue())),
    reorder: (items) => this.fundingBodyApi.sortFundingBody({ ids: _.map(items, p => p.id) }),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open<CreateFundingBody, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.fundingBodyApi.createFundingBody(command).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    });
  }

  edit(item: FundingBody) {
    this.dialogService.open<UpdateFundingBody, StaticDataUpdateOptions>(StaticDataUpdateDialogComponent, {
      data: {
        data: item,
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.fundingBodyApi.updateFundingBody(command).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    });
  }

  delete(item: FundingBody) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.fundingBodyApi.deleteFundingBody({ id: item.id }).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    })
  }
}
