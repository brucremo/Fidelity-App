import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, retry, map } from "rxjs/operators";
import { BehaviorSubject, Observable, throwError, Subscriber } from "rxjs";
import { environment } from "src/environments/environment";
import { LoginRequest } from "../models/loginRequest";
import { RequestResult } from "../models/requestResult";
import { BaseApiService } from "./base-api.service";
import { NotificationService } from "./notification.service";
import { JwtTokenService } from "./jwt-token.service";
import { JWTToken, RefreshTokenRequest } from "../models/jwtToken";
import { LocalStorageService } from "../services/local-storage.service";
import { SocialAuthService } from "angularx-social-login";
import {
  FacebookLoginProvider,
  GoogleLoginProvider,
} from "angularx-social-login";
import { TokenRequestModel } from "../models/third-party/tokenRequest";

@Injectable({
  providedIn: "root",
})
export class AuthenticationService {
  constructor(
    private jwt: JwtTokenService,
    private storage: LocalStorageService,
    private notificationService: NotificationService,
    private api: BaseApiService,
    private authService: SocialAuthService
  ) {}

  private apiController: string = "/api/authentication";
  private thirdpartyController: string = "/api/thirdpartyauthentication";

  // --- General ---
  public refreshToken(): Observable<boolean> {
    return Observable.create((observer: Subscriber<any>) => {
      var sub = this.refreshTokenRequest().subscribe((response) => {
        this.storage.set("AuthToken", response.token);
        this.storage.set("AuthTokenExpiry", response.expiresIn.toString());
        this.storage.set("AuthTokenRefresh", response.refreshToken);
        this.storage.set("AuthTokenTimestamp", new Date().toString());
        this.storage.set("AuthTokenExpiry", response.expiresIn.toString());
        sub.unsubscribe();
        observer.next(true);
        observer.complete();
      });
    });
  }

  public refreshTokenRequest(): Observable<JWTToken> {
    return this.api
      .post<JWTToken>(
        environment.apiPath + this.apiController + "/refreshtoken",
        new RefreshTokenRequest(
          this.storage.get("AuthToken"),
          this.storage.get("RefreshToken")
        )
      )
      .pipe(
        catchError((err) => {
          this.notificationService.showMessage(
            this.api.handleError(err).message
          );
          this.logout();
          return throwError(err);
        })
      );
  }

  // --- Third-Party SignIn ---
  public getJWTTokenForThirdParty(
    idToken: string,
    thirdParty: string,
    name: string,
    email: string
  ): Observable<JWTToken> {
    return this.api
      .post<JWTToken>(
        environment.apiPath + this.thirdpartyController + "/tokensignin",
        new TokenRequestModel(idToken, thirdParty, name, email)
      )
      .pipe(
        catchError((err) => {
          this.notificationService.showMessage(
            this.api.handleError(err).message
          );
          return throwError(err);
        })
      );
  }

  private authorizeThirdPartyLogin(
    idToken: any,
    thirdParty: string,
    name: string,
    email: string
  ): Observable<boolean> {
    return Observable.create((observer: Subscriber<any>) => {
      var sub = this.getJWTTokenForThirdParty(
        idToken,
        thirdParty,
        name,
        email
      ).subscribe((response) => {
        if (!response) {
          observer.next(false);
          observer.complete();
        }
        this.storage.set("AuthToken", response.token);
        this.storage.set("AuthTokenExpiry", response.expiresIn.toString());
        this.storage.set("AuthTokenRefresh", response.refreshToken);
        this.storage.set("AuthTokenTimestamp", new Date().toString());
        this.storage.set("AuthTokenExpiry", response.expiresIn.toString());
        sub.unsubscribe();
        observer.next(true);
        observer.complete();
      });
    });
  }

  // --- Gooogle ---
  public signInWithGoogle(): Observable<boolean> {
    return Observable.create((observer: Subscriber<any>) => {
      this.authService
        .signIn(GoogleLoginProvider.PROVIDER_ID)
        .then((response) => {
          this.authorizeThirdPartyLogin(
            response.idToken,
            "Google",
            response.name,
            response.email
          ).subscribe((response) => {
            observer.next(response);
            observer.complete();
          });
        })
        .catch((err) => {
          if (err.error == "popup_closed_by_user") {
            this.notificationService.showMessage("Logon cancelado!");
          } else {
            this.notificationService.showMessage(
              "Erro ao autenticar com Google!"
            );
          }
          observer.next(false);
          observer.complete();
        });
    });
  }

  public signInWithFB(): Observable<boolean> {
    return Observable.create((observer: Subscriber<any>) => {
      this.authService
        .signIn(FacebookLoginProvider.PROVIDER_ID)
        .then((response) => {
          this.authorizeThirdPartyLogin(
            response.authToken,
            "Facebook",
            response.name,
            response.email
          ).subscribe((response) => {
            observer.next(response);
            observer.complete();
          });
        })
        .catch((err) => {
          if (err.error == "popup_closed_by_user") {
            this.notificationService.showMessage("Logon cancelado!");
          } else {
            this.notificationService.showMessage(
              "Erro ao autenticar com Facebook!"
            );
          }
          observer.next(false);
          observer.complete();
        });
    });
  }

  public signOut(): void {
    this.authService.signOut();
  }

  // --- Proprietary SignIn ---
  get loggedInUser(): any {
    if (this.isLoggedIn) {
      return this.jwt.decodeToken().sub;
    } else {
      return null;
    }
  }

  get loggedInId(): any {
    if (this.isLoggedIn) {
      return this.jwt.decodeToken().id;
    } else {
      return null;
    }
  }

  get userPolicy(): string[] {
    if (this.isLoggedIn) {
      return this.jwt.decodeToken().rol;
    } else {
      return null;
    }
  }

  get isLoggedIn(): boolean {
    if (this.storage.get("AuthToken")) {
      return true;
    } else {
      return false;
    }
  }

  get isVendorAdmin(): boolean {
    return this.userPolicy.includes("VendorAdmin");
  }

  public getAuthToken(): string {
    return this.storage.get("AuthToken");
  }

  public logout(): Observable<boolean> {
    this.signOut();
    this.storage.remove("AuthToken");
    this.storage.remove("AuthTokenExpiry");
    this.storage.remove("AuthTokenRefresh");
    return this.api.adHocObservableOf(true);
  }

  public login(userName: string, password: string): Observable<boolean> {
    return Observable.create((observer: Subscriber<any>) => {
      var sub = this.getJWTToken(userName, password).subscribe(
      (response) => {
        if (!response) {
          observer.next(false);
          observer.complete();
        }
        this.storage.set("AuthToken", response.token);
        this.storage.set("AuthTokenExpiry", response.expiresIn.toString());
        this.storage.set("AuthTokenRefresh", response.refreshToken);
        this.storage.set("AuthTokenTimestamp", new Date().toString());
        this.storage.set("AuthTokenExpiry", response.expiresIn.toString());
        sub.unsubscribe();
        observer.next(true);
        observer.complete();
      }, 
      (error) => {
        this.notificationService.showMessage("Usuário ou senha inválido");
        observer.next(false);
        observer.complete();
      });
    });
  }

  public getJWTToken(userName: string, password: string): Observable<JWTToken> {
    return this.api
      .post<JWTToken>(
        environment.apiPath + this.apiController + "/login",
        new LoginRequest(userName, password)
      )
      .pipe(
        catchError((err) => {
          this.notificationService.showMessage(
            this.api.handleError(err).message
          );
          return throwError(err);
        })
      );
  }
}
