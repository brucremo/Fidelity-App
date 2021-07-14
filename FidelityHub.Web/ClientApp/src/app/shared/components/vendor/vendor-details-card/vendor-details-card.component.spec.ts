import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorDetailsCardComponent } from './vendor-details-card.component';

describe('VendorDetailsCardComponent', () => {
  let component: VendorDetailsCardComponent;
  let fixture: ComponentFixture<VendorDetailsCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendorDetailsCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorDetailsCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
