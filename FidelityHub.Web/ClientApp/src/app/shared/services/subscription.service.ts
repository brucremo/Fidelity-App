import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Subscription } from '../models/subscription';
import { BaseApiService } from './base-api.service';
import { of } from 'rxjs';
import { JwtTokenService } from './jwt-token.service';

@Injectable({
  providedIn: 'root'
})
export class SubscriptionService {

  constructor(
    private api: BaseApiService
  ) {
  }

  private apiController: string = "/api/Subscription";

  public getSubscriptions() : Observable<Subscription[]>{
    return this.api.get<Subscription[]>(environment.apiPath + this.apiController);
  }

  public emailExists(email: string) : Observable<boolean>{
    if(this.api.isStringInvalid(email)){
      return this.api.adHocObservableOf(false);
    }
    return this.api.get<boolean>(environment.apiPath + this.apiController + "/email-exists", {
      params:{
        email: email
      }
    });
  }

  public userNameExists(userName: string) : Observable<boolean>{
    if(this.api.isStringInvalid(userName)){
      return this.api.adHocObservableOf(false);
    }
    return this.api.get<boolean>(environment.apiPath + this.apiController + "/username-exists", {
      params:{
        userName: userName
      }
    });
  }
}
