import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoadingService } from '../services/loading.service';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private loadingService: LoadingService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (request.method === 'POST')
    {
      return next.handle(request);
    }

    if(request.method === 'DELETE')
    {
      return next.handle(request);
    }

    if(request.method === "GET" && request.url.includes('Exists'))
    {
      return next.handle(request);
    }

    this.loadingService.busy();
    return next.handle(request).pipe(delay(1000),
    finalize(() => this.loadingService.idle()));
  }
}
