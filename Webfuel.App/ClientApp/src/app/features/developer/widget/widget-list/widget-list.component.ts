import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ISearchFilter, IWidget, IWidgetQueryView } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';
import { DialogService } from 'core/dialog.service';
import { DataSource } from '../../../../shared/data-source';
import { WidgetCreateDialogComponent } from '../widget-create-dialog/widget-create-dialog.component';
import { WidgetUpdateDialogComponent } from '../widget-update-dialog/widget-update-dialog.component';

@Component({
  templateUrl: './widget-list.component.html'
})
export class WidgetListComponent {
  constructor(
    private dialogService: DialogService,
    private widgetApi: WidgetApi,
  ) {
  }

  filterForm = new FormGroup({
    search: new FormControl('')
  });

  dataSource = new DataSource<IWidgetQueryView, ISearchFilter>({
    fetch: (query) => this.widgetApi.queryWidget(query),
    filterForm: this.filterForm
  });

  add() {
    this.dialogService.open(WidgetCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: IWidgetQueryView) {
    this.dialogService.open(WidgetUpdateDialogComponent, {
      data: item,
      callback: () => this.dataSource.fetch()
    });
  }

  delete(item: IWidgetQueryView) {
    this.dialogService.confirmDelete({
      title: item.name,
      confirmedCallback: () => {
        this.widgetApi.deleteWidget({ id: item.id }, { successGrowl: "Widget Deleted" }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    });
  }
}
