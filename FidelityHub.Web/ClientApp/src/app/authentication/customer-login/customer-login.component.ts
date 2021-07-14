import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { Router } from "@angular/router";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { OverlayRef, Overlay } from "@angular/cdk/overlay";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import { BreakpointObserver } from "@angular/cdk/layout";
import { MatDialogRef } from "@angular/material/dialog";
import { LoadingService } from "src/app/shared/services/loading.service";

@Component({
  selector: "app-customer-login",
  templateUrl: "./customer-login.component.html",
  styleUrls: ["./customer-login.component.css"],
})
export class CustomerLoginComponent extends OutletComponent implements OnInit {
  @Output("loginComplete")
  loginComplete: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  registerClicked: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  forgotPasswordClicked: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private authService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router,
    private breakpointObserver: BreakpointObserver,
    public loading: LoadingService
  ) {
    super(breakpointObserver);
  }

  ngOnInit(): void {}

  public loginProviders: any[] = [
    { name: "Google", icon: "google_logo", description: "Entrar com Google" },
    { name: "Facebook", icon: "fb_logo", description: "Entrar com Facebook" },
  ];

  public isBusy = false;
  public userName: string;
  public password: string;

  // --- UI Event Handling ---
  public loginUsingProvider(type: any): void {
    if (!this.isBusy) {
      this.isBusy = true;
      if (type.name == "Google") {
        this.googleLogin();
      } else {
        this.facebookLogin();
      }
    }
  }

  login() {
    this.isBusy = true;
    this.authService.login(this.userName, this.password).subscribe(
      (response) => {
        this.isBusy = false;
        if(response){
          this.loginComplete.emit(true);
        }
      },
      (error) => {
      },
      () => {
        this.isBusy = false;
      }
    );
  }

  goToRegister(): void {
    this.registerClicked.emit(true);
  }

  forgotPassword(): void {
    this.forgotPasswordClicked.emit(true);
  }

  // --- Third-Party SignIn ---
  public googleLogin(): void {
    this.authService.signInWithGoogle().subscribe(
      (response) => {
        this.isBusy = false;
        if(response){
          this.loginComplete.emit(true);
        }
      },
      (error) => {
      },
      () => {
        this.isBusy = false;
      }
    );
  }

  public facebookLogin(): void {
    this.authService.signInWithFB().subscribe((response) => {
      this.isBusy = false;
      response
        ? this.loginComplete.emit(true)
        : this.notificationService.showMessage("Erro de login!");
    });
  }
}
