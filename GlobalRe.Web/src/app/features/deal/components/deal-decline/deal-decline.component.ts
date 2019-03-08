import { Component, Input, Inject, Injectable, ViewChild, ElementRef, AfterViewInit } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from "@angular/material";
import * as fromRoot from "../../../../store/index";
import * as fromNote from "../../../../actions/deals/deal-notes.actions";
import { NOTES_URL, ADD_NOTES_URL,DECLINE_API_URL,GlobalNotificationMessageType,EntityType,GlobalEventType,DECLINE_REASON_URL } from "../../../../app.config";
import {
  FormGroup,
  FormControl,
  Validator,
  Validators,
  FormBuilder,
  FormsModule,
  ReactiveFormsModule
} from "@angular/forms";
import { AddUserNotificationAction } from "../../../../actions/user-notification.action";
import { BroadcastEvent } from "../../../../shared/models/broadcast-event";
import { SharedEventService } from "../../../../shared/services/shared-event.service";
import { CancelPopup } from "../../../../shared/components/close-conformation/close-conformationpopup.component";
import { formControlBinding } from "@angular/forms/src/directives/ng_model";
import { CoreService } from "../../../../shared/services/core.service";
import { Store } from "@ngrx/store";
import * as moment from "moment";
import { Observable } from "rxjs";
@Component({
  selector: "deal-note-details",
  template: `
    <form [formGroup]="formadd" (ngSubmit)="onSave()">
      <div class="common-scss">
        <span>
          <h1 mat-dialog-title class="addNote" style="width:100%">{{data.modalTitle}}</h1>
          <i class="material-icons clear-btn-cls" (click)="!disabledAllBtn  && openDialog()"
            >clear</i
          >

  
        </span>
        <div>
        <div class="viewlinecontianer" [ngStyle]="{'height': showApproverNameSelect? '100px' : '85px' }">
        <div class="submission-name">
          <div class="submission-name-block">
          <span class="submission-name-value" 
          matTooltip="{{data.dealInfo.submissionName}}" 
          [matTooltipPosition]="'above'" 
          aria-label="submission name tooltip">{{ data.dealInfo.dealName }}</span>
            <span class="submission-name-label">CONTRACT NAME</span>
          </div>
          <div class="author-block" [hidden]="showApproverNameSelect">AUTHOR <div style="color: gray; margin-left: 17%; margin-top:-5%;">{{ data.declineInfo.authorName }}</div></div>
          <div [ngClass]="{'approver-name-section': showApproverNameSelect}" [hidden]="!showApproverNameSelect">
            <mat-label class="required-class author-title">AUTHOR  </mat-label>
            <div class="approver-name-select">
              <mat-form-field [floatLabel]="'never'">
                <mat-select #approverNameDrodown placeholder="Select..." formControlName="approverName" [(ngModel)]="selectedApproverName">
                  <mat-option *ngFor="let approver of approverList" [value]="approver.name" (click)="onApproverNameSelect($event, approver)">
                    {{ approver.name }}
                  </mat-option>
                </mat-select>
              </mat-form-field>
            </div>
          </div>
        </div>
        <div class="Contract">
          <div class="contractnumber-block">
            <span class="contract-number">{{ data.dealInfo.contractNumber }}</span>
            <span class="contract-number-label">CONTRACT #</span>
          </div>
          <div class="date-block" [ngStyle]="{'padding-top': showApproverNameSelect? '15px' : '5px' }">
            <span>DATE </span> <div style="color: gray; margin-left: 30%; margin-top:-11.5%;">{{ data.declineInfo.date }}</div>
          </div>
        </div>
        <div class="grid-id">
          <div class="grs-id-block">
            <span class="grs-id-value">{{ data.dealInfo.dealNumber }}</span>
            <span class="grs-id-label">DEAL NUMBER</span>
          </div>
        </div>
      
          </div>
        <mat-label class="required-class" style="height: 16px; width: 63px; color: #5E5959; font-size: 12px; line-height: 16px;">REASON</mat-label>
        <div style="margin-top:-2.5%;">
        <mat-form-field id="noEllipseDropDown" [floatLabel]="'never'">            
            <mat-select formControlName="reason" [(ngModel)]="data.declineInfo.reason">
              <mat-option
                *ngFor="let status of statusValues"
                [value]="status.value"
              (click)="checkForChange($event)"
              >
              {{ status.name }}
              </mat-option>
            </mat-select>
            <mat-label>Select...</mat-label>
          </mat-form-field>
        </div>

        <mat-label class="required-class" style="height: 16px; width: 63px; color: #5E5959; font-size: 12px; line-height: 16px;">COMMENTS</mat-label>
          <mat-form-field class="textarea-form-elem" style="margin-top: -2.5%;">
            <textarea matInput #notesTextarea class="textarea" formControlName="declineInfo"
            [(ngModel)]="data.declineInfo.comments"
            (input)= "checkForChange($event)"></textarea>
          </mat-form-field>
          
          <hr class="line" />
          <div mat-dialog-actions align="end" class="save_links">
            <button
              mat-button
              type="button"
              class="mat-button"
              
              (click)="openDialog()"
            >
              Cancel
            </button>
            <cancel-popup [position]="position" [popUpText]="popOverMsg" (closePopUp) = "closeCancelPopUp()" *ngIf="showCancelPopup === true"></cancel-popup>
            <button
              mat-button 
              type="submit"
              class="save_btn mat-button"
              [disabled]="formadd.invalid || disabledAllBtn || !data.isNoteEditable || disabledSaveBtn"
            >
              Save
            </button>
            
          </div>
        </div>
      </div>
    </form>
  `,
  styleUrls: ["deal-decline.component.scss"]
})
@Injectable()
export class DealDeclineComponent implements AfterViewInit {
  formadd: FormGroup = new FormGroup({
    reason: new FormControl("", [Validators.required]),
    declineInfo: new FormControl("", [Validators.required]),
    approverName: new FormControl("", [Validators.required])
  });
  disabledAllBtn: boolean = false;
  updatedReasonName : string;
  isAddNoteModal: boolean = true;
  originalCommentsData: string;
  originalReasonData:string;
  disabledSaveBtn: boolean = true;
  textAreaResizeFlag: boolean = false;
  showCancelPopup: boolean = false;
  showCancelPopupforClose: boolean = false;
  notescancelButton: boolean;
  closePopUp: boolean;
  popOverMsg: string;
  lookupList$: Observable<any>;
  approverList: any;
  statusValues=[];
  pearReviewNoteType: string = 'Peer Review';
  selectedApproverName: string = '';
  position: any;
  showApproverNameSelect: boolean = false;
  selectedApproverObj: any = {  };
  @ViewChild('notesTextarea') notesTextarea: ElementRef;
  @ViewChild('approverNameDrodown') approverNameDrodown: ElementRef;
  constructor(
    public snackBar: MatSnackBar,
    private _store: Store<fromRoot.AppState>,
    private _coreService: CoreService,
    private fBuilder: FormBuilder,
    public dialogRef: MatDialogRef<DealDeclineComponent>,
    public _sharedEventService: SharedEventService,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private elRef: ElementRef
  ) { 
    this.lookupList$ = this._store.select(fromRoot.getLookupList);
  }
  ngOnInit() {
    this.popOverMsg = `You will lose unsaved changes. Are you sure?`;
    this.lookupList$.subscribe(val => {
      console.log(val);
      this.approverList = val[1].results;
    });
    if (this.data.modalType == 'ADDNOTE') {
      this.isAddNoteModal = true;
    } else {
      this.isAddNoteModal = false;
    }
    this.originalCommentsData = this.data.declineInfo.comments;
    this.originalReasonData=this.data.declineInfo.reason;
    this.selectedApproverName = this.data.declineInfo.authorName;
    // this.popOverMsg3 = this.popOverMsg1 + "</br>" + this.popOverMsg2
    this._coreService.invokeGetEntityApi(DECLINE_REASON_URL).subscribe(val => {
      console.log(val); 
     val.results.map(data => {
       if(data.isActive == true)
       {
         this.statusValues.push(data);
       }
     })
  });
  }

  onApproverNameSelect($event, approver){
    if ($event) {
      $event.preventDefault();
    }
    this.selectedApproverObj = approver;
    this.selectedApproverName = approver.name;
    setTimeout(() => {
      this.notesTextarea.nativeElement.focus();
    }, 100);
  }
  onNoteTypeSelect($event, noteType) {
    if ($event) {
      $event.preventDefault();
    }
    this.data.declineInfo.noteTypeCode = noteType.noteTypeCode;
    if(noteType.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase()){
      this.showApproverNameSelect = true;
      this.selectedApproverName = '';
      /* setTimeout(() => {
        this.approverNameDrodown.nativeElement.focus();
      }, 100); */
    }else{
      this.showApproverNameSelect = false;
      this.selectedApproverName = this.data.declineInfo.authorName;
      this.selectedApproverObj = {};
      setTimeout(() => {
        this.notesTextarea.nativeElement.focus();
      }, 100);
    }
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  ngAfterViewInit() { // adding for IE detection of ElementRef

    if (this.showCancelPopup) {
      this.elRef.nativeElement.parentElement.parentElement.style.position = 'relative';
    }
  }
  declineDealServiceCall(requestObj) {
   return this._coreService.invokeUpdateEntityApi(requestObj, '/v1/deals/' +  this.data.dealInfo.dealNumber).subscribe(val => {
      if(val.messages && val.messages.length > 0){

        if(val.messages.find(item => item.severity == GlobalNotificationMessageType.ERROR_TYPE || item.severity == GlobalNotificationMessageType.FATAL_TYPE )){
          this.showCancelPopup = false;
          this.dialogRef.close();
          //to close quick edit
          //  this.data.closeQuickEdit.emit(false);
          //  this.data.refreshGrid.emit(false);
        }else{
          // this.dealDetails.reset();
          this.showCancelPopup = false;
         // this.onSaveClose(); 
        //  this.openSnackBar(val.messages[0].detail,'');
         this.dialogRef.close();
         //to close quick edit
          this.data.closeQuickEdit.emit(false);
          this.data.refreshGrid.emit(false);
          this._store.dispatch(new AddUserNotificationAction(val.messages));
          this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Deal_saved,
            EntityType.Deals,
            '',this.data.dealInfo));
        }
        this._store.dispatch(new AddUserNotificationAction(val.messages));
      }else{
        // need clarification when api giving other error
        this.showCancelPopup = false;
        this.dialogRef.close();
      }
    }
    );

   // return this._coreService.invokeUpdateEntityApi(requestObj, DECLINE_API_URL + this.data.dealInfo.dealNumber);
  }

  callbackHandler(val: any) {
    if (val && val.data) {
      // this._store.dispatch(
      //   new fromNote.LoadNotes(NOTES_URL + this.data.dealInfo.grsId)
      // );
      this.openSnackBar('Deal Declined Successfully!!!', '');
      this.dialogRef.close();
      //to close quick edit
       this.data.closeQuickEdit.emit(false);
       this.data.refreshGrid.emit(false);
    } else {
      this.disabledAllBtn = false;
    }
  }
  prepareRequestObj() {
    let reasonName;
    console.log(this.data.dealInfo);
    this.statusValues.map(status=>{
      if(status.value === this.data.declineInfo.reason)
      {
        reasonName= status.name;
        return reasonName;
      }
    })
    let requestObj = {
      data: {
        "dealNumber": this.data.dealInfo.dealNumber,
        "dealName": this.data.dealInfo.dealName,
        "statusCode": this.data.declineInfo.reason,
        "status": this.data.declineInfo.reason,
        "contractNumber": this.data.dealInfo.contractNumber,
        "inceptionDate": this.data.dealInfo.inceptionDate,
        "targetDate": this.data.dealInfo.targetDate,
        "priority": this.data.dealInfo.priority,
        "submittedDate": this.data.dealInfo.submittedDate,
        "primaryUnderwriterCode":this.data.dealInfo.primaryUnderwriterCode,
        "primaryUnderwriterName": this.data.dealInfo.primaryUnderwriterName,
        "secondaryUnderwriterCode": this.data.dealInfo.secondaryUnderwriterCode,
        "secondaryUnderwriterName": this.data.dealInfo.secondaryUnderwriterName,
        "technicalAssistantCode": this.data.dealInfo.technicalAssistantCode,
        "technicalAssistantName": this.data.dealInfo.technicalAssistantName,
        "modellerCode": this.data.dealInfo.modellerCode,
        "modellerName": this.data.dealInfo.modellerName,
        "cedantCode":this.data.dealInfo.cedantCode,
        "actuaryCode": this.data.dealInfo.actuaryCode,
        "actuaryName": this.data.dealInfo.actuaryName,
        "expiryDate": this.data.dealInfo.expiryDate,
        "brokerCode": this.data.dealInfo.brokerCode,
        "brokerName": this.data.dealInfo.brokerName,
        "brokerContactCode": this.data.dealInfo.brokerContactCode,
        "brokerContactName":this.data.dealInfo.brokerContactName,
        "ReasonForDecline": this.data.declineInfo.comments
      }
        // 'reason': this.data.declineInfo.reason,
        // 'comments': this.data.declineInfo.comments.trim()
    };
     
    return requestObj;
  }
  callResolver() {
   
    let requestObj = this.prepareRequestObj();
    return this.declineDealServiceCall(requestObj);
  
  }
  // saveHandler() {
  //   this.callResolver().subscribe(val => { this.callbackHandler(val) });
  // }
  onSave() {
    this.disabledAllBtn = true;
    //let notesText = this.data.declineInfo.notes.replace(/"/g,'\"');
    let requestObj = this.prepareRequestObj();
    return this.declineDealServiceCall(requestObj);
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
  checkForChange($event) {
   
    if ($event) {
      $event.preventDefault();
    }
    let updatedReasonvalue=this.formadd.controls['reason'].value
    if (this.originalCommentsData !== this.data.declineInfo.comments || 
        this.originalReasonData != updatedReasonvalue) {
      this.disabledSaveBtn = false;
    } else {
      this.disabledSaveBtn = true;
    }
  }
  openDialog(): void {

    this.elRef.nativeElement.parentElement.parentElement.style.position = 'relative';
    if (!this.disabledSaveBtn) {
      console.log("touched");
      this.getPosition()
      this.showCancelPopup = true;
    } else {
      this.dialogRef.close()
    }

  }
  getPosition(){
    this.position = {left:329,bottom:-105,from:'DealNote'}
  }

  closeCancelPopUp() {

    this.showCancelPopup = false;

  }
}
