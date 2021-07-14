import { BreakpointObserver } from "@angular/cdk/layout";
import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import { EmailRegex } from "src/app/shared/constants/validationConstants";
import { LoadingService } from "src/app/shared/services/loading.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { RegistrationService } from "../services/registration.service";

@Component({
  selector: "app-forgot-password",
  templateUrl: "./forgot-password.component.html",
  styleUrls: ["./forgot-password.component.css"],
})
export class ForgotPasswordComponent extends OutletComponent implements OnInit {

  @Output() onReturn: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output("onComplete") complete: EventEmitter<boolean> = new EventEmitter<boolean>();

  public isBusy = false;
  public form: FormGroup;

  constructor(
    public breakpointObserver: BreakpointObserver,
    public apiService: RegistrationService,
    public notification: NotificationService,
    private router: Router,
    public loading: LoadingService
  ) {
    super(breakpointObserver);
    this.form = new FormGroup({
      email: new FormControl("", [
        Validators.required,
        Validators.pattern(EmailRegex),
      ]),
    });
  }

  ngOnInit(): void {}

  public return(): void {
    this.onReturn.emit(true);
  }

  public sendResetRequest(): void {
    this.isBusy = true;
    var sub = this.apiService
      .sendForgotPasswordEmail(this.form.controls["email"].value)
      .subscribe(
        (response) => {
          this.notification.showMessage(
            "Email enviado, se você possui uma conta registrada com este endereço " +
              "você irá receber uma mensagem para trocar sua senha em breve."
          );
        },
        (error) => {
          this.notification.showMessage(
            "Erro ao enviar email, por favor tente novamente em breve"
          );
        },
        () => {
          this.complete.emit(true);
          this.isBusy = false;
        }
      );
  }
}
