import { Injectable } from "@angular/core";
import { ReportColumn, ReportDesign, ReportFilter, ReportFilterGroup, ReportFilterType, ReportSchema } from "../../../api/api.types";

import { InsertReportColumnDialog } from "./dialogs/insert-report-column/insert-report-column.dialog";
import { InsertReportFilterDialog } from "./dialogs/insert-report-filter/insert-report-filter.dialog";
import { UpdateReportColumnDialog } from "./dialogs/update-report-column/update-report-column.dialog";
import { DeleteReportColumnDialog } from "./dialogs/delete-report-column/delete-report-column.dialog";
import { DeleteReportFilterDialog } from "./dialogs/delete-report-filter/delete-report-filter.dialog";
import { UpdateReportFilterDialog } from "./dialogs/update-report-filter/update-report-filter.dialog";

@Injectable()
export class ReportDesignService {
  constructor(
    private insertReportColumnDialog: InsertReportColumnDialog,
    private insertReportFilterDialog: InsertReportFilterDialog,

    private updateReportColumnDialog: UpdateReportColumnDialog,
    private updateReportFilterDialog: UpdateReportFilterDialog,

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

  updateFilter(schema: ReportSchema, design: ReportDesign, filter: ReportFilter) {
    return this.updateReportFilterDialog.open({ schema: schema, design: design, filter: filter });
  }

  deleteFilter(schema: ReportSchema, design: ReportDesign, filter: ReportFilter) {
    return this.deleteReportFilterDialog.open({ schema: schema, design: design, filter: filter });
  }
}

