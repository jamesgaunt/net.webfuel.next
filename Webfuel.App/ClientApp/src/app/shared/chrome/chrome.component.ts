import { DOCUMENT } from '@angular/common';
import { Component, ElementRef, Inject, OnDestroy, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ResolveEnd, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { ClientConfiguration } from '../../api/api.types';
import { ConfigurationService } from '../../core/configuration.service';
import { DialogService } from '../../core/dialog.service';
import { GrowlService } from '../../core/growl.service';
import { LoginService } from '../../core/login.service';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import _ from 'shared/common/underscore';
import { ChangePasswordDialogComponent } from '../dialogs/change-password-dialog/change-password-dialog.component';

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
    public overlay: Overlay,
    public viewContainerRef: ViewContainerRef,
    @Inject(DOCUMENT) public document: Document

  ) {
    this.configuration = configurationService.configuration;

    this.darkMode = _.getLocalStorage("chrome.darkMode") || false;
    this.collapsed = _.getLocalStorage("chrome.collapsed") || false;
  }
  configuration: BehaviorSubject<ClientConfiguration | null>;


  ngOnInit(): void {
    this.router.events.forEach((event) => {
      if (event instanceof ResolveEnd) {

        this.resolveActiveSideMenu(event);
        this.hidden = event.state.root.firstChild!.data.chrome === false;
      }
    });
  }

  ngOnDestroy() {
  }

  logout() {
    this.closeUserMenu();
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

  // Hidden

  hidden = true;

  // Collapsed

  collapsed = false;

  toggleCollapsed() {
    this.collapsed = !this.collapsed;
    _.setLocalStorage("chrome.collapsed", this.collapsed);
  }

  // Dark Mode

  toggleDarkMode() {
    this.darkMode = !this.darkMode;
    _.setLocalStorage("chrome.darkMode", this.darkMode);
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

  // User Menu

  @ViewChild('userMenuTemplate', { static: false })
  private userMenuTemplate!: TemplateRef<any>;

  @ViewChild('userMenuAnchor', { static: false })
  private userMenuAnchor!: ElementRef<any>;

  userMenuRef: OverlayRef | null = null;

  openUserMenu() {
    this.userMenuRef = this.overlay.create({
      positionStrategy: this.overlay.position().flexibleConnectedTo(this.userMenuAnchor).withPositions([
        { originX: 'end', originY: 'bottom', overlayX: 'end', overlayY: 'top' },
      ]).withFlexibleDimensions(true).withPush(false),
      hasBackdrop: true,
    });
    this.userMenuRef.backdropClick().subscribe(() => this.closeUserMenu());
    const portal = new TemplatePortal(this.userMenuTemplate, this.viewContainerRef);
    this.userMenuRef.attach(portal);
  }

  closeUserMenu() {
    if (!this.userMenuRef)
      return;
    this.userMenuRef!.detach();
    this.userMenuRef = null;
  }

  changePassword() {
    this.closeUserMenu();
    this.dialogService.open(ChangePasswordDialogComponent);
  }
}
