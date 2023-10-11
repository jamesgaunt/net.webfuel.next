import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { GenderApi } from 'api/gender.api';
import { DialogService } from 'core/dialog.service';
import { GridDataSource } from '../../../shared/data-source/grid-data-source';
import { Router } from '@angular/router';
import _ from '../../../shared/underscore';
import { BehaviorSubject } from 'rxjs';
import { StaticDataCreateDialogComponent, StaticDataCreateOptions } from '../dialogs/static-data-create-dialog/static-data-create-dialog.component';
import { StaticDataUpdateOptions, StaticDataUpdateDialogComponent } from '../dialogs/static-data-update-dialog/static-data-update-dialog.component';
import { CreateGender, UpdateGender, Gender } from '../../../api/api.types';

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

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true }),
  });

  dataSource = new GridDataSource<Gender>({
    fetch: (query) => this.genderApi.queryGender(_.merge(query, this.filterForm.getRawValue())),
    reorder: (items) => this.genderApi.sortGender({ ids: _.map(items, p => p.id) }),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open<CreateGender, StaticDataCreateOptions>(StaticDataCreateDialogComponent, {
      data: {
        typeName: this.typeName
      },
      successCallback: (command) => {
        this.genderApi.createGender(command).subscribe((result) => {
          this.dataSource.fetch();
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
          this.dataSource.fetch();
        });
      }
    });
  }

  delete(item: Gender) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.genderApi.deleteGender({ id: item.id }).subscribe((result) => {
          this.dataSource.fetch();
        });
      }
    })
  }
}
