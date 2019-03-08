import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomDateComponent } from './ag-date-filter.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
describe('CustomDateComponent', () => {
  let component: CustomDateComponent;
  let fixture: ComponentFixture<CustomDateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule,TextMaskModule],
      declarations: [CustomDateComponent]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomDateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
 
});
