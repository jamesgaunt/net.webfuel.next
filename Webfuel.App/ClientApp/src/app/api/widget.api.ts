import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiService, ApiOptions } from '../core/api.service';
import { IWidget, ICreateWidgetCommand, IUpdateWidgetCommand, IDeleteWidgetCommand, IQueryResult, IQueryWidgetCommand } from './api.types';

@Injectable()
export class WidgetApi {
    constructor(private apiService: ApiService) { }
    
    public createWidget (command: ICreateWidgetCommand, options?: ApiOptions): Observable<IWidget> {
        return this.apiService.COMMAND("api/CreateWidget", command, options).pipe(map((res) => <IWidget>res.body));
    }
    
    public updateWidget (command: IUpdateWidgetCommand, options?: ApiOptions): Observable<IWidget> {
        return this.apiService.COMMAND("api/UpdateWidget", command, options).pipe(map((res) => <IWidget>res.body));
    }
    
    public deleteWidget (command: IDeleteWidgetCommand, options?: ApiOptions): Observable<any> {
        return this.apiService.COMMAND("api/DeleteWidget", command, options);
    }
    
    public queryWidget (command: IQueryWidgetCommand, options?: ApiOptions): Observable<IQueryResult<IWidget>> {
        return this.apiService.COMMAND("api/QueryWidget", command, options).pipe(map((res) => <IQueryResult<IWidget>>res.body));
    }
}

