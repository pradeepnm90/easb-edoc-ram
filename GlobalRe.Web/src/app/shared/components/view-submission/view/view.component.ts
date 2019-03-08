
import { Component, Inject, Output, EventEmitter, Input, OnInit, AfterViewInit, ElementRef, ViewChild } from "@angular/core";

import { Store } from "@ngrx/store";
import * as fromRoot from "../../../../store/index"

import { CoreService } from "../../../../shared/services/core.service";
import { SharedEventService } from "../../../../shared/services/shared-event.service";

import { USERViEW_API_URL, USERVIEW_DEFAULTVIEW_API_URL,USERVIEW_ADD_API_URL, USER_VIEW_SCREEN_NAME } from '../../../../app.config';
import * as fromUserView from '../../../../actions/deals/user-views.actions';
import { LoginUser } from "../../../models/login-user";

@Component({
    selector: "view",
    template: `
    <div class="main-view">
        <div>   
        <button
                    
        [disabled]="submissionValueDisable"
        [ngClass]="submissionValueDisable === true? 'addButtonview_Disable': 'addButtonview'"
        
        (click)="addFieldValue()">
        <span
            class="addCircle textSize18">
            <i class="fa fa-plus-circle" ></i>
        </span> 
        Add a view
    </button>

<div
    class="addButtonView__container"
    *ngIf="showInputView">
    <input matInput
        type="text"
        [maxLength] = "maxLength" 
        class="inputfield"
        [(ngModel)]="mySubmissionValue"
        (input)="checkDuplicateView($event.target.value)"
        #viewInputEle
        autofocus>
    <button 
        class="viewCheck"
        (click)="removeSubmission($event)">
            <i class="fa fa-times-circle "></i>
    </button>

    <button 
        [ngClass]="disableCheckView === true? 'disableView': 'viewCheck'"
        [disabled] = "disableCheckView"
        (click)="saveSubmissions($event)">
        <i class="fa fa-check-circle"></i>
    </button>
</div>
</div>

        <div>
        <ul class="listdeatil">        
        <ng-template ngFor let-item [ngForOf]="viewList" let-i="index" >
            <li *ngIf="i < 15" style="display:flex;align-items:center;justify-content:center"
            class="dropdownlinelistitem"
            >
            <a 
            matTooltip="{{item.data.viewname}}"
            [matTooltipPosition]="'above'" 
            class="menu-items"
            (click)="getUserState(item)">
            <span [style.color]="item.data.default === true? 'rgb(144,19,254)':'rgb(67,69,71)'">{{item.data.viewname}}
            </span> 
            </a>
            
            <span [ngClass]="item.data.customView === true?'onSubmission':'noSubmission'">
            <span [ngStyle]="{'visibility': item.data.customView? 'visible' : 'hidden'}" class="icon_style" style="padding-left:10px; padding-right:10px; cursor: pointer;" (click)="deleteUser(item)"><i class="fa fa-trash" ></i></span>
            <span class="icon_style marginL15">
                <i [ngClass]="item.data.default === true? 'fa fa-star':'fa fa-star-o'" style="color: #669509;"
                (click)="defaultView($event,item)" class="bookmark_icon"></i> </span>
            </span>
            </li>
        </ng-template>
       
    </ul>
        
        </div>
    </div>
`,
    styleUrls: ["view.component.scss"]
})

export class ViewComponent implements OnInit, AfterViewInit {
    showDropDownMenu: boolean = false;
    mySubmission: Array<any> = [];
    submissionValueDisable: boolean = false;
    addViewValue = [
        { value: "My Submission" },
        { value: "All Submission" },

    ];
    mySubmissionObj = {};
    mySubmissionValue;
    viewList: Array<any>;
    userviewscreenName = 'GRS.UW_Workbench';
    disableCheckView: boolean = true;
    showInputView: boolean;
    viewId: any;
    currentViewName: string;
    userDetails: any;
    isKeyMember: boolean; 
    maxLength: number = 50;// since db column size is 50
    @Output() addView = new EventEmitter();
    @Output() closeCurrentView = new EventEmitter();
    @Output() getUserViewState = new EventEmitter();
    @ViewChild('viewInputEle') viewInputEle: ElementRef;
    @Input() set currentView(value){
        this.currentViewName = value;
    }
    constructor(
        private _store: Store<fromRoot.AppState>,
        private _coreService: CoreService,
        private _sharedService: SharedEventService,
        private _eleMentRef: ElementRef
    ) { 
        
        this._store.select<LoginUser>(fromRoot.getAuthenticatedUser).subscribe(loginuser => {
            this.userDetails = loginuser;
        });
        this.isKeyMember = this.checkForKeyMember(this.userDetails.personId);
     }

    ngOnInit() {
        this._store.select(fromRoot.getUserViewList).subscribe(val => {
            this.viewList = val.map(viewName => {
                console.log("ViewName", viewName)
                return viewName;
            });
            console.log("Data", this.viewList);
            this.viewList.length >= 15 ? this.submissionValueDisable = true : this.submissionValueDisable = false; // disable addView if the array s greater than 15
            return this.viewList;
        })
    }
    checkForKeyMember(personId){
        let isKeyMember: boolean = false;
        this._store.select(fromRoot.getLookupList).subscribe(val => {
            if(val && val.length > 0){
                for(let i=1;i<5;i++){
                    if(val[i].results.find(item => item.value == personId)){
                        isKeyMember = true;
                    }
                }
            }
        }).unsubscribe();
        return isKeyMember;
    }
    dropdownMenu() {

        if (this.showDropDownMenu === false) {
            this.showDropDownMenu = true;
            this.mySubmission.pop();
            this.submissionValueDisable = false;
        }
        else {
            this.showDropDownMenu = false;
        }
    }

    addFieldValue() {
        this.mySubmissionValue = "";
        this.disableCheckView = true;
        this.showInputView = true;
        this.submissionValueDisable = true;
        setTimeout(() => {
           this.viewInputEle.nativeElement.focus();
        }, 200);

    }

    ngAfterViewInit()
    {
      this._eleMentRef.nativeElement.focus();
    }

    removeSubmission($event) {
        if ($event) {
            $event.preventDefault();
            $event.stopPropagation();
        }
        this.showInputView = false; //hide the input box
        this.submissionValueDisable = false;
        this.mySubmissionValue = "";
    }

    saveSubmissions($event) {
        if ($event) {
            $event.preventDefault();
            $event.stopPropagation();
        }
        this.submissionValueDisable = false;
        this.currentViewName =this.mySubmissionValue;
              this.addView.emit(this.mySubmissionValue)  // sending input to parent Deal container & binding in view submission button
        this.mySubmissionValue = "";
        this.showInputView = false;// close the input box on save or check

    }
//Check if value is empty or duplicate
    checkDuplicateView(InputValue) {
        console.log(InputValue.length)
        if (InputValue.trim().length > 0) {
            if (InputValue != " " || InputValue != undefined || InputValue != null) {
                this.disableCheckView = false;
                let InputValueToLow = InputValue.toLocaleUpperCase();
                this.viewList.find(x => {
                    let DuplicateView = x.data.viewname.toLocaleUpperCase();

                    if (DuplicateView === InputValueToLow) {
                        this.disableCheckView = true;
                        return true;
                    } else {
                        this.disableCheckView = false;
                        return false;
                    }
                })
            }
        } else {
            this.disableCheckView = true;
        }
    }
 
    prepareRequestDefaultObjd(item) {
        let screenName = "GRS.UW_Workbench";
        let requestObj = {
            data: {
                "viewId": item.data.viewId,
                "default": true,
                "layout": item.data.layout,
            } }
        return requestObj;
    }

    defaultView($event, item) {

        let requestObj = this.prepareRequestDefaultObjd(item) // forming request object for default selection
        let url = USERVIEW_DEFAULTVIEW_API_URL + item.data.viewId;
        this._coreService.invokeUpdateEntityApi(requestObj,url).subscribe(val => {
            if (val.data) {
                this._store.dispatch( // loading list User View,
                    new fromUserView.LoadUserViews(USERViEW_API_URL + this.userviewscreenName)
                );
            }
        }, err => {
            console.log(err)
        });
    }
    deleteUser(item) {
        this.viewId = item.data.viewId;
        console.log("items", item);
        let reqObj = {
            "screenName": USER_VIEW_SCREEN_NAME,
            "default": item.data.default,
            "keyMember": this.isKeyMember
        };
        this._store.dispatch( // loading list User View
            new fromUserView.DeleteUserViews({ url: USERVIEW_ADD_API_URL, id: this.viewId, data: reqObj })
        );

        if (this.currentViewName == item.data.viewname) {
            this.closeCurrentView.emit("Search");
        }
    }
	getUserState(getUserState){
        console.log("items",getUserState)
        this.getUserViewState.emit(getUserState)

    }
}




