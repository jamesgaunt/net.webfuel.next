import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../core/form.service';

@Component({
  selector: 'report-designer',
  templateUrl: './report-designer.component.html'
})
export class ReportDesignerComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
  ) {
  }

  ngOnInit() {
  }
}
