import { Component, Input } from "@angular/core";
import { Store } from "@ngrx/store";
import * as fromRoot from "../../../../store/index";
import { FileUploader } from "ng2-file-upload";
import * as fromKeyNonKeyDocument from "../../../../actions/deals/deal-key-nonKey-documents.actions";
import { ENTITY_TYPE_DEALS, DOCUMENTS_URL, PAGE_URL } from "../../../../app.config";
import { MatSnackBar } from "@angular/material";

@Component({
  selector: "deal-key-document",
  template: `
  <div class="list-item">
   <i class="fa fa-star green pad-7-r pad-10-l" aria-hidden="true"></i>
   <i
      class="fa fa-file-text-o txtRed    pad-7-right txtSize18"
      aria-hidden="true"
      ></i>
   <span class="txtSize13 fontOpenSans  " >{{documentDetail.docName | truncateDocTextCharacters: 54}}</span>
   <span class="txtSize13 fontOpenSans  " >&nbsp;(</span>
   <span class="txtSize13 fontOpenSans ">
   {{documentDetail.pageCount}}
   </span>
   <ng-template [ngIf]="documentDetail.pageCount > 1" [ngIfElse]="pageLesseathan1">
   <span class="txtSize13 fontOpenSans  cusrorPointer">pages)</span>
   </ng-template>
   <ng-template #pageLesseathan1>
      <span class="txtSize13 fontOpenSans  cusrorPointer" >page)</span>
   </ng-template>
   <i class="iconButton fa  txtSize18 floatR pad-10-r cusrorPointer chevronDoc  lineHt2_2"
   [ngClass]="docExpanded ? 'fa fa-chevron-circle-down': 'fa fa-chevron-circle-right'"
   (click)="toggleDocType($event)"aria-hidden="true"></i>
   <div *ngIf="documentDetail.pages.length > 0 && docExpanded">
      <ul class="doc-list-container pad-0-l">
         <li  class="txtSize13 fontOpenSans  doc-list"
         *ngFor=" let page of documentDetail.pages; let i = index" [attr.data-index]="i">
         <div class= "pad-25-l cusrorPointer list-item" (dblclick)="openDocs(i,$event)">
         <i
         class=" txtRed  pad-10-l  pad-7-right txtSize18 fa"
      aria-hidden="true"
      [ngClass] = page.fileType
      ></i>
         {{page.fileName | truncateDocTextCharacters: 64}}
         </div>
         </li>
      </ul>
   </div>
</div>
  `,
  styleUrls: ["deal-document.scss"]
})
export class DealKeyDocumentComponent {
  displayDocItem: boolean;
  docExpanded: boolean= false;
  documentDetail: any;
  constructor(public snackBar: MatSnackBar,private _store: Store<fromRoot.AppState>) {}
  @Input() set document(val) {
    this.documentDetail = val.data;
  /*   if(!this.documentDetail.docName)  FEW DMS DOCUMENT DOES NOT HAVE NAMES!!!!
    this.documentDetail.docName = "Un named document!"; */
  }
 
  toggleDocType($event){
    if ($event) {
      $event.preventDefault();
    }
    this.docExpanded = !this.docExpanded;
  }
  openDocs(pageIndex, $event){
    if ($event) {
      $event.preventDefault();
    }
    let pagetoFetch = pageIndex+1;
   this._store.dispatch(
    new fromKeyNonKeyDocument.LoadKeyDocsDownload(
      "v1/" + ENTITY_TYPE_DEALS + "/" + this.documentDetail.grsId + DOCUMENTS_URL + "/" + this.documentDetail.docid + PAGE_URL+"/"+pagetoFetch
    )
  );
  }
  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
  ngOnInit() {
  }

  ngOnDestroy() {
     this._store.dispatch(new fromKeyNonKeyDocument.ClearPageInfo());
  }
}
