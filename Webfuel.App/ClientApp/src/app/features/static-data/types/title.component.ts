import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateTitle, Title, QueryTitle, UpdateTitle } from '../../../api/api.types';
import { TitleApi } from '../../../api/title.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'title-list',
  templateUrl: '../shared/static-data.component.html'
})
export class TitleComponent extends StaticDataComponent<Title, QueryTitle, CreateTitle, UpdateTitle> {
  constructor(
    dataSource: TitleApi,
  ) {
    super(dataSource);
    this.typeName = "Title";
  }
}
