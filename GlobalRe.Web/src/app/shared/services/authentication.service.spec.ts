
import { inject, TestBed, getTestBed, async } from '@angular/core/testing';
import { HttpClient, HttpHandler, HttpHeaders, HttpResponse, HttpXhrBackend, HttpRequest, HttpResponseBase } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { StoreModule, Store, combineReducers, StateObservable } from '@ngrx/store';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { BASE_API_URL, USERNAME_API_URL ,USER_DISPLAYNAME_URL} from '../../app.config';
import { LoginUser } from '../../shared/models/login-user';

describe('Authentication Service', () => {
  let authService: AuthenticationService;
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
        AuthenticationService
      ]
    });

    authService = TestBed.get(AuthenticationService);
    httpMock = TestBed.get(HttpTestingController);

  });

  it('should return user and token successfully at login', () => {
    expect(authService.getToken()).toBeDefined();
  });

  it('windows authenticate and should return user name', (done) => {
    authService.getUserName().subscribe(response => {
      expect(response["value"].userName).toEqual('testUser');
      done();
    });

    const getUserRequest = httpMock.expectOne(USERNAME_API_URL);
    getUserRequest.flush({ UserName: "MARKELCORP\\testUser" });

    expect(getUserRequest).toBeDefined();
    expect(getUserRequest.request.method).toEqual('GET');

    httpMock.verify();
  });


  it('windows authenticate failed and should return empty user name', (done) => {
    authService.getUserName().subscribe(response => {
      expect(response["value"].userName).toEqual('');
      done();
    });

    const getUserRequest = httpMock.expectOne(USERNAME_API_URL);
    getUserRequest.flush({ UserName: '' });

    expect(getUserRequest).toBeDefined();
    expect(getUserRequest.request.method).toEqual('GET');

    httpMock.verify();
  });

  it('user authenticated and should return user information with token', (done) => {
    let mockUser = new LoginUser({
      userName: 'MARKELCORP\testUser',
      applicationVersion: '1.0',
      domainName: 'MARKELCORP',
      environment: 'QA',
      isAuthenticated: true,
      displayName:'keshav Dwivedi',
      access_token :'TEST123456789'
    });
    mockUser.access_token = 'TEST123456789';

    authService.authenticateUser("testUser").subscribe(response => {
      expect(response.isAuthenticated).toEqual(mockUser.isAuthenticated);
      expect(response.access_token).toEqual('TEST123456789');
      expect(authService.getToken()).toBeDefined();
      done();
    });

    const authenticateUserRequest = httpMock.expectOne(`${BASE_API_URL}token`);
    authenticateUserRequest.flush(mockUser);

    expect(authenticateUserRequest).toBeDefined();
    expect(authenticateUserRequest.request.method).toEqual('POST');

    httpMock.verify();
  });

  it('user authentication failed and should not return empty user information', (done) => {
    let mockUser = new LoginUser();

    authService.authenticateUser("testUser").subscribe(response => {
      expect(response.isAuthenticated).toBeFalsy();
      done();
    });

    const authenticateUserRequest = httpMock.expectOne(`${BASE_API_URL}token`);
    authenticateUserRequest.flush(mockUser);

    expect(authenticateUserRequest).toBeDefined();
    expect(authenticateUserRequest.request.method).toEqual('POST');

    httpMock.verify();
  });
  it('user getUserDisplayName should eturn empty user information', (done) => {
    let mockUser = [
      {
        "data": {
          "personId": 916914,
          "firstName": "Ravindra",
          "lastName": "Devkhile",
          "displayName": "Devkhile, Ravindra [ON CONTRACT]"
        },
        "links": null,
        "messages": []
      }
    ];
    authService.getUserDisplayName().subscribe(response => {
      done();
    });

    const getUserDisplayNameRequest = httpMock.expectOne(`${BASE_API_URL}`+ USER_DISPLAYNAME_URL);
    getUserDisplayNameRequest.flush(mockUser);
    expect(getUserDisplayNameRequest).toBeDefined();
    expect(getUserDisplayNameRequest.request.method).toEqual('GET');
    httpMock.verify();
  });
  it('Should called', inject([AuthenticationService], (authService) => {
    expect(authService).toBeDefined();
  }));
});
