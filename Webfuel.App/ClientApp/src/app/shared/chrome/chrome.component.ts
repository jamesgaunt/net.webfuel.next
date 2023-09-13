import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'chrome',
  templateUrl: './chrome.component.html',
  styleUrls: ['./chrome.component.scss']
})
export class ChromeComponent implements OnInit, OnDestroy {


  constructor(
    private router: Router,
  ) {
  }

  ngOnInit() {
  }

  ngOnDestroy() {
  }

  chromeCollapsed = false;

  toggleChrome() {
    this.chromeCollapsed = !this.chromeCollapsed;
  }
}
