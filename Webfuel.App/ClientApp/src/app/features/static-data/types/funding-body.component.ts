import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { CreateFundingBody, FundingBody, UpdateFundingBody } from '../../../api/api.types';
import { FundingBodyApi } from '../../../api/funding-body.api';
import _ from '../../../shared/common/underscore';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';

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

  staticDataSource = this.fundingBodyApi.fundingBodyDataSource;

  add() {
    this.dialogService.open<CreateFundingBody, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.fundingBodyApi.createFundingBody(command).subscribe((result) => {
          this.staticDataSource.changed?.emit();
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
          this.staticDataSource.changed?.emit();
        });
      }
    });
  }

  delete(item: FundingBody) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.fundingBodyApi.deleteFundingBody({ id: item.id }).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    })
  }

  sort(items: FundingBody[]) {
    this.fundingBodyApi.sortFundingBody({ ids: _.map(items, p => p.id) }).subscribe((result) => {
      this.staticDataSource.changed?.emit();
    })
  }
}
