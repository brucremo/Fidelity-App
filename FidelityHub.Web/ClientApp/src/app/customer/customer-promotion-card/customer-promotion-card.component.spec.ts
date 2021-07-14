import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerPromotionCardComponent } from './customer-promotion-card.component';

describe('CustomerPromotionCardComponent', () => {
  let component: CustomerPromotionCardComponent;
  let fixture: ComponentFixture<CustomerPromotionCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerPromotionCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerPromotionCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
