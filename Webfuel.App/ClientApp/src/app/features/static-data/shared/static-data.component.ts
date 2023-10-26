import { inject } from '@angular/core';
import { Query } from '../../../api/api.types';
import { IDataSource } from '../../../shared/common/data-source';
import { ConfirmDeleteDialogService } from '../../../shared/dialogs/confirm-delete/confirm-delete-dialog.component';
import { StaticDataCreateDialogService } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogService } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';

export class StaticDataComponent<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> {
  constructor(
    public dataSource: IDataSource<TItem, TQuery, TCreate, TUpdate>
  ) {
  }

  typeName = "Undefined";

  createStaticDataDialog = inject(StaticDataCreateDialogService);

  updateStaticDataDialog = inject(StaticDataUpdateDialogService);

  confirmDeleteDialog = inject(ConfirmDeleteDialogService);

  get canAdd() {
    return this.dataSource.create !== undefined;
  }

  onAdd() {
    this.createStaticDataDialog.open<TCreate>({
      typeName: this.typeName,
      enableHidden: this.enableHidden,
      enableFreeText: this.enableFreeText,
    }).subscribe((result) => {
      this.dataSource.create!(result).subscribe();
    });
  }

  get canEdit() {
    return this.dataSource.update !== undefined;
  }

  onEdit(item: TItem) {
    this.updateStaticDataDialog.open<TUpdate>({
      data: item,
      typeName: this.typeName,
      enableHidden: this.enableHidden,
      enableFreeText: this.enableFreeText,
    }).subscribe((result) => {
      this.dataSource.update!(result).subscribe();
    });
  }

  get canDelete() {
    return this.dataSource.delete !== undefined;
  }

  onDelete(item: TItem) {
    this.confirmDeleteDialog.open({ title: this.typeName }).subscribe(() => {
      this.dataSource.delete!({ id: (<any>item)['id'] }).subscribe();
    });
  }

  // Flexible Fields

  enableHidden = false;

  enableFreeText = false;
}
