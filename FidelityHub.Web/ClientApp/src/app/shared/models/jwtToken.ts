export class JWTToken {
  expiresIn: number;
  token: string;
  refreshToken: string;
}

export class RefreshTokenRequest {
  constructor(public accessToken: string, public refreshToken: string){}
}