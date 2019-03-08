import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { MaterialModule } from '../../../../shared/material/material.module';
import { DealFilterComponent } from './deal-filter.component';
import { FilterType } from "../../../../app.config";

describe('DealFilterComponent', () => {
  let component: DealFilterComponent;
  let fixture: ComponentFixture<DealFilterComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports:[MaterialModule],
      declarations: [ DealFilterComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should Call onFilterClick', () => {
    let event={preventDefault:()=>{}};
    component.onFilterClick(FilterType.PastInception,event);
    spyOn(component, 'onFilterClick');
    component.onFilterClick(FilterType.PastInception,event);
    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.onFilterClick).toHaveBeenCalledTimes(1);
    });
  });
});
