import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { Router } from "@angular/router";

@Component({
  selector: "app-vendor-login",
  templateUrl: "./vendor-login.component.html",
  styleUrls: ["./vendor-login.component.css"],
})
export class VendorLoginComponent implements OnInit {
  @Output("loginComplete") loginComplete: EventEmitter<
    boolean
  > = new EventEmitter<boolean>();

  constructor(
    private authService: AuthenticationService,
    private notificationService: NotificationService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  public userName: string = null;
  public password: string = null;
  public isBusy = false;

  login() {
    this.isBusy = true;
    this.authService
      .login(this.userName, this.password)
      .subscribe((response) => {
        this.isBusy = false;
        response
          ? this.loginComplete.emit(true)
          : this.notificationService.showMessage("Erro de login!");
      });
  }

  goToRegister(): void {
    this.router.navigate(["/registrar"]);
  }
}
