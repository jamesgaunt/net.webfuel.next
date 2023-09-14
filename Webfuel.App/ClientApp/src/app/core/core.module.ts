import { NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { ApiService } from './api.service';
import { DialogService } from './dialog.service';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
  ],
  declarations: [
  ],
  providers: [
    ApiService,
    DialogService
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule is already loaded. Import it in the AppModule only');
    }
  }
}

