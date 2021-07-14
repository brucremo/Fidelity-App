import { Component, OnInit, Inject } from "@angular/core";
import { AuthenticationService } from "src/app/shared/services/authentication.service";
import { environment } from "src/environments/environment";
import { NotificationService } from "src/app/shared/services/notification.service";
import { Router } from "@angular/router";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import { BreakpointObserver } from "@angular/cdk/layout";
import { UserType } from "../../shared/models/userRegistration"
import { LoadingService } from "src/app/shared/services/loading.service";

const googleLogoPath =
  "https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg";
const fbLogoPath =
  "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2e/Facebook_Icon_%28Single_Path_-_Transparent_%22f%22%29.svg/1200px-Facebook_Icon_%28Single_Path_-_Transparent_%22f%22%29.svg.png";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent extends OutletComponent implements OnInit {
  isBusy = false;
  showForgotPassword = false;

  constructor(
    public dialogRef: MatDialogRef<LoginComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private authService: AuthenticationService,
    private notificationService: NotificationService,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private breakpointObserver: BreakpointObserver,
    private router: Router,
    public loading: LoadingService
  ) {
    super(breakpointObserver);
    this.matIconRegistry.addSvgIcon(
      "logo",
      this.domSanitizer.bypassSecurityTrustResourceUrl(googleLogoPath)
    );
    this.isHandset.subscribe((response) => {
      response ? this.updateSize("600px") : this.updateSize("500px");
    });
  }

  ngOnInit() {}

  public userTypes: any[] = [
    { name: "Lojista", description: "", icon: "store" },
    { name: "Cliente", description: "", icon: "people" },
  ];

  private selectedUserType: string = null;

  // --- UI Event Handling ---
  public setUserType(userType: any): void {
    this.selectedUserType = userType.name;
  }

  public updateSize(height: string): void {
    this.dialogRef.updateSize("680px", height);
  }

  public completeLogin(): void {
    this.notificationService.showMessage(
      "Bem-vindo " + this.authService.loggedInUser
    );
    this.dialogRef.close();
  }

  public toggleForgotPassword(event: any): void {
    this.showForgotPassword = !this.showForgotPassword;
  }

  // --- Getters ---
  get isVendorSelected(): boolean {
    return this.selectedUserType == "Lojista" && !this.showForgotPassword;
  }

  get isClientSelected(): boolean {
    return !this.showForgotPassword;
  }

  get isNoneSelected(): boolean {
    return this.selectedUserType == null || this.selectedUserType == undefined && !this.showForgotPassword;
  }

  get title(): string {
    return this.showForgotPassword ? "Trocar Senha" : "Entrar";
  }

  // --- Third-Party SignIn ---
  public googleLogin(): void {
    this.isBusy = true;
    this.authService.signInWithGoogle().subscribe((response) => {
      response
        ? this.notificationService.showMessage("Login efetuado com sucesso!")
        : this.notificationService.showMessage("Erro de login!");
    });
  }

  public facebookLogin(): void {
    this.isBusy = true;
    this.authService.signInWithFB().subscribe((response) => {
      response
        ? this.notificationService.showMessage("Login efetuado com sucesso!")
        : this.notificationService.showMessage("Erro de login!");
    });
  }

  public userName: string = null;
  public password: string = null;

  login() {
    this.isBusy = true;
    this.authService
      .login(this.userName, this.password)
      .subscribe((response) => {
        response
          ? this.router.navigate(["home"])
          : this.notificationService.showMessage("Erro de login!");
      });
  }

  goToRegister(isCustomer: boolean): void {
    this.dialogRef.close();
    this.router.navigate(["/authentication/registrar"], {state : { userType: isCustomer ? UserType.Customer : UserType.Vendor }});
  }

  closeDialog(): void {
    this.dialogRef.close();
  }
}
