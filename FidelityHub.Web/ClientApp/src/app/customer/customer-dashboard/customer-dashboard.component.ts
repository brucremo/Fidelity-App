import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { map } from "rxjs/operators";
import { CustomerService } from "../customer.service";
import { Breakpoints, BreakpointObserver } from "@angular/cdk/layout";
import { Promotion } from "src/app/shared/models/promotion";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import { Vendor } from "src/app/vendor/models/vendor";
import { Observable, Subscriber } from "rxjs";
import { CustomerPromotionDashboardViewModel } from '../models/dashboard.model'
import { LoadingService } from "src/app/shared/services/loading.service";
import { NavTitleService } from "src/app/shared/services/nav-title.service";

@Component({
  selector: "app-customer-dashboard",
  templateUrl: "./customer-dashboard.component.html",
  styleUrls: ["./customer-dashboard.component.css"],
  encapsulation: ViewEncapsulation.None
})
export class CustomerDashboardComponent extends OutletComponent implements OnInit {

  public promotions: CustomerPromotionDashboardViewModel[] = [];
  public vendors: Vendor[] = [];
  public rowHeight: string = "200px";
  public isBusy: boolean = false;
  public cards: any;
  private title: string = "Carteira";

  constructor(
    private breakpointObserver: BreakpointObserver,
    private apiService: CustomerService,
    public loading: LoadingService,
    public titleService: NavTitleService
  ) {
    super(breakpointObserver);
    this.titleService.setNavTitle(this.title);
  }

  ngOnInit(){
    this.getEnrolledVendors();
  }

  // --- UI Support ---
  private generateVendorCard(data: CustomerPromotionDashboardViewModel): any {
    if(this.isHandset){
      return { data: data, cols: 2, rows: 1 };
    }else{
      return { data: data, cols: 1, rows: 1 };
    }
  }

  // --- Getters ---
  get gridListCols(): number {
    return 2;
  }

  get showDashboardData(): boolean {
    return !this.isBusy && this.promotions.length > 0;
  }

  get showLoading(): boolean {
    return this.isBusy;
  }

  get showNoData(): boolean {
    return !this.isBusy && this.promotions.length == 0;;
  }

  // --- API Calls ---
  public getEnrolledVendors(): void {
    this.isBusy = true;
    var sub = this.apiService
      .getEnrolledPromotions()
      .subscribe((response) => {
        this.promotions = response;
        this.cards = this.promotions.map(x => this.generateVendorCard(x));
        this.isBusy = false;
        sub.unsubscribe();
      });
  }
}
