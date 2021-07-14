import { Injectable } from "@angular/core";
import {
  ActivatedRouteSnapshot,
  Resolve,
  Router,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { LoadingService } from "src/app/shared/services/loading.service";
import { NotificationService } from "src/app/shared/services/notification.service";
import { AccountManagementService } from "./account-management.service";

@Injectable({
  providedIn: "any",
})
export class ResetPasswordResolver implements Resolve<boolean> {
  constructor(
    private apiService: AccountManagementService,
    private router: Router,
    private notification: NotificationService,
    public loading: LoadingService
  ) {}

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean | Observable<boolean> | Promise<boolean> {
    this.loading.startLoading();
    return new Observable<boolean>((observer) => {
      this.apiService
        .isResetPasswordTokenValid(route.queryParams["token"])
        .subscribe(
          (response) => {
            if (!response) {
              this.notification.showMessage(
                "Token de troca de senha inválido! Tente novamente..."
              );
              this.router.navigateByUrl("home");
              observer.next(false);
              observer.complete();
              this.loading.loadingComplete();
            } else {
              observer.next(response);
              observer.complete();
              this.loading.loadingComplete();
            }
          },
          (err) => {
            this.notification.showMessage(
              "Erro ao carregar, por favor tente mais tarde"
            );
            this.router.navigateByUrl("home");
            observer.next(false);
            observer.complete();
            this.loading.loadingComplete();
          }
        );
    });
  }
}
