import {
  Component,
  NgZone,
  Inject,
  ViewChild,
  ElementRef,
  OnInit,
  AfterViewInit,
  AfterViewChecked,
  ChangeDetectorRef,
} from "@angular/core";
import { BreakpointObserver, Breakpoints } from "@angular/cdk/layout";
import { Router, ActivatedRoute, RouterOutlet } from "@angular/router";
import { OutletComponent } from "../shared/components/outlet/outlet.component";
import { fader } from "../shared/animations/component-animations";
import {
  MatBottomSheetRef,
  MatBottomSheet,
  MAT_BOTTOM_SHEET_DATA,
} from "@angular/material/bottom-sheet";
import { AuthenticationService } from "../shared/services/authentication.service";
import { NotificationService } from "../shared/services/notification.service";
import { VendorSaleService } from "../vendor/services/vendor-sale.service";
import { SaleRequest } from "../vendor/models/sale-request";
import { LoginComponent } from "../authentication/login/login.component";
import { MatDialog } from "@angular/material/dialog";
import { RewardDialogComponent } from "../shared/components/dialogs/reward-dialog/reward-dialog.component";
import { RewardDialogData } from "../shared/models/dialogs/reward-dialog";
import { MatDrawer } from "@angular/material/sidenav";
import { BaseSignalrService } from "../shared/services/base-signalr.service";
import { LoadingService } from "../shared/services/loading.service";
import { MatToolbar } from "@angular/material/toolbar";
import { NavTitleService } from "../shared/services/nav-title.service";

@Component({
  selector: "app-navigation",
  templateUrl: "./navigation.component.html",
  styleUrls: ["./navigation.component.css"],
  animations: [fader],
})
export class NavigationComponent
  extends OutletComponent
  implements AfterViewInit, AfterViewChecked {
  public openScanner: boolean = false;
  @ViewChild("drawer") drawer: MatDrawer;

  constructor(
    private route: ActivatedRoute,
    private breakpointObserver: BreakpointObserver,
    public dialog: MatDialog,
    private router: Router,
    private auth: AuthenticationService,
    private bottomSheet: MatBottomSheet,
    private notification: NotificationService,
    private vendorService: VendorSaleService,
    private signalR: BaseSignalrService,
    public loading: LoadingService,
    public titleService: NavTitleService,
    private changeDetector : ChangeDetectorRef
  ) {
    super(breakpointObserver);
  }
  ngAfterViewChecked(): void {
    this.changeDetector.detectChanges();
  }

  ngAfterViewInit() {}

  // ----- Getters -----
  get isLoggedIn(): boolean {
    return this.auth.isLoggedIn;
  }

  get isCustomer(): boolean {
    if (!this.auth.userPolicy) {
      return null;
    }
    return this.auth.userPolicy.includes("Customer");
  }

  get isVendor(): boolean {
    if (!this.auth.userPolicy) {
      return null;
    }
    return this.auth.userPolicy.includes("Vendor");
  }

  get isVendorAdmin(): boolean {
    if (!this.auth.userPolicy) {
      return null;
    }
    return this.auth.userPolicy.includes("VendorAdmin");
  }

  get dashboardPath(): string {
    if (!this.auth.userPolicy) {
      return null;
    }
    if (this.auth.userPolicy.includes("Admin")) {
      return "vendor/dashboard";
    } else if (this.auth.userPolicy.includes("Customer")) {
      return "customer/dashboard";
    } else if (this.auth.userPolicy.includes("Vendor")) {
      return "vendor/dashboard";
    } else if (this.auth.userPolicy.includes("VendorAdmin")) {
      return "vendor/dashboard";
    } else if (this.auth.userPolicy.includes("Support")) {
      return "support/dashboard";
    }
  }

  get canScan(): boolean {
    if (!this.auth.userPolicy) {
      return null;
    }
    return (
      (this.auth.userPolicy.includes("VendorAdmin") ||
        this.auth.userPolicy.includes("Vendor") ||
        this.auth.userPolicy.includes("Admin")) &&
      this.isLoggedIn
    );
  }

  get isLoginRoute(): boolean {
    return this.router.url === "/login";
  }

  get loggedInUser(): string {
    var user = this.auth.loggedInUser as string;
    return user.slice(0, user.indexOf("@"));
  }

  get isBusy(): boolean {
    return this.loading.loadingStatus;
  }

  get currentViewTitle(): string {
    return this.titleService.navTitle;
  }

  // ----- Event Handling -----

  toggleMenu(): void {}

  goHome(): void {
    this.drawer.close();
    this.router.navigate(["home"]);
  }

  goToTermsConditions(): void {
    this.drawer.close();
    this.router.navigate(["privacidade"]);
  }

  goToWallet(): void {
    this.drawer.close();
    this.router.navigate(["customer/dashboard"]);
  }

  goDashboard(): void {
    this.drawer.close();
    this.router.navigate([this.dashboardPath]);
  }

  logout(): void {
    this.auth.logout().subscribe((response) => {
      response
        ? this.notification.showMessage("Logout efetuado com sucesso!")
        : this.notification.showMessage("Erro de logout!");
    });
    this.router.navigate(["home"]);
    this.drawer.close();
  }

  toggleScan(): void {
    this.openScanner = !this.openScanner;
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet.activatedRouteData["animation"];
  }

  openRewardDialog(): void {
    const dialogRef = this.dialog.open(RewardDialogComponent, {
      width: "350px",
      data: new RewardDialogData(),
    });

    dialogRef.afterClosed().subscribe((result) => {});
  }

  private showQRCodeSheet(scan: boolean): void {
    const bottomSheetRef = this.bottomSheet.open(QRCodeBottomSheet, {
      data: { openScanner: scan },
    });

    bottomSheetRef.afterDismissed().subscribe((result) => {
      if (result) {
        var sale = new SaleRequest(1, result, 1);
        this.vendorService.registerSale(sale).subscribe((response) => {
          if (response) {
            this.notification.showMessage("Venda registrada!");
          } else {
            this.notification.showMessage("Erro ao registrar venda");
          }

          if (response.data.amountUntilReward == 0) {
            this.openRewardDialog();
          }
        });
      }
    });
  }

  public navigate(route: string): void {
    this.drawer.close();
    this.router.navigate([route]);
  }

  openBottomSheet(scan: boolean): void {
    this.showQRCodeSheet(scan);
  }

  openLoginDialog(): void {
    const dialogRef = this.dialog.open(LoginComponent, {
      width: "700px",
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.drawer.close();
    });
  }
}

@Component({
  selector: "qr-code-sheet",
  templateUrl: "qr-code-sheet.html",
})
export class QRCodeBottomSheet {
  constructor(
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private _bottomSheetRef: MatBottomSheetRef<QRCodeBottomSheet>,
    private auth: AuthenticationService
  ) {}

  get userId(): string {
    return this.auth.loggedInId;
  }

  get openScanner(): boolean {
    return this.data.openScanner;
  }

  generateSaleRequest(): SaleRequest {
    return null;
  }

  openLink(event: MouseEvent): void {
    this._bottomSheetRef.dismiss();
    event.preventDefault();
  }

  onCodeResult(event: any): void {
    this._bottomSheetRef.dismiss(event);
  }
}
