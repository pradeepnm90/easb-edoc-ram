import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
//import { DealsService } from '../../deals.service';
import { DealStatus } from '../../../../models/deal-status';
//import {DEAL_HOLD,DEAL_BOUND} from '../../../../app.config';
import * as _ from "lodash";
@Component({
  selector: 'deal-substatus-type',
  template: `
  <div [ngClass]="(dealsSubStatusList && dealsSubStatusList.length>0)? 'sub-status-shown' : 'sub-status-hide'">
    <mat-card class="checkbox-panel-sec">    
    <mat-card class="checkbox-panel">  
    <mat-card-content *ngFor="let _inprogresssubtype of dealsSubStatusList;let i = index">      
          <mat-checkbox id="dashboard_dashBoardComponent_dealStatusSubType_matChkSubType_{{i}}"
                         [checked]="_inprogresssubtype.isSelected" 
                        [disabled]="_inprogresssubtype.count<=0"   
                        (change)="onSubStatusClick(_inprogresssubtype,$event.checked)"  >
                {{_inprogresssubtype.statusName}}
          </mat-checkbox>   
          <span  id="dashboard_dashBoardComponent_dealStatusSubType_subType_span_{{i}}"
            disabled="_inprogresssubtype.count<=0">
           {{_inprogresssubtype.count}}
          </span>       
      </mat-card-content>
      </mat-card> 
    </mat-card> 
    </div>`,
    styleUrls: ['./deal-substatus.component.scss']
})
export class DealSubStatusComponent implements OnInit {
  dealsSubStatusList: DealStatus[];
  @Output() apiurlEmitEvent = new EventEmitter();
  @Input() set dealStatusList(value: DealStatus[]) {
    this.dealsSubStatusList = value;
  }
  
  constructor() { }

  ngOnInit() { }
  onSubStatusClick(dealSubsSatus: DealStatus, isChecked) { 
  if(dealSubsSatus){
    dealSubsSatus.isSelected=isChecked;
      this.apiurlEmitEvent.emit({ dealSubsSatus: dealSubsSatus});
  }
  }
}
