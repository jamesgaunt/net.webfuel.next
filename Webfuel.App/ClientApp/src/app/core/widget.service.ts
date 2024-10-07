import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User } from "../api/api.types";
import { QueryOp, WidgetTypeEnum } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';

@Injectable()
export class WidgetService {
  constructor(
  ) {
  }

  widgetName(id: string) {
    switch (id) {
      case WidgetTypeEnum.ProjectSummary: return "Project Summary";
      case WidgetTypeEnum.TeamSupportSummary: return "Team Support Summary";
      default: return "UNKNOWN";
    }
  }
}

