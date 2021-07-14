import { BreakpointObserver } from "@angular/cdk/layout";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import {
  EmailRegex,
  PasswordRegex,
  PasswordRequirementMessagePT,
  PasswordControl
} from "src/app/shared/constants/validationConstants";
import { LoadingService } from "src/app/shared/services/loading.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { SubscriptionService } from "src/app/shared/services/subscription.service";
import { ThirdPartyService } from "src/app/shared/services/third-party.service";
import { CustomerRegistrationModel } from "../../models/customer-registration.model";
import { RegistrationService } from "../../services/registration.service";

@Component({
  selector: "app-register-client",
  templateUrl: "./register-client.component.html",
  styleUrls: ["./register-client.component.css"],
})
export class RegisterClientComponent extends OutletComponent implements OnInit {
  public firstFormGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public breakpointObserver: BreakpointObserver,
    private subscriptionService: SubscriptionService,
    private registrationService: RegistrationService,
    private thirdPartyService: ThirdPartyService,
    public notificationService: NotificationService,
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

  // --- API Calls ---
  getEmailExists(): any {
    return this.subscriptionService
      .emailExists(this.firstFormGroup.controls["email"].value)
      .subscribe((response) => {
        if (response) {
          this.firstFormGroup.controls["email"].setErrors({
            duplicate: response,
          });
        } else {
          this.firstFormGroup.controls["email"].setErrors(null);
        }
      });
  }

  registerUser(): any {
    var newUser = this.firstFormGroup.getRawValue() as CustomerRegistrationModel;
    newUser.hasAddress = false;
    newUser.userName = newUser.email;
    return this.registrationService
      .registerCustomer(newUser)
      .subscribe((response) => {
        response
          ? this.notificationService.showMessage(
              "Registro concluído, bem vindo a FidelityHub!"
            )
          : this.notificationService.showMessage("Erro ao registrar!");
        this.router.navigate(["/home"]);
      });
  }

  // --- Getters ---
  get badPasswordMessage(): string {
    return PasswordRequirementMessagePT;
  }

  get emailValid(): boolean {
    return this.firstFormGroup.controls["email"].valid;
  }

  get passwordValid(): boolean {
    return this.firstFormGroup.controls["password"].valid;
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
