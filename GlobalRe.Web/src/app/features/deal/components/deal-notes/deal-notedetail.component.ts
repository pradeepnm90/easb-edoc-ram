import { Component, Input, Inject, Injectable, ViewChild, ElementRef, AfterViewInit } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from "@angular/material";
import * as fromRoot from "../../../../store/index";
import * as fromNote from "../../../../actions/deals/deal-notes.actions";
import { NOTES_URL, ADD_NOTES_URL } from "../../../../app.config";
import { LoginUser } from '../../../../shared/models/login-user';
import {
  FormGroup,
  FormControl,
  Validator,
  Validators,
  FormBuilder,
  FormsModule,
  ReactiveFormsModule
} from "@angular/forms";
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
          <h1 mat-dialog-title class="addNote">{{ data.modalTitle }}</h1>
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
              aria-label="submission name tooltip">{{ data.dealInfo.submissionName }}</span>
                <span class="submission-name-label">CONTRACT NAME</span>
              </div>
              <div class="author-block" [hidden]="showApproverNameSelect">AUTHOR <div style="color: gray; margin-left: 17%; margin-top:-5%;">{{ data.noteInfo.authorName }}</div></div>
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
                <span>DATE </span> <div style="color: gray; margin-left: 30%; margin-top:-11.5%;">{{ data.noteInfo.date }}</div>
              </div>
            </div>
            <div class="grid-id">
              <div class="grs-id-block">
                <span class="grs-id-value">{{ data.dealInfo.grsId }}</span>
                <span class="grs-id-label">DEAL NUMBER</span>
              </div>
            </div>
          </div>

          <mat-label class="required-class" style="height: 16px; width: 63px; color: #5E5959; font-size: 12px; line-height: 16px;">NOTE TYPE</mat-label>
          <div style="margin-top:-2.5%;" [hidden]="!isAddNoteModal">
          <mat-form-field id="noEllipseDropDown" [floatLabel]="'never'">            
              <mat-select formControlName="noteTypes" [(ngModel)]="data.noteInfo.noteTypeName" style="margin-top:7%;">
                <mat-option
                  *ngFor="let noteType of data.noteTypes"
                  [value]="noteType.noteTypeName"
                  (click)="onNoteTypeSelect($event, noteType)"
                >
                  {{ noteType.noteTypeName }}
                </mat-option>
              </mat-select>
              <mat-label>Select...</mat-label>
            </mat-form-field>
          </div>
          <p style="margin: 5px 0px 35px 0px;" [hidden]="isAddNoteModal">{{this.data.noteInfo.noteTypeName}}</p>
          <mat-label class="required-class" style="height: 16px; width: 63px; color: #5E5959; font-size: 12px; line-height: 16px;">NOTE</mat-label>
          <mat-form-field class="textarea-form-elem" style="margin-top: -2.5%;">
            <textarea matInput #notesTextarea class="textarea" formControlName="noteInfo"
            [readonly]="!data.isNoteEditable" 
            [(ngModel)]="data.noteInfo.notes"
            (input)= "checkForChange($event)"></textarea>
          </mat-form-field>
          
          <hr class="line" />
          <div mat-dialog-actions align="end" class="save_links">
            <button
              mat-button
              type="button"
              class="mat-button"
              
              (click)="!disabledAllBtn  && openDialog()"
            >
              Cancel
            </button>
            <button
              mat-button 
              type="submit"
              class="save_btn mat-button"
              [disabled]="formadd.invalid || disabledAllBtn || !data.isNoteEditable || disabledSaveBtn"
            >
              Save
            </button>
            <cancel-popup [position]="position" [popUpText]="popOverMsg" (closePopUp) = "closeCancelPopUp()" [hidden]="!(showCancelPopup === true)"></cancel-popup>
          </div>
        </div>
      </div>
    </form>
  `,
  styleUrls: ["deal-notedetail.component.scss"]
})
@Injectable()
export class DealNoteDetail implements AfterViewInit {
  formadd: FormGroup = new FormGroup({
    noteTypes: new FormControl("", [Validators.required]),
    noteInfo: new FormControl("", [Validators.required]),
    approverName: new FormControl("", [Validators.required]),
  });
  disabledAllBtn: boolean = false;
  isAddNoteModal: boolean = true;
  originalNotesData: string;
  disabledSaveBtn: boolean = true;
  textAreaResizeFlag: boolean = false;
  showCancelPopup: boolean = false;
  showCancelPopupforClose: boolean = false;
  notescancelButton: boolean;
  closePopUp: boolean;
  popOverMsg: string;
  lookupList$: Observable<any>;
  approverList: any;
  pearReviewNoteType: string = 'Peer Review';
  selectedApproverName: string = '';
  showApproverNameSelect: boolean = false;
  selectedApproverObj: any = {};
  position: any;
  loginUser$: Store<any>;
  personId: any;
  @ViewChild('notesTextarea') notesTextarea: ElementRef;
  @ViewChild('approverNameDrodown') approverNameDrodown: ElementRef;
  constructor(
    public snackBar: MatSnackBar,
    private _store: Store<fromRoot.AppState>,
    private _coreService: CoreService,
    private fBuilder: FormBuilder,
    public dialogRef: MatDialogRef<DealNoteDetail>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private elRef: ElementRef
  ) { 
    this.lookupList$ = this._store.select(fromRoot.getLookupList);
  }
  ngOnInit() {

    this.popOverMsg = `You will lose unsaved changes. Are you sure?`;
    this.lookupList$.subscribe(val => {
      this.approverList = val[1].results;
    });
    if (this.data.modalType == 'ADDNOTE') {
      this.isAddNoteModal = true;
    } else {
      this.isAddNoteModal = false;

    }
    this.loginUser$ = this._store.select<LoginUser>(
      fromRoot.getAuthenticatedUser
    );
    this.loginUser$.subscribe((val) => {
      this.personId = val.personId;
    })

    this.originalNotesData = this.data.noteInfo.notes;
    this.selectedApproverName = this.data.noteInfo.authorName;
    //this.popOverMsg3 = this.popOverMsg1 + "</br>" + this.popOverMsg2
    let condition = (this.personId != this.data.noteInfo.createdBy && this.data.noteInfo.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase())
    if (condition) {
      this.showApproverNameSelect = false;
      this.isAddNoteModal = false;
    }
    else if (this.data.noteInfo.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase()) {
      this.showApproverNameSelect = true;  //Edit note "Peer Review validation"
      //this.selectedApproverName = '';
    } else {
      this.showApproverNameSelect = false;
      this.selectedApproverName = this.data.noteInfo.authorName;


    }
}
  ngAfterViewInit() { // adding for IE detection of ElementRef

    if (this.showCancelPopup) {
      this.elRef.nativeElement.parentElement.parentElement.style.position = 'relative';
    }
  }

  onApproverNameSelect($event, approver){
    if ($event) {
      $event.preventDefault();
    }
    this.selectedApproverObj = approver;
    this.selectedApproverName = approver.name;
    if(this.data.noteInfo.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase()) {
      if(this.formadd.valid){
        this.disabledSaveBtn = false;
      }
    }
    setTimeout(() => {
      this.notesTextarea.nativeElement.focus();
    }, 100);
  }
  onNoteTypeSelect($event, noteType) {
    if ($event) {
      $event.preventDefault();
    }
    this.data.noteInfo.noteTypeCode = noteType.noteTypeCode;
    if(noteType.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase()){
      this.showApproverNameSelect = true;
      this.selectedApproverName = '';
      /* setTimeout(() => {
        this.approverNameDrodown.nativeElement.focus();
      }, 100); */
    }else{
      this.showApproverNameSelect = false;
      this.selectedApproverName = this.data.noteInfo.authorName;
      this.selectedApproverObj = {};
      setTimeout(() => {
        this.notesTextarea.nativeElement.focus();
      }, 100);
    }
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  addNoteServiceCall(requestObj) {
    return this._coreService.invokePostEntityApi(requestObj, ADD_NOTES_URL);
  }
  editNoteServiceCall(requestObj) {
    return this._coreService.invokeUpdateEntityApi(requestObj, ADD_NOTES_URL);
  }
  callbackHandler(val: any) {
    if (val && val.data) {
      this._store.dispatch(
        new fromNote.LoadNotes(NOTES_URL + this.data.dealInfo.grsId)
      );
      this.openSnackBar('Note Saved Successfully', '');
      this.dialogRef.close();
    } else {
      this.disabledAllBtn = false;
    }
  }
  prepareRequestObj() {
    let requestObj = {
      data: {
        'notetype': this.data.noteInfo.noteTypeCode,
        //'notes': notesText,
        'notes': this.data.noteInfo.notes.trim()
      }
    };
    if (this.isAddNoteModal) {
      requestObj.data['dealNumber'] = this.data.dealInfo.grsId;
      requestObj.data['notedate'] = moment(this.data.noteInfo.date, 'MM/DD/YYYY').format('MM-DD-YYYY');
      if(this.data.noteInfo.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase() && this.selectedApproverObj){
        requestObj.data['whoentered'] = this.selectedApproverObj.value;
      }
    } else {
      
      requestObj.data['notenum'] = this.data.noteInfo.notenum;
      if(this.data.noteInfo.noteTypeName.toLowerCase() == this.pearReviewNoteType.toLowerCase()){
        requestObj.data['whoentered'] = this.selectedApproverObj.value;
      }
    }
    return requestObj;
  }
  callResolver() {
    let requestObj = this.prepareRequestObj();
    if (this.isAddNoteModal) {
      return this.addNoteServiceCall(requestObj);
    } else {
      return this.editNoteServiceCall(requestObj);
    }
  }
  saveHandler() {
    this.callResolver().subscribe(val => { this.callbackHandler(val) });
  }
  onSave(): void {
    this.disabledAllBtn = true;
    //let notesText = this.data.noteInfo.notes.replace(/"/g,'\"');
    this.saveHandler();
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
    if (this.originalNotesData.trim() == this.data.noteInfo.notes.trim() || this.data.noteInfo.notes.trim() == '')
    {
      this.disabledSaveBtn = true;
    } else {
      this.disabledSaveBtn = false;

    }
  }
  openDialog(): void {

    this.elRef.nativeElement.parentElement.parentElement.style.position = 'relative';
    if (!this.disabledSaveBtn) {
      console.log("touched");
    //  this.getPosition(event)
    this.getPosition()
      this.showCancelPopup = true;
    
    } else {
      this.dialogRef.close()
    }

  }


  getPosition(){
    this.position = {left:329,bottom:-105,from:'DealNote'}
  }

  // function to set the position of cancel popover
  // getPosition(event){ 
  //   console.log(event)  
  //  // let doc= this.elRef.nativeElement.querySelector('.cancel_btn').offsetWidth
  //   let positionRight =event.clientX;
  //   let positionBottom = event.clientY;
  //   this.positionLeftbottom = {positionRight,positionBottom}
  // }

  closeCancelPopUp() {

    this.showCancelPopup = false;

  }
}
