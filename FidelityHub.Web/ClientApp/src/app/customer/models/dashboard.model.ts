import { Promotion, PromotionSale } from "../../shared/models/promotion";

export class CustomerPromotionDashboardViewModel {
    promotion: Promotion;
    purchases: PromotionSale[];
}