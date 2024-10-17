import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort, User, Widget, WidgetTaskResult, WidgetTaskStatus } from "../api/api.types";
import { QueryOp, WidgetTypeEnum } from "../api/api.enums";
import { BehaviorSubject, Observable } from "rxjs";
import _ from 'shared/common/underscore';
import { WidgetApi } from "api/widget.api";
import { ConfigurationService } from "./configuration.service";

interface WidgetTask {
  id: string;
  initialised: boolean;
}

@Injectable()
export class WidgetService {
  constructor(
    private widgetApi: WidgetApi,
    private configurationService: ConfigurationService
  ) {
    this.configurationService.configuration.subscribe((result) => {
      if (result == null) {
        this.widgets.next([]);
        this._tasks = [];
      } else {
        this.loadWidgets();
      }
    })
  }

  // Widgets

  widgets = new BehaviorSubject<BehaviorSubject<Widget>[]>([]);

  loadWidgets() {
    this.widgetApi.selectActive().subscribe((result) => {

      var widgets: BehaviorSubject<Widget>[] = [];
      this._tasks = [];

      _.forEach(result, (widget) => {
        var existing = this.findWidget(widget.id);
        if (existing === undefined) {
          widgets.push(new BehaviorSubject(widget));
        } else {
          widgets.push(existing);
          existing.next(widget);
        }
      });
      this.widgets.next(widgets);
      this.processWidgets();
    });
  }

  findWidget(id: string): BehaviorSubject<Widget> | undefined {
    return _.find(this.widgets.value, (p) => p.value.id == id);
  }

  // Widget Processing

  processWidget(id: string) {
    if (this.isProcessing(id))
      return; // widget is already being processed

    if (this.findWidget(id) === undefined)
      return; // widget no longer exists

    this._tasks.push({ id: id, initialised: false });
    this.beginTaskProcessing();
  }

  processWidgets() {
    _.forEach(this.widgets.value, (widget) => {
      this.processWidget(widget.value.id);
    });
  }

  // Task Processing

  private __processingTasks = false;

  private beginTaskProcessing() {
    if (this.__processingTasks || this._tasks.length == 0)
      return;
    this.__processingTasks = true;
    this.__continueTaskProcessing();
  }

  private __continueTaskProcessing() {
    if (this._tasks.length == 0) {
      this.__processingTasks = false;
      return;
    }
    this.processTask(this._tasks[0]);
  }

  private __processingTimeout() {
    setTimeout(() => this.__continueTaskProcessing(), 100);
  }

  private processTask(task: WidgetTask) {

    if (task.initialised) {
      this.widgetApi.contineProcessing({ id: task.id }).subscribe({
        next: (result) => {
          this.processTaskResult(task.id, result);
          this.__processingTimeout();
        },
        error: () => {
          this.deleteTask(task.id);
          this.__processingTimeout();
        }
      });
    }
    else {
      this.widgetApi.beginProcessing({ id: task.id }).subscribe({
        next: (result) => {
          task.initialised = true;
          this.processTaskResult(task.id, result);
          this.__processingTimeout();
        },
        error: () => {
          this.deleteTask(task.id);
          this.__processingTimeout();
        }
      });
    }
  }

  private processTaskResult(id: string, result: WidgetTaskResult) {

    var task = this.findTask(id);
    if (task == undefined)
      return;

    if (result.status == WidgetTaskStatus.Processing) {
      return;
    }

    if (result.status == WidgetTaskStatus.Complete) {
      this.deleteTask(id);
      var widget = this.findWidget(id);
      if (widget !== undefined && result.widget != null)
        widget.next(result.widget);
      return;
    }

    if (result.status == WidgetTaskStatus.Cancelled) {
      this.deleteTask(id);
      return;
    }

    if (result.status == WidgetTaskStatus.Deferred) {
      this.deferTask(id);
      return;
    }

    // Shouldn't get here!
    this.deleteTask(id);
  }

  // Task List

  private _tasks: WidgetTask[] = [];

  private addTask(id: string) {
    var task = this.findTask(id);
    if (task !== undefined)
      return;
    this._tasks.push({ id: id, initialised: false });
  }

  private deleteTask(id: string) {
    this._tasks = _.remove(this._tasks, t => t.id == id);
  }

  private deferTask(id: string) {
    var task = this.findTask(id);
    if (task === undefined)
      return;

    this.deleteTask(id);
    this._tasks.push(task);
  }

  private findTask(id: string) {
    return _.find(this._tasks, t => t.id == id);
  }

  isProcessing(id: string) {
    return this.findTask(id) !== undefined;
  }
}

