export class UserRegistration {
  constructor(
      public name: string,
      public email: string,
      public password: string
    ) {  }
}

export enum UserType {
  Vendor = 1,
  Customer = 2
}