import {Component, ElementRef, Output, EventEmitter, ViewChild} from "@angular/core";
import {ICellRendererAngularComp} from "ag-grid-angular";

import { MatMenuTrigger } from "@angular/material";
import { Store } from "@ngrx/store";
import * as fromRoot from "../../../store/index";
import { Observable } from "rxjs";
import { LoginUser } from "../../models/login-user";

@Component({
    selector: 'ag-grid-action',
    template: `
    <button mat-icon-button [matMenuTriggerFor]="menu" 
    #menuTrigger="matMenuTrigger"
    (click)="openActionMenu($event)" (dblclick)="disableActionMenu($event)">
        <mat-icon>more_vert</mat-icon>
    </button>
    <mat-menu #menu="matMenu" [overlapTrigger]="false" yPosition="below" xPosition="before" (click)="disableSglclickEvent($event)" (dblclick)="disableSglclickEvent($event)">
    <button  *ngIf="showMenu" mat-menu-item (click) ="openEditErmsinWorkBench($event)"  (dblclick)="disableDblclickEvent($event)">
        <span>Open in ERMS</span>
    </button>
    </mat-menu>
    `
})
export class AgGridActionComponentRenderer implements ICellRendererAngularComp {
    @ViewChild(MatMenuTrigger) controlActionMenu: MatMenuTrigger;
    showMenu: boolean = false;
    @Output() unbindRowClick = new EventEmitter();
    actionParams: any;
    BASE_ERMS_DEAL_EDIT_API: string;
   
    readonly loginUser$: Observable<LoginUser>;
    constructor(private _eref: ElementRef, private _store: Store<fromRoot.AppState>){
        this.loginUser$ = this._store.select<LoginUser>(fromRoot.getAuthenticatedUser);
         this.loginUser$.subscribe(val=>{
         this.BASE_ERMS_DEAL_EDIT_API = val.LINK_ERMS_DEALEDIT_API;
    })
    }

    agInit(actionParams: any): void {
        this.actionParams = actionParams;
    }
    disableDblclickEvent($event){
        if($event){
            $event.preventDefault();
            $event.stopPropagation();
        }
        if(this.setTimeoutObj){
            clearTimeout(this.setTimeoutObj);
        }
        this.preventFlag = true;
    }
    disableSglclickEvent($event){
        if($event){
            $event.preventDefault();
            $event.stopPropagation();
        }  
    }
    openActionMenu($event) {
        this.showMenu = true;
        if($event){
            $event.preventDefault();
        }
        this.actionParams.context.componentParent.methodFromParent(`Deal number is: ${this.actionParams.data.data.dealNumber}`);
    }
    refresh(): boolean {
        return false;
    }
    setTimeoutObj: any;
    preventFlag: boolean = false;
    openEditErmsinWorkBench($event){
        if($event){
            $event.preventDefault();
            $event.stopPropagation();
        }
        this.setTimeoutObj = setTimeout(() => {
            if (!this.preventFlag) {
                this.controlActionMenu.closeMenu();
                window.open(this.BASE_ERMS_DEAL_EDIT_API+this.actionParams.data.data.dealNumber, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
            }
            this.preventFlag = false;
        }, 500);
      }
      disableActionMenu($event){
        if($event){
            $event.preventDefault();
        }
        this.controlActionMenu.closeMenu()
      }
}