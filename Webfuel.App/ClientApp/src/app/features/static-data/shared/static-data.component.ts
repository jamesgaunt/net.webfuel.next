import { inject } from '@angular/core';
import { Query } from '../../../api/api.types';
import { IDataSource } from '../../../shared/common/data-source';
import { CreateStaticDataDialog } from '../dialogs/create-static-data/create-static-data.dialog';
import { UpdateStaticDataDialog } from '../dialogs/update-static-data/update-static-data.dialog';
import { ConfirmDeleteDialog } from '../../../shared/dialogs/confirm-delete/confirm-delete.dialog';

export class StaticDataComponent<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> {
  constructor(
    public dataSource: IDataSource<TItem, TQuery, TCreate, TUpdate>
  ) {
  }

  typeName = "Undefined";

  createStaticDataDialog = inject(CreateStaticDataDialog);

  updateStaticDataDialog = inject(UpdateStaticDataDialog);

  confirmDeleteDialog = inject(ConfirmDeleteDialog);

  get canAdd() {
    return this.dataSource.create !== undefined;
  }

  onAdd() {
    this.createStaticDataDialog.open({
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
    this.updateStaticDataDialog.open({
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
