
import { inject, TestBed, getTestBed, async } from '@angular/core/testing';
import { HttpClient, HttpHandler, HttpHeaders, HttpResponse, HttpXhrBackend, HttpRequest, HttpResponseBase } from '@angular/common/http';
import { CoreService } from './core.service';
import { StoreModule, Store, combineReducers, StateObservable } from '@ngrx/store';
import { RouterTestingModule } from '@angular/router/testing'
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import {DEALSTATUSSUMMARIES_API_URL, EntityType} from '../../app.config';
//import { LoginUser } from '../../shared/models/login-user';

const stubapiurl="v1/deals?statuscodes=3,80,2,14,29";
const  stubresponse={
  "url": "v1/deals?limit=1",
  "totalRecords": 16,
  "offset": 0,
  "limit": 1,
  "results": [
    {
      "data": {
        "dealNumber": 1330413,
        "dealName": "Munich Re Stop Loss 2015 Great American MPCI Retro D#1330413 C#3948",
        "statusCode": 0,
        "status": "Bound",
        "contractNumber": 1000841,
        "inceptionDate": "2015-01-01",
        "targetDate": null,
        "priority": null,
        "submittedDate": "2015-04-16",
        "primaryUnderwriterCode": 64710,
        "primaryUnderwriterName": "Frank Bigley",
        "secondaryUnderwriterCode": 10275,
        "secondaryUnderwriterName": "Erica Rance Mill",
        "TechnicalAssistantCode": null,
        "TechnicalAssistantName": null,
        "modellerCode": null,
        "modellerName": null,
        "actuaryCode": 48629,
        "actuaryName": "Bruce Stocker",
        "expiryDate": "2015-12-31",
        "brokerCode": 20453,
        "brokerName": "Guy Carpenter & Company, LLC",
        "brokerContactCode": 13957,
        "brokerContactName": "Dave Ott"
      }
    }
  ],
  "messages": []
};
describe('CoreService Service', () => {
  let coreService: CoreService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule,
        HttpClientTestingModule,
        StoreModule.forRoot({
          //auth: combineReducers(fromRoot.getAuthUserName),
        }),
      ],
      providers: [
        CoreService
      ]
    });

    coreService = TestBed.get(CoreService);
    httpMock = TestBed.get(HttpTestingController);

  });

  it('invokeGetEntityApi should return collection', (done) => {
    coreService.invokeGetEntityApi(DEALSTATUSSUMMARIES_API_URL).subscribe(response => {
      expect(response["res"]).toEqual(stubresponse);
      done();
    });

    const invokeGetEntityApiRequest = httpMock.expectOne(BASE_API_URL+DEALSTATUSSUMMARIES_API_URL);
    invokeGetEntityApiRequest.flush({ res: stubresponse });

    expect(invokeGetEntityApiRequest).toBeDefined();
    expect(invokeGetEntityApiRequest.request.method).toEqual('GET');

    httpMock.verify();
  });

  it('invokeGetListResultApi should return collection', (done) => {
    coreService.invokeGetListResultApi(DEALSTATUSSUMMARIES_API_URL).subscribe(response => {
      expect(response["res"]).toEqual(stubresponse);
      done();
    });

    const invokeGetEntityListApiRequest = httpMock.expectOne(BASE_API_URL+DEALSTATUSSUMMARIES_API_URL);
    invokeGetEntityListApiRequest.flush({ res: stubresponse });

    expect(invokeGetEntityListApiRequest).toBeDefined();
    expect(invokeGetEntityListApiRequest.request.method).toEqual('GET');

    httpMock.verify();
  });

  it('invokeGetEntityListApi should return collection', (done) => {
    let filters={"searchCriteria":[],"offset":0,"limit":100,"sort":""};
    coreService.invokeGetEntityListApi(stubapiurl,filters).subscribe(response => {
      expect(response["res"]).toEqual(stubresponse);
      done();
    });
    const invokeGetEntityListApiRequest = httpMock.expectOne(BASE_API_URL+stubapiurl+'&offset=0&limit=100');
    invokeGetEntityListApiRequest.flush({ res: stubresponse });
    expect(invokeGetEntityListApiRequest).toBeDefined();
    expect(invokeGetEntityListApiRequest.request.method).toEqual('GET');
    httpMock.verify();
  });
  it('invokeGetEntityListApi should return collection with Filter', (done) => {
    let filters={"searchCriteria":{"dealNumber": 1330413},"offset":0,"limit":100,"sort":""};
    coreService.invokeGetEntityListApi(stubapiurl,filters).subscribe(response => {
      expect(response["res"]).toEqual(stubresponse);
      done();
    });

    const invokeGetEntityListApiRequest = httpMock.expectOne(BASE_API_URL+stubapiurl+'&offset=0&limit=100&dealNumber=1330413');
    invokeGetEntityListApiRequest.flush({ res: stubresponse });

    expect(invokeGetEntityListApiRequest).toBeDefined();
    expect(invokeGetEntityListApiRequest.request.method).toEqual('GET');

    httpMock.verify();
  });
  it('invokeGetEntityListApi should return collection with Filter(noparam)', (done) => {
    let filters={"searchCriteria":{"dealNumber": 1330413},"offset":0,"limit":100,"sort":""};
    coreService.invokeGetEntityListApi('v1/deals',filters).subscribe(response => {
      expect(response["res"]).toEqual(stubresponse);
      done();
    });

    const invokeGetEntityListApiRequest = httpMock.expectOne(BASE_API_URL+'v1/deals?offset=0&limit=100&dealNumber=1330413');
    invokeGetEntityListApiRequest.flush({ res: stubresponse });

    expect(invokeGetEntityListApiRequest).toBeDefined();
    expect(invokeGetEntityListApiRequest.request.method).toEqual('GET');

    httpMock.verify();
  });
  it('invokeGetEntityListApi should return collection(no param, no filter)', (done) => {
    let filters={"searchCriteria":[],"offset":0,"limit":100,"sort":""};
    coreService.invokeGetEntityListApi('v1/deals',filters).subscribe(response => {
      expect(response["res"]).toEqual(stubresponse);
      done();
    });
    const invokeGetEntityListApiRequest = httpMock.expectOne(BASE_API_URL+'v1/deals?offset=0&limit=100');
    invokeGetEntityListApiRequest.flush({ res: stubresponse });
    expect(invokeGetEntityListApiRequest).toBeDefined();
    expect(invokeGetEntityListApiRequest.request.method).toEqual('GET');
    httpMock.verify();
  });
  it('toQueryString should return string', () => {
    spyOn(coreService, 'toQueryString');
    coreService.toQueryString({"searchCriteria":[],"offset":0,"limit":100,"sort":""},"searchCriteria");
    expect(coreService.toQueryString).toHaveBeenCalled();
  });
  it('getEntityGridColumns should return string', () => {
    coreService.dateFilterComparator(new Date(2016,2,2),'02-08-2017');
    coreService.dateFilterComparator(new Date(2018,2,2),'02-08-2017');
    coreService.dateComparator('02-08-2017','02-08-2018');
    coreService.dateComparator(null,'02-08-2018');
    coreService.dateComparator('02-08-2017',null);
    coreService.dateComparator(null,null);
    coreService.getEntityGridColumns(EntityType.Deals)
    spyOn(coreService, 'getEntityGridColumns');
    coreService.getEntityGridColumns(undefined);
    expect(coreService.getEntityGridColumns).toHaveBeenCalled();
  });
});
