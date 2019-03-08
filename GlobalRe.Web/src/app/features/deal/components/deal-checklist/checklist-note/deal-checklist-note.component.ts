import { Component, Input, Inject, Injectable, ViewChild, ElementRef, AfterViewInit, OnInit } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar, MatInput } from "@angular/material";
import { FormGroup, FormControl, Validators } from '@angular/forms';
import * as moment from 'moment';
import { Store, ActionsSubject } from '@ngrx/store';
import * as fromRoot from "../../../../../store/index"
// import { Observable } from 'rxjs/Observable';
import { LoginUser } from '../../../../../shared/models/login-user';
import * as fromDealCheckList from './../../../../../actions/deals/deal-checkList.actions';
import { CHECKLIST, DEAL_API_URL } from "../../../../../app.config";


@Component({
  selector: 'deal-checklist-note',
  template: `  <form [formGroup]="checklistNote" (ngSubmit)="onSave()">
  <div class="common-scss">
    <span>
      <h1 mat-dialog-title class="addNote">{{title}}</h1>
      <i class="material-icons clear-btn-cls" (click)="closeDialog()"
        >clear</i>
    </span>

    <div>
      <div class="viewlinecontianer" style="height:85px">
        <div class="submission-name">
          <div class="submission-name-block">
            <span class="submission-name-value"
            matTooltip="{{dealName}}" 
            [matTooltipPosition]="'above'" >
           {{dealName}}
            </span>
            <span class="submission-name-label">
            CONTRACT NAME</span>
          </div>
          <div class="author-block">
            AUTHOR
            <div style="color:gray;margin-left:17%;margin-top:-5%"> {{author}}</div>
          </div>
        </div>
        <div class="Contract">
          <div class="contractnumber-block">
            <span class="contract-number">{{contractID}}</span>
            <span class="contact-number-label" style="font-size: 12px;padding-top: 5px;">CONTRACT#</span>
          </div>
          <div class="date-block" style="padding-top:5px">
            <span>DATE</span>
            <div style="color:grey;margin-left:30%;margin-top:-11.5%">{{date}}</div>
          </div>
        </div>
        <div class="grid-id">
              <div class="grs-id-block">
                <span class="grs-id-value">{{dealNumber}}</span>
                <span class="grs-id-label">DEAL NUMBER</span>
              </div>
        </div>
      </div>
    <div>
    <mat-form-field class="textarea-form-elem">
    <mat-label class="required-class" style="height: 16px; width: 63px; color: #5E5959; font-size: 12px; line-height: 16px;">NOTE</mat-label>
    <textarea matInput class="textarea"
    maxlength="200"
    formControlName="note"
    (input)= "checkForChange($event)"
   
></textarea>
  </mat-form-field>
    </div>

    <hr class="line" />
    <div mat-dialog-actions align="end" class="save_links">
      <button
        mat-button
        type="button"
        class="mat-button cancel_btn"
        (click)="closeDialog()"
      >
        Cancel
      </button>
      <button
        mat-button 
        type="submit"
        class="save_btn mat-button"
      [disabled]="disabledSaveBtn || !checklistNote.dirty"
      >
        Save
      </button>
     <cancel-popup [position]="position" [popUpText]="popOverMsg" (closePopUp) = "closeCancelPopUp()" [hidden]="!(showCancelPopup === true)"></cancel-popup>
    </div>
    </div>
  </div>
</form>`,
  styleUrls: ['./deal-checklist-note.component.scss']
})
export class DealChecklistNoteComponent implements OnInit {
  showApproverNameSelect: boolean = true;

  contractID: number;
  author: string;
  date;
  dealName: string;
  dealNumber: number;
  title: string;
  loginUser$: Store<any>;
  originalNotesData: string;
  disabledSaveBtn: boolean = true;
  showCancelPopup: boolean = false;
  popOverMsg: string;
  position: any;
  checklistNote: FormGroup = new FormGroup({
    note: new FormControl('',Validators.required)
  });
  
  constructor(
    public dialogRef: MatDialogRef<any>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private _store: Store<fromRoot.AppState>,
    private elRef: ElementRef,
    private actionsSubject$: ActionsSubject,
    public dialog: MatDialog,
  ) { }

  ngOnInit() {
    this.popOverMsg = `You will lose your unsaved changes. Are you sure?`;
    this.loginUser$ = this._store.select<LoginUser>(
      fromRoot.getAuthenticatedUser
    );

    if (this.data.modalType === 'ADDNOTECHECKLIST') {
      this.addNoteCheckList();
    }
    this.originalNotesData = this.data.modalData.notes.toLocaleUpperCase();
  }

  ngAfterViewInit() { // adding for IE detection of ElementRef

    if (this.showCancelPopup) {
      this.elRef.nativeElement.parentElement.parentElement.style.position = 'relative';
    }
  }

  addNoteCheckList() {
    this.contractID = this.data.modalData.contractNo;
    this.author = this.data.modalData.authorName;
    this.date = this.data.modalData.date;
    this.dealName = this.data.modalData.dealInfo;
    this.checklistNote.controls['note'].setValue(this.data.modalData.notes);
    this.dealNumber = this.data.modalData.dealNumber;
    this.title = this.data.modalData.modalTitle;
  }

  onSave() {
    if (this.checklistNote.valid && this.checklistNote.controls['note'].value.trim().length > 0) {
      this.onAdd();
    }
  }

  onAdd(){
    let personId;
    this.loginUser$.subscribe(val => {
    personId = val.personId
    });
   let requestObj = {
      data: {
        "entityNum": 1,
        "checklists": [
          {
            "checked": true,
            "note": this.checklistNote.controls['note'].value,
            "checkedDateTime":  moment().format('MM/DD/YYYY HH:mm:ss'),
            "personId": personId
          }]
      }
    }

    let url = DEAL_API_URL + '/' + this.dealNumber + '/' + CHECKLIST + '/' + this.data.modalData.checkList;

    this._store.dispatch(
      new fromDealCheckList.CheckDealChecklist({ requestObj, url})
    )
    this.dialogRef.close()
    }

  closeDialog(){ // needed for cancel popUp position
    
    if(this.checklistNote.dirty){
     // this.getPosition(event);
     this.elRef.nativeElement.parentElement.parentElement.style.position = 'relative';
     this.getPosition();
      this.showCancelPopup = true;
    }else{
      this.dialogRef.close();
    }
  }


  getPosition(){
    this.position = {left:329,bottom:-32,from:'CheckListnote'}
  }

// function to set the position of cancel popover
  // getPosition(event){ 
  //   console.log(event)  
  
  //   let positionRight =event.clientX;
  //   let positionBottom = event.clientY;
  //   this.position = {positionRight,positionBottom} // emitting the data from the button
  // }

  checkForChange($event) {
    if ($event) {
      $event.preventDefault();
    }
    if (this.originalNotesData.trim() === this.checklistNote.controls['note'].value.trim().toLocaleUpperCase()) {

      this.disabledSaveBtn = true;
    } else {
      this.disabledSaveBtn = false;
    }
  }

  closeCancelPopUp() {
   // this.dialog.closeAll();
    this.showCancelPopup = false;
  }

}
