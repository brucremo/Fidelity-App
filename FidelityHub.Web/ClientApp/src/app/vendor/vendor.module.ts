import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatCardModule } from "@angular/material/card";
import { MatMenuModule } from "@angular/material/menu";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { LayoutModule } from "@angular/cdk/layout";
import { Routes, RouterModule } from "@angular/router";
import { environment } from "src/environments/environment";
import { MatTableModule } from '@angular/material/table';
import { MatListModule } from '@angular/material/list'; 
import { MatExpansionModule } from '@angular/material/expansion'; 
import { FlexLayoutModule } from "@angular/flex-layout";
import { VendorAdminGuard } from "../shared/guards/vendoradmin.guard";
import { SaleListComponent } from './components/sale-list/sale-list.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCheckboxModule } from '@angular/material/checkbox'; 
import { ScrollingModule } from "@angular/cdk/scrolling";
import { ApproveConfirmationComponent } from "./components/approve-confirmation/approve-confirmation.component";
import { EditConfirmationComponent } from "./components/edit-confirmation/edit-confirmation.component";
import { DeleteConfirmationComponent } from "./components/delete-confirmation/delete-confirmation.component";

const routes: Routes = [
  {
    path: "sales",
    canLoad: [VendorAdminGuard],
    loadChildren: () =>
      import("./vendor-sales/vendor-sales.module").then(
        (m) => m.VendorSalesModule
      ),
  },
];

@NgModule({
  declarations: [
    SaleListComponent
  ],
  imports: [
    CommonModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    LayoutModule,
    FlexLayoutModule,
    MatTableModule,
    MatCheckboxModule,
    MatListModule,
    MatCheckboxModule,
    MatExpansionModule,
    MatTooltipModule,
    ScrollingModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    SaleListComponent
  ],
  entryComponents: [
    ApproveConfirmationComponent,
    DeleteConfirmationComponent,
    EditConfirmationComponent
  ]
})
export class VendorModule {}
