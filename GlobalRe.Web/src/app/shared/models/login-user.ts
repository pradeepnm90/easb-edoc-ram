export class LoginUser {
  userName: string;
  personId: number;
  applicationVersion: string;
  domainName: string;
  environment: string;
  isAuthenticated: boolean;
  tokenProxyRootUrl: string;
  authToken: string;
  access_token: string;
  displayName: string;
  canEditDeal: boolean;
  apiUrl: string;
  LINK_AMBEST_URL:string;
  LINK_AON_URL: string;
  LINK_ERMRCM_URL: string;
  LINK_ERMS_DEALEDIT_API: string;
  LINK_ERMS_URL: string;
  LINK_EXPENSES_URL: string;
  LINK_GUYCARPENTER_URL: string;
  LINK_IMAGE_RIGHT: string;
  LINK_MARKELEXTERNAL_URL: string;
  LINK_MYMARKEL_URL: string;
  LINK_QLIKVIEW_URL: string;
  LINK_SERVICEDESK_EMAIL: string;
  LINK_SNL_URL: string;
  LINK_WORKDAY_URL: string;
  production: boolean;
  impersonation: boolean;

  constructor(target: {
    userName?: string;//domain login user name
    personId?: number;
    applicationVersion?: string;
    domainName?: string;
    environment?: string;
    displayName?: string;
    isAuthenticated?: boolean;
    authToken?: string;
    access_token?: string;
    canEditDeal?: boolean;
    apiUrl?: string;
    LINK_AMBEST_URL?:string;
    LINK_AON_URL?: string;
    LINK_ERMRCM_URL?: string;
    LINK_ERMS_DEALEDIT_API?: string;
    LINK_ERMS_URL?: string;
    LINK_EXPENSES_URL?: string;
    LINK_GUYCARPENTER_URL?: string;
    LINK_IMAGE_RIGHT?: string;
    LINK_MARKELEXTERNAL_URL?: string;
    LINK_MYMARKEL_URL?: string;
    LINK_QLIKVIEW_URL?: string;
    LINK_SERVICEDESK_EMAIL?: string;
    LINK_SNL_URL?: string;
    LINK_WORKDAY_URL?: string;
    production?: boolean;
    impersonation?: boolean;

    
  } = {}) {
    this.userName = target.userName ? target.userName.toUpperCase() : "";
    this.domainName = target.domainName || "";
    this.applicationVersion = target.applicationVersion || "";
    this.environment = target.environment || "";
    this.access_token = target.access_token || "";
    this.authToken = target.authToken || "";
    this.personId = target.personId
    this.isAuthenticated = target.isAuthenticated || false;
    this.displayName = target.displayName ? target.displayName.toUpperCase() : this.userName;
    this.canEditDeal = target.canEditDeal || false;
    this.apiUrl = target.apiUrl;
    this.LINK_AMBEST_URL=target.LINK_AMBEST_URL;
    this.LINK_AON_URL=target.LINK_AON_URL;
    this.LINK_ERMRCM_URL=target.LINK_ERMRCM_URL;
    this.LINK_ERMS_DEALEDIT_API=target.LINK_ERMS_DEALEDIT_API;
    this.LINK_ERMS_URL=target.LINK_ERMS_URL;
    this.LINK_EXPENSES_URL=target.LINK_EXPENSES_URL;
    this.LINK_GUYCARPENTER_URL=target.LINK_GUYCARPENTER_URL;
    this.LINK_IMAGE_RIGHT=target.LINK_IMAGE_RIGHT;
    this.LINK_MARKELEXTERNAL_URL=target.LINK_MARKELEXTERNAL_URL;
    this.LINK_MYMARKEL_URL=target.LINK_MYMARKEL_URL;
    this.LINK_QLIKVIEW_URL=target.LINK_QLIKVIEW_URL;
    this.LINK_SERVICEDESK_EMAIL=target.LINK_SERVICEDESK_EMAIL;
    this.LINK_SNL_URL=target.LINK_SNL_URL;
    this.LINK_WORKDAY_URL= target.LINK_WORKDAY_URL;
    this.production=target.production;
    this.impersonation=target.impersonation;

  }
}