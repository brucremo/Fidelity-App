import { BreakpointObserver } from "@angular/cdk/layout";
import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, ActivatedRouteSnapshot, Router } from "@angular/router";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import {
  EmailRegex,
  PasswordRegex,
  PasswordRequirementMessagePT,
  PasswordControl
} from "src/app/shared/constants/validationConstants";
import { LoadingService } from "src/app/shared/services/loading.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { ResetPasswordModel } from "../models/reset-password.model";
import { AccountManagementService } from "../services/account-management.service";

@Component({
  selector: "app-reset-password",
  templateUrl: "./reset-password.component.html",
  styleUrls: ["./reset-password.component.css"],
})
export class ResetPasswordComponent extends OutletComponent implements OnInit {
  public firstFormGroup: FormGroup;
  public isBusy: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private apiService: AccountManagementService,
    private notification: NotificationService,
    private router: Router,
    private breakpointObserver: BreakpointObserver,
    public loading: LoadingService
  ) {
    super(breakpointObserver);
  }

  ngOnInit(): void {
    this.firstFormGroup = this.formBuilder.group({
      email: ["", [Validators.required, Validators.pattern(EmailRegex)]],
      password: PasswordControl,
      passwordConfirm: PasswordControl,
    });
  }

  public resetPassword(): void {
    this.isBusy = true;
    var resetModel = this.firstFormGroup.getRawValue() as ResetPasswordModel;
    this.activatedRoute.queryParams.subscribe((params) => {
      resetModel.resetToken = params['token'];
      this.apiService.resetForgottenPassword(resetModel).subscribe(
        (response) => {
          if(response){
            this.notification.showMessage("Senha alterada com sucesso!");
          }else{
            this.notification.showMessage("Token de troca de senha inválido! Tente novamente...");
          }
        },
        (err) => {
          this.notification.showMessage("Erro ao trocar senha, por favor tente mais tarde");
        },
        () => {
          this.router.navigateByUrl("/");
          this.isBusy = false;
        }
      );
    });
  }

  // --- Getters ---
  get badPasswordMessage(): string {
    return PasswordRequirementMessagePT;
  }

  get enableConfirm(): boolean {
    return this.passwordsMatch && this.passwordValid && this.passwordConfirmValid && this.emailValid;
  }

  get emailValid(): boolean {
    return this.firstFormGroup.controls['email'].valid;
  }

  get passwordValid(): boolean {
    return this.firstFormGroup.controls['password'].valid;
  }

  get passwordConfirmValid(): boolean {
    return (
      this.firstFormGroup.controls["passwordConfirm"].valid &&
      this.firstFormGroup.controls["password"].value ==
        this.firstFormGroup.controls["passwordConfirm"].value
    );
  }

  get passwordsMatch(): boolean {
    return (
      this.firstFormGroup.controls["password"].value ==
      this.firstFormGroup.controls["passwordConfirm"].value
    );
  }
}
