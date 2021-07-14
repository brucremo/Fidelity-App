import { VendorUnit } from "../../vendor/models/vendor";
import { Promotion, PromotionSale } from "../../shared/models/promotion";

export class SalesViewModel{
  vendorUnit: VendorUnit;
  sales: PromotionSale[];
}

export class SalesModel{
  promotion: Promotion;
  sales: PromotionSale[];
}

export class Sale {
  id: number;
  promotionId: number;
  promotion: any;
  userId: string;
  user: any;
  timestamp: Date;
  amount: number;
  approved: boolean;
  sellerId: string;
}