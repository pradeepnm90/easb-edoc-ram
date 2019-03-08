import { Injectable } from "@angular/core";
import { Effect, Actions } from "@ngrx/effects";
import { of } from "rxjs/observable/of";
import {
  LoadKeyNonKeyDocumentsSuccess,
  LoadKeyNonKeyDocumentsFail,
  LOAD_KEY_NON_KEY_DOCUMENTS,
  LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL,
  LoadKeyNonKeyDocumentsIndividualSuccess,
  DOWNLOAD_KEY_DOCS,
  LoadKeyDocsDownloadSuccess,
  LoadKeyDocsDownloadFail
} from "../actions/deals/deal-key-nonKey-documents.actions";
import { CoreService } from "../shared/services/core.service";
//import { DocumentService } from "../shared/services/document.service";
import { switchMap } from "rxjs/operator/switchMap";
import { MatSnackBar } from "@angular/material";

@Injectable()
export class KeyNonKeyDocumentEffects {
  constructor(private _actions: Actions,public snackBar: MatSnackBar,
     private _coreService: CoreService) {}
  @Effect()
  getKeyNonKeyDocuments$ = this._actions
    .ofType(LOAD_KEY_NON_KEY_DOCUMENTS)
    .switchMap(payload => {
      return this._coreService
        .invokeGetListResultApi(payload["payload"])
        .map(keyNonKeyDocuments => {
          return new LoadKeyNonKeyDocumentsSuccess(
            keyNonKeyDocuments["results"]
          );
        })
        .catch(err => {
          return of(new LoadKeyNonKeyDocumentsFail(err["error"]));
        });
    });
    @Effect()
    getKeyNonKeyDocumentsIndividual$ = this._actions
    .ofType(LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL)
    .switchMap(payload => {
      return this._coreService
        .invokeGetListArrayResultApiList(payload["payload"])
        .map(keyNonKeyDocuments => {
          return new LoadKeyNonKeyDocumentsIndividualSuccess(
            keyNonKeyDocuments
          );
        })
        .catch(err => {
          return of(new LoadKeyNonKeyDocumentsIndividualSuccess({}));
        });
    });
    @Effect()
    downnloadKeyDocs$ = this._actions
    .ofType(DOWNLOAD_KEY_DOCS)
    .switchMap(payload => {
      return this._coreService
        .invokeGetListResultApi(payload["payload"])
        .map(keyNonKeyDocuments => {
          return new LoadKeyDocsDownloadSuccess(
            keyNonKeyDocuments
          );
        })
        .catch(err => {
         this.openSnackBar('File not available on document management system !!', '');
          return of(new LoadKeyDocsDownloadFail({}));
        });
    });

    openSnackBar(message: string, action: string) {
      this.snackBar.open(message, action, {
        duration: 5000,
      });
    }
}

