export class TokenRequestModel {
  idToken: string;
  thirdParty: string;
  email: string;
  name: string;

  constructor(idToken: string, thirdParty: string, name: string = null, email: string = null){
    this.idToken = idToken;
    this.thirdParty = thirdParty;
    this.name = name;
    this.email = email;
  }
}
