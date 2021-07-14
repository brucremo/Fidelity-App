import { Component, Input, OnInit } from "@angular/core";
import { ChartType } from "angular-google-charts";
import { SalesModel, SalesViewModel } from "src/app/shared/models/sales-model";
import { LoadingService } from "src/app/shared/services/loading.service";
import { VendorSaleService } from "../../services/vendor-sale.service";

@Component({
  selector: "app-sales-analysis",
  templateUrl: "./sales-analysis.component.html",
  styleUrls: ["./sales-analysis.component.css"],
})
export class SalesAnalysisComponent implements OnInit {
  chartType: ChartType = ChartType.Bar;
  data: any[];
  isBusy: boolean;

  constructor(
    private saleService: VendorSaleService,
    private loading: LoadingService
  ) {
    this.isBusy = true;
    this.saleService
      .getSalesChartData(1, new Date(2020, 3, 1), new Date())
      .subscribe((res) => {
        console.log(res);
        this.data = res;
        this.isBusy = false;
      });
  }

  ngOnInit(): void {}
}
