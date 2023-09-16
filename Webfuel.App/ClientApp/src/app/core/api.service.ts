import _ from '../shared/underscore';
import { HttpInterceptor, HttpHandler, HttpClient, HttpEvent, HttpResponse, HttpRequest, HttpHeaders } from '@angular/common/http';
import { Observable, Observer, BehaviorSubject } from 'rxjs';
import { retry } from 'rxjs/operators';
import { Injectable } from '@angular/core';
// import { GrowlService } from './growl.service';

export interface ApiFile {
  file?: File;
  path?: string;
  data?: any;
}

export interface ApiOptions {
  errorGrowl?: boolean;
  savedGrowl?: boolean;
  retryCount?: number;
  file?: ApiFile;
}

@Injectable()
export class ApiService {

  // Needs to match the key in the AuthenticationService
  private key = 'IDENTITY_TOKEN';

  constructor(
    private httpClient: HttpClient,
    // private growlService: GrowlService
  ) { }

  pending = new BehaviorSubject<boolean>(false);

  private incCounter() {
    this._counter++;
    if (this._counter == 1)
      this.pending.next(true);
  }
  private decCounter() {
    this._counter--;
    if (this._counter == 0)
      this.pending.next(false);
  }
  private _counter = 0;

  public COMMAND<T>(url: string, data: T, options: ApiOptions): Observable<HttpResponse<Object>> {
    options = options || {};

    return this.enhance(this.httpClient.post(url,
      JSON.stringify(data),
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json', 'IDENTITY_TOKEN': this.token || "NONE" }),
        observe: 'response'
      }), options);
  }

  public GET(url: string, options: ApiOptions): Observable<HttpResponse<Object>> {
    options = options || {};
    if (options.retryCount === undefined)
      options.retryCount = 2;

    return this.enhance(this.httpClient.get(url,
      {
        headers: new HttpHeaders({ 'IDENTITY_TOKEN': this.token || "NONE" }),
        observe: 'response'
      }), options);
  }

  public POST<T>(url: string, data: T, options: ApiOptions): Observable<HttpResponse<Object>> {
    options = options || {};

    if (!options.file) {
      return this.enhance(this.httpClient.post(url,
        JSON.stringify(data),
        {
          headers: new HttpHeaders({ 'Content-Type': 'application/json', 'IDENTITY_TOKEN': this.token || "NONE" }),
          observe: 'response'
        }), options);
    } else {
      const formData = new FormData();
      if(options.file.file)
        formData.append("file", options.file.file);
      if(options.file.path)
        formData.append("path", options.file.path);
      if(options.file.data)
        formData.append("data", JSON.stringify(options.file.data));
      return this.enhance(this.httpClient.post(url,
        formData,
        {
          headers: new HttpHeaders({ 'IDENTITY_TOKEN': this.token || "NONE" }),
          observe: 'response'
        }), options);
    }
  }

  public PUT<T>(url: string, data: T, options: ApiOptions): Observable<HttpResponse<Object>> {
    options = options || {};

    return this.enhance(this.httpClient.put(
      url,
      JSON.stringify(data),
      {
        headers: new HttpHeaders({ 'Content-Type': 'application/json', 'IDENTITY_TOKEN': this.token || "NONE" }),
        observe: 'response'
      }), options);
  }

  public DELETE(url: string, options: ApiOptions): Observable<HttpResponse<Object>> {
    options = options || {};

    return this.enhance(this.httpClient.delete(
      url,
      {
        headers: new HttpHeaders({ 'IDENTITY_TOKEN': this.token || "NONE" }),
        observe: 'response'
      }), options);
  }

  private enhance(o: Observable<HttpResponse<Object>>, options: ApiOptions) {
    if (options.retryCount)
      o = o.pipe(retry(options.retryCount));
    o = o.pipe(this.let(options));
    return o;
  }

  private let(options: ApiOptions) {
    return (source: Observable<HttpResponse<Object>>) => {
      return Observable.create((observer: Observer<HttpResponse<Object>>) => {
        var open = true;
        this.incCounter();
        source.subscribe(
          (next) => {
            if (open) {
              open = false;
              this.decCounter();
            }
            if (options.savedGrowl === true) {
              // this.growlService.growlSaved();
            }
            observer.next(next);
          },
          (err) => {
            if (open) {
              open = false;
              this.decCounter();
            }
            if (options.errorGrowl !== false) {
              // this.growlService.growlError(err);
            }
            observer.error(err);
          },
          () => {
            if (open) {
              open = false;
              this.decCounter();
            }
            observer.complete();
          }
        );
      });
    };
  }

  private get token() {
    var token = _.getLocalStorage(this.key);
    if (!token)
      return null;
    return token;
  }
}
