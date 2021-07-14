import { Component } from "@angular/core";
import { MatIconRegistry } from "@angular/material/icon";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent {
  title = "app";

  constructor(
    private iconRegistry: MatIconRegistry,
    private sanitizer: DomSanitizer) {
    this.iconRegistry.addSvgIcon(
      'fb-logo',
      this.sanitizer.bypassSecurityTrustResourceUrl('./assets/images/icons/fb/fb-blue.svg')
    )
  }

  ngOnInit() {}
}
