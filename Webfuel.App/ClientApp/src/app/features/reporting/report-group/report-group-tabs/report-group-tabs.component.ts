import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportGroup } from '../../../../api/api.types';

@Component({
  selector: 'report-group-tabs',
  templateUrl: './report-group-tabs.component.html'
})
export class ReportGroupTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.reportGroup);
  }

  item!: ReportGroup;

  reset(item: ReportGroup) {
    this.item = item;
  }
}
