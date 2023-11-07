import { Injectable, inject } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { ClientConfiguration } from '../api/api.types';
import { ConfigurationApi } from '../api/configuration.api';
import { IdentityService } from "./identity.service";

@Injectable()
export class ConfigurationService {

  constructor(
    private configurationApi: ConfigurationApi,
    private identityService: IdentityService
  ) {
    this.identityService.identityChanged.subscribe(() => this.reloadConfiguration());
    this.reloadConfiguration();
  }

  get configuration() {
    return this._configuration;
  }
  _configuration = new BehaviorSubject<ClientConfiguration | null>(null);

  clearConfiguration() {
    this._configuration.next(null);
    console.log("Cleared Configuraion");
  }

  reloadConfiguration() {

    if (!this.identityService.isAuthenticated) {
      this.clearConfiguration();
      return;
    }

    this.configurationApi.getConfiguration().subscribe((configuration) => {
      this._configuration.next(configuration);
      console.log("Loaded Configuration: ", { configuration });
    });
  }

  static hasClaim(check: (c: ClientConfiguration) => boolean): boolean  {
    var service = inject(ConfigurationService);
    if (service.configuration.value != null)
      return check(service.configuration.value);
    return false;
  }
}

