import { Injectable } from "@angular/core";
import * as jwt_decode from 'jwt-decode';
import { JWTToken } from "../models/jwtToken";
import { LocalStorageService } from "./local-storage.service";
import { DateTime } from "luxon";

@Injectable({
  providedIn: "root",
})
export class JwtTokenService {

  jwtToken: JWTToken = new JWTToken();
  decodedToken: { [key: string]: string };

  constructor(
    private storage: LocalStorageService
  ) {}

  get token(): string {
    return this.jwtToken.token;
  }

  get isTokenExpired(): boolean {
    var receivedTimestamp = DateTime.fromISO(this.storage.get("AuthTokenTimestamp"));
    var tokenExpiry = parseInt(this.storage.get("AuthTokenExpiry"));
    return receivedTimestamp.plus({ seconds: tokenExpiry }) < new DateTime();
  }

  setToken(token: JWTToken) {
    if (token) {
      this.jwtToken.token = "Bearer " + token;
    }
  }

  decodeToken() {
    if (this.storage.get("AuthToken")) {
      return jwt_decode(this.storage.get("AuthToken"));
    }
  }

  getDecodeToken() {
    return jwt_decode(this.jwtToken);
  }

  getUser() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.displayname : null;
  }

  getEmailId() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.email : null;
  }

  getExpiryTime() {
    this.decodeToken();
    return this.decodedToken ? this.decodedToken.exp : null;
  }

  /*
  isTokenExpired(): boolean {
    const expiryTime = parseInt(this.getExpiryTime());
    if (expiryTime) {
      return 1000 * expiryTime - new Date().getTime() < 5000;
    } else {
      return false;
    }
  }*/
}
