import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User, Widget } from "../api/api.types";
import { QueryOp, WidgetTypeEnum } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';

@Injectable()
export class WidgetService {
  constructor(
  ) {
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
}

