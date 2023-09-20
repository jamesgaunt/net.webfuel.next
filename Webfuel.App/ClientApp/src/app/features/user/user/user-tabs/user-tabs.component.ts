import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IUser } from 'api/api.types';

@Component({
  selector: 'user-tabs',
  templateUrl: './user-tabs.component.html'
})
export class UserTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.user);
  }

  item!: IUser;

  reset(item: IUser) {
    this.item = item;
  }
}
