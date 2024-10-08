import { Component, OnInit } from '@angular/core';
import { StaticDataCache } from '../../../api/static-data.cache';
import { UserService } from '../../../core/user.service';
import { Router } from '@angular/router';
import { ManageWidgetDialog } from '../widget/management/manage-widget-dialog/manage-widget.dialog';
import { WidgetApi } from 'api/widget.api';
import { Widget, DashboardMetric } from 'api/api.types';
import { ConfigurationService } from 'core/configuration.service';
import { WidgetService } from 'core/widget.service';
import { WidgetTypeEnum } from 'api/api.enums';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  constructor(
    private router: Router,
    private widgetApi: WidgetApi,
    private manageWidgetDialog: ManageWidgetDialog,
    public userService: UserService,
    public staticDataCache: StaticDataCache,
    public configurationService: ConfigurationService,
    public widgetService: WidgetService
  ) {
  }

  ngOnInit(): void {
    this.configurationService.configuration.subscribe(config => {
      this.loadWidgets();
    });
  }

  widgets: Widget[] | null = null;

  WidgetTypeEnum = WidgetTypeEnum;

  loadWidgets() {
    if (this.configurationService.configuration.value == null) {
      this.widgets = [];
      return;
    } else {
      this.widgetApi.select({ userId: this.configurationService.configuration.value.userId }).subscribe((widgets) => {
        this.widgets = widgets;
      })
    }
  }

  manageWidgets() {
    if (this.widgets == null)
      return;
    this.manageWidgetDialog.open({
      userId: this.configurationService.configuration.value!.userId
    }).subscribe(() => {
      this.loadWidgets();
    });
  }
}
