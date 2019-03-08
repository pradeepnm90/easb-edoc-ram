import { Component, Input } from "@angular/core";
import { Store } from "@ngrx/store";
import * as fromRoot from "../../../../store/index";
import { FileUploader } from "ng2-file-upload";
import * as fromDocument from "../../../../actions/deals/deal-documents.actions";
import { ENTITY_TYPE_DEALS, DOCUMENTS_URL, PAGE_URL } from "../../../../app.config";

@Component({
  selector: "deal-document",
  template: `
  <div class="list-item">
  <i  *ngIf= "documentDetail.nonKeyDoc" class="fa fa-star-o green pad-7-r pad-10-l" aria-hidden="true"></i>
  <i  *ngIf= "documentDetail.KeyDoc && documentDetail.nonKeyDoc===false" class="fa fa-star green pad-7-r pad-10-l" aria-hidden="true"></i>
  <i  *ngIf= "documentDetail.KeyDoc===false && documentDetail.nonKeyDoc===false" class="fa fa-star white pad-7-r pad-10-l" aria-hidden="true"></i>
  <i
     class="fa fa-file-text-o txtRed    pad-7-right txtSize18"
     aria-hidden="true"
     ></i>
  <span class="txtSize13 fontOpenSans ">{{documentDetail.Name | truncateDocTextCharacters: 54}}</span>
  <span class="txtSize13 fontOpenSans ">&nbsp;(</span>
  <span class="txtSize13 fontOpenSans  " >
  {{documentDetail.docPageCount === 'undefined' ? '0' : documentDetail.docPageCount}}
  </span>
  <ng-template [ngIf]="documentDetail.docPageCount > 1" [ngIfElse]="pageLesseathan1">
  <span class="txtSize13 fontOpenSans  ">pages)</span>
  </ng-template>
  <ng-template #pageLesseathan1>
     <span class="txtSize13 fontOpenSans  " >page)</span>
  </ng-template>
  <i class="iconButton fa  txtSize18 floatR pad-10-r cusrorPointer chevronDoc lineHt2_2"
  [ngClass]="docExpanded ? 'fa fa-chevron-circle-down': 'fa fa-chevron-circle-right'"
  (click)="toggleDocType($event)"aria-hidden="true"></i>
  <div *ngIf="documentDetail.pages.length > 0 && docExpanded">
     <ul class="doc-list-container pad-0-l">
        <li  class="txtSize13 fontOpenSans  doc-list" 
        *ngFor=" let page of documentDetail.pages; let i = index" [attr.data-index]="i">
        <div class= "pad-25-l cusrorPointer list-item"(dblclick)="openDocs(i,$event)">
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
export class DealDocumentComponent {
  displayDocItem: boolean;
  documentDetail: any;
  totalDocCount: number;
  docExpanded: boolean = false;
  dealNumber: any;
  detailedDocumentDetail: any;
  constructor(private _store: Store<fromRoot.AppState>) {}

  @Input() set document(val) {
    this.documentDetail = val;
  }
  @Input() set docdetail(val) {
    this.detailedDocumentDetail = val;
  }
  @Input() set documentDealBelongsTo(val){
    this.dealNumber = val;
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
      new fromDocument.LoadDocsDownload(
        "v1/" + ENTITY_TYPE_DEALS + "/" + this.dealNumber + DOCUMENTS_URL + "/" + this.documentDetail.ID + PAGE_URL+"/"+pagetoFetch
      )
    );
  }

  ngOnInit() {
 
  }
}
