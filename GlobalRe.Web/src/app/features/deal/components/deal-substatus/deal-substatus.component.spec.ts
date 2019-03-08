import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DealSubStatusComponent } from './deal-substatus.component';
import {DealStatus} from '../../../../models/deal-status';
import {MaterialModule} from '../../../../shared/material/material.module';
import {EntityApiData} from '../../../../shared/models/entity-api-data';
 //import {DealStatus} from '../../../../../assets/deals.json';
import * as _ from "lodash";
import {By} from '@angular/platform-browser';

describe('Deal Sub status Component', () => {
  let component: DealSubStatusComponent;
  let fixture: ComponentFixture<DealSubStatusComponent>;
  let dealstatusmock:any= {
          isDisabled:false ,workflowId:1,workflowName:"xyz",
             statusCode: 16, statusName: "On Hold", sortOrder: 1, count: 28

  }  

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [ MaterialModule
      ],
      declarations: [ DealSubStatusComponent ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealSubStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call onSubStatusClick with checked', () => {  
    let dealstatusList: any= [{statusCode: 16, statusName: "On Hold",
           isSelected:false, sortOrder: 2, count: 28, statusSummary: [{}]}];
    component.onSubStatusClick(null,true)
    component.dealStatusList = dealstatusList;
    component.onSubStatusClick(dealstatusmock,true)
    fixture.detectChanges();
    component.onSubStatusClick(dealstatusmock,false)
    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.onSubStatusClick).toHaveBeenCalledTimes(1);
    });
   });  
});
