
import { Component, ElementRef, OnInit, OnChanges, Input, ContentChild } from "@angular/core";
import { SharedEventService } from "../../../shared/services/shared-event.service";
import { Observable } from "rxjs";
import { ExtendedSearchComponent } from "../extended-search/extended-search.component";
import { GlobalEventType } from "../../../app.config";

@Component({
    selector: "view-submission",
    template: `  
            <div>
                <button
                class="mySubmission"
                (click)="dropdownMenu()"
                matTooltip="{{dropDownLabel}}"
                [matTooltipPosition]="'above'"
                >
                    <span class="btn-text">
                        {{dropDownLabel}}</span>
                <mat-icon class="arrow-drop-down">arrow_drop_down</mat-icon>
                </button>

                <div
                    class="submissio_dropdown_popover"
                    *ngIf="showDropDownMenu">
                    <ng-content></ng-content>
                </div>
            </div>`,
    styleUrls: ["view-submission.component.scss"],
    host: {
        '(document:click)': 'onOutsideClick($event)',
    }
})

export class ViewSubmissionComponent implements OnChanges, OnInit {
    ngOnInit(): void {
        this._sharedService.getEvent().subscribe(event => {
            switch (event.eventType) {
                case GlobalEventType.Close_View_Dropdown: {
                    this.showDropDownMenu = false;
                    this.dropDownLabel = this.closeViewDropDown;
                    break;
                }
            }
        });
    }
    showDropDownMenu: boolean = false;
    mySubmission: Array<any> = [];
    submissionValueDisable: boolean = false;

    dropDownLabel = "My Submissions";
    mySubmissionObj = {};
    @Input() closeViewDropDown;
    @ContentChild(ExtendedSearchComponent) extendedSearchComponent: ExtendedSearchComponent;
    constructor(private _eref: ElementRef,
        private _sharedService: SharedEventService) { }

    ngOnChanges() {
       
        if (this.closeViewDropDown != undefined)  {
            this.showDropDownMenu = false;
            this.dropDownLabel = this.closeViewDropDown;
        }
       
      
    }

    dropdownMenu() {
        if (this.showDropDownMenu === false) {
            this.showDropDownMenu = true;
            this.mySubmission.pop();
            this.submissionValueDisable = false;
        }
        else {
            this.showDropDownMenu = false;
            if(this.extendedSearchComponent){
                this.extendedSearchComponent.hideBtnPanel();
            }
        }
    }
    onOutsideClick($event){
        if (!this._eref.nativeElement.contains($event.target)){
            if(this.extendedSearchComponent){
                this.extendedSearchComponent.hideBtnPanel();
            }
            this.showDropDownMenu = false;
        }
    }


}