import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DealStatusComponent } from './deal-status.component';
import {MaterialModule} from '../../../../shared/material/material.module';
import { StoreModule} from '@ngrx/store';

describe('DealStatusComponent', () => {
  let component: DealStatusComponent;
  let fixture: ComponentFixture<DealStatusComponent>;

  const dealstatusmock:any =
  {
  data: {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
  statusSummary:[{isDisabled:false ,workflowId:1,workflowName:"xyz",statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,},
  ]},
  links : [{type: "RelatedEntity", rel: "Deals", href: "v1/deals?statuscodes=1000", method: "GET"}]
}
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ MaterialModule,StoreModule.forRoot({
        })
      ],
      declarations: [ DealStatusComponent ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealStatusComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should call onClickDealStatus', () => {
    component.onClickDealStatus(true,null);
    component.onClickDealStatus(true,dealstatusmock);
    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
    });
  });

});
