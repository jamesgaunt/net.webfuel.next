import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SupportRequest } from '../../../api/api.types';

@Component({
  selector: 'support-request-tabs',
  templateUrl: './support-request-tabs.component.html'
})
export class SupportRequestTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.supportRequest);
  }

  item!: SupportRequest;

  reset(item: SupportRequest) {
    this.item = item;
  }
}
