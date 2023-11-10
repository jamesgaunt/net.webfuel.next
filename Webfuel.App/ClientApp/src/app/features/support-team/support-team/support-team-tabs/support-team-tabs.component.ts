import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportTeam } from '../../../../api/api.types';

@Component({
  selector: 'support-team-tabs',
  templateUrl: './support-team-tabs.component.html'
})
export class SupportTeamTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.supportTeam);
  }

  item!: SupportTeam;

  reset(item: SupportTeam) {
    this.item = item;
  }
}
