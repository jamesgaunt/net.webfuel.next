import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Researcher } from '../../../../api/api.types';

@Component({
  selector: 'researcher-tabs',
  templateUrl: './researcher-tabs.component.html'
})
export class ResearcherTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.researcher);
  }

  item!: Researcher;

  reset(item: Researcher) {
    this.item = item;
  }
}
