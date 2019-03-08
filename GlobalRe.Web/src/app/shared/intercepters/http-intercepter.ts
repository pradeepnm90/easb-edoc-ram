import {Injectable, Injector} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler,
        HttpInterceptor, HttpResponse} from '@angular/common/http';
import {HttpRequest} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {AuthenticationService} from '../services/authentication.service';
import {CoreService} from '../services/core.service';
import {Guid} from '../utils/guid';

@Injectable()
export class HttpExInterceptor implements HttpInterceptor {
  constructor(private _injector: Injector) {
  }

  intercept(request: HttpRequest<any>,
            next: HttpHandler): Observable<HttpEvent<any>> {
    const authService = this._injector.get(AuthenticationService);
    let clonedRequest = request;
    let isAuth = (request.url.indexOf('token') > 0) || (request.url.indexOf('whoami') > 0);
    if (request && request.url != '' && !isAuth) {
        clonedRequest = request.clone({
          setHeaders: {
              'Content-Type': 'application/json',
              'X-Request-ID': Guid.newGuid(),
              'Authorization': `Bearer ${authService.getToken()}`
          }
        });
    }

    return next.handle(clonedRequest).do((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
      }
    }, (err: any) => {
          if (err instanceof HttpErrorResponse) {
            if (err.status === 401) {
              //authService.reAuthenticate(request.url);
            }
          }
    });
  }

  private now() {
    if (!Date.now())
      return new Date().getTime();
    else
      return Date.now();
  }
}
