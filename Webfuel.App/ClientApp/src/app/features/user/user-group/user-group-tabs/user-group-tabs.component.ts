import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IUserGroup } from 'api/api.types';

@Component({
  selector: 'user-group-tabs',
  templateUrl: './user-group-tabs.component.html'
})
export class UserGroupTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.userGroup);
  }

  item!: IUserGroup;

  reset(item: IUserGroup) {
    this.item = item;
  }
}
