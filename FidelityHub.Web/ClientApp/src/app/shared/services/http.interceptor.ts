import {
  HttpEvent,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpInterceptor,
} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError, map, retry, switchMap } from "rxjs/operators";
import { AuthenticationService } from "./authentication.service";
import { JwtTokenService } from "./jwt-token.service";
import { LocalStorageService } from "./local-storage.service";

@Injectable()
export class HttpIntercept implements HttpInterceptor {
  constructor(
    private storage: LocalStorageService,
    private auth: AuthenticationService,
    private jwt: JwtTokenService
  ) {}

  private addAuthHeader(request: HttpRequest<any>): HttpRequest<any> {
    return (request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.storage.get("AuthToken")}`,
      },
    }));
  }

  private handleResponseError(error, request?, next?): any {
    // Business error
    if (error.status === 400) {
      // Show message
    }

    // Invalid token error
    else if (error.status === 401) {
      return this.auth.refreshToken().pipe(
        switchMap(() => {
          request = this.addAuthHeader(request);
          return next.handle(request);
        }),
        catchError((e) => {
          if (e.status !== 401) {
            return this.handleResponseError(e);
          } else {
            this.auth.logout();
          }
        })
      );
    }

    // Access denied error
    else if (error.status === 403) {
      // Show message
      // Logout
      this.auth.logout();
    }

    // Server error
    else if (error.status === 500) {
      this.auth.logout();
    }

    // Maintenance error
    else if (error.status === 503) {
      // Show message
      this.auth.logout();
    }

    return throwError(error);
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<any> {
    request = this.addAuthHeader(request);

    return next.handle(request).pipe(
      catchError((error) => {
        return this.handleResponseError(error, request, next);
      })
    );
  }
}
