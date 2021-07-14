export class LoginRequest {
  userName: string;
  password: string;
  rememberLogin: boolean;

  constructor(userName: string, password: string){
    this.userName = userName;
    this.password = password;
    this.rememberLogin = true;
  }
}
