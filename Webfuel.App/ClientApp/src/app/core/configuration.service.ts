import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { ClientConfiguration } from '../api/api.types';
import { ConfigurationApi } from '../api/configuration.api';
import { EventService } from "./event.service";

@Injectable()
export class ConfigurationService {

  constructor(
    private configurationApi: ConfigurationApi,
    private eventService: EventService
  ) {
    this.eventService.identityChanged.subscribe(() => this.reloadConfiguration());
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
    this.configurationApi.getConfiguration().subscribe((result) => {
      this._configuration.next(result);
      console.log("Loaded Configuration");
      console.log(result);
    });
  }
}

