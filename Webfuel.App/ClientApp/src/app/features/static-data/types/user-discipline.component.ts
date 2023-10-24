import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateUserDiscipline, UserDiscipline, QueryUserDiscipline, UpdateUserDiscipline } from '../../../api/api.types';
import { UserDisciplineApi } from '../../../api/user-discipline.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'user-discipline-list',
  templateUrl: '../shared/static-data.component.html'
})
export class UserDisciplineComponent extends StaticDataComponent<UserDiscipline, QueryUserDiscipline, CreateUserDiscipline, UpdateUserDiscipline> {
  constructor(
    dataSource: UserDisciplineApi,
  ) {
    super(dataSource);
    this.typeName = "User Discipline";
    this.enableHidden = true;
    this.enableFreeText = true;
  }
}
