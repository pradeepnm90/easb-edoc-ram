import { Injectable } from "@angular/core";
import { Effect, Actions } from "@ngrx/effects";
import { of } from "rxjs/observable/of";
import {
  LoadDocumentsSuccess,
  LOAD_DOCUMENTS,
  LoadDocumentsFail,
  LOAD_DOCUMENT_INDIVIDUAL,
  LoadDocumentsIndividualSuccess,
  LoadDocumentsIndividualFail,
  DOWNLOAD_DOCS,
  LoadDocsDownloadSuccess,
  LoadDocsDownloadFail
} from "../actions/deals/deal-documents.actions";
import { CoreService } from "../shared/services/core.service";
import { DocumentService } from "../shared/services/document.service";
import { switchMap } from "rxjs/operator/switchMap";
import { MatSnackBar } from "@angular/material";

@Injectable()
export class DocumentEffects {
  constructor(
    private _actions: Actions,
    public snackBar: MatSnackBar,
    private _coreService: CoreService,
    private _documentService: DocumentService
  ) {}

  @Effect()
  getDocuments$ = this._actions.ofType(LOAD_DOCUMENTS).switchMap(payload => {
    return this._coreService
      .invokeGetListResultApi(payload["payload"])
      .map(docs => {
        return new LoadDocumentsSuccess(docs[0].Contents);
      })
      .catch(err => {
        return of(new LoadDocumentsFail(err));
      });
  });
  @Effect()
  getDocumentsIndividual$ = this._actions
  .ofType(LOAD_DOCUMENT_INDIVIDUAL)
  .switchMap(payload => {
    return this._coreService
      .invokeGetListArrayResultApiList(payload["payload"])
      .map(docs => {
        return new LoadDocumentsIndividualSuccess(
          docs
        );
      })
      .catch(err => {
        return of(new LoadDocumentsIndividualFail(err));
      });
  });
  @Effect()
    downnloadDocs$ = this._actions
    .ofType(DOWNLOAD_DOCS)
    .switchMap(payload => {
      return this._coreService
        .invokeGetListResultApi(payload["payload"])
        .map(keyNonKeyDocuments => {
          return new LoadDocsDownloadSuccess(
            keyNonKeyDocuments
          );
        })
        .catch(err => {
          this.openSnackBar('File not available on document management system', '');
          return of(new LoadDocsDownloadFail({}));
        });
    });
    openSnackBar(message: string, action: string) {
      this.snackBar.open(message, action, {
        duration: 5000,
      });
    }
}
