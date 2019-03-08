import { Component, Input } from "@angular/core";
import { Store } from "@ngrx/store";
import * as fromRoot from "../../../../store/index";
import * as fromNote from "../../../../actions/deals/deal-notes.actions";
import { NOTES_URL, GlobalEventType } from "../../../../app.config";
import { Observable } from "rxjs/Observable";
import { DealNoteDetail } from "../deal-notes/deal-notedetail.component";
import {
  NOTE_TYPE_COLOR_CONFIG,
  DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE,
  DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE,
  NOTE_URL
} from "../../../../app.config";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { LoginUser } from "../../../../shared/models/login-user";
import * as moment from "moment";
import { CoreService } from "../../../../shared/services/core.service";
import { BroadcastEvent } from "../../../../shared/models/broadcast-event";
import { SharedEventService } from "../../../../shared/services/shared-event.service";
@Component({
  selector: "deal-notes",
  template: `
    <div class=" notes-panel-container">
      <div class="notes-panel">
        <div class="notesHeader ">
          <span
            ><i class="fa fa-sticky-note-o textSize23 " aria-hidden="true"></i
          ></span>
          <span class="txtSize20 pad-15-l">Notes</span>
          <span
            class="noteMl floatR"
            style="cursor: pointer;  "
            (click)="openDialog()"
          >
            <span><i class="fa fa-plus-circle textSize18"></i></span>
            <span class="txtSize14">Add Note</span>
          </span>
        </div>

        <div class="note-inner-container" *ngIf="noteList.length > 0">
          <div class="note-list-container">
            <div
              *ngFor="let item of noteList"
              class="noteItem"
              (click)="openEditDialog($event, item.data.notenum)"
            >
              <div style="width: 100%;"><deal-note [note]="item"></deal-note></div>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class DealNoteListComponent {
  lookupList$: Observable<any>;
  noteTypesObj: {};
  noteList: any;
  noteTypes: any;
  userDetails: any;
  currentNoteInfo: any;
  loginUser$: Store<any>;
  constructor(
    private _store: Store<fromRoot.AppState>,
    public dialog: MatDialog,
    private _coreService: CoreService,
    public _sharedEventService: SharedEventService
  ) {
    this.lookupList$ = this._store.select(fromRoot.getLookupList);
    this.loginUser$ = this._store.select<LoginUser>(
      fromRoot.getAuthenticatedUser
    );
  }
  currentDealInfo: any;
  @Input() set currentDealData(val) {
    this.currentDealInfo = {
      grsId: val.data.dealNumber,
      submissionName: val.data.dealName,
      contractNumber: val.data.contractNumber
    };
    this._store.dispatch(
      new fromNote.LoadNotes(NOTES_URL + this.currentDealInfo.grsId)
    );
  }
  ngOnInit(): void {
    this.loginUser$.subscribe(val => {
      this.userDetails = val;
    });
    this.noteList = [];
    this.noteTypes = [];
    this.lookupList$
      .subscribe(val => {
        let temp = {},
          tempNoteType: any,
          tempNoteTypes = [];
        tempNoteType = val[5].results.map(item => {
          temp[item.value] = item;
          if (item.isActive) {
            tempNoteTypes.push({
              noteTypeCode: item.value,
              noteTypeName: item.name
            });
          }
          return item;
        });
        this.noteTypesObj = temp;
        this.noteTypes = tempNoteTypes;
      })
      .unsubscribe();

    this._store.select(fromRoot.getNoteList).subscribe(val => {
      this.noteList = val.map(note => {
        let tempObj = Object.assign({}, note);
        tempObj.data["noteTypeFontColor"] = NOTE_TYPE_COLOR_CONFIG[
          tempObj.data.notetype
        ]
          ? NOTE_TYPE_COLOR_CONFIG[tempObj.data.notetype].fontColor
          : DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE;
        tempObj.data["noteTypeBackgroundColor"] = NOTE_TYPE_COLOR_CONFIG[
          tempObj.data.notetype
        ]
          ? NOTE_TYPE_COLOR_CONFIG[tempObj.data.notetype].backColor
          : DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE;
          tempObj.data["fullName"] = this.noteTypesObj[tempObj.data.notetype].name;

        if(NOTE_TYPE_COLOR_CONFIG[tempObj.data.notetype].backColor !== DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE){
          tempObj.data["noteTypeName"] = this.noteTypesObj[
            tempObj.data.notetype
          ].name.charAt(0).toUpperCase();
        }
        else{
          let noteTypeAcronym = this.noteTypesObj[tempObj.data.notetype].name.split("/").join(",").split(" ");
          tempObj.data["noteTypeName"] = (noteTypeAcronym.length >1) ?
            (noteTypeAcronym[0].charAt(0).toUpperCase() + noteTypeAcronym[1].charAt(0).toUpperCase())
        :
           (noteTypeAcronym[0].charAt(0).toUpperCase()+noteTypeAcronym[0].charAt(1).toUpperCase())
        }
        return tempObj;
      });
    });
  }
  ngOnDestroy() {
    this._store.dispatch(new fromNote.ClearNotes());
  }
  openDialog(): void {
    let currentNoteInfo = {
      notenum: "",
      noteTypeCode: "",
      noteTypeName: "",
      notes: "",
      authorName: this.userDetails.displayName,
      date: moment().format("MM/DD/YYYY")
    };
    this.openDialogHandler(currentNoteInfo, true, "ADDNOTE", "Add Note");
  }
  openEditDialog($event, notenum) {
    if ($event) {
      $event.preventDefault();
    }
    console.log(NOTE_URL, notenum);
    this._coreService.invokeGetEntityApi(NOTE_URL + notenum).subscribe(val => {
      if (val && val.results && val.results.length > 0) {
        let noteVal = val.results[0].data;
        console.log("value",noteVal)
        let currentNoteInfo = {
          notenum: noteVal.notenum,
          noteTypeCode: noteVal.notetype,
          noteTypeName: this.noteTypesObj[noteVal.notetype].name,
          notes: noteVal.notes.replace(/(?:\\[rn]|[\r\n])/g, "\n"), //this.noteVal.notes.replace(/(?:\\[rn]|[\r\n])/g, '<br/>');
          authorName: noteVal.firstName + " " + noteVal.lastName,
          createdBy: noteVal.createdBy,
          date: moment(noteVal.notedate).format("MM/DD/YYYY")
          
        };
        let isNoteEditable =
          noteVal.createdBy == this.userDetails.personId ? true : false; // changed to createdBy 
        this.openDialogHandler(
          currentNoteInfo,
          isNoteEditable,
          "EDITNOTE",
          "Edit Note"
        );
      } else {
      }
    });
  }
  openDialogHandler(currentNoteInfo, isNoteEditable, modalType, modalTitle) {
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Restrict_from_Closing, 'Quick Deal', 'Retain Quick Deal', {data: true}));
    const copmRef = this;
    const dialogRef = this.dialog.open(DealNoteDetail, {
      minHeight: "435px",
      width: "665px",
      disableClose: true,
      autoFocus: false,
      data: {
        modalType: modalType,
        modalTitle: modalTitle,
        isNoteEditable: isNoteEditable,
        noteTypes: copmRef.noteTypes,
        dealInfo: copmRef.currentDealInfo,
        noteInfo: currentNoteInfo
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Restrict_from_Closing, 'Quick Deal', 'Retain Quick Deal', {data: false}));
      console.log("The dialog was closed", result);
    });
  }
}
