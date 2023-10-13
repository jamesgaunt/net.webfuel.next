import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TitleApi } from 'api/title.api';
import { DialogService } from 'core/dialog.service';
import { CreateTitle, Title, UpdateTitle } from '../../../api/api.types';
import _ from '../../../shared/common/underscore';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';

@Component({
  selector: 'title-list',
  templateUrl: '../shared/standard.component.html'
})
export class TitleComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private titleApi: TitleApi,
  ) {
  }

  typeName = "Title";

  staticDataSource = this.titleApi.titleDataSource;

  add() {
    this.dialogService.open<CreateTitle, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.titleApi.createTitle(command).subscribe((result) => {
          this.staticDataSource.changed.emit();
        });
      }
    });
  }

  edit(item: Title) {
    this.dialogService.open<UpdateTitle, StaticDataUpdateOptions>(StaticDataUpdateDialogComponent, {
      data: {
        data: item,
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.titleApi.updateTitle(command).subscribe((result) => {
          this.staticDataSource.changed.emit();
        });
      }
    });
  }

  delete(item: Title) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.titleApi.deleteTitle({ id: item.id }).subscribe((result) => {
          this.staticDataSource.changed.emit();
        });
      }
    })
  }

  sort(items: Title[]) {
    this.titleApi.sortTitle({ ids: _.map(items, p => p.id) }).subscribe((result) => {
      this.staticDataSource.changed.emit();
    })
  }
}
