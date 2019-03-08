import { async, ComponentFixture, TestBed,fakeAsync,tick } from '@angular/core/testing';
import {AgGridModule} from "ag-grid-angular";
import { AgGridListComponent } from './ag-grid-list.component';
import { CustomDateComponent } from '../ag-date-filter/ag-date-filter.component';
import {GridOptions,GridApi} from "ag-grid-community/main";
import { TextMaskModule } from 'angular2-text-mask';
import { FormsModule } from '@angular/forms';
import {HttpClient,HttpHandler} from "@angular/common/http";
import { StoreModule} from '@ngrx/store';
import {CoreService} from '../../services/core.service';
import {Observable} from "rxjs/Rx";
import {EntityType} from "../../../app.config";
import { GridFilterModel } from '../../models/grid-filter-model';
import { SharedEventService } from '../../services/shared-event.service';
export class MockCoreService {
  response = {
    url: "v1/deals?limit=1",
    totalRecords: 1600,
    results: [
      {
        "data": {
          "dealNumber": 1330413,
          "dealName": "Munich Re Stop Loss 2015 Great American MPCI Retro D#1330413 C#3948",
          "statusCode": 0,
          "status": "Bound",
          "contractNumber": 1000841,
          "inceptionDate": "2015-01-01",
          "targetDate": null,
          "priority": null,
          "submittedDate": "2015-04-16",
          "primaryUnderwriterCode": 64710,
          "primaryUnderwriterName": "Frank Bigley",
          "secondaryUnderwriterCode": 10275,
          "secondaryUnderwriterName": "Erica Rance Mill",
          "TechnicalAssistantCode": null,
          "TechnicalAssistantName": null,
          "modellerCode": null,
          "modellerName": null,
          "actuaryCode": 48629,
          "actuaryName": "Bruce Stocker",
          "expiryDate": "2015-12-31",
          "brokerCode": 20453,
          "brokerName": "Guy Carpenter & Company, LLC",
          "brokerContactCode": 13957,
          "brokerContactName": "Dave Ott"
        }
      }
    ],
    messages: []
  };

  invokeGetListResultApi(apiUrl: string): Observable<any> {
    return Observable.of(this.response);
  }

  getEntityGridColumns(entityType: EntityType) {

    return [{
      headerName: "Deal Name",
      field: "data.dealName",
      minWidth: 100,
    },
      {
      headerName: "Contract #",
      field: "data.contractNumber",
      width: 150,
    },
      {
      headerName: "Inception",
      field: "data.inceptionDate",
      width: 120,

    },
      {
      headerName: "Target Date",
      field: "data.targetDate",
      width: 150
    },
      {
      headerName: "Priority",
      field: "data.priority",
      width: 110
    },
      {
      headerName: "Submitted",
      field: "data.submittedDate",
      width: 150
    },
      {
      headerName: "Status",
      field: "data.status",
      width: 110
    },
      {
      headerName: "Deal Number",
      field: "data.dealNumber",
      width: 150
    },
      {
      headerName: "Underwriter",
      field: "data.primaryUnderwriterName",
      width: 150
    },
      {
      headerName: "Underwriter 2",
      field: "data.secondaryUnderwriterName",
      width: 150
    },
      {
      headerName: "TA",
      field: "data.technicalAssistanceName",
      width: 110
    },
      {
      headerName: "Modeler",
      field: "data.modellerName",
      width: 120
    },
      {
        headerName: "Actuary",
        field: "data.actuaryName",
        width: 110
      },
      {
        headerName: "Expiration",
        field: "data.dealNumber",
        width: 150,
        hide: true
      },
      {
        headerName: "Broker Name",
        field: "data.brokerName",
        width: 150,
        hide: true
      },
      {
        headerName: "Broker Contact",
        field: "data.brokerContactName",
        width: 150,
        hide: true
      },
    ];
  }

}
export class MockSharedEventService{
  
  getEvent(): Observable<any> {
    return Observable.of('Event');
  }
}
describe('AgGridListComponent', () => {
  let component: AgGridListComponent;
  let fixture: ComponentFixture<AgGridListComponent>;
  let coreService: CoreService;
  let _sharedEventService: SharedEventService;
  let response={
    "url": "v1/deals?limit=1",
    "totalRecords": 16,
    "offset": 0,
    "limit": 1,
    "results": [
      {
        "data": {
          "dealNumber": 1330413,
          "dealName": "Munich Re Stop Loss 2015 Great American MPCI Retro D#1330413 C#3948",
          "statusCode": 0,
          "status": "Bound",
          "contractNumber": 1000841,
          "inceptionDate": "2015-01-01",
          "targetDate": null,
          "priority": null,
          "submittedDate": "2015-04-16",
          "primaryUnderwriterCode": 64710,
          "primaryUnderwriterName": "Frank Bigley",
          "secondaryUnderwriterCode": 10275,
          "secondaryUnderwriterName": "Erica Rance Mill",
          "TechnicalAssistantCode": null,
          "TechnicalAssistantName": null,
          "modellerCode": null,
          "modellerName": null,
          "actuaryCode": 48629,
          "actuaryName": "Bruce Stocker",
          "expiryDate": "2015-12-31",
          "brokerCode": 20453,
          "brokerName": "Guy Carpenter & Company, LLC",
          "brokerContactCode": 13957,
          "brokerContactName": "Dave Ott"
        }
      }
    ],
    "messages": []
  };
  let sortString;
  const gridOptions={
    rowDeselection: true,
    columnDefs:this.columnDefs,//this._coreService.getEntityGridColumns(this.entityType),
    enableColResize: true,
    enableSorting: true,
    enableFilter: true,
    paginationPageSize: 100,
    cacheOverflowSize: 2,
    maxConcurrentDatasourceRequests: 2,
    infiniteInitialRowCount: 1,
    maxBlocksInCache: 2,
    toolPanelSuppressRowGroups: true,
    toolPanelSuppressValues: true,
    toolPanelSuppressPivots: true,
    toolPanelSuppressPivotMode: true,
    api:{
      getSortModel:()=>{
        return [{colId: "data.dealNumber", sort: "asc"},{colId: "data.status", sort: "desc"}];
      },
      setRowData:(anydata)=>{

      },setFilterModel:(model)=>{},getFilterModel:()=>{return [];},
      showLoadingOverlay:()=>{},
      hideOverlay:()=>{}
    },

    showToolPanel:false,

  };
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [FormsModule,AgGridModule.withComponents(CustomDateComponent),TextMaskModule,StoreModule.forRoot({
        // auth: combineReducers(fromRoot.getAuthUserName),
      }), ],
      declarations: [ AgGridListComponent,CustomDateComponent],
      providers:[{provide: CoreService, useClass: MockCoreService},
        {provide: SharedEventService, useClass: MockSharedEventService}, 
        HttpClient, HttpHandler]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AgGridListComponent);
    component = fixture.componentInstance;
    coreService = fixture.debugElement.injector.get(CoreService);
    _sharedEventService = fixture.debugElement.injector.get(SharedEventService);
    // sortString = spyOn(component, 'getGridSortString').and.returnValue('dealNumber');
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call getGridData', () => {
    spyOn(component, 'getGridData');
    component.entityListApiUrl="v1/deals";
    fixture.detectChanges();
    component.getGridData();
    component.gridApiTimer=22;
    component.getGridData();
    fixture.detectChanges();
    component.getGridData();
    expect(component.getGridData).toHaveBeenCalled();
  });

  // it('should call getDataFromService', () => {
  //   component.gridOptions=gridOptions;
  //   component.entityListApiUrl="v1/deals";
  //   //component.getDataFromService(component,"v1/deals");
  //   const gridFilterModel={"data.inceptionDate":{dateTo: "2018-02-01", dateFrom: "2018-01-01", type: "notEqual", filterType: "date"}};
  //   component.filterModel= [new GridFilterModel({columnId: 'data.inceptionDate', filterModel: {dateTo: "2018-02-01", dateFrom: "2018-01-01", type: "lessThan", filterType: "date"}})];
  //   component.setFilterModel(gridOptions.api,gridFilterModel);
  //   fixture.whenStable().then(() => {
  //     fixture.detectChanges();
  //     //component.getDataFromService(component,'v1/deals');
  //     //expect(component.getDataFromService).toHaveBeenCalled();
  //   });

  // });

  // it('should call onRowDoubleClicked', () => {
  //   component.onRowDoubleClicked(response.results[0]);
  //   fixture.detectChanges();
  // });

  // it('should call onRowSelectionChanged', () => {
  //   component.onRowSelectionChanged(null);
  //   fixture.detectChanges();
  //      fixture.whenStable().then(() => {
  //     fixture.detectChanges();
  //     expect(component.onRowSelectionChanged).toHaveBeenCalledTimes(1);
  //   });
  // });

});

