import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { CreateFundingStream, FundingStream, UpdateFundingStream } from '../../../api/api.types';
import { FundingStreamApi } from '../../../api/funding-stream.api';
import _ from '../../../shared/common/underscore';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';

@Component({
  selector: 'funding-stream',
  templateUrl: '../shared/standard.component.html'
})
export class FundingStreamComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private fundingStreamApi: FundingStreamApi,
  ) {
  }

  typeName = "Funding Stream";

  staticDataSource = this.fundingStreamApi.fundingStreamDataSource;

  add() {
    this.dialogService.open<CreateFundingStream, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.fundingStreamApi.createFundingStream(command).subscribe((result) => {
          this.staticDataSource.changed.emit();
        });
      }
    });
  }

  edit(item: FundingStream) {
    this.dialogService.open<UpdateFundingStream, StaticDataUpdateOptions>(StaticDataUpdateDialogComponent, {
      data: {
        data: item,
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.fundingStreamApi.updateFundingStream(command).subscribe((result) => {
          this.staticDataSource.changed.emit();
        });
      }
    });
  }

  delete(item: FundingStream) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.fundingStreamApi.deleteFundingStream({ id: item.id }).subscribe((result) => {
          this.staticDataSource.changed.emit();
        });
      }
    })
  }

  sort(items: FundingStream[]) {
    this.fundingStreamApi.sortFundingStream({ ids: _.map(items, p => p.id) }).subscribe((result) => {
      this.staticDataSource.changed.emit();
    })
  }
}
