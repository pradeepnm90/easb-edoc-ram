import { Component, Input } from "@angular/core";
import { Documents } from "../../../../models/document";
import { Store } from "@ngrx/store";
import * as fromRoot from "../../../../store/index";
import { FileUploader } from "ng2-file-upload";
import * as fromDocument from "../../../../actions/deals/deal-documents.actions";
import { ENTITY_TYPE_DEALS, DOCUMENTS_URL } from "../../../../app.config";
import { t } from "@angular/core/src/render3";

@Component({
  selector: "deal-documents",
  templateUrl: "./deal-documents-list.html",
  styleUrls: ["deal-document.scss"]
})
export class DealDocumentListComponent {
  currentDealInfo: any;
  keyListContainer: any;
  nonkeyListContainer: any;
  keyNonKeyCombined: any[];
  isKeyDoc: boolean = false;
  isnonKeyDoc: boolean = false;
  uploader: FileUploader = new FileUploader({ url: "" });
  constructor(private _store: Store<fromRoot.AppState>) {}
  @Input() docTypeList$: Array<Documents>;
  @Input() set currentDealData(val) {
    this.currentDealInfo = {
      grsId: val
    };
    }
  @Input() set keyList(val){
    console.log(val, "KEYLIST")
    this.keyListContainer = val;
  }
  
  @Input() set nonKeyList(val){
    console.log(val, "NONKEYLIST")
    this.nonkeyListContainer = val;
  }
  ngOnInit() {
    console.log("INSIDE DEAL DOC");
    this.renderTreeView();
  }
  ngOnDestroy() {
   // this._store.dispatch(new fromDocument.ClearDocuments());
  }

  renderTreeView(){
    this.keyNonKeyCombined  = this.keyListContainer.concat(this.nonkeyListContainer);
    let urlArray : string[] = [];
    this._store.select(fromRoot.getDocumentList).subscribe(val => {
      this.docTypeList$ = val.map(item => {
        let tempObj = Object.assign(new Documents(), item);

        if(item["Contents"].length > 0 ){
          for(let i=0; i < item["Contents"].length; i++){
          urlArray.push( "v1/" + ENTITY_TYPE_DEALS + "/" + this.currentDealInfo.grsId +
           DOCUMENTS_URL + "/" + item["Contents"][i].ID + "/false");
          }
          this._store.dispatch(
            new fromDocument.LoadDocumentsIndividual(
              urlArray
          ));
            }
            else{
              tempObj["totalPageCount"] = 0;
            }
           setTimeout(()=>{
            this._store.select(fromRoot.getDocumentList).subscribe(val => {
              this.docTypeList$ = val.map(item => {
                let tempObj = Object.assign(new Documents(), item);
             //tempObj["nonKeyDoc"] = false;
             if(item["Contents"].length > 0 ){
              for(let i=0; i < item["Contents"].length; i++){
               
                if(item["Contents"][i]["docType"]){
                  item["Contents"][i]["nonKeyDoc"] = false;
                  item["Contents"][i]["KeyDoc"] = false;
                  let TreedocType = item["Contents"][i]["docType"];
                  let TreedocID = item["Contents"][i]["ID"];
                  let keyDocFilterArray = this.keyNonKeyCombined.filter(d =>{
                    return ((d.data.docType === TreedocType) && (d.data.docid === TreedocID))
                  }); 
                  let nonKeyDocFilteredArray = this.keyNonKeyCombined.filter(d => d.data.docType === TreedocType);
                  if (keyDocFilterArray.length > 0) {
                    nonKeyDocFilteredArray = [];
                    item["Contents"][i]["nonKeyDoc"] = false;
                    item["Contents"][i]["KeyDoc"] = true;
                    
                  }
                  if (nonKeyDocFilteredArray.length > 0 ){
                    keyDocFilterArray = [];
                    item["Contents"][i]["KeyDoc"] = false;
                    item["Contents"][i]["nonKeyDoc"] = true;
                  }
                }
              }
            }
            else{
              tempObj["totalPageCount"] = 0;
            }
              return tempObj;
            });
            });
       
         }, 1000);
      
        return tempObj;
      });
    }).unsubscribe();
    
    console.log(this.docTypeList$, "this.docTypeList$");
  }
}
