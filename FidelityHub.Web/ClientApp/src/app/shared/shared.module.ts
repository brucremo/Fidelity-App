import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuItemComponent } from './components/menu-item/menu-item.component';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule } from '@angular/router';
import { RewardDialogComponent } from './components/dialogs/reward-dialog/reward-dialog.component';
import { SaleDialogComponent } from './components/dialogs/sale-dialog/sale-dialog.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { BaseSignalrService } from './services/base-signalr.service';
import { VendorDetailsCardComponent } from './components/vendor/vendor-details-card/vendor-details-card.component';
import { FacebookButtonComponent } from './components/facebook-button/facebook-button.component';
import { FullLoadingComponent } from './components/full-loading/full-loading.component'
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { DatePipe } from './pipes/date.pipe';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { ConfirmationSheetComponent } from './components/confirmation-sheet/confirmation-sheet.component';
import { ComponentHostComponent } from './components/component-host/component-host.component';
import { CommandBarComponent } from './components/command-bar/command-bar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SubMenuComponent } from './components/sub-menu/sub-menu.component';
import { SaleStatusIconPipe } from './pipes/sale-status-icon.pipe';

@NgModule({
  declarations: [
    MenuItemComponent,
    RewardDialogComponent,
    SaleDialogComponent,
    VendorDetailsCardComponent,
    FacebookButtonComponent,
    FullLoadingComponent,
    DatePipe,
    ComponentHostComponent,
    ConfirmationSheetComponent,
    CommandBarComponent,
    SubMenuComponent,
    SaleStatusIconPipe
  ],
  imports: [
    CommonModule,
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
    RouterModule,
    MatDialogModule,
    MatToolbarModule,
    MatTooltipModule,
    FlexLayoutModule,
    MatProgressSpinnerModule,
    MatBottomSheetModule
  ],
  exports: [
    MenuItemComponent,
    FacebookButtonComponent,
    FullLoadingComponent,
    CommandBarComponent,
    SaleStatusIconPipe
  ],
  entryComponents :[
    RewardDialogComponent,
    ConfirmationSheetComponent
  ]
})
export class SharedModule { }
