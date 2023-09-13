import { NgModule } from '@angular/core';
import { EventLogApi } from './event-log.api';

@NgModule({
    providers: [
        EventLogApi
    ]
})
export class ApiModule { }

