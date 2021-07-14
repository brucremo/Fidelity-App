import { Address } from "../../shared/models/address";

export class VendorUnit {
  id: number;
  email: string;
  phone: string;
  mobile: string;
  description: string;
  website: string;
  openingHours: number;
  closingHours: number;
  vendorId: number;
  address: Address;
}

export class Subscription {
  id: number;
  title: string;
  description: string;
  recurrenceDays: number;
  price: number;
  active: boolean;
}

export class Vendor {
  id: number;
  userName: string;
  legalName: string;
  governmentId: string;
  email: string;
  phone: string;
  mobile: string;
  description: string;
  lobId: number;
  userTypeId: number;
  subscription: Subscription;
  address: Address;
}
