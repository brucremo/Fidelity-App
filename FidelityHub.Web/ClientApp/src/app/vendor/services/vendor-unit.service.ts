import { Injectable } from "@angular/core";
import { BaseApiService } from "../../shared/services/base-api.service";
import { RequestResult } from "../../shared/models/requestResult";
import { environment } from '../../../environments/environment'
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { SaleRequest } from "../models/sale-request";
import { SalesViewModel } from "../../shared/models/sales-model";
import { AuthenticationService } from "../../shared/services/authentication.service";
import { NotificationService } from "../../shared/services/notification.service";
import { VendorUnit } from "../models/vendor";

@Injectable({
  providedIn: 'root'
})
export class VendorUnitService {

  constructor(
    private api: BaseApiService,
    private http: HttpClient,
    protected auth: AuthenticationService,
    private notification: NotificationService,
  ){
  }

  private apiAdminController: string = "/api/vendoradmin";
  private apiVendorController: string = "/api/vendor";

  public getVendorUnits() : Observable<VendorUnit[]>{
    return this.http.get<VendorUnit[]>(environment.apiPath + this.apiAdminController + "/units");
  }
}
