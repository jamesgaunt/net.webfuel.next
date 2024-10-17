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
    private manageWidgetDialog: ManageWidgetDialog,
    public userService: UserService,
    public staticDataCache: StaticDataCache,
    public widgetService: WidgetService
  ) {
  }

  ngOnInit(): void {
    this.widgetService.processWidgets();
  }

  WidgetTypeEnum = WidgetTypeEnum;

  manageWidgets() {
    this.manageWidgetDialog.open({
    }).subscribe(() => {
    });
  }
}
