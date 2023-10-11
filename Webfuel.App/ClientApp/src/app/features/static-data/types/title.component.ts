import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { TitleApi } from 'api/title.api';
import { DialogService } from 'core/dialog.service';
import { GridDataSource } from '../../../shared/data-source/grid-data-source';
import { Router } from '@angular/router';
import _ from '../../../shared/underscore';
import { BehaviorSubject } from 'rxjs';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateOptions, StaticDataUpdateDialogComponent } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';
import { CreateTitle, UpdateTitle, Title } from '../../../api/api.types';

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

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true }),
  });

  dataSource = new GridDataSource<Title>({
    fetch: (query) => this.titleApi.queryTitle(_.merge(query, this.filterForm.getRawValue())),
    reorder: (items) => this.titleApi.sortTitle({ ids: _.map(items, p => p.id) }),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open<CreateTitle, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.titleApi.createTitle(command).subscribe((result) => {
          this.dataSource.fetch();
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
          this.dataSource.fetch();
        });
      }
    });
  }

  delete(item: Title) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.titleApi.deleteTitle({ id: item.id }).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    })
  }
}
