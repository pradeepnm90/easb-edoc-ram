import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { DealStatus } from '../../../../models/deal-status';
import { EntityApiData } from '../../../../shared/models/entity-api-data';
import { Store } from '@ngrx/store';
import * as applicationStateActions from '../../../../actions/application-state.action';
import * as fromRoot from '../../../../store/index';
import { ApplicationState } from '../../../../shared/models/application-state';
import { Alert } from 'selenium-webdriver';


@Component({
  selector: 'deal-status',
  template: `
    <mat-card class="dashboard-panel" >  
    <mat-card class="panel_block">  
      <mat-card-content  *ngFor="let _deal of dealsList;let i = index" 
           dashboard_dashBoardComponent_matCard_ class="panel_loopblock">
        <mat-button-toggle  type="checkbox"
            id="dashboard_dashBoardComponent_matButtonToggle_{{i}}"            
            [disabled]="_deal.data.isDisabled" 
            [checked]="_deal.data.isSelected"           
            (click)="onClickDealStatus($event,_deal)">
              
            <span>{{ _deal.data.count }}</span>
            <span class="small-txt">{{_deal.data.statusName }}</span>        
                        

        </mat-button-toggle>
        <span class="down_arrow hide_sec"></span>
        </mat-card-content>
        </mat-card>
    </mat-card> `,

})
export class DealStatusComponent implements OnInit {
  dealsList: EntityApiData<DealStatus>[];

  @Input() set dealStatusList(value: EntityApiData<DealStatus>[]) {
    this.dealsList = value;
  }
  @Output() dealStatus = new EventEmitter();

  isCtrlKey: boolean = false;
  isMultiSelectionMode:boolean = false;
  applicationState$: Observable<ApplicationState>;

  constructor(private _store: Store<fromRoot.AppState>) {
    this.applicationState$ = _store.select(fromRoot.getApplicationState);
  }

  ngOnInit() {
    this.applicationState$.subscribe(state =>{
      console.log("Multi Selction Mode : " + state.multiSelectionMode);
      this.isMultiSelectionMode = state.multiSelectionMode;     
    }
    );
  }

  onClickDealStatus(event:any,_deal: EntityApiData<DealStatus>) {
    //let objComp = this;
    // console.log("onClickDealStatus: isMultiSelectionMode " + this.isMultiSelectionMode);
    if (_deal && _deal.data) {
      _deal.data.isSelected =!_deal.data.isSelected;
      _deal.data.isCtrlKey = this.isMultiSelectionMode;
      this.dealStatus.emit({ deal: _deal, dealslist: this.dealsList });
    }
  }
 
}
