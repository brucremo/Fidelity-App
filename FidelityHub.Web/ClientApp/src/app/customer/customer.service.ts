import { Injectable } from '@angular/core';
import { JwtTokenService } from '../shared/services/jwt-token.service';
import { LocalStorageService } from '../shared/services/local-storage.service';
import { NotificationService } from '../shared/services/notification.service';
import { BaseApiService } from '../shared/services/base-api.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { SalesModel } from '../shared/models/sales-model';
import { Promotion } from '../shared/models/promotion';
import { AuthenticationService } from '../shared/services/authentication.service';
import { Vendor } from '../vendor/models/vendor';
import { CustomerPromotionDashboardViewModel } from './models/dashboard.model'

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(
    private auth: AuthenticationService,
    private api: BaseApiService
  ) {
  }

  private apiController: string = "/api/customer";

  public getEnrolledVendors(): Observable<Vendor[]> {
    return this.api.get<Vendor[]>(environment.apiPath + this.apiController + "/vendors", {
      params : {
        customerId: this.auth.loggedInId
      },
    });
  }

  public getEnrolledPromotions(): Observable<CustomerPromotionDashboardViewModel[]> {
    return this.api.get<CustomerPromotionDashboardViewModel[]>(environment.apiPath + this.apiController + "/promotions", {
      params : {
        customerId: this.auth.loggedInId
      },
    });
  }

  public getEnrolledPromotionSales(promotionId: number): Observable<Promotion[]> {
    return this.api.get<Promotion[]>(environment.apiPath + this.apiController + "/promotion/purchases", {
      params : {
        customerId: this.auth.loggedInId,
        promotionId: `${promotionId}`
      },
    });
  }
}
