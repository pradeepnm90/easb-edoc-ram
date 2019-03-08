import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { RouterTestingModule } from "@angular/router/testing";
import { DashboardContainer } from "./dashboard.container";
import { DealStatusComponent } from "../deal/components/deal-status/deal-status.component";
import { MaterialModule } from "../../shared/material/material.module";
//import { DealsService } from '../deal/deals.service';
import { StoreModule, Store } from "@ngrx/store";
import { DealContainer } from "../deal/deal.container";
import { DealListComponent } from "../deal/components/deal-list/deal-list.component";
import { DealDetailsComponent } from "../deal/components/deal-details/deal-details.component";
import { DealSubStatusComponent } from "../deal/components/deal-substatus/deal-substatus.component";
import { AgGridListComponent } from "../../shared/components/ag-grid-list/ag-grid-list.component";
import "ag-grid-enterprise";
import { AgGridModule } from "ag-grid-angular/main";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
//import { HttpClient, HttpHandler } from "@angular/common/http";
import * as actions from "../../actions/auth.action";
import { LoginUser } from "../../shared/models/login-user";
import * as fromRoot from "../../store/index";

import { DealFilterComponent } from "../deal/components/deal-filter/deal-filter.component";
import { MomentModule } from "angular2-moment";
import { TruncatePipe } from "../deal/components/deal-notes/note-text-pipe";
import { TruncateDocumentTextPipe } from "../deal/components/deal-documents/document-text-pipe";

describe("DashboardContainer", () => {
  let component: DashboardContainer;
  let fixture: ComponentFixture<DashboardContainer>;
  let store: Store<fromRoot.AppState>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        MaterialModule,
        ReactiveFormsModule,
        FormsModule,
        StoreModule.forRoot({ reducer: fromRoot.metaReducer }),
        AgGridModule.withComponents([]),
        MomentModule
      ],
      declarations: [
        DashboardContainer,
        DealStatusComponent,
        DealContainer,
        DealListComponent,
        AgGridListComponent,
        DealSubStatusComponent,
        DealFilterComponent,
        TruncatePipe,
        TruncateDocumentTextPipe,
        DealDetailsComponent
      ],
      providers: [
        //DealsService,
        //HttpClient,
        //HttpHandler
      ]
    }).compileComponents();

    store = TestBed.get(Store);
  }));

  beforeEach(() => {
    let mockUser = new LoginUser({
      userName: "testUser",
      applicationVersion: "1.0",
      domainName: "MARKELCORP",
      environment: "QA",
      isAuthenticated: true
    });
    const action = new actions.LoginSuccessAction({ user: mockUser });
    store.dispatch(action);

    fixture = TestBed.createComponent(DashboardContainer);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
