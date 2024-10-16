import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User, Widget } from "../api/api.types";
import { QueryOp, WidgetTypeEnum } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';
import { WidgetApi } from "api/widget.api";
import { ConfigurationService } from "./configuration.service";

interface WidgetRefreshTask {
  activeRequest: boolean;
}

@Injectable()
export class WidgetService {
  constructor(
    private widgetApi: WidgetApi,
    private configurationService: ConfigurationService
  ) {
    this.configurationService.configuration.subscribe((result) => {
      this.loadWidgets();
    })
    this.processRefreshTasks();
  }

  widgets = new BehaviorSubject<BehaviorSubject<Widget>[]>([]);

  private _refreshTasks: { [key: string]: WidgetRefreshTask | undefined } = {};

  loadWidgets() {
    this.widgetApi.selectActive().subscribe((result) => {
      var subjects: BehaviorSubject<Widget>[] = [];

      _.forEach(result, (widget) => {
        var existingSubject = this.findSubjectById(widget.id);
        if (existingSubject === undefined) {
          subjects.push(new BehaviorSubject(widget));
        } else {
          subjects.push(existingSubject);
          existingSubject.next(widget);
        }
      });
      this.widgets.next(subjects);
      this.refreshWidgets();
    });
  }

  findSubjectById(id: string): BehaviorSubject<Widget> | undefined {
    return _.find(this.widgets.value, (p) => p.value.id == id);
  }

  refreshWidget(id: string) {
    if (this._refreshTasks[id] == undefined)
      this._refreshTasks[id] = { activeRequest: false };
  }

  refreshWidgets() {
    _.forEach(this.widgets.value, (widgetSubject) => {
      this.refreshWidget(widgetSubject.value.id);
    })
  }

  private processRefreshTasks() {
    for (var id in this._refreshTasks) {
      this.processRefreshTask(id);
    }
    setTimeout(() => this.processRefreshTasks(), 1000);
  }

  private processRefreshTask(id: string) {
    var task = this._refreshTasks[id];
    if (task === undefined || task.activeRequest)
      return;

    task.activeRequest = true;
    this.widgetApi.refresh({ id: id }).subscribe({
      next: (result) => {
        if (result.complete) {
          var subject = this.findSubjectById(id);
          if (subject !== undefined)
            subject.next(result.widget);
          this._refreshTasks[id] = undefined;
        } else {
          task!.activeRequest = false;
        }
      },
      error: () => {
        this._refreshTasks[id] = undefined;
      }
    });
  }
}

