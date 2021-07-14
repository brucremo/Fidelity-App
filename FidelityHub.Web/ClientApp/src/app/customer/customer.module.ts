import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { LayoutModule } from '@angular/cdk/layout';
import { RouterModule, Routes, Router } from '@angular/router';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CustomerPromotionCardComponent } from './customer-promotion-card/customer-promotion-card.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MenuItemComponent } from '../shared/components/menu-item/menu-item.component';
import { AppModule } from '../app.module';
import { SharedModule } from '../shared/shared.module';
import { MatFormFieldModule } from '@angular/material/form-field';

const routes: Routes = [
  {
    path: 'dashboard',
    component: CustomerDashboardComponent
  }
];

@NgModule({
  declarations: [
    CustomerDashboardComponent,
    CustomerPromotionCardComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    MatGridListModule,
    MatCardModule,
    MatFormFieldModule,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    LayoutModule,
    FlexLayoutModule,
    MatProgressSpinnerModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class CustomerModule { }
