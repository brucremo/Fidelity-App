import { Component, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators, Form, AbstractControl } from "@angular/forms";
import {
  EmailRegex,
  BRGovIdRegex,
  BRPhoneRegex,
  BRPostalCode,
} from "src/app/shared/constants/validationConstants";
import { SubscriptionService } from "src/app/shared/services/subscription.service";
import { Subscription } from "src/app/shared/models/subscription";
import { ThirdPartyService } from "src/app/shared/services/third-party.service";
import { LOBSection } from "src/app/shared/models/third-party/CNAE/lobSection";
import { NotificationService } from "src/app/shared/services/notification.service";

@Component({
  selector: "app-register-vendor",
  templateUrl: "./register-vendor.component.html",
  styleUrls: ["./register-vendor.component.css"],
})
export class RegisterVendorComponent implements OnInit {
  constructor(
    private subscriptionService: SubscriptionService,
    private thirdPartyService: ThirdPartyService,
    public notification: NotificationService
  ) {
    this.getSubscriptions();
    this.getLOBSections();
  }

  ngOnInit(): void {}

  lobs: LOBSection[] = [];
  subscriptions: Subscription[] = [];
  registerForm: FormGroup = new FormGroup({
    // dbo.Users / dbo.AspNetUsers
    firstName: new FormControl("", [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(60),
    ]),
    lastName: new FormControl("", [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(80),
    ]),
    userName: new FormControl("", [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(255),
    ]),
    email: new FormControl("", [
      Validators.required,
      Validators.pattern(EmailRegex),
    ]),
    password: new FormControl("", [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(30),
    ]),
    userTypeId: new FormControl(3, [Validators.required]),
  });
  subscriptionForm: FormGroup = new FormGroup({
    subscriptionId: new FormControl(null, [Validators.required]),
  });
  userForm: FormGroup = new FormGroup({
    firstName: new FormControl("", [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(60),
    ]),
    lastName: new FormControl("", [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(80),
    ]),
    userName: new FormControl(" ", [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(255),
    ]),
    email: new FormControl("", [
      Validators.required,
      Validators.pattern(EmailRegex),
    ]),
    password: new FormControl("", [
      Validators.required,
      Validators.minLength(6),
      Validators.maxLength(30),
    ]),
    phone: new FormControl("", [
      Validators.required,
      Validators.pattern(BRPhoneRegex),
    ]),
    mobile: new FormControl("", [Validators.pattern(BRPhoneRegex)]),
  });
  businessForm: FormGroup = new FormGroup({
    // usr.Vendor
    legalName: new FormControl("", [
      Validators.required,
      Validators.minLength(2),
      Validators.maxLength(60),
    ]),
    governmentId: new FormControl("", [
      Validators.required,
      Validators.pattern(BRGovIdRegex),
    ]),
    phone: new FormControl("", [
      Validators.required,
      Validators.pattern(BRPhoneRegex),
    ]),
    mobile: new FormControl("", [Validators.pattern(BRPhoneRegex)]),
    description: new FormControl(""),
    lobId: new FormControl("", [Validators.required]),
    // usr.Address
    postalCode: new FormControl("", [
      Validators.required,
      Validators.pattern(BRPostalCode),
    ]),
    streetNumber: new FormControl("", [Validators.required, Validators.min(1)]),
    street: new FormControl({ value: "", disabled: true }, [
      Validators.required,
    ]),
    region: new FormControl({ value: "", disabled: true }, [
      Validators.required,
    ]),
    city: new FormControl({ value: "", disabled: true }, [Validators.required]),
    state: new FormControl({ value: "", disabled: true }, [
      Validators.required,
    ]),
    complement: new FormControl(""),
    country: new FormControl({ value: "Brasil", disabled: true }, [
      Validators.required,
    ]),
  });

  // --- Flags ---
  userNameExists: boolean = false;
  emailExists: boolean = false;

  // --- API Calls ---
  getSubscriptions(): void {
    var sub = this.subscriptionService
      .getSubscriptions()
      .subscribe((response) => {
        this.subscriptions = response; //.filter(x => x.active == true);
        this.subscriptions.sort();
        sub.unsubscribe();
      });
  }

  getLOBSections(): void {
    var sub = this.thirdPartyService.getLOBGroups().subscribe((response) => {
      this.lobs = response; //.filter(x => x.active == true);
      sub.unsubscribe();
    });
  }

  getUserNameExists(): any {
    return this.subscriptionService
      .userNameExists(this.userNameControl.value)
      .subscribe((response) => {
        if(response) {
          this.userNameControl.setErrors({ "duplicate" : response });
        }else{
          this.userNameControl.setErrors(null);
        }
      });
  }

  getEmailExists(): any {
    return this.subscriptionService
      .emailExists(this.emailControl.value)
      .subscribe((response) => {
        if(response) {
          this.emailControl.setErrors({ "duplicate" : response });
        }else{
          this.emailControl.setErrors(null);
        }
      });
  }

  // --- Getters ---
  get selectedSubscription(): Subscription {
    if (!this.subscriptionForm.controls["subscriptionId"].value) {
      return new Subscription();
    }
    return this.subscriptions.find(
      (x) => x.id == this.subscriptionForm.controls["subscriptionId"].value
    );
  }

  get firstName(): string {
    return this.subscriptionForm.controls["firstName"].value;
  }

  get lastName(): string {
    return this.subscriptionForm.controls["lastName"].value;
  }

  get userNameControl(): AbstractControl {
    return this.userForm.controls["userName"];
  }

  get emailControl(): AbstractControl {
    return this.userForm.controls["userName"];
  }

  // --- UI Event Handling ---
  selectSubscription(subscription: Subscription): void {
    this.subscriptionForm.controls["subscriptionId"].setValue(subscription.id);
  }

  onPostalCodeComplete(): void {
    if (this.businessForm.controls["postalCode"].valid) {
      var sub = this.thirdPartyService
        .getAddressWithPostalCodeBR(
          this.businessForm.controls["postalCode"].value
        )
        .subscribe((response) => {
          if (!response.erro) {
            this.businessForm.controls["street"].setValue(response.logradouro);
            this.businessForm.controls["region"].setValue(response.bairro);
            this.businessForm.controls["city"].setValue(response.localidade);
            this.businessForm.controls["state"].setValue(response.uf);
          } else {
            this.businessForm.controls["street"].setValue(null);
            this.businessForm.controls["region"].setValue(null);
            this.businessForm.controls["city"].setValue(null);
            this.businessForm.controls["state"].setValue(null);
            this.businessForm.controls["postalCode"].setValue(null);
            this.notification.showMessage("CEP Invalido");
          }
          sub.unsubscribe();
        });
    }
  }
}
