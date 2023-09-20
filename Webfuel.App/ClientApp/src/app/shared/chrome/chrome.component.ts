import { Component, OnInit, OnDestroy } from '@angular/core';
import { NavigationEnd, ResolveEnd, Router } from '@angular/router';
import { GrowlService } from '../../core/growl.service';
import { IdentityService } from '../../core/identity.service';
import { IIdentityToken } from '../../api/api.types';

@Component({
  selector: 'chrome',
  templateUrl: './chrome.component.html',
  styleUrls: ['./chrome.component.scss']
})
export class ChromeComponent implements OnInit, OnDestroy {

  constructor(
    private router: Router,
    public growlService: GrowlService,
    public identityService: IdentityService,
  ) {
  }

  identity: IIdentityToken | null = null;

  get email() {
    if (this.identity)
      return this.identity.user.email;
    return "";
  }

  ngOnInit(): void {
    this.router.events.forEach((event) => {
      if (event instanceof ResolveEnd) {
        this.chromeHidden = event.state.root.firstChild?.data.chrome === false;
      }
    });
    this.identityService.token.subscribe((result) => {
      this.identity = result;
    });
  }

  ngOnDestroy() {
  }

  chromeCollapsed = false;

  chromeHidden = true;

  toggleChrome() {
    this.chromeCollapsed = !this.chromeCollapsed;
  }
}
