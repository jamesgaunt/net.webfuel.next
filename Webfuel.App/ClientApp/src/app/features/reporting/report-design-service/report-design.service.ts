import { Injectable } from "@angular/core";
import { ReportDesignApi } from "../../../api/report-design.api";
import { ReportColumn, ReportDesign, ReportField, ReportFieldType, ReportFilter, ReportFilterNumber, ReportFilterNumberCondition, ReportFilterString, ReportFilterStringCondition, ReportFilterType, ReportSchema } from "../../../api/api.types";
import { AddReportColumnDialog } from "./dialogs/add-report-column/add-report-column.dialog";
import { EditReportColumnDialog } from "./dialogs/edit-report-column/edit-report-column";
import { ConfirmDeleteDialog } from "../../../shared/dialogs/confirm-delete/confirm-delete.dialog";
import { tap } from "rxjs/operators";
import { AddReportFilterDialog } from "./dialogs/add-report-filter/add-report-filter.dialog";
import { GrowlService } from "../../../core/growl.service";
import _ from 'shared/common/underscore';

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
    return this.confirmDeleteDialog.open({ title: "Delete Column" }).pipe(
      tap(() => { design.columns = design.columns.filter(p => p !== column); })
    );
  }

  // Filters

  addFilter(schema: ReportSchema, design: ReportDesign, filters: ReportFilter[]) {
    return this.addReportFilterDialog.open({ schema: schema, design: design, filters: filters }).pipe(
      tap((id) => this._addFilter(schema, design, filters, id))
    );
  }

  private _addFilter(schema: ReportSchema, design: ReportDesign, filters: ReportFilter[], id: string) {
    var field = this._getField(schema, id);
    if (!field)
      return;

    var filter = this._getFilterForField(field);
    if (!filter)
      return;

    filters.push(filter);
  }

  editFilter(schema: ReportSchema, design: ReportDesign, filter: ReportFilter) {
    switch (filter.filterType) {
      case ReportFilterType.Group:
        break;
      case ReportFilterType.String:
        break;
      default:
        this.growlService.growlDanger("Unrecognised filter type: " + filter.filterType);
        return;
    }
  }

  // Helpers

  private _getField(schema: ReportSchema, fieldId: string) {
    var field = schema.fields.find(p => p.id == fieldId);
    if (!field)
      this.growlService.growlDanger("Field not found: " + fieldId);
    return field;
  }

  private _getFilterForField(field: ReportField): ReportFilter | undefined {
    switch (field.fieldType) {

      case ReportFieldType.String:
        return <ReportFilterString>{
          filterType: ReportFilterType.String,
          fieldId: field.id,
          condition: ReportFilterStringCondition.Contains,
          value: ""
        };

      case ReportFieldType.Number:
        return <ReportFilterNumber>{
          filterType: ReportFilterType.Number,
          fieldId: field.id,
          condition: ReportFilterNumberCondition.EqualTo,
          value: null
        }

      default:
        this.growlService.growlDanger("Unrecognised field type: " + field.fieldType);
        return undefined;
    }
  }
}

