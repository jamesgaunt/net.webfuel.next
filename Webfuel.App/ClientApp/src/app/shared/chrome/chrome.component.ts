import { Component, OnInit, OnDestroy } from '@angular/core';
import { NavigationEnd, ResolveEnd, Router } from '@angular/router';
import { GrowlService } from '../../core/growl.service';
import { IdentityService } from '../../core/identity.service';
import { ClientConfiguration, IStaticDataModel } from '../../api/api.types';
import { ConfigurationService } from '../../core/configuration.service';
import { BehaviorSubject } from 'rxjs';
import { LoginService } from '../../core/login.service';
import { StaticDataService } from '../../core/static-data.service';

@Component({
  selector: 'chrome',
  templateUrl: './chrome.component.html',
  styleUrls: ['./chrome.component.scss']
})
export class ChromeComponent implements OnInit, OnDestroy {

  constructor(
    private router: Router,
    public growlService: GrowlService,
    public configurationService: ConfigurationService,
    public staticDataService: StaticDataService,
    public loginService: LoginService
  ) {
    this.configuration = configurationService.configuration;
    this.staticData = staticDataService.staticData;
  }

  configuration: BehaviorSubject<ClientConfiguration | null>;

  staticData: BehaviorSubject<IStaticDataModel | null>

  ngOnInit(): void {
    this.router.events.forEach((event) => {
      if (event instanceof ResolveEnd) {

        this.resolveActiveSideMenu(event);
        this.chromeHidden = event.state.root.firstChild!.data.chrome === false;
      }
    });
  }

  ngOnDestroy() {
  }

  chromeCollapsed = false;

  chromeHidden = true;

  toggleChrome() {
    this.chromeCollapsed = !this.chromeCollapsed;
  }

  logout() {
    this.loginService.logout();
  }

  activeSideMenu: any;

  resolveActiveSideMenu(event: ResolveEnd) {
    this.activeSideMenu = "";
    var node = event.state.root;
    while (node.children.length > 0) {
      node = node.children[0];
      this.activeSideMenu = node.data.activeSideMenu || this.activeSideMenu;
    }
  }
}
