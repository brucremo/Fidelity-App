import { Injectable } from "@angular/core";
import { BaseApiService } from "../../shared/services/base-api.service";
import { RequestResult } from "../../shared/models/requestResult";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { SaleRequest } from "../models/sale-request";
import { SalesModel, SalesViewModel } from "../../shared/models/sales-model";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { BaseSignalrService } from "src/app/shared/services/base-signalr.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { GraphData } from "src/app/shared/models/graphdata";
import { PromotionSale } from "src/app/shared/models/promotion";

@Injectable({
  providedIn: "root",
})
export class VendorSaleService {
  constructor(
    private api: BaseApiService,
    private http: HttpClient,
    protected auth: AuthenticationService,
    private notification: NotificationService,
    private signalR: BaseSignalrService
  ) {
    if (this.auth.isLoggedIn) {
      this.signalR.startConnection();
      if (this.signalR.isHubConnected) {
        this.signalR.hubConnection.on("saleconfirmation", (data: any) => {
          this.notification.showMessage(
            `${data.vendorName} registrou sua compra para ${data.promotionName}`
          );
        });
      }
    }
  }

  ngOnDestroy() {
    this.signalR.stopConnection();
  }

  // --- SignalR ---
  public registerSignalEvents(): void {}

  private apiAdminController: string = "/api/vendoradmin";
  private apiController: string = "/api/vendor";

  // GET
  public getSales(
    vendorUnitId: number,
    from: Date,
    to: Date
  ): Observable<SalesViewModel> {
    return this.http.get<SalesViewModel>(
      environment.apiPath + this.apiAdminController + "/sales",
      {
        params: {
          vendorUnitId: `${vendorUnitId}`,
          from: from.toDateString(),
          to: to.toDateString(),
        },
      }
    );
  }

  public getSalesChart(
    vendorUnitId: number,
    from: Date,
    to: Date
  ): Observable<any[]> {
    return this.http.get<any[]>(
      environment.apiPath + this.apiAdminController + "/sales-chart",
      {
        params: {
          vendorUnitId: `${vendorUnitId}`,
          from: from.toDateString(),
          to: to.toDateString(),
        },
      }
    );
  }

  public getSalesChartData(
    vendorUnitId: number,
    from: Date,
    to: Date
  ): Observable<any[]> {
    return new Observable((observer) => {
      this.getSalesChart(vendorUnitId, from, to).subscribe((res) => {
        var data = res.map((x) => [x.description, x.data]);
        observer.next(data);
        observer.complete();
      });
    });
  }

  // CREATE
  public registerSale(request: SaleRequest): Observable<RequestResult> {
    return this.http.post<RequestResult>(
      environment.apiPath + this.apiController + "/register-sale",
      request,
      {
        headers: this.api.getAuthorizationHeader(),
      }
    );
  }

  // EDIT
  public editSale(sale: PromotionSale): Observable<RequestResult> {
    var list: PromotionSale[] = [];
    list.push(sale);
    return this.http.put<RequestResult>(
      environment.apiPath + this.apiAdminController + "/edit-sales",
      list
    );
  }

  // DELETE
  public deleteSale(sale: PromotionSale): Observable<RequestResult> {
    return this.http.delete<RequestResult>(
      environment.apiPath + this.apiAdminController + "/delete-sale",
      {
        params: {
          saleId: sale.id.toString(),
        },
      }
    );
  }
}
