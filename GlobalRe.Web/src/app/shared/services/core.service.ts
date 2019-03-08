import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Rx';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import {EntityType,HttpActionType,ApiLinkType, NOTES_URL,
  NOTE_TYPE_URL} from '../../app.config';
import {Store} from "@ngrx/store";
import {PagingRequest} from '../models/paging-request';
import {EntityApiData} from '../models/entity-api-data';
import {ApiHelper} from '../utils/api-helper';
import { LoginUser } from '../../shared/models/login-user';
import * as fromRoot from '../../store/index';
import { AddUserNotificationAction } from '../../actions/user-notification.action';
@Injectable()

export class CoreService {
  BASE_API_URL =""
  public logeInUser$: Observable<any>;
  public baseApiUrl$: Observable<string>;
  constructor(private _store: Store<fromRoot.AppState>,private _http: HttpClient) {
        this.logeInUser$ = this._store.select(fromRoot.getAuthenticatedUser);
        this.baseApiUrl$ = this._store.select(fromRoot.getBaseApiUrlValue);
          this.baseApiUrl$.subscribe(dataApi =>{
          this.BASE_API_URL = dataApi +'/'; 
        })
  }

  invokeGetEntityOptionsApi(apiUrl: string) {

    //debugger
    if(!apiUrl){
        console.log("--------- get entity options with empty path");
        return;
    }


    return this._http.options(this.BASE_API_URL + apiUrl)
        .map((res) => {
            return res;
        }).share()
        .catch(err => {
            //this.handleApiError(err, "(Options)", null);
            return Observable.throw(err); // observable needs to be returned or exception raised
        })
}
  invokeGetEntityApi(apiUrl: string, callerFunc?: string):Observable<any> {
    
     console.log(apiUrl)
     return this._http.get(this.BASE_API_URL + apiUrl)
      .map((res) => {

        return res;
      })
      .catch(err => {
        return Observable.of([]);
      });
  }
  invokeDeleteEntityApi(apiUrl: string, id: any, reqParam: any): Observable<any> {
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: reqParam
    };
    console.log('delete', apiUrl, id, reqParam);
    return this._http.delete(this.BASE_API_URL + apiUrl + '/' + `${id}`, options)
      .map((res) => {
        return res;
      })
      .catch(err => {
        return Observable.of([]);
      });
  }
  invokeGetListResultApi(apiUrl: string):Observable<any> {
        
    
    let encodeApiUrl=encodeURI(this.BASE_API_URL + apiUrl);
    return this._http
      .get(encodeApiUrl)
      .map((res:any) => {
        return res;
      })
      .catch(error => {
        return Observable.throw(error);
      });
  }
  invokeGetListArrayResultApiList(apiUrl: string[]):Observable<any> {
    let httpGetArray : any = [];

    for(let apiUrlVal of apiUrl){
      httpGetArray.push(this._http.get(encodeURI(this.BASE_API_URL + apiUrlVal)))
    }
    return Observable.forkJoin(httpGetArray);
   // let encodeApiUrl=encodeURI(this.BASE_API_URL + apiUrl);
   /*  return this._http
      .get(encodeApiUrl)
      .map((res:any) => {
        return res;
      })
      .catch(error => {
        return Observable.throw(error);
      }); */
  }
  
//   invokeUpdateEntityApi(entityData: any, apiUrl: string) {
//       return this._http.put(this.BASE_API_URL + apiUrl, JSON.stringify(entityData))
//         .map((res) => {
//             return res;
//         })
//         .catch(err => {
//             //this.handleApiError(err, "(Update)", entityData);
//             return Observable.of(0); // observable needs to be returned or exception raised
//         });
//
//
// }

  invokeUpdateEntityApi(entityData: any,putApiUrl:string='') {
    let apiUrl=putApiUrl
    if(putApiUrl==='')
     apiUrl = ApiHelper.getEntityActionApiUrl(entityData.links, HttpActionType.put,  ApiLinkType.self);
    return this._http.put(this.BASE_API_URL + apiUrl, JSON.stringify(entityData.data))
      .map((res) => {
        return res;
      })
      .catch(err => {
        console.log("error handling",err);
        if(err.error && err.error.details && err.error.details.length > 0)
        {
        this._store.dispatch(new AddUserNotificationAction(err.error.details));
        }
        return Observable.of(entityData); // observable needs to be returned or exception raised
      });
  }
  invokePostEntityApi(entityData: any, postApiUrl:string='') {
    let apiUrl=postApiUrl;
    if(postApiUrl==='')
      return;
    return this._http.post(this.BASE_API_URL + apiUrl, entityData.data)
      .map((res) => {
        return res;
      })
      .catch(err => {
        return Observable.of(entityData); // observable needs to be returned or exception raised
      });
  }
  invokeGetEntityListApi(apiUrl: string, searchCriteria: PagingRequest<any>, callerFunc?: string): Observable<any> {
   if (searchCriteria) {
      const queryString1 = this.toQueryString(searchCriteria, "searchCriteria");
      const queryString2 = this.toQueryString(searchCriteria.searchCriteria);
      if (queryString1 !== "" && queryString2 !== "")
        apiUrl = apiUrl + ((apiUrl.indexOf('?') > 0) ? '&' : "?") + queryString1 + "&" + queryString2;
      else if (queryString1 !== "")
        apiUrl = apiUrl + ((apiUrl.indexOf('?') > 0) ? '&' : "?") + queryString1;
    }
    console.log(this.BASE_API_URL + apiUrl)
    return this._http.get<EntityApiData<any>>(this.BASE_API_URL + apiUrl)
      .map((res) => {
        return res;
      }).catch(err => {
        return Observable.of({ totalRecords: 0, results: [] }); // observable needs to be returned or exception raised
      });
  }
  toQueryString(obj: any, excludePropObj: string = null): string {
    const str = [];
    for (const prop in obj)
      if (obj.hasOwnProperty(prop) && obj[prop] != null && obj[prop] !== "" && prop !== excludePropObj) {
        str.push(encodeURIComponent(prop) + "=" + encodeURIComponent(obj[prop].toString()));
      }
    if (str.length > 0)
      return str.join("&");
    return "";
  }
  getEntityGridColumns(entityType: EntityType) {
    switch (entityType) {
      case EntityType.Deals:
        return this.getDealsColumn();
      default:
        return this.getDealsColumn();
    }

  }
  dateComparator(date1, date2) {
   const monthToComparableNumber= (date)=> {
      if (date === undefined || date === null || date.length !== 10) {
        return null;
      }
      const yearNumber = date.substring(6, 10);
      const dayNumber= date.substring(3, 5);
      const monthNumber  = date.substring(0, 2);
      const result = yearNumber * 10000 + monthNumber * 100 + dayNumber;
      return result;
    };
    const date1Number = monthToComparableNumber(date1);
    const date2Number = monthToComparableNumber(date2);
    if (date1Number === null && date2Number === null) {
      return 0;
    }
    if (date1Number === null) {
      return -1;
    }
    if (date2Number === null) {
      return 1;
    }
    return date1Number - date2Number;
  }
  dateFilterComparator(filterLocalDateAtMidnight, cellValue)
  {
    const dateAsString = cellValue;
    if (dateAsString == null) return -1;
    const dateParts = dateAsString.split("-");
    if(dateParts) {
      const cellDate = new Date(Number(dateParts[2]), Number(dateParts[0]) - 1, Number(dateParts[1]));
      if (filterLocalDateAtMidnight.getTime() === cellDate.getTime()) {
        return 0;
      }
      if (cellDate < filterLocalDateAtMidnight) {
        return -1;
      }
      if (cellDate > filterLocalDateAtMidnight) {
        return 1;
      }
    }
    else
      return -1;
  }
  private getDealsColumn() {
    return [{
      headerName: "Deal Name",
      field: "data.dealName",
      filter: "agTextColumnFilter",
        filterParams: {
          applyButton: true,
          clearButton: true,
          // filterOptions: ["equals, notEqual, startsWith, endsWith, contains", "notContains"],
          // debounceMs: 0,
          caseSensitive: false,
          suppressAndOrCondition: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray; z-index: 2;"/>',
          },
          
    }, 
    {
      headerName: "Contract #",
      field: "data.contractNumber",
      filter: "agNumberColumnFilter",      
      filterParams: {
        suppressMenu: true,         applyButton: true,
        clearButton: true},
        icons:{
          filter: '<i class="fa fa-filter" style="color:gray"/>',
        }
    }, 

    {
      headerName: "Submission Type",
      field: "data.renewalName",
      filter: "agSetColumnFilter",      
      filterParams: {
        suppressMenu: true,         applyButton: true,
        clearButton: true},
        icons:{
          filter: '<i class="fa fa-filter" style="color:gray"/>',
        },
    }, 
    {
      headerName: "Inception",
      field: "data.inceptionDate",
      floatingfiltercomponent: "agDateInput",
      filter: "agDateColumnFilter",
      comparator: this.dateComparator,
      filterParams: {
        suppressMenu: true,
        applyButton: true,clearButton:true, inRangeInclusive:true,
        comparator: this.dateFilterComparator,
        filterOptions: ["equals", "notEqual", "lessThan", "lessThanOrEqual", "greaterThan", "greaterThanOrEqual", "inRange"],

      },
      icons:{
        filter: '<i class="fa fa-filter" style="color:gray"/>',
      },
      cellClassRules :{
        isDate: function() {
            return true;
        }
    }
    }

      , {
        headerName: "Target Date",
        field: "data.targetDate",
        filter: "agDateColumnFilter",
        comparator: this.dateComparator,
        filterParams: {
          suppressMenu: true,
          apply: true,inRangeInclusive:true,
          comparator: this.dateFilterComparator,
          clearButton:true,
          filterOptions: ["equals", "notEqual", "lessThan", "lessThanOrEqual", "greaterThan", "greaterThanOrEqual", "inRange"],

        },
        icons:{
          filter: '<i class="fa fa-filter" style="color:gray"/>',
        },
        cellClassRules :{
          isDate: function() {
              return true;
          }
      }

      }, {
        headerName: "Priority",
        field: "data.priority",
         filter: "agNumberColumnFilter",      filterParams: {
          suppressMenu: true,          applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },
          

      }, {
        headerName: "Submitted",
        field: "data.submittedDate",
        filter: "agDateColumnFilter",
        comparator: this.dateComparator,
        filterParams: {suppressMenu: true,
          apply: true,inRangeInclusive:true,
          comparator: this.dateFilterComparator,
          clearButton:true,
          filterOptions: ["equals", "notEqual", "lessThan", "lessThanOrEqual", "greaterThan", "greaterThanOrEqual", "inRange"],

        },
        icons:{
          filter: '<i class="fa fa-filter" style="color:gray"/>',
        },
        cellClassRules :{
          isDate: function() {
              return true;
          }
      }
      }, {
        headerName: "Status",
        field: "data.status",
        filterParams: { suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },
      }, {
        headerName: "Deal Number",
        field: "data.dealNumber",
        filter: "agNumberColumnFilter",      
        filterParams: { suppressMenu: true,        applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },
      },
       {
        headerName: "Underwriter",
        field: "data.primaryUnderwriterName",
        filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,       applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },

      }, 
      {
        headerName: "Underwriter 2",
        field: "data.secondaryUnderwriterName",
        // filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,        applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },

      }, {
        headerName: "TA",
        field: "data.technicalAssistantName",
        filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,          applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },

      }, {
        headerName: "Modeler",
        field: "data.modellerName",
        filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,          applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },

      },
      {
        headerName: "Actuary",
        field: "data.actuaryName",
        filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          },

      },
      {
        headerName: "Expiration",
        field: "data.expiryDate",
        hide: true,
        filter: "agDateColumnFilter",
        comparator: this.dateComparator,
        filterParams: {
          suppressMenu: true,
          applyButton: true,clearButton:true, inRangeInclusive:true,
          comparator: this.dateFilterComparator,
          filterOptions: ["equals", "notEqual", "lessThan", "lessThanOrEqual", "greaterThan", "greaterThanOrEqual", "inRange"],

        },
        icons:{
          filter: '<i class="fa fa-filter" style="color:gray"/>',
        },
        cellClassRules :{
          isDate: function() {
              return true;
          }
      }
      },
      {
        headerName: "Broker Name",
        field: "data.brokerName",
        hide: true,
        filter: "agTextColumnFilter",
        filterParams: {suppressMenu: true,          applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }

      },
      {
        headerName: "Broker Contact",
        field: "data.brokerContactName",
        hide: true,
        filter: "agTextColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "Broker Group",
        field: "data.brokerGroupName",
        hide: true,
        filter: "agTextColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "Cedant",
        field: "data.cedantName",
        hide: true,
        filter: "agTextColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "Cedant Group",
        field: "data.cedantGroupName",
        hide: true,
        filter: "agTextColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "Pre-Bind Proc",
        field: "data.chkPreBindProcessing",
        hide: true,
        filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "Modeling",
        field: "data.chkModeling",
        hide: true,
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "UW Compliance",
        field: "data.chkUWCompliance",
        hide: true,
        filter: "agSetColumnFilter",
        filterParams: {suppressMenu: true,         applyButton: true,
          clearButton: true},
          icons:{
            filter: '<i class="fa fa-filter" style="color:gray"/>',
          }
      },
      {
        headerName: "Actions",
        field: "data.action",
        cellRenderer: "AgGridActionComponentRenderer",
        menuTabs:[],
        floatingFilterComponentParams: {suppressFilterButton:true}
      }
    ];
  }


}
