import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { GenderApi } from 'api/gender.api';
import { DialogService } from 'core/dialog.service';
import { CreateGender, Gender, UpdateGender } from '../../../api/api.types';
import _ from '../../../shared/common/underscore';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateDialogComponent, StaticDataUpdateOptions } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';

@Component({
  selector: 'gender',
  templateUrl: '../shared/standard.component.html'
})
export class GenderComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private genderApi: GenderApi,
  ) {
  }

  typeName = "Gender";

  staticDataSource = this.genderApi.genderDataSource;

  add() {
    this.dialogService.open<CreateGender, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.genderApi.createGender(command).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    });
  }

  edit(item: Gender) {
    this.dialogService.open<UpdateGender, StaticDataUpdateOptions>(StaticDataUpdateDialogComponent, {
      data: {
        data: item,
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.genderApi.updateGender(command).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    });
  }

  delete(item: Gender) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.genderApi.deleteGender({ id: item.id }).subscribe((result) => {
          this.staticDataSource.changed?.emit();
        });
      }
    })
  }

  sort(items: Gender[]) {
    this.genderApi.sortGender({ ids: _.map(items, p => p.id) }).subscribe((result) => {
      this.staticDataSource.changed?.emit();
    })
  }
}
