import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SalesConfigComponent } from "./sales-config/sales-config.component";
import { SalesAnalysisComponent } from "./sales-analysis/sales-analysis.component";
import { SalesManagementComponent } from "./sales-management/sales-management.component";
import { Routes, RouterModule } from "@angular/router";
import { LayoutModule } from "@angular/cdk/layout";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatIconModule } from "@angular/material/icon";
import { MatListModule } from "@angular/material/list";
import { MatMenuModule } from "@angular/material/menu";
import { MatTableModule } from "@angular/material/table";
import { GoogleChartsModule } from "angular-google-charts";
import { HttpClientModule } from "@angular/common/http";
import { BrowserModule } from "@angular/platform-browser";
import { SalesDashboardComponent } from "./sales-dashboard/sales-dashboard.component";
import { SalesFilterComponent } from "./sales-filter/sales-filter.component";
import { VendorModule } from "../vendor.module";
import { MatInputModule } from "@angular/material/input";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatTooltipModule } from "@angular/material/tooltip";
import { SharedModule } from "src/app/shared/shared.module";
import { ScrollingModule } from "@angular/cdk/scrolling";
import { EditConfirmationComponent } from "../components/edit-confirmation/edit-confirmation.component";
import { ApproveConfirmationComponent } from "../components/approve-confirmation/approve-confirmation.component";
import { DeleteConfirmationComponent } from "../components/delete-confirmation/delete-confirmation.component";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { FormsModule } from "@angular/forms";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatNativeDateModule } from "@angular/material/core";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatDividerModule } from "@angular/material/divider";

const routes: Routes = [
  {
    path: "analysis",
    component: SalesAnalysisComponent,
  },
  {
    path: "management",
    component: SalesManagementComponent,
  },
  {
    path: "config",
    component: SalesManagementComponent,
  },
  {
    path: "dashboard",
    component: SalesDashboardComponent,
  },
];

@NgModule({
  declarations: [
    SalesConfigComponent,
    SalesAnalysisComponent,
    SalesManagementComponent,
    SalesDashboardComponent,
    SalesFilterComponent,
    EditConfirmationComponent,
    ApproveConfirmationComponent,
    DeleteConfirmationComponent,
  ],
  imports: [
    CommonModule,
    MatGridListModule,
    SharedModule,
    MatCardModule,
    MatMenuModule,
    FormsModule,
    MatIconModule,
    MatDatepickerModule,
    MatProgressBarModule,
    MatButtonModule,
    MatTooltipModule,
    MatCheckboxModule,
    LayoutModule,
    FlexLayoutModule,
    MatNativeDateModule,
    MatTableModule,
    MatListModule,
    MatExpansionModule,
    MatInputModule,
    CommonModule,
    VendorModule,
    MatCheckboxModule,
    GoogleChartsModule,
    MatSidenavModule,
    MatDividerModule,
    HttpClientModule,
    ScrollingModule,
    RouterModule.forChild(routes),
  ],
})
export class VendorSalesModule {}
