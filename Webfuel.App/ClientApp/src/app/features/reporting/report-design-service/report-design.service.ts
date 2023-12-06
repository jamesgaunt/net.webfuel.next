import { Injectable } from "@angular/core";
import { ReportDesignApi } from "../../../api/report-design.api";
import { ReportColumn, ReportDesign, ReportField, ReportFieldType, ReportFilter, ReportFilterGroup, ReportFilterNumber, ReportFilterNumberCondition, ReportFilterString, ReportFilterStringCondition, ReportFilterType, ReportSchema } from "../../../api/api.types";
import { AddReportColumnDialog } from "./dialogs/add-report-column/add-report-column.dialog";
import { EditReportColumnDialog } from "./dialogs/edit-report-column/edit-report-column";
import { ConfirmDeleteDialog } from "../../../shared/dialogs/confirm-delete/confirm-delete.dialog";
import { tap } from "rxjs/operators";
import { AddReportFilterDialog } from "./dialogs/add-report-filter/add-report-filter.dialog";
import { GrowlService } from "../../../core/growl.service";
import _ from 'shared/common/underscore';
import { Observable } from "rxjs";

@Injectable()
export class ReportDesignService {
  constructor(
    private reportDesignApi: ReportDesignApi,
    private growlService: GrowlService,

    private addReportColumnDialog: AddReportColumnDialog,
    private editReportColumnDialog: EditReportColumnDialog,

    private addReportFilterDialog: AddReportFilterDialog,

    private confirmDeleteDialog: ConfirmDeleteDialog,

  ) {

  }

  // Columns

  addColumn(schema: ReportSchema, design: ReportDesign) {
    return this.addReportColumnDialog.open({ schema: schema, design: design });
  }

  editColumn(schema: ReportSchema, design: ReportDesign, column: ReportColumn) {
    return this.editReportColumnDialog.open({ schema: schema, design: design, column: column });
  }

  deleteColumn(schema: ReportSchema, design: ReportDesign, column: ReportColumn) {
    return this.confirmDeleteDialog.open({ title: "Column" }).pipe(
      tap(() => { design.columns = design.columns.filter(p => p !== column); })
    );
  }

  // Filters

  addFilter(schema: ReportSchema, design: ReportDesign, filters: ReportFilter[]) {
    return this.addReportFilterDialog.open({ schema: schema, design: design, filters: filters })
  }

  editFilter(schema: ReportSchema, design: ReportDesign, filter: ReportFilter) {
    switch (filter.filterType) {
      case ReportFilterType.Group:
        break;
      case ReportFilterType.String:
        break;
      case ReportFilterType.Number:
        break;
      default:
        this.growlService.growlDanger("Unrecognised filter type: " + filter.filterType);
        return;
    }
  }

  deleteFilter(schema: ReportSchema, design: ReportDesign, filter: ReportFilter) {
    return this.confirmDeleteDialog.open({ title: "Filter" }).pipe(
      tap(() => {
        var filterArray = this._findFilterArray(filter, design.filters);
        if (!filterArray)
          return;
        filterArray.splice(filterArray.indexOf(filter), 1);
      })
    );
  }

  // Helpers

  private _findFilterArray(filter: ReportFilter, filters: ReportFilter[]): ReportFilter[] | undefined {
    for (var i = 0; i < filters.length; i++) {
      if (filters[i] === filter)
        return filters;

      if (filters[i].filterType === ReportFilterType.Group) {
        var result = this._findFilterArray(filter, (<ReportFilterGroup>filters[i]).filters);
        if (result)
          return result;
      }
    }
    return undefined;
  }
}

