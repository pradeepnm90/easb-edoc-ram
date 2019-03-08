import { Component, Input } from "@angular/core";
import { KeyNonKeyDocument } from "../../../../models/key-non-key-document";
import { Store } from "@ngrx/store";
import * as fromRoot from "../../../../store/index";
import { FileUploader } from "ng2-file-upload";
import * as fromDocument from "../../../../actions/deals/deal-documents.actions";
import * as fromKeyNonKeyDocument from "../../../../actions/deals/deal-key-nonKey-documents.actions";
import { ENTITY_TYPE_DEALS, KEYNONKEY_URL, DOCUMENTS_URL } from "../../../../app.config";

@Component({
  selector: "deal-key-non-key-documents",
  templateUrl: "./deal-documents-list.html",
  styleUrls: ["deal-document.scss"]
})
export class DealKeyNonKeyDocumentListComponent {
  currentDealInfo: any;
  uploader: FileUploader = new FileUploader({ url: "" });
  constructor(private _store: Store<fromRoot.AppState>) {}
  @Input() keyNonKeyDocumentList: any;
  @Input() keyDocumentList: any;
  whichView: string = "Tree";
  defaultView: string = "Key Documents";
  isKeyView: boolean = true;
  isTreeView: boolean = false;
  dealID: any;
  renderTreeViewOrNot: boolean = false;
  @Input() set currentDealData(val) {
    this.currentDealInfo = {
    grsId: val.data.dealNumber
     //grsId: 1383315
     // hardcoded deal number  for sufficient record details
    };
    this._store.dispatch(
      new fromDocument.LoadDocuments(
        "v1/" +
          ENTITY_TYPE_DEALS +
          "/" +
          this.currentDealInfo.grsId +
          DOCUMENTS_URL
      )
    );
    this._store.dispatch(
      new fromKeyNonKeyDocument.LoadKeyNonKeyDocuments(
        "v1/" + ENTITY_TYPE_DEALS + "/" + this.currentDealInfo.grsId + KEYNONKEY_URL + "true"
      )
    );
  }
  flagToRestrictServiceCall: boolean = false;
  
  ngOnInit() {
    this.isTreeView = false;
    let urlArray : string[] = [];
    this._store.select(fromRoot.getKeyNonKeyDocumentList).subscribe(val => {
      this.keyNonKeyDocumentList = val.filter(item => {
        return !item.data.docid;
      });
      this.keyNonKeyDocumentList = this.keyNonKeyDocumentList.map(valItem => {
     
      this.keyDocumentList = val.filter(item => {
        return item.data.docid;
      });
      if(this.keyDocumentList.length > 0 && !this.flagToRestrictServiceCall){    ///DOCUMENT LEVEL
        this.flagToRestrictServiceCall = true;

        for(let i=0;i< this.keyDocumentList.length; i++){

          urlArray.push("v1/" + ENTITY_TYPE_DEALS + "/" + this.currentDealInfo.grsId +
          DOCUMENTS_URL + "/" + this.keyDocumentList[i].data.docid + "/false");
        }
          this._store.dispatch(
            new fromKeyNonKeyDocument.LoadKeyNonKeyDocumentsIndividual(
              urlArray 
            )
          );
        }
      setTimeout(()=>{
      for(let i=0; i< this.keyNonKeyDocumentList.length; i++){
        let parentPageCount = 0;
        for(let j=0;j<this.keyDocumentList.length;j++){
          if(this.keyNonKeyDocumentList[i].data.docType == this.keyDocumentList[j].data.docType){
            parentPageCount = parentPageCount + this.keyDocumentList[j].data.pageCount || 0;
            this.keyDocumentList[j]["data"]["grsId"] = this.currentDealInfo.grsId;
          }
        }
        this.keyNonKeyDocumentList[i].data['pageCount'] = parentPageCount;
        this.renderTreeViewOrNot = true;


      } },1000);
      console.log(this.keyNonKeyDocumentList, "this.keyNonKeyDocumentList");
      return Object.assign(new KeyNonKeyDocument(), valItem);
    });
    });
  }
  ngOnDestroy() {
    this._store.dispatch(new fromKeyNonKeyDocument.ClearKeyNonKeyDocuments());
  }

  toggleView($event) {
    if ($event) {
      $event.preventDefault();
    }
    if (this.isKeyView) {
      this.isKeyView = false;
      this.isTreeView = true;
      this.whichView = "Key Documents";
      this.defaultView = "Tree" ;     
    } else {
      this.isKeyView = true;
      this.whichView = "Tree";
      this.isTreeView = false;
      this.defaultView = "Key Documents";
    }
  }
}
