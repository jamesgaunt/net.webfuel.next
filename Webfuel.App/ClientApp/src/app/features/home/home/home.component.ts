import { Component, OnInit } from '@angular/core';
import { StaticDataCache } from '../../../api/static-data.cache';
import { UserService } from '../../../core/user.service';
import { Router } from '@angular/router';
import { ManageWidgetDialog } from '../widget/management/manage-widget-dialog/manage-widget.dialog';
import { WidgetApi } from 'api/widget.api';
import { Widget, DashboardMetric } from 'api/api.types';
import { ConfigurationService } from 'core/configuration.service';
import { WidgetService } from 'core/widget.service';

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
      if (config == null) {
        this.widgets = [];
        return;
      }
      this.widgetApi.select({ userId: config.userId }).subscribe((widgets) => {
        this.widgets = widgets;
      })
    });
  }

  widgets: Widget[] | null = null;

  manageWidgets() {
    this.manageWidgetDialog.open().subscribe(() => {

    });
  }
}
