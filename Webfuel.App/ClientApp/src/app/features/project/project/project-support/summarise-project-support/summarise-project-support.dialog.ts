import { Component, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProjectSupport, SupportProvided } from 'api/api.types';
import { ProjectSupportApi } from 'api/project-support.api';
import { StaticDataCache } from 'api/static-data.cache';
import { UserApi } from 'api/user.api';
import { FormService } from 'core/form.service';
import { DialogBase, DialogComponentBase } from 'shared/common/dialog-base';
import _ from 'shared/common/underscore';
export interface SummariseProjectSupportDialogData {
  items: ProjectSupport[];
}

@Injectable()
export class SummariseProjectSupportDialog extends DialogBase<ProjectSupport, SummariseProjectSupportDialogData> {
  open(data: SummariseProjectSupportDialogData) {
    return this._open(SummariseProjectSupportDialogComponent, data, {
      width: "1000px"
    });
  }
}

@Component({
  selector: 'summarise-project-support-dialog',
  templateUrl: './summarise-project-support.dialog.html'
})
export class SummariseProjectSupportDialogComponent extends DialogComponentBase<ProjectSupport, SummariseProjectSupportDialogData> {

  constructor(
    private formService: FormService,
    private projectSupportApi: ProjectSupportApi,
    public userApi: UserApi,
    public staticDataCache: StaticDataCache,
  ) {
    super();
    this.items = this.data.items;
    this.staticDataCache.supportProvided.query({ skip: 0, take: 100 }).subscribe((result) => this.categories = result.items);
  }

  cancel() {
    this._cancelDialog();
  }

  items: ProjectSupport[] = [];

  categories: SupportProvided[] = [];

  containsCategory(category: SupportProvided) {
    return _.some(this.items || [], (p) => _.some(p.supportProvidedIds, (s) => s == category.id));
  }
}
