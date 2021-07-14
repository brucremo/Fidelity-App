import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { CustomerPromotionDashboardViewModel } from '../models/dashboard.model'
import { MenuItemComponent } from '../../shared/components/menu-item/menu-item.component';
import { NavItem } from '../../shared/interfaces/nav-item.interface';
import { PromotionSale } from '../../shared/models/promotion';
import { slideInOut } from 'src/app/shared/animations/component-animations';
import { DateTime } from "luxon";

@Component({
  selector: 'app-customer-promotion-card',
  templateUrl: './customer-promotion-card.component.html',
  styleUrls: ['./customer-promotion-card.component.css'],
  animations: [slideInOut]
})
export class CustomerPromotionCardComponent implements OnInit {

  public animationState = 'in';
  public expanded: boolean = false;

  @Input() data: CustomerPromotionDashboardViewModel;

  @Output() cardClass: EventEmitter<string> = new EventEmitter<string>();

  constructor() { 
  }
  ngOnInit(): void {
    if(this.data.purchases.length < this.threshold) {
      var remainingPurchases = this.threshold - this.data.purchases.length;

      for(var i = 0; i < remainingPurchases; i++){
        this.data.purchases.push(null);
      }
    }
  }

  // --- Getters ---
  get title(): string {
    return this.data.promotion.description;
  }

  get endDate(): string {
    return this.data.promotion.endDate.toString();
  }

  get vendorUnit(): string {
    return this.data.promotion.vendorUnit.description;
  }

  get threshold(): number {
    return this.data.promotion.promotionType.threshold;
  }

  get purchases(): PromotionSale[] {
    return this.data.purchases;
  }

  get navItems(): NavItem[] {
    var nav: NavItem[] = [
      {
        displayName: '...',
        iconName: 'close',
        children: [
          {
            displayName: 'Lojista',
            iconName: 'store'
          }
        ]
      }
    ];

    return nav;
  }

  get cardContentClass(): string {
    return "expanded-mobile";
  }

  // --- UI Event Handling ---
  public toggleDetails(): void {
    this.cardClass.emit("expanded-mobile");
    console.log(this.expanded)
    if(this.expanded){
      this.expanded = false;
    } else {
      this.expanded = true;
    }
  }

  // --- UI Helpers ---
  public getIconForPurchase(sale: PromotionSale) : string {
    if(!sale){
      return "check_circle_outline";
    }
    return "check_circle";
  }

  public getTooltip(sale: PromotionSale) : string {
    if(!sale){
      return null;
    }
    return "Comprado em " + DateTime.fromFormat(sale.timestamp.toString(), "DD/MM/YYYY");
  }

  public disableTooltip(sale: PromotionSale): boolean {
    if(sale){
      return true;
    }else{
      return false;
    }
  }
}
