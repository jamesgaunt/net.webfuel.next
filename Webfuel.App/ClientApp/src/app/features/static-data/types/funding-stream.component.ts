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
import { CreateFundingStream, UpdateFundingStream, FundingStream } from '../../../api/api.types';
import { FundingStreamApi } from '../../../api/funding-stream.api';

@Component({
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

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true }),
  });

  dataSource = new GridDataSource<FundingStream>({
    fetch: (query) => this.fundingStreamApi.queryFundingStream(_.merge(query, this.filterForm.getRawValue())),
    reorder: (items) => this.fundingStreamApi.sortFundingStream({ ids: _.map(items, p => p.id) }),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open<CreateFundingStream, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.fundingStreamApi.createFundingStream(command).subscribe((result) => {
          this.dataSource.fetch();
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
          this.dataSource.fetch();
        });
      }
    });
  }

  delete(item: FundingStream) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.fundingStreamApi.deleteFundingStream({ id: item.id }).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    })
  }
}
