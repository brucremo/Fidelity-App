import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { OutletComponent } from "src/app/shared/components/outlet/outlet.component";
import { BreakpointObserver } from "@angular/cdk/layout";
import { ActivatedRoute } from "@angular/router";
import { MailService } from "src/app/shared/services/mail.service";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { NotificationService } from "src/app/shared/services/notification.service";
import { EmailRegex } from "src/app/shared/constants/validationConstants";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";
import { LoadingService } from "src/app/shared/services/loading.service";
import { NavTitleService } from "src/app/shared/services/nav-title.service";

@Component({
  selector: "app-home",
  templateUrl: "./home.component.html",
  styleUrls: ["./home.component.css"],
})
export class HomeComponent extends OutletComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    public breakpointObserver: BreakpointObserver,
    public mailService: MailService,
    public notification: NotificationService,
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer,
    public loading: LoadingService,
    public titleService: NavTitleService
  ) {
    super(breakpointObserver);
    this.titleService.clearNavTitle();
  }

  // --- Variables ---
  @ViewChild("vantagens") vantagens: ElementRef;
  public email: FormControl = new FormControl(null, [Validators.required, Validators.pattern(EmailRegex)]);
  public isBusy: boolean = false;

  ngOnInit() {}

  public sendContactRequest(): void {
    this.isBusy = true;
    this.mailService.sendContactRequest(this.email.value).subscribe((response) => {
      if(response){
        this.notification.showMessage(response.message);
      }else{
        this.notification.showMessage("Erro ao enviar mensagem");
      }
      this.email.reset();
      this.isBusy = false;
    });
  }

  public arrowClicked(): void {
    const targetElement = this.vantagens.nativeElement;
    targetElement.scrollIntoView({ behavior: "smooth" });
  }
}
