import { Component, OnInit, ElementRef, ContentChild, Output, EventEmitter, Input, ViewEncapsulation } from '@angular/core';
import { Store} from '@ngrx/store';
import * as fromRoot from "../../../../store/index"
import { Observable } from 'rxjs/Observable';
import { MatDialog } from '@angular/material';
import { DealChecklistNoteComponent } from './checklist-note/deal-checklist-note.component';
import { __core_private_testing_placeholder__ } from '@angular/core/testing';
import * as moment from 'moment';

import { DEAL_API_URL,CHECKLIST, GlobalEventType } from '../../../../app.config';
import * as fromDealCheckList from './../../../../actions/deals/deal-checkList.actions';
import { LoginUser } from '../../../../shared/models/login-user';
import { DealNoteDetail } from '../deal-notes/deal-notedetail.component';
import { SharedEventService } from '../../../../shared/services/shared-event.service';
import { BroadcastEvent } from '../../../../shared/models/broadcast-event';




@Component({
  selector: 'deal-checklist',
  templateUrl: './deal-checklist.component.html',
  styleUrls: ['./deal-checklist.component.scss'],
  host: {
    '(document:click)': 'oncloseClick($event)',
},
encapsulation: ViewEncapsulation.None


})
export class DealChecklistComponent implements OnInit {
checklist;
DealChecklist$: Observable<any>;
dealCheckList: any;
allExpandState = false;
note: string;
outerLoop: number;
innerloop: number;
loginUser$: Store<any>;
onPin:boolean = false;
personId: any;
pinValue = '';
@Output() onPinChecked= new EventEmitter();
@Input() dealdetails;

  constructor(
    private _store: Store<fromRoot.AppState>,
    private dialog: MatDialog,
    private _eref: ElementRef,
    public _sharedEventService: SharedEventService
  ) { }

  ngOnInit() {
  this.allExpandState = true;
  this.DealChecklist$ =  this._store.select(fromRoot.getDealCheckList);
  this.DealChecklist$.subscribe(val =>{
  this.dealCheckList = val;
  });

  this.loginUser$ = this._store.select<LoginUser>(
    fromRoot.getAuthenticatedUser
  );
    this.loginUser$.subscribe((val) => {
      this.personId = Number(val.personId);
    })
   }

  onTask(tasks, checklist, event) {
    // close the deal notes on uncheck
    this.outerLoop = -1;
    this.innerloop = -1;
 
    let currentDateTime = moment().format('MM/DD/YYYY HH:mm:ss');
    let requestObj;
    let url = DEAL_API_URL + '/' + checklist.dealNum + '/' + CHECKLIST + '/' + tasks.chkListNum;
    this.loginUser$.subscribe(val => {
    this.personId = Number(val.personId)
    });
    tasks.note === null ? this.note ="" : this.note = tasks.note 
    if (event.checked) {
      requestObj = {
        data: {
          "entityNum": 1,
          "checklists": [
            {
              "checked": event.checked,
              "note": this.note,
              "checkedDateTime": currentDateTime,
              "personId": this.personId
            }]
        }
      }
    } else {
      requestObj = {
        data: {
          "entityNum": 1,
          "checklists": [{ "checked": event.checked }]
        }
      };
    }
  //  console.log("Comp",requestObj)
    this._store.dispatch(
      new fromDealCheckList.CheckDealChecklist({ requestObj, url})
    )
  }


   // splitting the date
   checkDate(checkListdate){
     
    let checkDate="";
     if(checkListdate !== null || checkListdate !== undefined || checkListdate !==""){
      checkDate = checkListdate.split(" ")
      return checkDate[0];
     }
   }

  // splitting time
  checkTime(checkListtime) {
    let checkTime = "";
    let timeNoSecond;
    checkTime = checkListtime.split(" ")
    timeNoSecond = checkTime[1].split(":")
    let checkedTime = timeNoSecond[0] + ':' + timeNoSecond[1] + checkTime[2]; // order the date 
    return checkedTime;
  }

// show Task note
noteShow(i,j){
  if(this.outerLoop === i && this.innerloop === j){
    this.outerLoop = -1;
    this.innerloop = -1;
  }else{
    this.outerLoop = i;
    this.innerloop = j;
  }
}

opencheckListAdd(checklist,index){
  this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Restrict_from_Closing, 'Quick Deal', 'Retain Quick Deal', {data: true}));
  let note;
  checklist.data.checklists[index].note === null ? note="": note = checklist.data.checklists[index].note ;
  //console.log(checklist)

if(this.onPin === false){
 this.pinValue ="opened"
}else{
  this.pinValue ='';
}
  let currentNoteInfo = {
    dealInfo: this.dealdetails.data.dealName,
    dealNumber: checklist.data.dealNum,
    contractNo: this.dealdetails.data.contractNumber,
    notes: note,
    authorName: checklist.data.checklists[index].personFirstName + ' ' + checklist.data.checklists[index].personLastName,
    checkList: checklist.data.checklists[index].chkListNum,
    date: checklist.data.checklists[index].checkedDateTime.split(" ")[0],
    modalTitle: checklist.data.checklists[index].chkListName
  };

  const dialogRef = this.dialog.open(DealChecklistNoteComponent, {
    minHeight: "435px",
    width: "665px",
    disableClose: true,
    autoFocus: false,
    data: {
    modalData: currentNoteInfo,
    modalType:'ADDNOTECHECKLIST'
    }
  });
  dialogRef.afterClosed().subscribe(result => {
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Restrict_from_Closing, 'Quick Deal', 'Retain Quick Deal', {data: false}));
   // console.log("The dialog was closed", result);
  });
 
  }

  oncloseClick($event) {
    let target = this._eref.nativeElement.contains($event.target)
    this.onPinChecked.emit({ pin: this.onPin, target: target, pinValue:this.pinValue });

  }

  pinChecked(){
    this.onPin = this.onPin? false: true;
    this.pinValue='';
  }

  notes(note) {
    if (note !== null && note.trim().length > 0) {
      return true;
    } else {
      return false;
    }
  }
}
