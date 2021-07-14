import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestResult } from '../../shared/models/requestResult';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { BaseApiService } from '../../shared/services/base-api.service';
import { environment } from "../../../environments/environment";
import { ResetPasswordModel } from "../models/reset-password.model"

@Injectable({
  providedIn: 'root'
})
export class AccountManagementService {

  constructor(
    private auth: AuthenticationService,
    private api: BaseApiService
  ) {
  }

  private apiController: string = "/api/account";

  public sendForgotPasswordEmail(email: string): Observable<RequestResult> {
    return this.api.get<RequestResult>(environment.apiPath + this.apiController + "/forgot-password", {
      params : {
        email: email
      }
    });
  }

  public isResetPasswordTokenValid(token: string): Observable<boolean> {
    return this.api.get<boolean>(environment.apiPath + this.apiController + "/validate-reset-token", {
      params : {
        token: token
      }
    });
  }

  public resetForgottenPassword(model: ResetPasswordModel): Observable<any> {
    return this.api.post<any>(environment.apiPath + this.apiController + "/reset-forgotten-password", model);
  }
}