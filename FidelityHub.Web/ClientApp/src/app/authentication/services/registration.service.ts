import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestResult } from '../../shared/models/requestResult';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { BaseApiService } from '../../shared/services/base-api.service';
import { environment } from '../../../environments/environment';
import { CustomerRegistrationModel } from '../models/customer-registration.model';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(
    private auth: AuthenticationService,
    private api: BaseApiService
  ) {
  }

  private apiController: string = "/api/registration";

  public registerCustomer(customer: CustomerRegistrationModel): Observable<RequestResult> {
    return this.api.post<RequestResult>(environment.apiPath + this.apiController + "/register-customer", customer);
  }

  private accountController: string = "/api/account";

  public sendForgotPasswordEmail(email: string): Observable<RequestResult> {
    return this.api.get<RequestResult>(environment.apiPath + this.accountController + "/forgot-password", {
      params : {
        email: email
      }
    });
  }
}
