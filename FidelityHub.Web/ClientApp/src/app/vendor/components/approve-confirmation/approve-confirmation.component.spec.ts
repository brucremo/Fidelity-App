import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveConfirmationComponent } from './approve-confirmation.component';

describe('ApproveConfirmationComponent', () => {
  let component: ApproveConfirmationComponent;
  let fixture: ComponentFixture<ApproveConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveConfirmationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
