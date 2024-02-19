import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';

@Component({
  selector: 'project-tabs',
  templateUrl: './project-tabs.component.html'
})
export class ProjectTabsComponent implements OnInit  {

  constructor(
    private route: ActivatedRoute,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.project);
  }

  item!: Project;

  reset(item: Project) {
    this.item = item;
  }
}
