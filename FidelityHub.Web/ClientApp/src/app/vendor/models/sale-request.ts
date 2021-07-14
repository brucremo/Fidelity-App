import { analyzeAndValidateNgModules } from "@angular/compiler";

export class SaleRequest{
  promotionId: number;
  userId: string;
  amount: number;

  constructor(promotionId: number, userId: string, amount: number){
    this.promotionId = promotionId;
    this.userId = userId;
    this.amount = amount;
  }
}
