import { RequestResult } from "../models/requestResult";
import { Observable, Subscriber } from "rxjs";
import { Injectable } from "@angular/core";
import { JwtTokenService } from "./jwt-token.service";
import { HttpClient, HttpHeaders, HttpHandler } from "@angular/common/http";
import { LocalStorageService } from "./local-storage.service";

@Injectable({
  providedIn: 'root'
})
export class BaseApiService extends HttpClient {

  constructor(
    private storage: LocalStorageService,
    private jwt: JwtTokenService,
    handler: HttpHandler
  ) {
    super(handler);
  }

  public getAuthorizationHeader() : any {
    return { Authorization: "Bearer " + this.storage.get("AuthToken") };
  }

  public handleError(error) {
    if (error.error instanceof ErrorEvent) {
      // client-side error
      return new RequestResult(error.error.headers.status, error.error.message, null);
    } else {
      // server-side error
      return new RequestResult(error.headers.status, error.message, null);
    }
  }

  public adHocObservableOf(data: any) {
    return Observable.create((observer: Subscriber<any>) => {
      observer.next(data);
      observer.complete();
    });
  }

  public isStringInvalid(str: string): boolean {
    return str == null || str == "" || str == " " || str == undefined;
  }
}
