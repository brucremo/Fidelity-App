import { BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OutletComponent } from 'src/app/shared/components/outlet/outlet.component';
import { LoadingService } from 'src/app/shared/services/loading.service';
import { NavTitleService } from 'src/app/shared/services/nav-title.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { VendorSaleService } from '../../services/vendor-sale.service';

@Component({
  selector: 'app-sales-dashboard',
  templateUrl: './sales-dashboard.component.html',
  styleUrls: ['./sales-dashboard.component.css']
})
export class SalesDashboardComponent extends OutletComponent implements OnInit {

  private title: string = "Vendas";
  public isInsightsBusy: boolean = true;

  constructor(
    public loading: LoadingService,
    public titleService: NavTitleService,
    public saleService: VendorSaleService,
    public notification: NotificationService,
    public breakpointObserver: BreakpointObserver,
    public router: Router
  ) {
    super(breakpointObserver);
    this.titleService.setNavTitle(this.title);
  }

  ngOnInit(): void {
  }

  // --- UI Event Handling ---
  public navigateTo(route: string): void {
    this.router.navigate([route]);
  }

  // --- Getters ---
  get saleItems(): any[] {
    return [
      /*{
        title: "Análise",
        link: "vendor/sales/analysis",
        icon: "analytics",
      },*/
      {
        title: "Gerenciamento",
        link: "vendor/sales/management",
        icon: "account_balance",
      },
      /*{
        title: "Configurações",
        link: "vendor/sales/settings",
        icon: "settings",
      },*/
    ];
  }

  get insightItemsFirst(): any[] {
    return [
      {
        title: "Promoções Ativas",
        value: "1",
      },
      {
        title: "Vendas Hoje",
        value: "10"
      },
    ];
  }
}
