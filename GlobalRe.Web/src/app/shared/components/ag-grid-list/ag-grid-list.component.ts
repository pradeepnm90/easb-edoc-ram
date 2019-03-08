import { Component, OnInit,Input,Output ,EventEmitter} from '@angular/core';
import {GridApi, GridOptions,FilterStage} from "ag-grid-community/main";
import {CustomDateComponent} from '../ag-date-filter/ag-date-filter.component';
import {CoreService} from '../../services/core.service';
import {GridFilterModel} from '../../models/grid-filter-model';
import "ag-grid-enterprise";
import * as _ from "lodash";
import {Observable} from "rxjs/Observable";
import { GlobalEventType } from '../../../app.config';
import { SharedEventService } from '../../services/shared-event.service';
import { AgGridActionComponentRenderer } from '../ag-grid-action/ag-grid-action.component';
import { customUserViewState } from '../../../store/userViewState.reducer';
import { Store } from '@ngrx/store';
import * as fromRoot from '../../../store/index';
import { LoadUserViewAgGridState, UpdateUserViewsStateMsg } from '../../../actions/deals/user-views.actions';
import { ApiHelper } from '../../utils/api-helper';
import { count } from 'rxjs/operators';
@Component({
  selector: 'ag-grid-list',
  template: `<ag-grid-angular [attr.id]="gridID" #agGridLocation  style="width: 125%; height: 550px; !important;"
                 class="ag-theme-material" [gridOptions]="gridOptions" [excelStyles]="excelStyles" [popupParent]="popupParent"
                 (firstDataRendered)="autoSizeAll()" [rowSelection]="rowSelection"
                              [rowData]="itemData$ | async" [frameworkComponents]="frameworkComponents" [sideBar]="sideBar"
                               (rowDoubleClicked)="onRowClicked($event)" (rowDataChanged)="onRowDataChanged($event)" [context]="context" 
                               (sortChanged)="agGridConfigStoreHandler($event)" (filterChanged)="agGridConfigStoreHandler($event)"
                               (columnVisible)="agGridConfigStoreHandler($event)" (columnPinned)="agGridConfigStoreHandler($event)"
                               (columnResized)="agGridConfigStoreHandler($event)" (columnMoved)="agGridConfigStoreHandler($event)"
                               (columnEverythingChanged)="agGridConfigStoreHandler($event)" [getContextMenuItems]="getContextMenuItems">
</ag-grid-angular>`,
  styles: [`
    :host {
      display: flex;
      flex-wrap: wrap;
      justify-content: center;
    } 
  `]
})
export class AgGridListComponent implements OnInit {
  isGridReady: boolean=false;
  itemData$:Observable<any[]>;
  _entityListApiUrl:string;
  excelStyles: any;
  _entityType:string;
context : any;
  gridOptions: any;
  gridApiTimer: any;
  columnDefs: any[] = [];
  gridID="grdList_";
  frameworkComponents;
  _entityListTotal;
  _apiDataSubscription;
  currentSelectedNodeId=undefined;
  sideBar: any;
  filterState: any;
  popupParent: any;
  private gridApi : any;
  private gridColumnApi: any;
  agGridConfigState$: Observable<customUserViewState>;
  rowSelection: string;
  @Input() set entityListApiUrl(value) {
    // this.isGridReady = false;
    this.isGridReady=false;
    if(value && value.value && value.value.length>0) {
      this._entityListApiUrl = value.value;
      
      this.getGridData();
    }
  }
  @Input() set filterModel(value:GridFilterModel[]) {
    // this.isGridReady = false;
    if(this.gridOptions.api&&value&&value.length>0) {
      const filterState = this.gridOptions.api.getFilterModel();
      console.log('filterModel changed in ag grid', filterState);
      value.map(filterModel=>
      {
        filterState[filterModel.columnId]=filterModel.filterModel;
      });
      this.setFilterModel(this.gridOptions.api,filterState);
    }
  }
  @Output()
  doubleClickSelected = new EventEmitter();

  @Output()  rowDoubleClicked = new EventEmitter();
  @Output()  totalRowCount = new EventEmitter();
  @Output() rowDealCount = new EventEmitter();
  @Input() set entityType(value)
  {
    this._entityType=value;
  }
  @Input() set updateCurrentNode(nodeData)
  {
    if(this.gridOptions.api&&nodeData&&this.currentSelectedNodeId>=0) {
      this.gridOptions.api.getRowNode(this.currentSelectedNodeId).data.data=nodeData.data;
      this.gridOptions.api.refreshView();
    }
  }
  @Input()  showHeaderToolPanel=true;
  @Input() rowSelectionMode='single';
  @Input() set refreshGrid(value:any)
  {
    if(value===true)
    {
       this.getGridData();
       this.refreshGrid=false;
    }
  }

  constructor(private _coreService: CoreService,private _store: Store<fromRoot.AppState>, public _sharedEventService: SharedEventService) {
   //this.isGridReady =false;
    const gridComponent = this;
    this.rowSelection = "single";
    const _gridOptions: GridOptions = {
      rowDeselection: true,
      enableColResize: true,
      getRowStyle: this.getRowStyleSelected,
      columnDefs: gridComponent._coreService.getEntityGridColumns(gridComponent._entityType),
      enableSorting: true,
      enableFilter: false,
      suppressHorizontalScroll:false,
      toolPanelSuppressRowGroups: true,
      toolPanelSuppressValues: true,
      toolPanelSuppressPivots: true,
      toolPanelSuppressPivotMode: true,
      showToolPanel: false,
      floatingFiltersHeight: 50,
      floatingFilter: true, 
      suppressMenuHide: true,
      suppressRowClickSelection: true,
      // suppressLoadingOverlay:true,
      suppressNoRowsOverlay:true,
      localeText: {noRowsToShow: 'No data retrieved'},
      onFilterChanged: function() {  
        gridComponent.gridOptions.api.hidePopupMenu();
        if (gridComponent.gridApi.getDisplayedRowCount()==0 && gridComponent.isGridReady == true)
          {
            gridComponent.gridOptions.suppressNoRowsOverlay=false;
            gridComponent.gridOptions.api.showNoRowsOverlay();
            
          }
          else
          {
            gridComponent.gridOptions.api.hideOverlay();
          }
      },
      onFilterModified: function() {
        if (gridComponent.gridApi.getDisplayedRowCount()==0 && gridComponent.isGridReady == true)
          {
            gridComponent.gridOptions.suppressNoRowsOverlay=false;
            gridComponent.gridOptions.api.showNoRowsOverlay();
            
          }
          else
          {
            gridComponent.gridOptions.api.hideOverlay();
          }
        },
        headerHeight:30,
	      onGridReady: (params) => {
          
        // gridComponent.isGridReady = false;
        gridComponent.gridApi = params.api;
        gridComponent.gridColumnApi = params.columnApi;
		
        if (gridComponent.gridOptions.api)
          gridComponent.gridOptions.api.setColumnDefs(gridComponent._coreService.getEntityGridColumns(gridComponent._entityType));
        this.frameworkComponents = { agDateInput: CustomDateComponent, AgGridActionComponentRenderer: AgGridActionComponentRenderer };
        this.context = {componentParent: this};
        gridComponent.getGridData();
      },
      onModelUpdated: (params) => {
        //console.log("onmodelupdate fired");
        // console.log(this.gridApi.getDisplayedRowCount());
        // if (gridComponent.gridApi.getDisplayedRowCount()==0)
        //   {
        //     gridComponent.gridOptions.api.showNoRowsOverlay();
            
        //   }
        //   else
        //   {
        //     gridComponent.gridOptions.api.hideOverlay();
        //     // gridComponent.isGridReady = false;
        //   }
      },
      onDisplayedColumnsChanged: (params) => {
        console.log(params);

        gridComponent.gridOptions.api.setFilterModel(null);
      },
      onFirstDataRendered:(params) => {
        // console.log("onFirstDataRendered fired");
        // gridComponent.isGridReady=false;
      }

    }
   
    this.excelStyles = [{
            id: 'isDate',
            dataType: 'dateTime',
            numberFormat: {
              format: 'mm/dd/yyyy;@'
            }
        }
    ],
    this.gridOptions = _gridOptions;

    this.sideBar = {
      toolPanels: [
        {
          id: "columns",
          labelDefault: "",
          labelKey: "columns",
          iconKey: "columns",
          toolPanel: "agColumnsToolPanel",
          toolPanelParams: {
            suppressRowGroups: true,
            suppressValues: true,
            suppressPivots: true,
            suppressPivotMode: true,
            
            
            suppressColumnExpandAll: true
          }
        }
      ]
    };
    this.popupParent = document.querySelector("body");
    this._sharedEventService.getEvent()
      .subscribe(event => {
        switch (event.eventType) {
          case GlobalEventType.Reset_AgGrid: {
            this.resetGridState();
            break;
          }
          case GlobalEventType.Ag_Grid_Clear_Selection: {
            this.clearAGGridSelection();
            break;
          }
          case GlobalEventType.Ag_Grid_Retain_Selection: {
            this.retainAGGridSelection(event.data);
            break;
          }
          case GlobalEventType.Ag_Grid_Retain_Focus: {
            this.retainAGGridFocus(event.data);
            break;
          }
        }
      });
      this.agGridConfigState$ = this._store.select(fromRoot.agGridState)
  }
  getContextMenuItems(params) {
    var result = [
       "copy",
      "copyWithHeaders",
      "paste",
      "separator",
      "csvExport"
     
    ];
    return result;
  }
  clearAGGridSelection(){
    if(this.gridOptions.api){
      this.gridOptions.api.deselectAll();
      this.gridOptions.api.clearFocusedCell();
    }
  }
  retainAGGridSelection(data){
    this.gridOptions.api.forEachNode( (node,i) => {
      let nodeIndex = i;
      if (data === node.data.data.dealNumber) {
          node.setSelected(true, true);
          this.gridOptions.api.ensureIndexVisible(nodeIndex);
      }
    });
  }
  retainAGGridFocus(data){
    this.gridOptions.api.forEachNode( (node,i) => {
      let nodeIndex = i;
      if (data === node.data.data.dealNumber) {
        this.gridOptions.api.ensureIndexVisible(nodeIndex);
      // this.gridOptions.api.deselectNode(node);
      node.setSelected(false);
      }
    });
  }
  resetGridState() {
    if(this.gridOptions.api){
      this.gridOptions.api.setSortModel(null);
      this.gridOptions.api.setFilterModel(null);
      // this.isGridReady=false;
    }
    
  }
  autoSizeAll() {
    var allColumnIds = [];
    this.gridColumnApi.getAllColumns().forEach(function(column) {
      allColumnIds.push(column.colId);
    });
    this.gridColumnApi.autoSizeColumns(allColumnIds);
  }
  setViewGridState(data){
    if(this.gridColumnApi){
      if(data.colState && data.colState.length > 0){
        this.gridColumnApi.setColumnState(data.colState);
      }else{
        this.gridColumnApi.resetColumnState();
      }
      
    }
    if(this.gridApi){
      this.gridApi.setSortModel(data.sortState);
      this.gridApi.setFilterModel(data.filterState);
    }
  }
  setFilterModel(gridApi:any,model:any) {
    gridApi.setFilterModel(model);
  }
  ngOnInit() {
    this.gridID="grdList_"+this._entityType;
    this.frameworkComponents = { agDateInput: CustomDateComponent,AgGridActionComponentRenderer: AgGridActionComponentRenderer  };
    // this.gridOptions.hideOverlay();
  }
  agGridConfigStoreHandler($event){
  let currentCount =  this.gridOptions.api.getModel().rootNode.childrenAfterFilter.length;
  this.rowDealCount.emit(currentCount)
    /* if(this.gridColumnApi && this.gridApi){
      let customViewConfigState = {
        userviewAgGridState: this.getAgGridObj(),
        inceptionFilterState: '',
        customViewLoadMsg: 'fromAgGrid'
      };
      this._store.dispatch(new LoadUserViewAgGridState(customViewConfigState));
    } */
  }
    getAgGridObj(){
     
      let colState = this.gridColumnApi.getColumnState();
      let sortState = this.gridApi.getSortModel();
      let filterState = this.gridApi.getFilterModel();

      // console.log("column state saved");
      // console.log('colState', JSON.stringify(colState));
      // console.log('sortState', JSON.stringify(sortState));
      // console.log('filterState', JSON.stringify(filterState));
      let stateObj: any = {
        colState: colState,
        sortState: sortState,
        filterState: filterState
      };
      return stateObj;
    }
 
  getGridData() {
    const gridComponent = this;
    if (this.gridApiTimer) {
      clearTimeout(gridComponent.gridApiTimer)
      this.gridApiTimer = setTimeout(function () {
        gridComponent.getDataFromService(gridComponent,gridComponent._entityListApiUrl);
      }, 1000);
    } else {
      gridComponent.getDataFromService(gridComponent,gridComponent._entityListApiUrl);
      gridComponent.gridApiTimer = setTimeout(function () {
      }, 1000);
    }
    console.log("number of rows:" && gridComponent._entityListTotal);
    
  }
  private getRowStyleSelected(params) {
    if (params.data && params.node.selected) {
      return {
        'color': '#9AA3A8'
      };
    } else if (params.data) {
      return {
        'color': 'gray'
      };
    }
    return null;
  }
  rowDataBlankFlag: boolean = false;
  getDataFromService(gridComponent:any,apiUrl) {
    gridComponent.currentSelectedNodeId=-1;
    if(gridComponent&&gridComponent.gridOptions&&gridComponent.gridOptions.api){
      gridComponent.filterState = gridComponent.gridOptions.api.getFilterModel();
      gridComponent.gridOptions.api.setRowData([]);
      this.rowDataBlankFlag = true;
    }else{
      gridComponent.filterState = null;
    } 
    if (gridComponent&&gridComponent.gridOptions&&apiUrl&&apiUrl.length>0) {
      
      if(gridComponent.gridOptions.api) {
        gridComponent.gridOptions.api.showLoadingOverlay();
      }
      gridComponent._apiDataSubscription = gridComponent._coreService.invokeGetListResultApi(apiUrl, "(get ag-grid data list)")
        .subscribe((response) => {
          gridComponent._entityListTotal = response.totalRecords;
          gridComponent.itemData$=Observable.of(response.results);
          gridComponent.totalRowCount.emit({totalCount:response.totalRecords});
          if (gridComponent.gridOptions.api) {
            gridComponent.gridOptions.api.hideOverlay();
            gridComponent.isGridReady=true;
          }
        }, (error) => {
          gridComponent._entityListTotal = 0;
          gridComponent.itemData$=Observable.of([]);
          gridComponent.totalRowCount.emit({totalCount:0});
          if (gridComponent.gridOptions.api) {
            gridComponent.gridOptions.api.hideOverlay();
            gridComponent.isGridReady=true;
          }
        });
    }
    
  }
  onRowDataChanged($event){
    /* if (this.filterState) {
      this.gridOptions.api.setFilterModel(this.filterState);
    } */
    if(!this.rowDataBlankFlag){
      this.agGridConfigState$.subscribe(val =>{
        console.log("Custom AG Grid State inside ag grid component", val);
        if(val.customViewLoadMsg == 'success'){
          this.setViewGridState(val.userviewAgGridState);
          let customViewConfigState = {
            userviewAgGridState: val.userviewAgGridState,
            inceptionFilterState: val.inceptionFilterState,
            customViewLoadMsg: 'fromAgGrid'
          };
          //this._store.dispatch(new UpdateUserViewsStateMsg('fromAgGrid'));
          setTimeout(() => {
            //this.gridOptions.api.setFilterModel(val.userviewAgGridState.filterState);
            this._store.dispatch(new UpdateUserViewsStateMsg('fromAgGrid'));
          }, 1000);
          
        }else{
          if (this.filterState) {
            console.log('onRowDataChanged else filterState', this.filterState);
            this.gridOptions.api.setFilterModel(this.filterState);
            //this.gridOptions.api.setFilterModel(null);
          }
        }
        
     }).unsubscribe();
    }else{
      if (this.filterState) {
        console.log('onRowDataChanged else filterState', this.filterState);
        //this.gridOptions.api.setFilterModel(this.filterState);
        this.gridOptions.api.setFilterModel(null);
      }
      this.rowDataBlankFlag = false;
    }
    this.clearAGGridSelection();
  }
  // onRowSelectionChanged(event)
  // {
  //   console.log("Selection changed ->>>"+event);
  // }
  // onRowDoubleClicked(data)
  // {
  //   console.log("Row Double clicked-->>" + data);
  //     this.doubleClickSelected.emit(data);
  // }
  onRowClicked(event)
  {
    this.currentSelectedNodeId=event.node.rowIndex;
    let coloneDeal=_.clone(event);
    coloneDeal.data= _.cloneDeep(event.data);
    event.node.setSelected(true);
    this.rowDoubleClicked.emit(coloneDeal);
  }

  // onModelUpdated(event) {
  //   console.log(this.gridApi.getDisplayedRowCount());
  //   if (this.gridApi.getDisplayedRowCount()==0)
  //   {
  //     this.gridOptions.api.showNoRowsOverlay();
      
  //   }
  //   else
  //   {
  //     this.gridOptions.api.hideOverlay();
  //     // gridComponent.isGridReady = false;
  //   }
  // }

  methodFromParent(cell) {
    console.log(cell + "!");
  }
}