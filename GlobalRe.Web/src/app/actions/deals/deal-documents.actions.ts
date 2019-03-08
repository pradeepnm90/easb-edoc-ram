import {Action} from '@ngrx/store';

export const LOAD_DOCUMENTS = '[Documents] Load Documents';
export const LOAD_LOAD_DOCUMENTS_FAIL = '[Documents] Load Documents fail';
export const LOAD_LOAD_DOCUMENTS_SUCCESS = '[Documents] Load Documents success';
export const CLEAR_LOAD_DOCUMENTS = '[Documents] Clear Documents';
export const LOAD_DOCUMENT_INDIVIDUAL =
  "[Documents] Individual Documents";
  export const LOAD_DOCUMENT_INDIVIDUAL_SUCCESS =
  "[Documents] Individual Documents Success";
  export const LOAD_DOCUMENT_INDIVIDUAL_FAIL =
  "[Documents] Individual Documents Success";
  export const DOWNLOAD_DOCS =
  "[Documents] Download docs request";
  export const DOWNLOAD_DOCS_SUCCESS =
  "[Documents] Download docs success";
  export const DOWNLOAD_DOCS_FAIL =
  "[Documents] Download docs Fail";
  export const DOWNLOAD_PAGE =
  "[KeyNonKeyDocuments] Download page request";
  export const DOWNLOAD_PAGE_SUCCESS =
  "[KeyNonKeyDocuments] Download page success";
  export const DOWNLOAD_PAGE_FAIL =
  "[KeyNonKeyDocuments] Download page Fail";

export class LoadDocuments implements Action {
    readonly type  = LOAD_DOCUMENTS;
    constructor(public payload: any) {}
}
export class LoadDocumentsFail implements Action {
    readonly type  = LOAD_LOAD_DOCUMENTS_FAIL;
    constructor(public payload: any){}
}
export class LoadDocumentsSuccess implements Action {
    readonly type  = LOAD_LOAD_DOCUMENTS_SUCCESS;
    constructor(public payload: any){}
}
export class ClearDocuments implements Action {
    readonly type  = CLEAR_LOAD_DOCUMENTS;
    constructor(){}
}
export class LoadDocumentsIndividual implements Action {
    readonly type = LOAD_DOCUMENT_INDIVIDUAL;
    constructor(public payload: any) {}
  }
  export class LoadDocumentsIndividualSuccess implements Action {
    readonly type = LOAD_DOCUMENT_INDIVIDUAL_SUCCESS;
    constructor(public payload: any[]) {}
  }
  export class LoadDocumentsIndividualFail implements Action {
    readonly type = LOAD_DOCUMENT_INDIVIDUAL_FAIL;
    constructor(public payload: any) {}
  }

  export class LoadDocsDownload implements Action {
    readonly type = DOWNLOAD_DOCS;
    constructor(public payload: any) {}
  }
  export class LoadDocsDownloadSuccess implements Action {
    readonly type = DOWNLOAD_DOCS_SUCCESS;
    constructor(public payload: any) {}
  }
  export class LoadDocsDownloadFail implements Action {
    readonly type = DOWNLOAD_DOCS_FAIL;
    constructor(public payload: any) {}
  }
  export class LoadPageDownload implements Action {
  readonly type = DOWNLOAD_PAGE
  constructor(public payload: any) {}
}
export class LoadPageDownloadSuccess implements Action {
  readonly type = DOWNLOAD_PAGE_SUCCESS;
  constructor(public payload: any) {}
}
export class LoadKeyPageDownloadFail implements Action {
  readonly type = DOWNLOAD_PAGE_FAIL;
  constructor(public payload: any) {}
}

export type DocumentsAction = LoadDocuments
 | LoadDocumentsFail
 | LoadDocumentsSuccess
 | LoadDocumentsIndividualSuccess
 | LoadDocumentsIndividual
 | ClearDocuments
 | LoadDocsDownload
 | LoadDocsDownloadSuccess
 | LoadDocsDownloadFail
 | LoadPageDownload
  | LoadPageDownloadSuccess
  | LoadKeyPageDownloadFail;