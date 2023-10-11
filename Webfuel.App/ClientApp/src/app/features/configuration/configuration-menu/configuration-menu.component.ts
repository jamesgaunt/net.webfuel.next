import { Component, OnInit, inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, ActivatedRouteSnapshot, ResolveFn, Router, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { ConfigurationService } from '../../../core/configuration.service';
import { ClientConfiguration } from '../../../api/api.types';

@Component({
  selector: 'configuration-menu',
  templateUrl: './configuration-menu.component.html'
})
export class ConfigurationMenuComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private configurationService: ConfigurationService
  ) {
    this.configuration = configurationService.configuration
  }

  ngOnInit() {
  }

  configuration: BehaviorSubject<ClientConfiguration | null>;
}
