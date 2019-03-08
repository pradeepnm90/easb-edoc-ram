import { Component,Input,Output, OnInit ,EventEmitter,AfterViewInit, ViewChild} from '@angular/core';
import { Observable} from 'rxjs/Observable';
import { EntityType,FilterType} from '../../../../app.config';
import * as moment from 'moment';
import { GridFilterModel } from '../../../../shared/models/grid-filter-model';
import {AgGridListComponent} from '../../../../shared/components/ag-grid-list/ag-grid-list.component';
@Component({
  selector: 'deal-list',
 template:`
 <div class="deal-list-container">
    <ag-grid-list [entityListApiUrl]="entityListApiUrl | async" [updateCurrentNode]="updatedDeal | async"
    [entityType]="entityType" [filterModel]="filterModel | async" [refreshGrid]="refreshGrid" 
    (rowDoubleClicked)="onRowClicked($event)" (rowDealCount)="rowDealCount($event)"></ag-grid-list>
    </div>
 `,
})
export class DealListComponent implements OnInit, AfterViewInit {
  entityListApiUrl;
  entityType: EntityType;
  refreshGrid = false;
  filterModel;
  updatedDeal;
  @Input() set filter(value: string) {
    const gridFilterModel = this.getGridFilterModel(value);
    this.filterModel = Observable.of([gridFilterModel]);
  }
  @Input() set updateCurrentDeal(nodeData)
  {
     {
       this.updatedDeal=Observable.of(nodeData);

    }
  }
  @Output()  rowDoubleClicked = new EventEmitter();
  @Output() DealCountRow = new EventEmitter()
  @Input() set apiURL(value) {
    if(value && value.value && value.value.length>0) {
      this.entityListApiUrl = Observable.of(value);
    }
  }
  @ViewChild(AgGridListComponent) AgGridListComponent;

  constructor() {
    this.entityType = EntityType.Deals;
  }

  ngOnInit() {
  }

  ngAfterViewInit(){
    console.log("Deal list",this.AgGridListComponent)
  }


  getGridFilterModel(filterType: string): GridFilterModel {
    let gridFilterModel: GridFilterModel;
    if (filterType ===FilterType.PastInception) {
      const fromDate = moment(new Date(Date.now())).format('YYYY-MM-DD');
      gridFilterModel = new GridFilterModel({columnId: 'data.inceptionDate_1', filterModel: {dateTo: null, dateFrom: fromDate, type: "lessThan", filterType: "date"}});
    }
    else if (filterType ===FilterType.Within30Days) {
      const fromDate = moment(new Date(Date.now())).format('YYYY-MM-DD');
      const toDate = moment(new Date(Date.now())).add(29, 'days').format('YYYY-MM-DD');
      gridFilterModel = new GridFilterModel({columnId: 'data.inceptionDate_1', filterModel: {dateTo: toDate, dateFrom: fromDate, type: "inRange", filterType: "date"}});
    }
    else if (filterType ===FilterType.Over30Days) {
      const fromDate = moment(new Date(Date.now())).add(29, 'days').format('YYYY-MM-DD');
      const toDate = moment(new Date(Date.now())).format('YYYY-MM-DD');
      gridFilterModel = new GridFilterModel({columnId: 'data.inceptionDate_1', filterModel: {dateTo: null, dateFrom: fromDate, type: "greaterThan", filterType: "date"}});
    }
    else
      gridFilterModel = new GridFilterModel({columnId: 'data.inceptionDate_1', filterModel: null});
    return gridFilterModel;
  }

  onRowClicked(event)
  {
    this.rowDoubleClicked.emit(event);
  }

  getngGridData(){
    return this.AgGridListComponent.getAgGridObj()
    }

    rowDealCount(count){
      this.DealCountRow.emit(count)
    }
}
