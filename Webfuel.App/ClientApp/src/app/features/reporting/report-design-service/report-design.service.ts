import { Injectable } from "@angular/core";
import { tap } from "rxjs/operators";
import { ReportColumn, ReportDesign, ReportFilter, ReportFilterGroup, ReportFilterType, ReportSchema } from "../../../api/api.types";
import { GrowlService } from "../../../core/growl.service";
import { ConfirmDeleteDialog } from "../../../shared/dialogs/confirm-delete/confirm-delete.dialog";

import { InsertReportColumnDialog } from "./dialogs/insert-report-column/insert-report-column.dialog";
import { InsertReportFilterDialog } from "./dialogs/insert-report-filter/insert-report-filter.dialog";
import { UpdateReportColumnDialog } from "./dialogs/update-report-column/update-report-column.dialog";
import { DeleteReportColumnDialog } from "./dialogs/delete-report-column/delete-report-column.dialog";
import { DeleteReportFilterDialog } from "./dialogs/delete-report-filter/delete-report-filter.dialog";

@Injectable()
export class ReportDesignService {
  constructor(
    private growlService: GrowlService,
    private confirmDeleteDialog: ConfirmDeleteDialog,

    private insertReportColumnDialog: InsertReportColumnDialog,
    private insertReportFilterDialog: InsertReportFilterDialog,

    private updateReportColumnDialog: UpdateReportColumnDialog,

    private deleteReportColumnDialog: DeleteReportColumnDialog,
    private deleteReportFilterDialog: DeleteReportFilterDialog,
  ) {

  }

  // Columns

  insertColumn(schema: ReportSchema, design: ReportDesign) {
    return this.insertReportColumnDialog.open({ schema: schema, design: design });
  }

  updateColumn(schema: ReportSchema, design: ReportDesign, column: ReportColumn) {
    return this.updateReportColumnDialog.open({ schema: schema, design: design, column: column });
  }

  deleteColumn(schema: ReportSchema, design: ReportDesign, column: ReportColumn) {
    return this.deleteReportColumnDialog.open({ schema: schema, design: design, column: column });
  }

  // Filters

  insertFilter(schema: ReportSchema, design: ReportDesign) {
    return this.insertReportFilterDialog.open({ schema: schema, design: design })
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
    return this.deleteReportFilterDialog.open({ schema: schema, design: design, filter: filter });
  }
}

