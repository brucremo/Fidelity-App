import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RequestResult } from '../models/requestResult';
import { environment } from '../../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class MailService {

  constructor(
    private http: HttpClient
  ) { }

  private apiController: string = "/api/Mail";

  public sendContactRequest(emailAddress: string) : Observable<RequestResult>{
    return this.http.get<RequestResult>(environment.apiPath + this.apiController + "/contact-request", {
      params : {
        emailAddress : emailAddress
      }
    });
  }
}
