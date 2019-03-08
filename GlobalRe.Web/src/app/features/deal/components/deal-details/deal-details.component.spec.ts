import { async, ComponentFixture, TestBed } from "@angular/core/testing";
import { StoreModule } from "@ngrx/store";
import { DealDetailsComponent } from "./deal-details.component";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { MaterialModule } from "../../../../shared/material/material.module";
import { FormBuilder } from "@angular/forms";
import { EntityApiData } from "../../../../shared/models/entity-api-data";
import { Deal } from "../../../../models/deal";

import { CoreService } from "../../../../shared/services/core.service";
import { HttpClient, HttpHandler } from "@angular/common/http";
import { DealNoteListComponent } from "../deal-notes/deal-notelist.component";
import { DealNoteComponent } from "../deal-notes/deal-note.component";
import { TruncatePipe } from "../deal-notes/note-text-pipe";
import { SharedModule } from "../../../../shared/shared.module";
import { TruncateDocumentTextPipe } from "../deal-documents/document-text-pipe";
import { DealKeyNonKeyDocumentListComponent } from "../deal-key-non-key-documents/deal-key-non-key-document-list.component";
import { DealDocumentComponent } from "../deal-documents/deal-document.component";
import { DealKeyDocumentComponent } from "../deal-key-non-key-documents/deal-document.component";
import { DealDocumentListComponent } from "../deal-documents/deal-document-list.component";

TestBed.configureTestingModule({
  declarations: [
    DealNoteListComponent,
    DealNoteComponent,
    DealDocumentComponent,
    TruncatePipe,
    TruncateDocumentTextPipe,
    DealKeyNonKeyDocumentListComponent,
    DealKeyDocumentComponent,
    DealDocumentListComponent
  ]
});

describe("DealDetailsComponent", () => {
  let component: DealDetailsComponent;
  let fixture: ComponentFixture<DealDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        StoreModule.forRoot({}),
        ReactiveFormsModule,
        FormsModule,
        MaterialModule,
        SharedModule
      ],
      declarations: [
        DealDetailsComponent,
        DealNoteListComponent,
        DealNoteComponent,
        DealDocumentComponent,
        DealKeyNonKeyDocumentListComponent,
        DealKeyDocumentComponent,
        TruncatePipe,
        TruncateDocumentTextPipe,
        DealDocumentListComponent
      ],
      providers: [FormBuilder, CoreService, HttpClient, HttpHandler]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealDetailsComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });

  it("should pass dealGridRowData to component", () => {
    const data: EntityApiData<Deal> = {
      data: {
        actuaryCode: "null",
        actuaryName: null,
        brokerCode: 56456,
        brokerContactCode: 983218,
        brokerContactName: "Bill O'Grady",
        brokerName: "Guy Carpenter",
        contractNumber: 0,
        dealName: "Whatever It Takes Tranmissions - 2018",
        dealNumber: 1366061,
        expiryDate: "01-01-2019",
        inceptionDate: "",
        modellerCode: "null",
        modellerName: null,
        primaryUnderwriterCode: "950995",
        primaryUnderwriterName: "Roslin Gibson",
        priority: null,
        secondaryUnderwriterCode: "null",
        secondaryUnderwriterName: null,
        status: "On Hold",
        statusCode: "16",
        submittedDate: "",
        targetDate: "",
        technicalAssistantCode: "950996",
        technicalAssistantName: "Kate Trent",
        action: ''
      },
      links: [],
      messages: []
    };
    component.dealGridRowData = data;
  });

  it("should pass canEditDealByRole to component", () => {
    component.canEditDealByRole = true;
  });

  it("should pass lookupListValue to component", () => {
    let value: any[] = [];
    component.lookupListValue = value;
    value = [
      {
        results: [{ name: "Bound", value: "0" }],
        totalRecords: 1,
        url: "/v1/lookups/dealstatuses"
      },
      {
        results: [{ name: "Bound", value: "0" }],
        totalRecords: 1,
        url: "/v1/lookups/dealstatuses"
      },
      {
        results: [{ name: "Bound", value: "0" }],
        totalRecords: 1,
        url: "/v1/lookups/dealstatuses"
      },
      {
        results: [{ name: "Bound", value: "0" }],
        totalRecords: 1,
        url: "/v1/lookups/dealstatuses"
      },
      {
        results: [{ name: "Bound", value: "0" }],
        totalRecords: 1,
        url: "/v1/lookups/dealstatuses"
      }
    ];
    component.lookupListValue = value;
  });

  it("form invalid when empty", () => {
    //   expect(component.dealDetails.valid).toBeTruthy();
  });

  it("should call cancelClick", () => {
    component.onCancelClick();
    component.isCancelClicked.emit(false);
    fixture.whenStable().then(() => {
      fixture.detectChanges();
      expect(component.onCancelClick).toHaveBeenCalledTimes(1);
    });
  });

  it("should call createDealDetailsForm", () => {
    component.ngOnInit();
  });

  it("should call formSubmit", () => {
    component.ngOnInit();
    const data: EntityApiData<Deal> = {
      data: {
        actuaryCode: "null",
        actuaryName: null,
        brokerCode: 56456,
        brokerContactCode: 983218,
        brokerContactName: "Bill O'Grady",
        brokerName: "Guy Carpenter",
        contractNumber: 0,
        dealName: "Whatever It Takes Tranmissions - 2018",
        dealNumber: 1366061,
        expiryDate: "01-01-2019",
        inceptionDate: "",
        modellerCode: "null",
        modellerName: null,
        primaryUnderwriterCode: "950995",
        primaryUnderwriterName: "Roslin Gibson",
        priority: null,
        secondaryUnderwriterCode: "null",
        secondaryUnderwriterName: null,
        status: "On Hold",
        statusCode: "16",
        submittedDate: "",
        targetDate: "",
        technicalAssistantCode: "950996",
        technicalAssistantName: "Kate Trent",
        action: ''
      },
      links: [],
      messages: []
    };
    component.currentDeal = data;
    component.onFormSubmit();
  });

  it("should call validatenumeric", () => {
    const documentEvent: any = document.createEvent("CustomEvent");
    documentEvent.keyCode = 17;
    component.validatenumeric(documentEvent);
    documentEvent.keyCode = 52;
    component.validatenumeric(documentEvent);
    documentEvent.keyCode = "";
    component.validatenumeric(documentEvent);
  });

  // it("should call evaluateSubmitState", () => {
  //   component.canEditDealPermission = true;
  //   component.isBoundDeal = false;
  //   component.evaluateSubmitState();
  //   expect(component.shouldDisableSubmit).toBe(false);
  // });
});
