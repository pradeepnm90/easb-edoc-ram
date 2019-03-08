
//API URL
export const USER_DISPLAYNAME_URL = "v1/persons?personId=0";
export const DEALBY_STATUS = "v1/lookups/dealstatuses";
export const EXTENDED_SEARCH_URL = "v1/lookups/exposureTree";
export const DEALSTATUSSUMMARIES_API_URL="v1/dealstatussummaries";
export const DEAL_API_URL = "v1/deals";
export const DECLINE_API_URL = "v1/deals/";
export const DEFAULTSUBDIVISION_API_URL="v1/personprofile";
export const ENTITY_TYPE_DEALS="deals";
export const DOCUMENTS_URL = "/documents";
export const PAGE_URL = "/pages";
export const KEYNONKEY_URL = "/keydoctypes/";
export const NOTES_URL = "/v1/Notes?dealNumber=";
export const ADD_NOTES_URL = '/v1/Notes';
export const NOTE_URL = '/v1/Notes/';
export const NOTE_TYPE_URL = "v1/lookups/notetypes";
export const DECLINE_REASON_URL = "/v1/lookups/dealstatuses?StatusGroup=decline";
export const DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE = '#FFFFFF'; 
export const DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE = '#000000'; 
export const NOTE_TYPE_COLOR_CONFIG = {
  '66': { 'fontColor': '#FFFFFF', 'backColor': '#FFC206' },//Misc/General  
  '46': { 'fontColor': '#FFFFFF', 'backColor': '#3B99FC' },//Underwriting Info  
  '67': { 'fontColor': '#FFFFFF', 'backColor': '#7AB800' }, //Negotiation  
  '68': { 'fontColor': '#FFFFFF', 'backColor': '#009AA6' }, //Accomodations 
  '69': { 'fontColor': '#FFFFFF', 'backColor': '#A0A1A3' }, //Modeling/Pricing 
  '70': { 'fontColor': '#FFFFFF', 'backColor': '#B71234' }, //Internal Communications  
  '6': { 'fontColor': '#000000', 'backColor': '#DFE0E0' }, //Peer review  
  '2': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },// Milestone
  '4': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },// Deal Update
  '5': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//New Info
  '7': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Meeting Notes
  '8': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Contract Wording
  '9': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Submission Questions/Comments
  '20': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Claim Update
  '21': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Expense Projection
  '22': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Settlement Details
  '23': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Financial Details
  '24': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Counsel Reports
  '25': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Telephone Memo
  '27': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Status Update Request
  '28': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Acknowledgement Memo
  '29': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Counsel Note
  '30': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Related Claim
  '31': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Coverage Limits Comment
  '40': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Actuarial Review
  '41': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },// Claim Info
  '42': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Deal History
  '43': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Exposure Code Change
  '44': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Indemnity Reserve Change
  '45': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Terms and Conditions Dispute Note
  '47': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Modeller Note
  '48': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Termination Note
  '49': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },// Peer Review non Grs
  '50': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Claim Note
  '51': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Claim query
  '53': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Diary
  '54': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Claim Summary
  '55': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Large Loss Report
  '56': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Closing Note
  '57': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Manager/Supervisor Review
  '58': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//News Article
  '65': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE, 'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Dummy Task for Deal Approval Received / Peer Review
  '1': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE,  'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE },//Call Log
  '3': { 'fontColor': DEFAULT_FONTCOLOUR_FOR_NO_NOTETYPE,  'backColor': DEFAULT_BACKCOLOUR_FOR_NO_NOTETYPE }//Decision

}
export const DOC_TYPE_ICON_CONFIG = {
  'DOC':{'icon': "fa-file-word-o"}, // please refer font-awesome.css to add future icons 
  'DOCX':{'icon': "fa-file-word-o"},
  'XLS':{'icon': "fa-file-excel-o"},
  'XLSX':{'icon': "fa-file-excel-o"},
  'PPT':{'icon': "fa-file-powerpoint-o"},
  'PPTX':{'icon': "fa-file-powerpoint-o"},
  'PDF':{'icon': "fa-file-pdf-o"},
  'MSG':{'icon': "fa-envelope-o"},
  'PNG':{'icon': "fa-file-image-o"},
  'JPEG':{'icon': "fa-file-image-o"}
};
export const DEFAULT_DOC_TYPE_ICON_CONFIG = 'fa-file-text-o';

export const STATUSNAME_API_URL="v1/lookups/dealstatuses";
export const UNDERWRITERS_API_URL="v1/lookups/rolepersons?roles=GlobalRe.Underwriter,GlobalRe.Underwriter Manager";
export const TAs_UAs_API_URL="v1/lookups/rolepersons?roles=GlobalRe.UA/TA,GlobalRe.Property UA/TA";
export const ACTUARIES_API_URL="v1/lookups/rolepersons?roles=GlobalRe.Actuary,GlobalRe.Actuary Manager";
export const MODELERES_API_URL="v1/lookups/rolepersons?roles=GlobalRe.Modeler,GlobalRe.Modeler Manager";


export const USERViEW_API_URL = "v1/userViews?screenname=";
export const USERVIEW_ADD_API_URL = "v1/userViews";
export const USERVIEW_DEFAULTVIEW_API_URL ="v1/userViews/";
export const USER_VIEW_SCREEN_NAME ="GRS.UW_Workbench";
export const USER_VIEW_ALL_SUBMISSIONS = "All Submissions";
export const USER_VIEW_MY_SUBMISSIONS = "My Submissions";

export const CHECKLIST = "checklists"




export class EntityType {
  static none = "None";
  static Deals = "Deals";
  static DealStatusSummaries = "DealStatusSummaries";
  static Subdivision = "Subdivision";
  static Note = "Note";
  static Document = "Document";
  static KeyNonKeyDocument = "KeyNonKeyDocument";
}
export class FilterType {
  static All = "None";
  static PastInception = "PastInception";
  static Within30Days = "Within30Days";
  static Over30Days  = "Over30Days";

}
export class HttpActionType {
  static get = "GET";
  static post = "POST";
  static put = "PUT";
  static delete = "DELETE";
  static patch = "PATCH";
  static option = "OPTIONS";

}
export class ApiLinkType{
  static entity = "Entity";
  static self = "Self";
  static subEntity = "SubEntity";
  static action = "Action";
  static referenceEntity = "ReferenceEntity";
}

export class GlobalEventType {
  static notification:string = "notification";
  static info: string = "info";
  static error: string = "error";
  static apiError: string = "apiError";
  static warning: string = "warning";
  static success: string = "success";

  static Deal_saved="dealSaved";
  static DownloadError = "There are no documents in DMS";
  static Reset_AgGrid = "reset ag grid filters";
  static Close_View_Dropdown  = "Close view dropdown";
  static Ag_Grid_Clear_Selection = "Clear ag grid selection";
  static Ag_Grid_Retain_Selection = "Retain ag grid selection";
  static Ag_Grid_Row_Selection = "Retain ag grid row selection";
  static Ag_Grid_Retain_Focus = "Retain ag grid focus";

  static Restrict_from_Closing = "Restrict quick edit from closing";
}

export class GlobalNotificationMessageType{
  static UNDEFINED_TYPE = 'Undefined'; 
  static DEBUG_TYPE = 'Debug';
  static INFO_TYPE = 'Info';
  static WARNING_TYPE = 'Warn';
  static ERROR_TYPE = 'Error';
  static FATAL_TYPE = 'Fatal';
}
export const NOTIFICATION_MESSAGE_TIMER_VALUE = 7000;
