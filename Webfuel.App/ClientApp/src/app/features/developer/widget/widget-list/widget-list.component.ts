import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ISimpleQuery, IWidget } from 'api/api.types';
import { WidgetApi } from 'api/widget.api';
import { DialogService } from 'core/dialog.service';
import { DataSource } from '../../../../shared/data-source';
import { WidgetDialogComponent } from '../widget-dialog/widget-dialog.component';

@Component({
  templateUrl: './widget-list.component.html'
})
export class WidgetListComponent {
  constructor(
    private dialogService: DialogService,
    private widgetApi: WidgetApi

  ) {
  }

  dataSource = new DataSource<IWidget, ISimpleQuery>({
    fetch: (query) => this.widgetApi.query({ query: query })
  });

  add() {
    this.dialogService.open(WidgetDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: IWidget) {
    this.dialogService.open(WidgetDialogComponent, {
      data: item,
      callback: () => this.dataSource.fetch()
    });
  }

  delete(item: IWidget) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.widgetApi.delete({ widgetId: item.id }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    })
  }
}
