import { Injectable } from "@angular/core";
import {
  CanLoad,
  Route,
  UrlSegment,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
} from "@angular/router";
import { Observable } from "rxjs";
import { AuthenticationService } from "../services/authentication.service";

@Injectable({
  providedIn: "root",
})
export class VendorAdminGuard implements CanLoad {
  constructor(
    private auth: AuthenticationService,
    private router: Router
    ) {}

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean> | Promise<boolean> | boolean {
    if(!this.auth.isVendorAdmin){
      this.router.navigate(["home"]);
    }
    return this.auth.isVendorAdmin;
  }
}
