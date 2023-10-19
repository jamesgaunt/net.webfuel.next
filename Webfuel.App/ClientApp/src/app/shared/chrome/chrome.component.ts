import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { ResolveEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { ClientConfiguration } from '../../api/api.types';
import { ConfigurationService } from '../../core/configuration.service';
import { DialogService } from '../../core/dialog.service';
import { GrowlService } from '../../core/growl.service';
import { LoginService } from '../../core/login.service';

@Component({
  selector: 'chrome',
  templateUrl: './chrome.component.html'
})
export class ChromeComponent implements OnInit, OnDestroy {

  constructor(
    private router: Router,
    public growlService: GrowlService,
    public configurationService: ConfigurationService,
    public loginService: LoginService,
    public dialogService: DialogService,
    @Inject(DOCUMENT) public document: Document
  ) {
    this.configuration = configurationService.configuration;
  }
  configuration: BehaviorSubject<ClientConfiguration | null>;


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
    this.dialogService.confirm({
      title: "Logout",
      message: "Are you sure you want to logout?",
      confirmedCallback: () => {
        this.loginService.logout();
      }
    });
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

  toggleDarkMode() {
    this.darkMode = !this.darkMode;
  }

  get darkMode(): boolean {
    return this.document.body.classList.contains('dark-mode');
  }
  set darkMode(value) {
    if (value)
      this.document.body.classList.add('dark-mode');
    else
      this.document.body.classList.remove('dark-mode');
  }
}
