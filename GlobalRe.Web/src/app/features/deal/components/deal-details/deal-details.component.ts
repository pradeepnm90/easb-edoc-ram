
import {
  Component,
  Input,
  Output,
  OnInit,
  EventEmitter,
  state,
  ElementRef,
  ViewChild,
  Inject
} from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import * as _ from "lodash";
import { FormGroup, Validators, FormControl } from "@angular/forms";
import { DealStatus } from "../../../../models/deal-status";
import { EntityApiData } from "../../../../shared/models/entity-api-data";
import { Deal } from "../../../../models/deal";
import * as fromRoot from "../../../../store/index";
import { Store } from "@ngrx/store";
import * as moment from "moment";
import { MatDatepickerInputEvent } from "@angular/material/datepicker";
import { DealDeclineComponent } from "../deal-decline/deal-decline.component";
import { DEAL_API_URL, GlobalEventType, GlobalNotificationMessageType, EntityType ,DECLINE_REASON_URL} from "../../../../app.config";
import * as fromDealCheckList from './../../../../actions/deals/deal-checkList.actions';
import { SharedEventService } from "../../../../shared/services/shared-event.service";
import { BroadcastEvent } from "../../../../shared/models/broadcast-event";
import { CoreService } from "../../../../shared/services/core.service";
import { AddUserNotificationAction } from "../../../../actions/user-notification.action";
import { Observable } from "rxjs";
import { LoginUser } from "../../../../shared/models/login-user";
import { DOCUMENT } from "@angular/common";
@Component({
  selector: "deal-details",
  templateUrl: "./deal-details.component.html",
  styleUrls :["deal-details.component.scss"],
  host: {
    '(document:click)': 'onOutsideClick($event)',
  }
})
export class DealDetailsComponent implements OnInit {
  noteType;
  _noteType;
  BASE_ERMS_DEAL_EDIT_API: string;
 userDetails: any;
  formChanged: boolean = false;
  popOverMsg:string;
  showCancelPopup: boolean = false;
  dealDetails: FormGroup;
  underwriters: any;
  statusnames: any;
  dealNumberforDocuments: string = "447123005";
  tas: any;
  noteTypesObj: {};
  targetDateErrorOccured: boolean = false;
  targetDateErrorMessage: string = "";
  displayDocs: boolean = false;
  displayNotes: boolean = false;
  dealNumber: any;
  actuaries: any;
  modelers: any;
  expirationDate: any;
  inceptionDate: any; 
  submissionDate: any;
  shouldDisableSubmit: boolean = true;
  disablebasedOnEditPermission: boolean = false;
  disableDropdown: boolean = false;
  isBoundDeal: boolean = false;
  canEditDealPermission: boolean = false;
  isDealSubmit = false;
  currentDeal: EntityApiData<Deal>;
  formValue:any;
  dealNameHeader: string;
  position: any;
  contractNumber: number; 
  public numbermask = [/\d/, /\d/, /\d/, /\d/, /\d/, /\d/, /\d/];
  @ViewChild('saveBtn') saveBtn: ElementRef;
  displayCheckList: boolean = false;
  onConCheckListPin: boolean = false;
  statusName: any;
  statusCode: any;
  statusreferncearray = [];
  primaryUnderwriterCode :any;
  primaryUnderwriterName: any;
  secondaryUnderwriterCode :any;
  secondaryUnderwriterName: any;
  technicalAssistantCode: any;
  technicalAssistantName: any;
  actuaryCode: any;
  actuaryName: any;
  modellerCode: any;
  modellerName: any;
  underwritersprimaryref = [];
  underwriterssecondryref =[];
  underwriterssecondary = [];
  tasreference = [];
  actuariesRef  = [];
  modelersRef = [];
  readonly loginUser$: Observable<LoginUser>;
  isDealDetailVisible: boolean;
  prventFromAutoClose: boolean;
  childComponentFlag: any = false;
  constructor(
    private _eref: ElementRef,
      public dialog: MatDialog,
    private _store: Store<fromRoot.AppState>,
    public _sharedEventService: SharedEventService,
    private _coreService: CoreService,
    @Inject(DOCUMENT) document
  ) {
    this.loginUser$ = this._store.select<LoginUser>(fromRoot.getAuthenticatedUser);
    this.loginUser$.subscribe(val=>{
      this.BASE_ERMS_DEAL_EDIT_API = val.LINK_ERMS_DEALEDIT_API;
    });
  }
  @Output() isSaveClicked =new EventEmitter();
  @Output() refreshGrid =new EventEmitter();
  @Output() isCancelClicked = new EventEmitter();
  @Output() submitDealDetail = new EventEmitter();
  @Output() isDealChecklist = new EventEmitter();
  @Input() set dealGridRowData(value: EntityApiData<Deal>) {
    this.isDealSubmit = false;
    this.formValue=value;
    if (this.dealDetails) {
      this.dealNameHeader = value.data.dealName;
      this.dealNumber = value.data.dealNumber; 
      this.contractNumber = value.data.contractNumber; 
      this.statusName = value.data.status;
      this.statusCode = value.data.statusCode
      this.primaryUnderwriterCode = value.data.primaryUnderwriterCode;
      this.primaryUnderwriterName = value.data.primaryUnderwriterName;
      this.secondaryUnderwriterCode = value.data.secondaryUnderwriterCode;
      this.secondaryUnderwriterName = value.data.secondaryUnderwriterName;
      this.technicalAssistantCode = value.data.technicalAssistantCode;
      this.technicalAssistantName = value.data.technicalAssistantName;
      this.actuaryCode = value.data.actuaryCode;
      this.actuaryName = value.data.actuaryName;
      this.modellerCode = value.data.modellerCode;
      this.modellerName = value.data.modellerName;
      //if (!this.canEditDealByRole)
      this.isBoundDeal = value.data.status === "Bound" ? true : false;
      if (value.data.inceptionDate && value.data.inceptionDate.length > 0)
        value.data.inceptionDate = moment(
          value.data.inceptionDate, "MM/DD/YYYY").format("MM/DD/YYYY");
      else value.data.inceptionDate = "";
 
      if (value.data.targetDate && value.data.targetDate.length > 0)
        value.data.targetDate = moment(
          value.data.targetDate, 'MM/DD/YYYY');
      else value.data.targetDate = "";

      if (value.data.submittedDate && value.data.submittedDate.length >0)
      value.data.submittedDate = moment(
        value.data.submittedDate, "MM/DD/YYYY").format("MM/DD/YYYY");
      else value.data.submittedDate = "";

      if (value.data.expiryDate && value.data.expiryDate.length > 0)
        value.data.expiryDate = moment(
          value.data.expiryDate, "MM/DD/YYYY").format("MM/DD/YYYY");
      else value.data.expiryDate = "";
      value.data.statusCode = String(value.data.statusCode);

      value.data.primaryUnderwriterCode = String(
        value.data.primaryUnderwriterCode
      );
      value.data.secondaryUnderwriterCode = String(
        value.data.secondaryUnderwriterCode
      );
      value.data.technicalAssistantCode = String(
        value.data.technicalAssistantCode
      );
      this.showCancelPopup = false;
      value.data.actuaryCode = String(value.data.actuaryCode);
      value.data.modellerCode = String(value.data.modellerCode);
      this.currentDeal = value;
      this.dealDetails.patchValue(value.data);
       this.evaluateSubmitState();
      this.dealNumber = value.data.dealNumber;
      this.displayNotes = false;
      this.displayDocs = false;
      this.targetDateErrorOccured = false;
      this.targetDateErrorMessage = "";

      this.statusnames = [];
      let valueFound = false;
      this.statusreferncearray.forEach(element => {
          if((element.isActive) || ( element.value == this.statusCode)){
            this.statusnames.push(element);  //status non GRS value validation
          }
          if( element.value == this.statusCode){
            valueFound = true;
          }
      });
      if(!valueFound && this.statusName != null && this.statusCode !='null'){
        let obj = {
          "name": this.statusName,
          "value": String(this.statusCode)
        }
        this.statusnames.push(obj);
      }

      this.underwriters = [];
      let primaryValaueFound = false;
      this.underwritersprimaryref.forEach(element => {
          if((element.isActive) || (element.value == this.primaryUnderwriterCode)){
            this.underwriters.push(element);  //primaryUnderwriter non GRS value validation
           console.log("underwriter:",this.underwriters);

          }
          if(element.value == this.primaryUnderwriterCode){
            primaryValaueFound = true;
          }
      });
      if(!primaryValaueFound && this.primaryUnderwriterName != null && this.primaryUnderwriterCode != 'null'){
        let obj = {
          "name": this.primaryUnderwriterName,
          "value": String(this.primaryUnderwriterCode)
        }
        this.underwriters.push(obj);
      }

      this.underwriterssecondary = [];
      let secondaryValaueFound = false;
      this.underwriterssecondryref.forEach(element => {
          if((element.isActive) || (element.value == this.secondaryUnderwriterCode)){
            this.underwriterssecondary.push(element);  //secondaryUnderwriter non GRS value validation
          }
          if(element.value == this.secondaryUnderwriterCode){
            secondaryValaueFound = true;
          }
      });
      if(!secondaryValaueFound && this.secondaryUnderwriterName != null && this.secondaryUnderwriterCode != 'null'){
        let obj = {
          "name": this.secondaryUnderwriterName,
          "value": String(this.secondaryUnderwriterCode)
        }
        this.underwriterssecondary.push(obj);
      }
      this.tas = [];
      let tasValaueFound = false;
      this.tasreference.forEach(element => {
          if((element.isActive) || (element.value == this.technicalAssistantCode)){
            this.tas.push(element);  //UA/TA non GRS value validation
          }
          if(element.value == this.technicalAssistantCode){
            tasValaueFound = true;
          }
      });
      if(!tasValaueFound && this.technicalAssistantName != null && this.technicalAssistantCode != 'null'){
        let obj = {
          "name": this.technicalAssistantName,
          "value": String(this.technicalAssistantCode)
        }
        this.tas.push(obj);  
      }
      this.actuaries = [];
      let actuariesValaueFound = false;
      this.actuariesRef.forEach(element => {
          if((element.isActive) || (element.value == this.actuaryCode)){
            this.actuaries.push(element);  //ACTUARIES non GRS value validation
          }
          if(element.value == this.actuaryCode){
            actuariesValaueFound = true;
          }
      });
      if(!actuariesValaueFound && this.actuaryName !=null && this.actuaryCode != 'null'){
        let obj = {
          "name": this.actuaryName,
          "value": String(this.actuaryCode)
        }
        this.actuaries.push(obj); 

      }
      this.modelers = [];
      let modelersValaueFound = false;
      this.modelersRef.forEach(element => {
          if((element.isActive) || (element.value == this.modellerCode)){
            this.modelers.push(element);  //Modelers non GRS value validation
            console.log("modelras:",this.modelers);
          }
          if(element.value == this.modellerCode){
            modelersValaueFound = true;
          }
      });
      if(!modelersValaueFound && this.modellerName != null && this.modellerCode != 'null'){
        let obj = {
          "name": this.modellerName,
          "value": String(this.modellerCode)
        }
        this.modelers.push(obj);  
      }
    }
  }
  @Input() set lookupListValue(value: any) {
    if (value[0]) {
      //TODO
      //STATUS
      this.statusreferncearray = value[0].results;
      //UNDERWRITER
    }
    if (value[1]) {
      this.underwritersprimaryref = value[1].results;
      this.underwriterssecondryref = value[1].results;

    }
    if (value[2]) {
      //TA's
      this.tasreference = value[2].results;
    }
    if (value[3]) {
      //aCTUARIES
      this.actuariesRef = value[3].results;
    }
    if (value[4]) {
      //MODELERES
      this.modelersRef = value[4].results;
    }
  }
  @Input() set canEditDealByRole(value: boolean) {
    this.canEditDealPermission = value;
    this.evaluateSubmitState();
  }
  @Input() set isDealDetailCompVisible(value: boolean){
    this.isDealDetailVisible = value;
  }

  ngOnInit() {
    this.popOverMsg = `You will lose your unsaved changes. Are you sure?`;
    this.targetDateErrorOccured = false;
    this.targetDateErrorMessage = "";
    this.createDealDetailsForm();
    //this.dealDetails.get("targetDate").disable();
    this.loginUser$.subscribe(val => {
      this.userDetails = val;
    });
    this._sharedEventService.getEvent()
      .subscribe(event => {
        switch (event.eventType) {
          case GlobalEventType.Restrict_from_Closing: {
            setTimeout(() => {
              //console.log('Restrict_from_Closing', event.data);
              this.childComponentFlag = event.data.data;
            }, 10);
            break;
          }
        }
      });
  }

 evaluateSubmitState() {
  let canEdit = this.canEditDealPermission && !this.isBoundDeal;
  this.disablebasedOnEditPermission =  canEdit? true : false;
  this.disableDropdown  = this.disablebasedOnEditPermission;
  if(this.dealDetails && !this.dealDetails.dirty)
  {
    this.shouldDisableSubmit= true;
  }
  else
  {
    this.shouldDisableSubmit= false;
  }

}

getPosition(){
  this.position = {top:66, right:262, from: 'DealDetails'}
}

// getPosition(event){ 
//   console.log(event)  
//  // let doc= this.elRef.nativeElement.querySelector('.cancel_btn').offsetWidth

//   let positionRight =event.clientX;
//   let positionBottom = event.clientY;
//   this.positionLeftbottom = {positionRight,positionBottom}
// }
  createDealDetailsForm() {
    this.dealDetails = new FormGroup({
      dealName: new FormControl({ value: [""], maxlength: 100}),
      contractNumber: new FormControl({value: [""],disabled: true}),
      statusCode: new FormControl({disabled: false}),
      inceptionDate: new FormControl({value: "", disabled: true}),
      expiryDate: new FormControl({value: "", disabled: true}),
      dealNumber: new FormControl({value: "", disabled: true}),
      targetDate: new FormControl({value: "",disabled: false}),
      submittedDate: new FormControl({value: "",disabled: true }),
      priority: new FormControl( {value: [""], maxlength: 7}),
      primaryUnderwriterCode: new FormControl({disabled: false}),
      secondaryUnderwriterCode: new FormControl({disabled: false}),
      technicalAssistantCode: new FormControl({disabled: false}),
      actuaryCode: new FormControl({disabled: false}),
      modellerCode: new FormControl({disabled: false})
    });
  }
  onCancelClick() {
    this.showCancelPopup = true;
   }
 updateFormChanged() {
  this.formChanged=true;
  this.prventFromAutoClose = true;
  if(this.dealDetails.dirty)
  {
    this.referenceFun(false);
  }
  else{
    this.shouldDisableSubmit=true;
  }
}
  onTargetChange($event){
    this.prventFromAutoClose = true;
    if(this.dealDetails.value.targetDate !=null){
      let checkForBlank = this.dealDetails.value.targetDate.format('MM/DD/YYYY');
      if(checkForBlank == '01/01/0001' && this.formValue.data.targetDate == ''){
        this.shouldDisableSubmit=true;
      }else if(moment(this.formValue.data.targetDate).format('MM/DD/YYYY') == checkForBlank){
        this.shouldDisableSubmit=true;
      }else{
        this.shouldDisableSubmit=false;
      }
      this.targetDateErrorOccured = false;
      this.targetDateErrorMessage ="";
    }
    else{
      this.targetDateErrorOccured = true;
      this.targetDateErrorMessage ="Invalid date";
      this.shouldDisableSubmit=true;
    }
  }

  openDialog(event): void {

    if(this.formValue.data.priority == null)
    {
      this.formValue.data.priority="";
    }
    if(this.dealDetails.value.priority == null){
      this.dealDetails.value.priority = "";
    }
    if(this.dealDetails.value.primaryUnderwriterCode == null)
    {
      this.dealDetails.value.primaryUnderwriterCode="null";
    }
    if(this.dealDetails.value.secondaryUnderwriterCode == null)
    {
      this.dealDetails.value.secondaryUnderwriterCode="null";
    }
    if(this.dealDetails.value.technicalAssistantCode == null)
    {
      this.dealDetails.value.technicalAssistantCode="null";
    }
    if(this.dealDetails.value.actuaryCode == null)
    {
      this.dealDetails.value.actuaryCode="null";
    }
    if(this.dealDetails.value.modellerCode == null)
    {
      this.dealDetails.value.modellerCode="null";
    }
    if (this.dealDetails.value.dealName != this.formValue.data.dealName
      || this.dealDetails.value.statusCode != this.formValue.data.statusCode
      ||this.dealDetails.value.priority != this.formValue.data.priority
      || this.dealDetails.value.primaryUnderwriterCode != this.formValue.data.primaryUnderwriterCode
      ||   moment(this.dealDetails.value.targetDate, 'MM/DD/YYYY').format('MM-DD-YYYY') !=
      moment(this.formValue.data.targetDate, 'MM/DD/YYYY').format('MM-DD-YYYY')
      || this.dealDetails.value.secondaryUnderwriterCode != this.formValue.data.secondaryUnderwriterCode
      || this.dealDetails.value.technicalAssistantCode != this.formValue.data.technicalAssistantCode
      || this.dealDetails.value.actuaryCode != this.formValue.data.actuaryCode
      || this.dealDetails.value.modellerCode != this.formValue.data.modellerCode
    ) {
      console.log(this.dealDetails)
      console.log(this.formValue)
       this.referenceFun(true);
     
    }
    else{
      this.dealDetails.markAsPristine();
      this.isCancelClicked.emit({isCancelled:false,dealClosed: this.dealNumber});
    }
    
    // this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Retain_Selection, 'Deal', 'Retain deal selection', this.dealNumber));
 }
 cancelButton($event){
  this.isCancelClicked.emit(false); 
this.showCancelPopup = false;
this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Row_Selection, 'Deal', 'Retain deal row selection', true));
this.dealDetails.reset();
this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Retain_Focus, 'Deal', 'Retain deal selection', this.dealNumber));
}

  closeCancelPopUp() {
    this.prventFromAutoClose = true;
    this.dialog.closeAll();
    this.showCancelPopup = false;
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Retain_Selection, 'Deal', 'Retain deal selection', this.dealNumber));
  }
  
  onSaveClose(){
    this.isSaveClicked.emit(false);
  }
  
  onFormSubmit() {
    this.shouldDisableSubmit = true;
    if (this.dealDetails.valid) {
      this.isDealSubmit = true;
      console.log(this.dealDetails.value, "this.dealDetails.value;");
      const formDeal = this.dealDetails.value;
      const properties = Object.getOwnPropertyNames(formDeal);
      properties.map(propName => {
        if (formDeal[propName] == null) this.currentDeal.data[propName] = null;
        if (
          formDeal[propName] &&
          this.currentDeal.data[propName] !== undefined
        ) {
          this.currentDeal.data[propName] = formDeal[propName];
          if (propName == "targetDate" && this.currentDeal.data[propName] != '') {
            let targetDateTempVal = formDeal[propName].format('MM/DD/YYYY');
            this.currentDeal.data[propName] = (targetDateTempVal == '01/01/0001')? '': targetDateTempVal;
              //.toLocaleDateString()
              //.replace(/[^ -~]/g, "");
          }
        }
      });

      /**
       * for error masg related implementation
       */
      this._coreService.invokeUpdateEntityApi(this.currentDeal, '/v1/deals/' + this.currentDeal.data['dealNumber']).subscribe(val => {
        if(val.messages && val.messages.length > 0){
          if(val.messages.find(item => item.severity == GlobalNotificationMessageType.ERROR_TYPE || item.severity == GlobalNotificationMessageType.FATAL_TYPE )){
            this.showCancelPopup = false;
          }else{
            this.dealDetails.reset();
            this.showCancelPopup = false;
            this.onSaveClose(); 
            this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Deal_saved,
              EntityType.Deals,
              '',this.currentDeal));
          }
          this._store.dispatch(new AddUserNotificationAction(val.messages));
        }else{
          this.shouldDisableSubmit = false;
          // need clarification when api giving other error
        }
      });
      //this.submitDealDetail.emit(this.currentDeal);
  }
  else{
      this.shouldDisableSubmit = true;
    }
  }
  onPriorityChange()
  {
    if(this.formValue.data.priority == null)
    {
      this.formValue.data.priority="";
    }
  if(this.formValue.data.priority != this.dealDetails.value.priority)
  {
    this.shouldDisableSubmit=false;
  }
  else
  {
    this.shouldDisableSubmit=true;
  }
  }
  dealNameChange($event){
    if($event.target.value.trim().length >0){
      if(this.formValue.data.dealName !== $event.target.value)
      {
        this.shouldDisableSubmit=false;
      }
      else
      {
        this.shouldDisableSubmit=true;
      }
    }
    else
    {
      this.shouldDisableSubmit=true;
    }
  
  }
  validatenumeric(evt) {
    const charCode = evt.which;
    if (charCode >= 32 && (charCode < 48 || charCode > 57)) {
      evt.preventDefault();
    }
  }
 onNotesClick($event) {
  this.displayDocs = false;
  this.displayCheckList = false;
  this.displayNotes = !this.displayNotes;
}
onDocsClick($event) {
  this.displayNotes = false;
  this.displayCheckList = false;
  this.displayDocs = !this.displayDocs;
}
  openEditErms(receiveDealNumber){
    console.log(receiveDealNumber, "receiveDealNumber");
    window.open(this.BASE_ERMS_DEAL_EDIT_API+receiveDealNumber, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
referenceFun(showPopUp){
  if(this.formValue.data.priority == null)
    {
      this.formValue.data.priority="";
    }
    if (this.dealDetails.value.dealName !== this.formValue.data.dealName
        || this.dealDetails.value.statusCode !== this.formValue.data.statusCode
        || this.formValue.data.priority != this.dealDetails.value.priority
        || this.dealDetails.value.primaryUnderwriterCode !== this.formValue.data.primaryUnderwriterCode
        || this.dealDetails.value.targetDate !== this.formValue.data.targetDate
        || this.dealDetails.value.secondaryUnderwriterCode !== this.formValue.data.secondaryUnderwriterCode
        || this.dealDetails.value.technicalAssistantCode !== this.formValue.data.technicalAssistantCode
        || this.dealDetails.value.actuaryCode !== this.formValue.data.actuaryCode
        || this.dealDetails.value.modellerCode !== this.formValue.data.modellerCode
    ) {
      if(showPopUp==true)
      {
        //showing pop up
        this.showCancelPopup = true;
        this.getPosition()

      }
      else
      {
        //disabling save button 
        this.shouldDisableSubmit=false;
      }
    } else {
      if(showPopUp==true)
      {
        this.showCancelPopup = false;
        this.isCancelClicked.emit(false);
      }
      else
      {
        this.shouldDisableSubmit=true;
      }
    }
}
 onOutsideClick($event){
  if (!this._eref.nativeElement.contains($event.target)){
    //console.log('onOutsideClick quick deal out side', $event.target, this._eref.nativeElement);
    // For Ag Grid
    let agGridElement = document.getElementById('grdList_Deals') as HTMLElement;
    let agGridElemFlag = agGridElement.contains($event.target);
    let agGridMenuElement = document.querySelectorAll('.ag-menu')[0] as HTMLElement;
    let agGridMenuFlag;
    if(agGridMenuElement){
      agGridMenuFlag = agGridMenuElement.contains($event.target);
    }else{
      agGridMenuFlag = agGridElemFlag? true :  false;
    }
    let childElem  = ($event.target as Element).className;
    if(childElem.match('ag-icon') || childElem.match('ag-menu') || childElem.match('ag-tab')){
      agGridMenuFlag = true;
    }
    //console.log('agGridMenuElement', agGridMenuElement, agGridMenuFlag);
    let agGridFlag = agGridElemFlag || agGridMenuFlag;
    //console.log('agGridFlag', agGridFlag);
    // for material select cdk-overlay-backdrop
    let materialPopup = document.querySelectorAll('.cdk-overlay-container')[0] as HTMLElement;
    let materialPopupFlag;
    if(materialPopup){
      materialPopupFlag = materialPopup.contains($event.target);
    }
    if(childElem.match('mat-calendar-body-cell') || childElem.match('mat-option-text')){
      materialPopupFlag = true;
    }

    //check for user notification panel 
    let userNotificationPanel = document.querySelectorAll('.user-notification-panel')[0] as HTMLElement;
    let userNotificationPanelFlag = userNotificationPanel.contains($event.target);
    if(childElem.match('user-notification-panel-close-btn')){
      userNotificationPanelFlag = true;
    }
    //console.log('materialPopupFlag', materialPopupFlag);    
    //console.log('childComponentFlag', this.childComponentFlag);
    if(this.isDealDetailVisible && !this.prventFromAutoClose && !agGridFlag && !materialPopupFlag && !this.childComponentFlag && !userNotificationPanelFlag){
      this.openDialog($event);
    }
  }
  this.prventFromAutoClose = false;
} 

onDealCheckList($event){
  if ($event) {
    $event.preventDefault();
  }
  this.displayCheckList = !this.displayCheckList; //toogle checklist

  this.onConCheckListPin = true; // pin functionality
  this.displayNotes = false;
  this.displayDocs = false;
  if(this.displayCheckList){   
  this._store.dispatch(
    new fromDealCheckList.LoadDealChecklist(DEAL_API_URL+ '/'+this.dealNumber+'/checklists')
  );
  }
}


  closeCheckList(value) {
    
    if (this.onConCheckListPin) {
      this.onConCheckListPin = false;
      return false;
    }

    if (value.target) {
      this.displayCheckList = true;

    } else if (!value.target && value.pin) {
      this.displayCheckList = true;
    }else if( value.target === false && value.pinValue =="opened"){
      this.displayCheckList = true;
  } else {
      this.displayCheckList = false;

    }
    value.pinValue ='';
    this.isDealChecklist.emit(this.displayCheckList)
  }
  openDeclineDialog() {
    let currentDeclineInfo =
    {
      reason:"",
      comments: "",
      authorName: this.userDetails.displayName,
      date: moment().format("MM/DD/YYYY")
    };
  
  this.openDialogHandler(currentDeclineInfo, true, "Decline", "Decline Submission");

  }
  openDialogHandler(currentDeclineInfo, isNoteEditable, modalType, modalTitle) {
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Restrict_from_Closing, 'Quick Deal', 'Retain Quick Deal', {data: true}));
    const copmRef = this;
    const dialogRef = this.dialog.open(DealDeclineComponent, {
      minHeight: "435px",
      width: "665px",
      disableClose: true,
      autoFocus: false,
      data: {
        modalType: modalType,
        modalTitle: modalTitle,
        isNoteEditable: isNoteEditable,
         closeQuickEdit: copmRef.isCancelClicked,
         refreshGrid: copmRef.refreshGrid,
         dealInfo: copmRef.currentDeal.data,
        declineInfo: currentDeclineInfo
      }
    });
    dialogRef.afterClosed().subscribe(result => {
      this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Restrict_from_Closing, 'Quick Deal', 'Retain Quick Deal', {data: false}));
      console.log("The dialog was closed", result);
    });
  }
}
