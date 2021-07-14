import { Pipe, PipeTransform } from '@angular/core';
import { PromotionSale } from '../models/promotion';

@Pipe({
  name: 'saleStatusIcon'
})
export class SaleStatusIconPipe implements PipeTransform {

  transform(value: PromotionSale): string {
    if(!value || value.removed){
      return "check_circle_outline";
    } else if(value.approved){
      return "check_circle";
    } else {
      return "pending"
    }
  }

}
