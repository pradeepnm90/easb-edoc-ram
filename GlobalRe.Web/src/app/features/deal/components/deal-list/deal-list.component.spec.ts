import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { DealListComponent } from './deal-list.component';
 import {AgGridListComponent} from '../../../../shared/components/ag-grid-list/ag-grid-list.component';
 import "ag-grid-enterprise";
 import {AgGridModule} from "ag-grid-angular/main";
 import {DealsService} from '../../deals.service';
 import {CoreService} from '../../../../shared/services/core.service';
 import {StoreModule,Store } from '@ngrx/store';
 import {HttpClient,HttpHandler} from "@angular/common/http";
 import {DealStatusComponent} from '../deal-status/deal-status.component';
 import {MaterialModule} from '../../../../shared/material/material.module';
 import { EntityType,FilterType} from '../../../../app.config';
import { SharedEventService } from '../../../../shared/services/shared-event.service';
describe('DealListComponent', () => {
  let component: DealListComponent;
  let fixture: ComponentFixture<DealListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [MaterialModule,
        StoreModule.forRoot({

        }),
        AgGridModule.withComponents(
          []
      )
      ],
      declarations: [
        DealListComponent ,
        AgGridListComponent,
        DealStatusComponent
      ],
      providers: [
        DealsService,
        CoreService,
        HttpClient,
        HttpHandler,
        SharedEventService
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  it('should pass apiurl to component', () => {
    component.entityType=EntityType.Deals;
    component.apiURL='';
    fixture.detectChanges();
    expect(component).toBeTruthy();
});
  it('should Call getGridFilterModel', () => {

    component.getGridFilterModel(FilterType.PastInception);
    component.getGridFilterModel(FilterType.Within30Days);
    component.getGridFilterModel(FilterType.Over30Days);
    component.getGridFilterModel(FilterType.All);
    spyOn(component, 'getGridFilterModel');
    component.getGridFilterModel(FilterType.All);

    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.getGridFilterModel).toHaveBeenCalledTimes(1);
    });
  });
});
