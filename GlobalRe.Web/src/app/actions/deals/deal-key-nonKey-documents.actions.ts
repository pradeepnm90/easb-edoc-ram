import { Action } from "@ngrx/store";

export const LOAD_KEY_NON_KEY_DOCUMENTS =
  "[KeyNonKeyDocuments] Load KeyNonKeyDocuments";
export const LOAD_KEY_NON_KEY_DOCUMENTS_FAIL =
  "[KeyNonKeyDocuments] Load KeyNonKeyDocuments fail";
export const LOAD_KEY_NON_KEY_DOCUMENTS_SUCCESS =
  "[KeyNonKeyDocuments] Load KeyNonKeyDocuments success";
export const CLEAR_LOAD_KEY_NON_KEY_DOCUMENTS =
  "[KeyNonKeyDocuments] Clear KeyNonKeyDocuments";
  export const LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL =
  "[KeyNonKeyDocuments] Load KeyNonKeyDocuments Individual";
  export const LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL_SUCCESS =
  "[KeyNonKeyDocuments] Load KeyNonKeyDocuments Individual Success"; 
  export const DOWNLOAD_KEY_DOCS =
  "[KeyNonKeyDocuments] Download docs request";
  export const DOWNLOAD_KEY_DOCS_SUCCESS =
  "[KeyNonKeyDocuments] Download docs success";
  export const DOWNLOAD_KEY_DOCS_FAIL =
  "[KeyNonKeyDocuments] Download docs Fail";
  export const DOWNLOAD_KEY_PAGE =
  "[KeyNonKeyDocuments] Download key page request";
  export const DOWNLOAD_KEY_PAGE_SUCCESS =
  "[KeyNonKeyDocuments] Download key  page success";
  export const DOWNLOAD_KEY_PAGE_FAIL =
  "[KeyNonKeyDocuments] Download key  page Fail";
  export const CLEAR_PAGE_INFO =
  "[KeyNonKeyDocuments] clear page info";

export class LoadKeyNonKeyDocuments implements Action {
  readonly type = LOAD_KEY_NON_KEY_DOCUMENTS;
  constructor(public payload: any) {}
}
export class LoadKeyNonKeyDocumentsFail implements Action {
  readonly type = LOAD_KEY_NON_KEY_DOCUMENTS_FAIL;
  constructor(public payload: any) {}
}
export class LoadKeyNonKeyDocumentsSuccess implements Action {
  readonly type = LOAD_KEY_NON_KEY_DOCUMENTS_SUCCESS;
  constructor(public payload: any) {}
}
export class ClearKeyNonKeyDocuments implements Action {
  readonly type = CLEAR_LOAD_KEY_NON_KEY_DOCUMENTS;
  constructor() {}
}
export class LoadKeyNonKeyDocumentsIndividual implements Action {
  readonly type = LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL;
  constructor(public payload: any) {}
}
export class LoadKeyNonKeyDocumentsIndividualSuccess implements Action {
  readonly type = LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL_SUCCESS;
  constructor(public payload: any) {}
}
export class LoadKeyDocsDownload implements Action {
  readonly type = DOWNLOAD_KEY_DOCS;
  constructor(public payload: any) {}
}
export class LoadKeyDocsDownloadSuccess implements Action {
  readonly type = DOWNLOAD_KEY_DOCS_SUCCESS;
  constructor(public payload: any) {}
}
export class LoadKeyDocsDownloadFail implements Action {
  readonly type = DOWNLOAD_KEY_DOCS_FAIL;
  constructor(public payload: any) {}
}
export class LoadKeyPageDownload implements Action {
  readonly type = DOWNLOAD_KEY_PAGE
  constructor(public payload: any) {}
}
export class LoadKeyPageDownloadSuccess implements Action {
  readonly type = DOWNLOAD_KEY_PAGE_SUCCESS;
  constructor(public payload: any) {}
}
export class LoadKeyKeyPageDownloadFail implements Action {
  readonly type = DOWNLOAD_KEY_PAGE_FAIL;
  constructor(public payload: any) {}
}
export class ClearPageInfo implements Action {
  readonly type = CLEAR_PAGE_INFO;
  constructor() {}
}


export type KeyNonKeyDocumentsAction =
  | LoadKeyNonKeyDocuments
  | LoadKeyNonKeyDocumentsFail
  | LoadKeyNonKeyDocumentsSuccess
  | ClearKeyNonKeyDocuments
  | LoadKeyNonKeyDocumentsIndividual
  | LoadKeyNonKeyDocumentsIndividualSuccess
  | LoadKeyDocsDownload
  | LoadKeyDocsDownloadSuccess
  | LoadKeyDocsDownloadFail
  | LoadKeyPageDownload
  | LoadKeyPageDownloadSuccess
| ClearPageInfo
  | LoadKeyKeyPageDownloadFail;
