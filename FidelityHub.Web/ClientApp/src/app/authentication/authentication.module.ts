import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Routes, RouterModule } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { MatGridListModule } from "@angular/material/grid-list";
import { MatCardModule } from "@angular/material/card";
import { MatMenuModule } from "@angular/material/menu";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonModule } from "@angular/material/button";
import { LayoutModule } from "@angular/cdk/layout";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { RegisterComponent } from "./register/register.component";
import { AuthCallbackComponent } from "./auth-callback/auth-callback.component";
import { FlexLayoutModule } from "@angular/flex-layout";
import { MatInputModule } from "@angular/material/input";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { RegisterVendorComponent } from "./register/register-vendor/register-vendor.component";
import { RegisterClientComponent } from "./register/register-client/register-client.component";
import { MatStepperModule } from '@angular/material/stepper';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CustomerLoginComponent } from './customer-login/customer-login.component';
import { VendorLoginComponent } from './vendor-login/vendor-login.component';
import { HttpClientModule } from "@angular/common/http";
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatDividerModule } from "@angular/material/divider";
import { CoolSocialLoginButtonsModule } from '@angular-cool/social-login-buttons';
import { SharedModule } from "../shared/shared.module";
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';

const routes: Routes = [
  {
    path: "login",
    component: LoginComponent,
    data: { animation: "Login" },
  },
  {
    path: "registrar",
    component: RegisterComponent,
    data: { animation: "Register" },
  }
];

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AuthCallbackComponent,
    RegisterVendorComponent,
    RegisterClientComponent,
    CustomerLoginComponent,
    VendorLoginComponent,
    ForgotPasswordComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    MatGridListModule,
    MatCardModule,
    MatDividerModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    LayoutModule,
    MatButtonModule,
    HttpClientModule,
    FlexLayoutModule,
    MatCardModule,
    MatMenuModule,
    MatAutocompleteModule,
    MatIconModule,
    MatStepperModule,
    MatFormFieldModule,
    MatInputModule,
    MatFormFieldModule,
    MatSlideToggleModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    MatProgressBarModule,
    CoolSocialLoginButtonsModule,
    RouterModule.forChild(routes),
  ],
  providers : [
  ]
})
export class AuthenticationModule {}
