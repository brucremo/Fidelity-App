import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { RouterModule, ExtraOptions } from "@angular/router";

import { AppComponent } from "./app.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  NavigationComponent,
  QRCodeBottomSheet,
} from "./navigation/navigation.component";
import { LayoutModule } from "@angular/cdk/layout";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatButtonModule } from "@angular/material/button";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatIconModule, MatIconRegistry } from "@angular/material/icon";
import { MatListModule } from "@angular/material/list";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatCardModule } from "@angular/material/card";
import { MatMenuModule } from "@angular/material/menu";
import { MatFormFieldModule } from "@angular/material/form-field";
import { FlexLayoutModule } from "@angular/flex-layout";
import { OutletComponent } from "./shared/components/outlet/outlet.component";
import { MatInputModule } from "@angular/material/input";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { QRCodeModule } from "angularx-qrcode";
import { MatBottomSheetModule } from "@angular/material/bottom-sheet";
import { ZXingScannerModule } from "@zxing/ngx-scanner";
import {
  SocialLoginModule,
  SocialAuthServiceConfig,
} from "angularx-social-login";
import { MatDialogModule } from "@angular/material/dialog";
import {
  GoogleLoginProvider,
  FacebookLoginProvider,
  AmazonLoginProvider,
} from "angularx-social-login";
import { LoginComponent } from "./authentication/login/login.component";
import { OverlayPortalComponent } from "./shared/components/overlay-portal/overlay-portal.component";
import {
  OverlayContainer,
  FullscreenOverlayContainer,
} from "@angular/cdk/overlay";
import { HttpIntercept } from "./shared/services/http.interceptor";
import { SharedModule } from "./shared/shared.module";
import {
  NgxGoogleAnalyticsModule,
  NgxGoogleAnalyticsRouterModule,
} from "ngx-google-analytics";
import { environment } from "src/environments/environment";
import { ServiceWorkerModule } from "@angular/service-worker";
import { CanLoadModuleGuard } from "../app/shared/guards/canloadmodule.guard";
import { FooterComponent } from "./navigation/footer/footer.component";
import { HomeComponent } from "./webpage/home/home.component";
import { LoadingService } from "./shared/services/loading.service";

//const routerOptions: ExtraOptions = {
//anchorScrolling: "enabled",
//};

@NgModule({
  declarations: [
    AppComponent,
    NavigationComponent,
    OutletComponent,
    OverlayPortalComponent,
    QRCodeBottomSheet,
    FooterComponent,
    
  ],
  imports: [
    NgxGoogleAnalyticsModule.forRoot(environment.googleAnalytics),
    NgxGoogleAnalyticsRouterModule,
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    ZXingScannerModule,
    FormsModule,
    QRCodeModule,
    RouterModule.forRoot(
      [
        {
          path: "",
          loadChildren: () =>
            import("./webpage/webpage.module").then((m) => m.WebpageModule),
        },
        {
          path: "authentication",
          loadChildren: () =>
            import("./authentication/authentication.module").then(
              (m) => m.AuthenticationModule
            ),
        },
        {
          path: "customer",
          canLoad: [CanLoadModuleGuard],
          loadChildren: () =>
            import("./customer/customer.module").then((m) => m.CustomerModule),
        },
        {
          path: "support",
          canLoad: [CanLoadModuleGuard],
          loadChildren: () =>
            import("./support/support.module").then((m) => m.SupportModule),
        },
        {
          path: "vendor",
          canLoad: [CanLoadModuleGuard],
          loadChildren: () =>
            import("./vendor/vendor.module").then((m) => m.VendorModule),
        },
        {
          path: "account",
          loadChildren: () =>
            import("./account/account.module").then((m) => m.AccountModule),
        },
      ]
    ),
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatSidenavModule,
    FormsModule,
    BrowserModule,
    MatIconModule,
    MatListModule,
    MatCardModule,
    MatMenuModule,
    MatProgressBarModule,
    MatIconModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    FlexLayoutModule,
    SharedModule,
    SocialLoginModule,
    MatProgressSpinnerModule
  ],
  entryComponents: [QRCodeBottomSheet, LoginComponent, OverlayPortalComponent],
  providers: [
    {
      provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              ""
            ),
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider(""),
          },
        ],
      } as SocialAuthServiceConfig,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpIntercept,
      multi: true,
    },
    LoadingService
  ],
  bootstrap: [AppComponent],
  exports: [],
})
export class AppModule {}
