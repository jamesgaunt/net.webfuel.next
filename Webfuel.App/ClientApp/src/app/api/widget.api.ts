import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { IWidget, ICreateWidgetCommand, IUpdateWidgetCommand, IDeleteWidgetCommand, IQueryResult, IQueryWidgetCommand } from './api.types';

@Injectable()
export class WidgetApi {
    constructor(private apiService: ApiService) { }
    
    public createWidget (command: ICreateWidgetCommand, options?: ApiOptions): Observable<IWidget> {
        options = options || {};
        return this.apiService.COMMAND("api/CreateWidget?r=" + Math.random(), command, options).pipe(map((res) => <IWidget>res.body));
    }
    
    public updateWidget (command: IUpdateWidgetCommand, options?: ApiOptions): Observable<IWidget> {
        options = options || {};
        return this.apiService.COMMAND("api/UpdateWidget?r=" + Math.random(), command, options).pipe(map((res) => <IWidget>res.body));
    }
    
    public deleteWidget (command: IDeleteWidgetCommand, options?: ApiOptions): Observable<any> {
        options = options || {};
        return this.apiService.COMMAND("api/DeleteWidget?r=" + Math.random(), command, options);
    }
    
    public queryWidget (command: IQueryWidgetCommand, options?: ApiOptions): Observable<IQueryResult<IWidget>> {
        options = options || {};
        return this.apiService.COMMAND("api/QueryWidget?r=" + Math.random(), command, options).pipe(map((res) => <IQueryResult<IWidget>>res.body));
    }
}

