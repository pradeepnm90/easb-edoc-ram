import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { DealContainer } from "./deal.container";
import { DealDetailsComponent } from "../deal/components/deal-details/deal-details.component";
import { DealStatusComponent } from "../deal/components/deal-status/deal-status.component";
import { DealListComponent } from "../deal/components/deal-list/deal-list.component";
import { DealSubStatusComponent } from "../deal/components/deal-substatus/deal-substatus.component";
import { StoreModule } from "@ngrx/store";
import { RouterTestingModule } from "@angular/router/testing";
import { MaterialModule } from "../../shared/material/material.module";
import "ag-grid-enterprise";
import { MatDialogModule, MatCheckboxModule } from "@angular/material";
import { SharedModule } from "../../shared/shared.module";
import { EntityApiData } from "../../shared/models/entity-api-data";
import { DealStatus } from "./../../models/deal-status";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { MomentModule } from "angular2-moment";
import { DealFilterComponent } from "../deal/components/deal-filter/deal-filter.component";
import { TruncatePipe } from "./components/deal-notes/note-text-pipe";
import { TruncateDocumentTextPipe } from "./components/deal-documents/document-text-pipe";

let stubUrl = "v1/deals?statuscodes=80,2,14,29";
let subDivisionUrl = "v1/deals?subdivision=1,2";

let stubdealstatuslist: any = [
  {
    data: {
      isDisabled: false,
      workflowId: 1,
      workflowName: "xyz",
      isSelected: true,
      statusCode: 16,
      statusName: "On Hold",
      sortOrder: 2,
      count: 28,
      statusSummary: [
        {
          isDisabled: false,
          workflowId: 1,
          workflowName: "test",
          isSelected: true,
          statusCode: 100,
          statusName: "Under Review",
          sortOrder: 101,
          count: 100
        },
        {
          isDisabled: false,
          workflowId: 1,
          workflowName: "test",
          isSelected: true,
          statusCode: 101,
          statusName: "Authorize",
          sortOrder: 102,
          count: 200
        }
      ]
    },
    links: [
      {
        type: "RelatedEntity",
        rel: "Deals",
        href: "v1/deals?statuscodes=1000",
        method: "GET"
      }
    ]
  }
];

const subDivisionlist: any = [
  {
    data: {
      id: 1,
      workflowName: "xyz",
      isSelected: false,
      name: "On Hold",
      sortOrder: 2,
      subDivisions: [
        { id: 1, isSelected: true, name: "On Hold", sortOrder: 2 },
        { id: 2, isSelected: false, name: "On Hold1", sortOrder: 2 }
      ]
    },
    links: [
      {
        type: "RelatedEntity",
        rel: "Deals",
        href: "v1/deals?statuscodes=1000",
        method: "GET"
      }
    ]
  },
  {
    data: {
      id: 5,
      workflowName: "xyz",
      isSelected: true,
      name: "On Hold",
      sortOrder: 2,
      subDivisions: [
        { id: 4, isSelected: true, name: "On Hold2", sortOrder: 2 },
        { id: 3, isSelected: true, name: "On Hold2", sortOrder: 2 }
      ]
    },
    links: [
      {
        type: "RelatedEntity",
        rel: "Deals",
        href: "v1/deals?subdivisions=4,3",
        method: "GET"
      }
    ]
  },
  {
    data: {
      id: 7,
      workflowName: "xyz",
      isSelected: true,
      name: "On Hold",
      sortOrder: 2,
      subDivisions: []
    },
    links: [
      {
        type: "RelatedEntity",
        rel: "Deals",
        href: "v1/deals?subdivisions=1000",
        method: "GET"
      }
    ]
  },
  {
    data: {
      id: 8,
      workflowName: "xyz",
      isSelected: false,
      name: "On Hold8",
      sortOrder: 2,
      subDivisions: []
    },
    links: [
      {
        type: "RelatedEntity",
        rel: "Deals",
        href: "v1/deals?subdivisions=1000",
        method: "GET"
      }
    ]
  }
];

let stubdealdata: any = {
  deal: {
    data: {
      statusCode: 16,
      statusName: "On Hold",
      isSelected: true,
      sortOrder: 2,
      count: 28,
      statusSummary: [{}]
    },
    links: [
      {
        type: "RelatedEntity",
        rel: "Deals",
        href: "v1/deals?subdivisions=4,3",
        method: "GET"
      }
    ]
  },
  dealslist: [
    {
      data: {
        statusCode: 16,
        statusName: "On Hold",
        isSelected: true,
        sortOrder: 2,
        count: 28,
        statusSummary: [{}]
      },
      links: [
        {
          type: "RelatedEntity",
          rel: "Deals",
          href: "v1/deals?subdivisions=4,3",
          method: "GET"
        }
      ]
    }
  ]
};

describe("DealContainer Component", () => {
  let component: DealContainer;
  let fixture: ComponentFixture<DealContainer>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        MaterialModule,
        ReactiveFormsModule,
        FormsModule,
        StoreModule.forRoot({}),
        SharedModule,
        MatDialogModule,
        MatCheckboxModule,
        MomentModule
      ],
      declarations: [
        DealContainer,
        TruncatePipe,
        TruncateDocumentTextPipe,
        DealDetailsComponent,
        DealStatusComponent,
        DealFilterComponent,
        DealListComponent,
        DealSubStatusComponent
      ],
      providers: []
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealContainer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });

  // it('should call onclick deal container and show grid,show sub status panel ', () => {
  //   component.showGridDetails=true;
  //   component.showSubDealstatus=true;
  //   component.onClickDealStatus(null);
  //   component.onClickDealStatus(stubdealdata);

  //   fixture.whenStable().then(() => {
  //     fixture.detectChanges();
  //     expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //   });
  // });

  // it('should call onclick deal container and show grid,hide sub status panel ', () => {
  //   if(stubdealdata.dealslist.find(deal=>deal.data.isSelected)){
  //     stubdealdata.dealslist[0].isSelected=false;
  //     stubdealdata.deal.isSelected=false;
  //   }

  //   component.onClickDealStatus(stubdealdata);
  //   component.showGridDetails=false;
  //   component.showSubDealstatus=false;

  //   fixture.whenStable().then(() => {
  //     fixture.detectChanges();
  //     expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //   });
  // });

  // it('should call onclick deal container and hide grid,hide sub status panel ', () => {
  //   stubdealdata.dealslist=[];

  //       component.onClickDealStatus(stubdealdata);
  //       component.showGridDetails=false;
  //       component.showSubDealstatus=false;

  //       fixture.whenStable().then(() => {
  //         fixture.detectChanges();
  //         expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //       });
  //     });
  // it('should call onclick sub deal status  method with selected code', () => {
  //   component.apiUrl =stubUrl;
  //   component.onClickSubDealStatus(stubdealdata);

  //   fixture.whenStable().then(() => {
  //     fixture.detectChanges();
  //      expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //   });

  // });

  // it('should create urls for grid data if deal have sub status ', () => {
  //   component.apiUrl  =stubUrl;
  //   let testStubdealstatuslist:any= [{"data": {isDisabled:false ,workflowId:1,workflowName:"xyz",isSelected:true,statusCode: 16, statusName: "On Hold", sortOrder: 2, count: 28,
  //       statusSummary:[]},
  //     links : []
  //   }]
  //   component.updateUrl(testStubdealstatuslist);
  //   component.updateUrl(stubdealstatuslist);
  //   fixture.whenStable().then(() => {
  //     fixture.detectChanges();
  //     expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //   });
  // });

  // it('should create urls for grid data if deal dont have sub status ', () => {
  //   // if(stubdealstatuslist.find(deal=>deal.data.statusSummary)){
  //   //   stubdealstatuslist[0].data.statusSummary=null;
  //   // }

  //   debugger
  //     component.apiUrl  =stubUrl;
  //     component.dealsBaseApi=subDivisionUrl;
  //     component.updateUrl(stubdealstatuslist);

  //     fixture.whenStable().then(() => {
  //       fixture.detectChanges();
  //       expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //     });
  //   });

  //   it('should create urls for grid data if deal dont have sub status and links are not null ', () => {
  //       component.apiUrl  =stubUrl;
  //       component.dealsBaseApi=null;
  //       component.updateUrl(stubdealstatuslist);

  //       fixture.whenStable().then(() => {
  //         fixture.detectChanges();
  //         expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //       });
  //     });

  //     it('should create urls for grid data if deal dont have sub status and link is null ', () => {
  //         component.apiUrl  =stubUrl;
  //         component.dealsBaseApi=null;
  //         component.updateUrl(stubdealstatuslist.find(data=>data.links==null));
  //         component.updateApiUrl(subDivisionlist,[]);
  //         fixture.whenStable().then(() => {
  //           fixture.detectChanges();
  //           expect(component.onClickDealStatus).toHaveBeenCalledTimes(1);
  //         });
  //       });
});
