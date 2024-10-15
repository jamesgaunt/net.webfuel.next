import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User, Widget, WidgetDataResponse } from "../api/api.types";
import { QueryOp, WidgetTypeEnum } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';
import { WidgetApi } from "api/widget.api";

export interface WidgetDataSource {
  widgetId: string;
  generating: boolean; // if true there is a pending call to the server side generator
  complete: boolean; // if true the data is complete
  data: string;
}

@Injectable()
export class WidgetService {
  constructor(
    private widgetApi: WidgetApi
  ) {
    this.processDataSources();
  }

  widgetTitle(widget: Widget) {
    return this.widgetTypeName(widget.widgetTypeId);
  }

  widgetTypeName(widgetTypeId: string) {
    switch (widgetTypeId) {
      case WidgetTypeEnum.ProjectSummary: return "Project Summary";
      case WidgetTypeEnum.TeamSupport: return "Team Support";
      default: return "UNKNOWN";
    }
  }

  getDataSource(widget: Widget): Observable<WidgetDataSource> {
    if (this._dataSourceCache[widget.id] === undefined) {
      this.generateDataSource(this._dataSourceCache[widget.id] = new BehaviorSubject<WidgetDataSource>({
        widgetId: widget.id,
        generating: false,
        complete: false,
        data: ""
      }));
    }
    return this._dataSourceCache[widget.id];
  }

  processDataSources() {
    for (var key in this._dataSourceCache) {

      var dataSource = this._dataSourceCache[key];
      if (dataSource == undefined || dataSource.value.generating || dataSource.value.complete)
        continue;

      this.generateDataSource(dataSource);

    }
    setTimeout(() => this.processDataSources(), 2000);
  }

  generateDataSource(dataSource: BehaviorSubject<WidgetDataSource>) {
    dataSource.next(_.merge(dataSource.value, { generating: true }));

    this.widgetApi.generate({ widgetId: dataSource.value.widgetId }).subscribe({
      next: (result) => {
        dataSource.next({
          widgetId: dataSource.value.widgetId,
          generating: false,
          complete: result.complete,
          data: result.data
        });
      },
      error: () => {
        dataSource.next({
          widgetId: dataSource.value.widgetId,
          generating: false,
          complete: true,
          data: ""
        });
      }
    });
  }

  // Widget Data Source Cache

  private _dataSourceCache: { [key: string]: BehaviorSubject<WidgetDataSource> } = {}; // Preserved for lifetime of client application
}

