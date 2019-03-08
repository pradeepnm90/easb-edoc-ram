import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealChecklistComponent } from './deal-checklist.component';

describe('DealChecklistComponent', () => {
  let component: DealChecklistComponent;
  let fixture: ComponentFixture<DealChecklistComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealChecklistComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealChecklistComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
