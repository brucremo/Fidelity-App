import { BreakpointObserver } from "@angular/cdk/layout";
import { Component, Input, OnChanges, OnInit, ViewChild } from "@angular/core";
import { MatSidenav } from "@angular/material/sidenav";
import { Router } from "@angular/router";
import {
  IFilterOption,
  INavigatedComponent,
  ISortOption,
} from "src/app/shared/components/command-bar/command-bar.component";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import { PromotionSale } from "src/app/shared/models/promotion";
import { SalesViewModel } from "src/app/shared/models/sales-model";
import { LoadingService } from "src/app/shared/services/loading.service";
import { NavTitleService } from "src/app/shared/services/nav-title.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { VendorUnitService } from "../../services/vendor-unit.service";
import { VendorSaleService } from "../../services/vendor-sale.service";
import { SaleListComponent } from "../../components/sale-list/sale-list.component";

export interface FilterSelection {
  name: string;
  selected: boolean;
  subfilters?: FilterSelection[];
}

@Component({
  selector: "app-sales-management",
  templateUrl: "./sales-management.component.html",
  styleUrls: ["./sales-management.component.css"],
})
export class SalesManagementComponent
  extends OutletComponent
  implements OnInit {
  private title: string = "Gerenciamento";
  public isBusy: boolean = false;

  public viewModel: SalesViewModel = null;
  public units: any[];

  @ViewChild("filterSidenav") filterSidenav: MatSidenav;
  @ViewChild("saleList") saleList: SaleListComponent;

  constructor(
    public loading: LoadingService,
    public titleService: NavTitleService,
    public saleService: VendorSaleService,
    public unitService: VendorUnitService,
    public notification: NotificationService,
    public breakpointObserver: BreakpointObserver,
    public router: Router
  ) {
    super(breakpointObserver);
    this.titleService.setNavTitle(this.title);
    this.getUnits();
  }

  ngOnInit(): void {}

  // --- UI Event Handling ---
  public return(): void {
    this.router.navigate(["/vendor/sales/dashboard"]);
  }

  public toggleSidenav(sidenav: MatSidenav): void {
    sidenav.toggle();
  }

  public applyFilters(): void {
    this.filterSidenav.close();
    this.getSales();
  }

  public sortByDate(): void {
    this.isBusy = true;
    console.log(this.isBusy)
    var fn = (a, b) => {
      if (a.timestamp < b.timestamp) {
        return 1;
      }
      if (a.timestamp > b.timestamp) {
        return -1;
      }
      return 0;
    };
    setTimeout(() => {
      this.saleList.sort(fn);
    }, 1000);
    this.isBusy = false;
  }

  public sortByStatus(): void {}

  // --- Filters ---
  // Status
  public showApproved: boolean = false;
  public showPending: boolean = false;
  public showRemoved: boolean = false;

  // Stores
  public showAllStores: boolean = true;
  public selectedStoresId: number[] = [];

  // Period
  public dateFrom: Date = new Date(2020, 1, 1);
  public dateTo: Date = new Date();

  // --- Getters ---
  get previousComponent(): INavigatedComponent {
    return {
      title: "Dashboard",
      route: "/vendor/dashboard",
    };
  }

  get filterOptions(): IFilterOption[] {
    return [
      { title: "Todas", icon: "select_all" },
      {
        title: "Status",
        icon: "fact_check",
        subOptions: [
          { title: "Aprovadas", icon: "check_circle" },
          { title: "Pendentes", icon: "pending_actions" },
          { title: "Removidas", icon: "delete" },
        ],
      },
      {
        title: "Data",
        icon: "date_range",
        subOptions: [
          { title: "De", icon: "today" },
          { title: "Até", icon: "insert_invitation" },
        ],
      },
      {
        title: "Unidade",
        icon: "store",
      },
    ];
  }

  get sortOptions(): ISortOption[] {
    return [
      { title: "Data", icon: "date_range" },
      { title: "Status", icon: "fact_check" },
    ];
  }

  // --- API Calls ---
  // Sales
  public getSales(): void {
    this.isBusy = true;
    this.saleService
      .getSales(this.units[0].id, this.dateFrom, this.dateTo)
      .subscribe((response) => {
        this.viewModel = response;
        this.filterSales();
        this.isBusy = false;
      });
  }

  public removeSale(sale: PromotionSale): void {
    sale.removed = !sale.removed;
    this.saleService.editSale(sale).subscribe(
      (response) => {
        this.notification.showMessage(
          `Venda ${!sale.removed ? "restaurada" : "removida"} com sucesso!`
        );
      },
      (err) => {
        this.notification.showMessage(
          `Erro ao ${
            !sale.removed ? "restaurar" : "remover"
          } venda. Por favor tente novamente mais tarde.`
        );
        sale.removed = !sale.removed;
      }
    );
  }

  public verifySale(sale: PromotionSale): void {
    sale.approved = !sale.approved;
    this.saleService.editSale(sale).subscribe(
      (response) => {
        this.notification.showMessage(
          `Venda ${!sale.approved ? "revogada" : "aprovada"} com sucesso!`
        );
      },
      (err) => {
        this.notification.showMessage(
          `Erro ao ${
            !sale.approved ? "revogar" : "aprovar"
          } venda. Por favor tente novamente mais tarde.`
        );
        sale.approved = !sale.approved;
      }
    );
  }

  // Units
  public getUnits(): void {
    this.isBusy = true;
    this.unitService.getVendorUnits().subscribe((response) => {
      this.units = response;
      this.isBusy = false;
    });
  }

  // --- Helpers ---
  private filterSales(): void {
    this.isBusy = true;
    var sales: PromotionSale[] = [];

    this.viewModel.sales.forEach((y) => {
      if (!this.showApproved && !this.showPending && !this.showRemoved) {
        sales.push(y);
      }

      if (this.showApproved && y.approved && !y.removed) {
        sales.push(y);
      }

      if (this.showPending && !y.approved && !y.removed) {
        sales.push(y);
      }

      if (this.showRemoved && y.removed) {
        sales.push(y);
      }
    });
    this.viewModel.sales = sales;
    this.isBusy = false;
  }
}
