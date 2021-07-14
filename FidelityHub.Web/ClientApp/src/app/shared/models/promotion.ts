import { VendorUnit } from "../../vendor/models/vendor";

export class Promotion {
  id: number;
  startDate: Date;
  endDate: Date;
  promotionHourStart: Date;
  promotionHourEnd: number;
  vendorUnit: VendorUnit;
  promotionType: PromotionType;
  description: string;
}

export class PromotionType {
  id: number;
  description: string;
  threshold: number;
}

export class PromotionSale {
  id: number;
  promotion: Promotion;
  userId: string;
  timestamp: Date;
  amount: number;
  approved: boolean;
  removed: boolean;
}
