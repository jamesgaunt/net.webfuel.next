import { Component, inject } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { Query } from '../../../api/api.types';
import { IDataSource } from '../../../shared/common/data-source';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';
import _ from 'shared/common/underscore';

export class StaticDataComponent<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> {
  constructor(
    public dataSource: IDataSource<TItem, TQuery, TCreate, TUpdate>
  ) {
  }

  typeName = "Undefined";

  dialogService = inject(DialogService);

  get canAdd() {
    return this.dataSource.create !== undefined;
  }

  onAdd() {
    this.dialogService.open<TCreate, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.dataSource.create!(command).subscribe((result) => {
        });
      }
    });
  }

  get canEdit() {
    return this.dataSource.update !== undefined;
  }

  onEdit(item: TItem) {
    this.dialogService.open<TUpdate, StaticDataUpdateOptions>(StaticDataUpdateDialogComponent, {
      data: {
        data: item,
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.dataSource.update!(command).subscribe((result) => {
        });
      }
    });
  }

  get canDelete() {
    return this.dataSource.delete !== undefined;
  }

  onDelete(item: TItem) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.dataSource.delete!({ id: (<any>item)['id'] }).subscribe((result) => {
        });
      }
    })
  }
}