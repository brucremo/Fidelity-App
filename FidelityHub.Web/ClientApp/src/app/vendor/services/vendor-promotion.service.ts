import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Promotion } from '../../shared/models/promotion';
import { AuthenticationService } from '../../shared/services/authentication.service';
import { BaseApiService } from '../../shared/services/base-api.service';
import { NotificationService } from '../../shared/services/notification.service';
import { VendorUnit } from '../models/vendor';

@Injectable({
  providedIn: 'root'
})
export class VendorPromotionService {

  constructor(
    private api: BaseApiService,
    private http: HttpClient,
    protected auth: AuthenticationService,
    private notification: NotificationService,
  ){
  }

  private apiAdminController: string = "/api/vendoradmin";
  private apiVendorController: string = "/api/vendor";

  public getVendorPromotions() : Observable<Promotion[]>{
    return this.http.get<Promotion[]>(environment.apiPath + this.apiAdminController + "/promotions-vendor");
  }
}
