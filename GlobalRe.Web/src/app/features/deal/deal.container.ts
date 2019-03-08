import { Component, OnInit, NgZone, Input, ViewChild, AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs/Rx';

import { EntityApiData } from '../../shared/models/entity-api-data';
import { DealStatus } from '../../models/deal-status';
import * as _ from "lodash";
import { Store } from '@ngrx/store';
import * as collection from '../../actions/deals/deal-status.actions';
import * as dealdetailcollection from '../../actions/deals/deal-details.actions';
import * as fromRoot from '../../store/index';
import {DEALSTATUSSUMMARIES_API_URL, EntityType,GlobalEventType, EXTENDED_SEARCH_URL, 
        DEAL_API_URL, USER_VIEW_SCREEN_NAME, USERVIEW_ADD_API_URL, USERViEW_API_URL, FilterType, USER_VIEW_MY_SUBMISSIONS, USER_VIEW_ALL_SUBMISSIONS} from '../../app.config';
import { ApiUrl } from '../../shared/models/api-url';
import * as apiUrlCollection from "../../actions/apiurl.action";
import { Subdivisions } from "../../models/subdivisions";
import { LoginUser } from '../../shared/models/login-user';
import { MatSnackBar } from '@angular/material';
import { SharedEventService } from "../../shared/services/shared-event.service";
import * as moment from 'moment';
import { ExtendedSearchInnerState } from '../../store/extended-search.reducer';
import { LoadExtendedSearchData, ClearActiveSateExtendedSearchData, ClearActiveSaveSateExtendedSearchData } from '../../actions/deals/extended-search.action';
import { BroadcastEvent } from '../../shared/models/broadcast-event';
import * as fromUserView from '../../actions/deals/user-views.actions';
import {DealDetailsComponent} from '../../features/deal/components/deal-details/deal-details.component';
import {DealListComponent} from '../../features/deal/components/deal-list/deal-list.component';
import { customViewState, customUserViewState } from '../../store/userViewState.reducer';
import { ExtendedSearchComponent } from '../../shared/components/extended-search/extended-search.component';
import * as fromDealCheckList from '../../actions/deals/deal-checkList.actions';
moment.locale('en-us');
@Component({
  selector: 'deal-container',
  templateUrl: "./deal.container.html",
  styleUrls: ['./deal.container.scss']
})

export class DealContainer implements OnInit, AfterViewInit {
  apiUrl;
  filterSelected: string;
  filterSelectedForView: string;
  dealsBaseApi: string;
  dealStatusName: string;
  showSubDealstatus = false;
  resetTimeSelection = true;
  apiUrls$: Observable<ApiUrl[]>;
  dealStatusList$: Observable<EntityApiData<DealStatus>[]>;
  dealSubStatusList$: Observable<DealStatus[]>;
  showGridDetails = false;
  isEditMode = false;
  dealStatusCode: number;
  dealstatussummary: string;
  lastUpdated: any;
  dealGridRowData: EntityApiData<DealStatus>;
  lookupList$: Observable<any>;
  canEditDealPermission: boolean = false;
  readonly loginUser$: Observable<LoginUser>;
  extendedSearchMainData$: Observable<ExtendedSearchInnerState>;
  extendedSearchActiveState$: Observable<ExtendedSearchInnerState>;
  extendedSearchSaveState$: Observable<ExtendedSearchInnerState>;
  updatedDeal;
  extendedSearchActiveState: ExtendedSearchInnerState;
  closeViewDropDown: any;
  extendedSearchSaveState: ExtendedSearchInnerState;
  lblTextObj: any = {};
  lblTextArr: string [] = [];
  agGridConfigState$: Observable<customUserViewState>;
  userViewListMsg$: Observable<any>;
  dealSelectedTotalCount: number  = 0;
  totalDealcount: any ={};
  callCheckList: boolean =false;
  viewProfileSubmissionValue$: Observable<string>;
  @ViewChild(DealListComponent) DealListComponent;
  @ViewChild(DealDetailsComponent) DealDetailsComponent;
  @ViewChild('extendedSearchRef') extendedSearchRef: ExtendedSearchComponent;
  userDetails: any;
  constructor(public zone: NgZone, private _store: Store<fromRoot.AppState>, public _sharedEventService: SharedEventService, public snackBar: MatSnackBar) {

    this.dealStatusList$ = this._store.select(fromRoot.getDealStatus);
    this.apiUrls$ = this._store.select(fromRoot.getApiList);
    this.lookupList$ = this._store.select(fromRoot.getLookupList);
    this._store.select<LoginUser>(fromRoot.getAuthenticatedUser).subscribe(loginuser => {
      this.canEditDealPermission = loginuser.canEditDeal;
      this.userDetails = loginuser;
    });
    this.lastUpdated = new Date();
    this.extendedSearchMainData$ = this._store.select(fromRoot.getExtendedSearchMainData);
    this.extendedSearchActiveState$ = this._store.select(fromRoot.getExtendedSearchActiveState);
    this.extendedSearchSaveState$ = this._store.select(fromRoot.getExtendedSearchSaveState);
    this.agGridConfigState$ = this._store.select(fromRoot.agGridState);
    this.userViewListMsg$ = this._store.select(fromRoot.getUserViewListMsg)
  }

  ngOnInit() {
    const componentObj = this;
    
    this.userViewListMsg$.subscribe(viewListMsg => {
      if(viewListMsg == 'first_time_data'){
        this._store.select(fromRoot.getUserViewList).subscribe(val => {
          let defaultView = val.find(item => item.data.default);
          if(defaultView){
            this.getUserViewState(defaultView);
            this.dealCount()
          }
        }).unsubscribe();
      }
    });
    this.dealStatusList$.subscribe(dealsLIst => {
      if (dealsLIst && dealsLIst.length > 0)
        dealsLIst.map(deal => {
          /* if (this.dealStatusCode !== undefined && deal.data.statusCode !== undefined && this.dealStatusName !== undefined
            && this.dealStatusCode === deal.data.statusCode && this.dealStatusName === deal.data.statusName) {
            this.dealSubStatusList$ = Observable.of(deal.data.statusSummary);
          } */
          if (deal.data.statusSummary != null && deal.data.statusSummary.length > 0) {
            this.dealSubStatusList$ = Observable.of(deal.data.statusSummary);
          }
        });
        if(dealsLIst.find(deal => deal.data.isSelected && deal.data.count > 0)){
          this.showGridDetails = true;
        }else{
          this.showGridDetails = false;
        }
    });
    this._sharedEventService.getEvent()
      .subscribe(event => {
        switch (event.eventType) {
          case GlobalEventType.Deal_saved: {
            this.updatedDeal = event.data;
            this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Retain_Focus, 'Deal', 'Retain deal focus', event.data.data.dealNumber));
           // componentObj.resetApiUrls();
            break;
          }
        }
      });
      this.extendedSearchMainData$.subscribe(val => {
        console.log('extended search main data', val);
      });
      this.extendedSearchActiveState$.subscribe(val => {
        console.log('extended search active state data chaned');
        this.extendedSearchActiveState = val;
      });
      this.extendedSearchSaveState$.subscribe(val => {
        console.log('extended search save state data changed');
        this.extendedSearchSaveState = val;
      });

      this.agGridConfigState$.subscribe(val =>{
        console.log("Custom AG Grid State", val);
        if(val.customViewLoadMsg == 'success'){
          this.resetTimeSelection=false;
          this.dealStatusList$.subscribe(dealslist => {
            if (dealslist.find(deal => deal.data.isSelected)) {
              let inProgressCount = dealslist.find(deal => deal.data.isSelected && deal.data.statusSummary != null && deal.data.statusSummary.length > 0);
              if (inProgressCount) {
                this.showSubDealstatus = true;
              } else {
                this.showSubDealstatus = false;
              }
            }
            else {
              this.showSubDealstatus = false;
            }
          }
          ).unsubscribe();
          this.filterSelectedForView = val.inceptionFilterState;
        }
     });
   }
  ngAfterViewInit(){
    setTimeout(() => {
      this.DealDetailsComponent.dealDetails.reset();
    }, 1000);
this.DealListComponent // get the status of the child
}

  dispatchGetDealAction(dealsummaryurl: string) {
    this._store.dispatch(new collection.GetDealStatusAction(dealsummaryurl));
  }
  onFilterClick(event)
  {
    this.filterSelected=event.criteria;
    this.resetTimeSelection=false;
  }
  getSelectedCodes(itemList){
    let sList = [];
    itemList.map(item => {
      if(item.isSelected){
        sList.push(item.code);
      }
      return item;
    });
    return sList;
  }
  updateApiUrl(){
    let dealstatussummariesUrlParam = DEALSTATUSSUMMARIES_API_URL;
    let dealsApiUrlParam = DEAL_API_URL;
    let urlStr = '';
    let sdList = [], pl2List = [], egList = [], enList = [], kmnList = [];
    sdList = this.getSelectedCodes(this.extendedSearchSaveState && this.extendedSearchSaveState.subdivision || []);
    pl2List = this.getSelectedCodes(this.extendedSearchSaveState && this.extendedSearchSaveState.productLine2 || []);
    egList = this.getSelectedCodes(this.extendedSearchSaveState && this.extendedSearchSaveState.exposureGroup || []);
    enList = this.getSelectedCodes(this.extendedSearchSaveState && this.extendedSearchSaveState.exposureName || []);
    kmnList = this.getSelectedCodes(this.extendedSearchSaveState && this.extendedSearchSaveState.keyMemberName || []);
    if(sdList.length>0){
      urlStr = 'subdivisions=' + sdList.join(',');
    }
    if(pl2List.length > 0){
      let tempUrl = 'productlines=' + pl2List.join(',');
      urlStr = urlStr.length > 0? urlStr + '&' + tempUrl : tempUrl;
    }
    if(egList.length > 0){
      let tempUrl = 'exposureGroups=' + egList.join(',');
      urlStr = urlStr.length > 0? urlStr + '&' + tempUrl : tempUrl;
    }
    if(enList.length > 0){
      let tempUrl = 'exposureTypes=' + enList.join(',');
      urlStr = urlStr.length > 0? urlStr + '&' + tempUrl : tempUrl;
    }
    if(kmnList.length > 0){
      let tempUrl = 'personIds=' + kmnList.join(',');
      urlStr = urlStr.length > 0? urlStr + '&' + tempUrl : tempUrl;
    }
    if (urlStr && urlStr.length > 0) {
      dealstatussummariesUrlParam += '?' + urlStr;
      dealsApiUrlParam += '?' + urlStr;
    }
    this.dispatchUpdateApiURlAction([new ApiUrl({key: EntityType.DealStatusSummaries, url: dealstatussummariesUrlParam}), new ApiUrl({key: EntityType.Deals, url: dealsApiUrlParam})]);
  }

  dispatchUpdateApiURlAction(apiUrls: ApiUrl[]) {
    this._store.dispatch(new apiUrlCollection.UpdateApiUrlAction(apiUrls));
  }
  resetApiUrls() {
    this.resetLastUpdated();
    this.updateApiUrl();
    this.apiUrls$.subscribe(apiUrls => {
      if (apiUrls)
        apiUrls.map(apimodel => {
          console.log('Api Url:' + apimodel.apiUrl);
          if (apimodel.key === EntityType.DealStatusSummaries) {
            this.dealstatussummary = apimodel.apiUrl;
            this.dispatchGetDealAction(this.dealstatussummary);
          }
          else if (apimodel.key === EntityType.Deals) {
            this.dealsBaseApi = apimodel.apiUrl;
            this.dealStatusList$.subscribe(deals => {
              this.makeApiUrlObservable(this.updateUrl(deals));
              // this.resetTimeSelection=true;
            }
            ).unsubscribe();
          }
        });
    }
    ).unsubscribe();
  }
  onClickDealStatus(dealStatus: any) {
  if(this.DealDetailsComponent.dealDetails.dirty)
    {
      this.DealDetailsComponent.referenceFun(true);
    }
    else {
    this.isEditMode = false;
    this.resetLastUpdated();
    //this.resetTimeSelection = true;
    if(this.closeViewDropDown == USER_VIEW_MY_SUBMISSIONS){
      setTimeout(() => {
        this.apiUrlForMySubmissions();
      }, 500);
    }else{
      setTimeout(() => {
        this.resetApiUrls();
      }, 500);
    }
    if (dealStatus && dealStatus.deal) {
      //this.dispatchGetDealAction(this.dealstatussummary);
      this._store.dispatch(new collection.UpdateDealStatusAction(dealStatus.deal));
      this.dealStatusCode = dealStatus.deal.data.statusCode;
      this.dealStatusName = dealStatus.deal.data.statusName;
      this.dealCount()
      //this.dealSubStatusList$ = Observable.of(dealStatus.deal.data.statusSummary);
      //this.makeApiUrlObservable(this.updateUrl(dealStatus.dealslist));
      // this.resetTimeSelection=true;
      //this.showSubDealstatus = (dealStatus.deal.data.statusSummary != null && dealStatus.deal.data.statusSummary.length > 0);
      if (dealStatus.dealslist.find(deal => deal.data.isSelected)) {
        this.showGridDetails = true;
        let inProgressCount = dealStatus.dealslist.find(deal => deal.data.isSelected && deal.data.statusSummary != null && deal.data.statusSummary.length > 0);
        if (inProgressCount) {
          this.showSubDealstatus = true;
        } else {
          this.showSubDealstatus = false;
        }
      }
      else {
        this.showGridDetails = false;
        this.showSubDealstatus = false;
      }
    }}
  }

  //funtion counts total number of deals
  dealCount() {
    this.dealStatusList$.subscribe(val => {
      this.dealSelectedTotalCount = 0;
      val.filter((items) => {
        if (items.data.isSelected === true) {
          this.dealSelectedTotalCount += items.data.count;
          this.dealCountRow(this.dealSelectedTotalCount)
          return true;
        }
      });
    });
  }

  updateSubUrl($event: any) {
    let selectedDealsStatusList: DealStatus[];
    selectedDealsStatusList = _.filter($event, function (o) { return o.isSelected; });
    const selectedStatusCode = selectedDealsStatusList.map(deals => {
      return deals.statusCode;
    }).join(',');
    return selectedStatusCode;
  }

  updateUrl($event: EntityApiData<DealStatus>[]) {
    if ($event !== undefined) {
      let finalUrl = '';
      let selectedDealsStatusList: EntityApiData<DealStatus>[];
      selectedDealsStatusList = _.filter($event, function (o) {
        return o.data.isSelected;
      });

      const selectedStatusCode = selectedDealsStatusList.map(deals => {
        if (deals.data.statusSummary && deals.data.statusSummary.length > 0)
          return this.updateSubUrl(deals.data.statusSummary);
        else
          return deals.data.statusCode;
      }).join(',');

      const link = ($event.find(dl => dl.links && dl.links.length > 0))
        ? $event.find(dl => dl.links && dl.links.length > 0).links
        : null;

      if (selectedStatusCode && selectedStatusCode.length > 0) {
        if (link && link.length > 0) {
          this.showGridDetails = true;
          finalUrl = (this.dealsBaseApi && this.dealsBaseApi.indexOf('=') > 0 && link)
            ? this.dealsBaseApi + '&' + link.find(lnk => lnk.rel === EntityType.Deals).href.split('=')[0].split('?')[1] + "=" +
            selectedStatusCode.replace(/^,|,$/g, '')
            : link.find(lnk => lnk.rel === EntityType.Deals).href.split('=')[0] + "=" + selectedStatusCode.replace(/^,|,$/g, '');
        } else
          this.showGridDetails = false;
      }
      else {
        this.showGridDetails = false;
      }
      return finalUrl;
    }
  }
  onClickSubDealStatus(dealSubStatus: DealStatus) {
    this.isEditMode = false;
    //this.resetTimeSelection = true;
    this.resetLastUpdated();
    if(this.closeViewDropDown == USER_VIEW_MY_SUBMISSIONS){
      setTimeout(() => {
        this.apiUrlForMySubmissions();
      }, 500);
    }else{
      setTimeout(() => {
        this.resetApiUrls();
      }, 500);
    }
    this._store.dispatch(new collection.UpdateSubDealStatusAction(dealSubStatus));
    /* this.dealStatusList$.subscribe(deallist => {
      this.makeApiUrlObservable(this.updateUrl(deallist));
    }).unsubscribe(); */
  }
  onRowClicked(event) {
    console.log(this.DealDetailsComponent);
   let dealClicked = event.data;
    if (event) {
      this.resetLastUpdated();
      this.isEditMode = true;
      if(this.DealDetailsComponent.dealDetails.dirty)
      {
        console.log("changed");
        this.DealDetailsComponent.referenceFun(true);
        this._sharedEventService.getEvent().subscribe(event => {
          if(event.eventType ==GlobalEventType.Ag_Grid_Row_Selection){
            if(event.data ==true){
              this.isEditMode = true;
              this.DealDetailsComponent.referenceFun(false);
              this.DealDetailsComponent.showCancelPopup = false;
              this.DealDetailsComponent.dealDetails.reset();
              this.DealDetailsComponent.dialog.closeAll();
              this.DealDetailsComponent.dealDetails.reset();
              this.dealGridRowData = dealClicked;
              this.upDateCheckList();
              this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Retain_Selection, 'Deal', 'Retain deal selection', dealClicked.data.dealNumber));
            }
            else{
              this.isEditMode = false;
            }
          }
        });
      }
      else
      {
       console.log("not changed");
       this.dealGridRowData = dealClicked;
       this.upDateCheckList();
      }
    }
  }
  onCancelDealDetail(cancelInfo) {
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Ag_Grid_Retain_Focus, 'Deal', 'Retain deal row focus', cancelInfo.dealClosed));
    this.isEditMode = cancelInfo.isCancelled;
  }
  
  onSaveDealDetail() {
    this.isEditMode = false;
  }
  onSubmitDealDetail(event) {
    this._store.dispatch(new dealdetailcollection.UpdateDealsAction(event));
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }

  captureState() {
    let agGridState;
    let inceptionState;
    let dealStatus

    if (this.DealListComponent != undefined) {
      agGridState = this.DealListComponent.getngGridData();
      inceptionState = this.filterSelected; // fetching inception state
    } else {
      agGridState = {
        colState: [],
        sortState: [],
        filterState: {}
      };
      inceptionState = FilterType.All;
    }
    /* let customViewConfigState = {
      userviewAgGridState: agGridState,
      inceptionFilterState: inceptionState

    };
    this._store.dispatch( // loading list User View
      new fromUserView.LoadUserViewAgGridState(customViewConfigState)); */

    this.dealStatusList$.subscribe((val) => {
      dealStatus = JSON.parse(JSON.stringify(val)); //changing the state
    }).unsubscribe()

    dealStatus.map(val => { // forming a object so its property count to null
      val.data.count = null;
      if (val.data.statusSummary && val.data.statusSummary.length) {
        for (let ss of val.data.statusSummary) {
          ss.count = null; // statusSummary to null 
        }
      }
      return val;
    });

    let screenState = {
      extendedSearch: this.extendedSearchSaveState,
      dealStatus: dealStatus,
      inceptionFilter: inceptionState,
      agGrid: agGridState
    };

    return screenState;
  }

  addView(submissionValue) {

    this.closeViewDropDown = submissionValue;
    let screenState = this.captureState(); // capture state objects

    let requestObj = {
      data: {
        "screenName": USER_VIEW_SCREEN_NAME,
        "viewname": submissionValue,
        "layout": JSON.stringify(screenState)
      }
    }
    this._store.dispatch(new fromUserView.AddUserViews({ requestObj: requestObj, url: USERVIEW_ADD_API_URL })); // post api to add a view
  }

  getUserViewState(getState) {
    this.closeViewDropDown = getState.data.viewname;
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Close_View_Dropdown));
    if(getState.data.viewname == USER_VIEW_ALL_SUBMISSIONS){
      this.setAllSubmissionsView();
    }else if(getState.data.viewname == USER_VIEW_MY_SUBMISSIONS){
      this.setMySubmissionsView();
    }else{
      this.setCustomView(getState);
    }
    console.log("State loaded successfully")
  }
  clearExistingSearch(){
    this._store.dispatch(new ClearActiveSaveSateExtendedSearchData());
    this._store.dispatch(new collection.ClearDealStatusState());
    let customViewConfigState = {
      userviewAgGridState: {
        colState: [],
        sortState: [],
        filterState: {}
      },
      inceptionFilterState: FilterType.All,
      customViewLoadMsg: 'success'
    };
    
    setTimeout(() => {
      this._store.dispatch(new fromUserView.LoadUserViewAgGridState(customViewConfigState));
    }, 1000);
  }
  setAllSubmissionsView(){
    this.clearExistingSearch();
    setTimeout(() => {
      this.resetApiUrls();
      this.lblTextObj = this.extendedSearchRef.prepareLblText();
    }, 1000);
  }
  updateUrlForMySubmissions(){
    this.clearExistingSearch();
    let dealstatussummariesUrlParam = DEALSTATUSSUMMARIES_API_URL;
    let dealsApiUrlParam = DEAL_API_URL;
    let urlStr = 'personIds=' + this.userDetails.personId;
    if (urlStr && urlStr.length > 0) {
      dealstatussummariesUrlParam += '?' + urlStr;
      dealsApiUrlParam += '?' + urlStr;
    }
    this.dispatchUpdateApiURlAction([new ApiUrl({key: EntityType.DealStatusSummaries, url: dealstatussummariesUrlParam}), new ApiUrl({key: EntityType.Deals, url: dealsApiUrlParam})]);
  }
  apiUrlForMySubmissions(){
    this.apiUrls$.subscribe(apiUrls => {
      if (apiUrls)
        apiUrls.map(apimodel => {
          console.log('Api Url:' + apimodel.apiUrl);
          if (apimodel.key === EntityType.DealStatusSummaries) {
            this.dealstatussummary = apimodel.apiUrl;
            this.dispatchGetDealAction(this.dealstatussummary);
          }
          else if (apimodel.key === EntityType.Deals) {
            this.dealsBaseApi = apimodel.apiUrl;
            this.dealStatusList$.subscribe(deals => {
              this.makeApiUrlObservable(this.updateUrl(deals));
              // this.resetTimeSelection=true;
            }
            ).unsubscribe();
          }
        });
    }
    ).unsubscribe();
  }
  setMySubmissionsView(){
    this.resetLastUpdated();
    this.updateUrlForMySubmissions();
    setTimeout(() => {
      this.apiUrlForMySubmissions();
    }, 1000);
  }
  setCustomView(getState){
    this._store.dispatch(new ClearActiveSaveSateExtendedSearchData());
    this._store.dispatch(new collection.ClearDealStatusState());
    let url = USERVIEW_ADD_API_URL + '/' + getState.data.viewId
    this._store.dispatch(new fromUserView.LoadUserViewsSate(url));
    setTimeout(() => {
      this.resetApiUrls();
      this.lblTextObj = this.extendedSearchRef.prepareLblText();
    }, 2000);
  }

  resetLastUpdated() {
    this.lastUpdated = new Date();
  }
  closeCurrentView(Search) {
    this.closeViewDropDown = Search; // array value from child to submission dropdown
  }
  refreshView($event) {
    if ($event) {
      $event.preventDefault();
    }
    this.resetLastUpdated();
    this.makeApiUrlObservable(this.apiUrl.value);
    this.apiUrls$.subscribe(apiUrls => {
      console.log('apiUrls$', apiUrls);
      if (apiUrls)
        apiUrls.map(apimodel => {
          console.log('Api Url:' + apimodel.apiUrl);
          if (apimodel.key === EntityType.DealStatusSummaries) {
            this.dealstatussummary = apimodel.apiUrl;
            this.dispatchGetDealAction(this.dealstatussummary);
          }
        });
    }
    ).unsubscribe();
  }
  makeApiUrlObservable(apiUrl: string) {
    this.apiUrl = { 'value': apiUrl, 'randomValue': Math.random() };
  }
  showExtendedSerchPanel: boolean = false;
  openSearchPanel($event) {
    this.showExtendedSerchPanel = !this.showExtendedSerchPanel;
  }
  applyBtnHandler($event) {
    console.log('extended search', $event);
    this.lblTextObj = $event;
    this.closeViewDropDown = 'Search';
    this.resetApiUrls();
    this._sharedEventService.setEvent(new BroadcastEvent(GlobalEventType.Reset_AgGrid));
    this.resetTimeSelection = true;
    this.filterSelectedForView = FilterType.All;
  }

  upDateCheckList(){
    console.log("test", this.dealGridRowData)
    if(this.callCheckList){
      this._store.dispatch(
        new fromDealCheckList.LoadDealChecklist(DEAL_API_URL+ '/'+this.dealGridRowData.data.dealNumber+'/checklists')
      );
      }
  }

  onCheckListtShow(event){// when checklist pin is true call the API
    this.callCheckList = event;
   }   

  dealCountRow(count){
    this.totalDealcount = {x: count , y:this.dealSelectedTotalCount}
  }
}
