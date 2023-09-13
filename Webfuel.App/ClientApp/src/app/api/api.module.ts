import { NgModule } from '@angular/core';
import { WidgetApi } from './widget.api';
import { EventLogApi } from './event-log.api';

@NgModule({
    providers: [
        WidgetApi,
        EventLogApi
    ]
})
export class ApiModule { }

