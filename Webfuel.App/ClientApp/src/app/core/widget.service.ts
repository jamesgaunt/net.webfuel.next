import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User, Widget } from "../api/api.types";
import { QueryOp, WidgetTypeEnum } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';
import { WidgetApi } from "api/widget.api";

@Injectable()
export class WidgetService {
  constructor(
    private widgetApi: WidgetApi
  ) {
    this.loadWidgets();
    this.refreshWidgets();
  }

  widgets = new BehaviorSubject<BehaviorSubject<Widget>[]>([]);

  private loadWidgets() {
    this.widgetApi.selectActive().subscribe((result) => {
      var subjects: BehaviorSubject<Widget>[] = [];
      _.forEach(result, (widget) => {
        subjects.push(new BehaviorSubject(widget));
      })
      this.widgets.next(subjects);
    });
  }

  refreshWidgets() {
    _.forEach(this.widgets.value, (widgetSubject) => {
      if (widgetSubject.value.dataCurrent)
        return; // widgets data is current so nothing to do

      // TODO: Check if there is an outstanding refresh server call

      this.refreshWidget(widgetSubject);
    });
    setTimeout(() => this.refreshWidgets(), 1000);
  }

  refreshWidget(widgetSubject: BehaviorSubject<Widget>) {
    this.widgetApi.refresh({ id: widgetSubject.value.id }).subscribe((result) => {
      widgetSubject.next(result);
    });
  }
}

