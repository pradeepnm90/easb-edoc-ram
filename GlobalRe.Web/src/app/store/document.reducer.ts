import * as fromDocuments from "../actions/deals/deal-documents.actions";
import { Documents } from "../models/document";
import {saveAs as importedSaveAs} from "file-saver";
import { DEFAULT_DOC_TYPE_ICON_CONFIG, DOC_TYPE_ICON_CONFIG } from "../app.config";

export interface DocumentState {
  currentDocumentList: any[];
  documentDetails: any;
  // EntityApiData<Note>[] | null;
}
export const initialState: DocumentState = {
    currentDocumentList: [],
	documentDetails: {}
};

export function reducer(
  state = initialState,
  action: fromDocuments.DocumentsAction
): DocumentState {
  switch (action.type) {
    case fromDocuments.LOAD_LOAD_DOCUMENTS_SUCCESS: {
      const currentDocumentList = action["payload"];
      return {
        currentDocumentList: currentDocumentList,
		documentDetails: {}
      };
    }
    case fromDocuments.LOAD_LOAD_DOCUMENTS_FAIL: {
      return {
        currentDocumentList: [],
		documentDetails: {}
      };
    }
    case fromDocuments.CLEAR_LOAD_DOCUMENTS: {
      return {
        currentDocumentList: [],
		documentDetails: {}
      };
    }
    case fromDocuments.LOAD_DOCUMENT_INDIVIDUAL_SUCCESS: {
      let docoumentDetailCollection = action["payload"];
      let tempDoc = state.currentDocumentList.map(item =>{
        item["totalPageCount"] = 0;
        if(item["Contents"].length >0){
          let matchedCollection: any;
          for(let i =0; i< item["Contents"].length; i++ ){
             matchedCollection = docoumentDetailCollection.filter(d =>d.ID === item["Contents"][i].ID);
            item["Contents"][i]['docPageCount'] = 0;
            item["Contents"][i]["pages"] = []; let pages = [];
            if(matchedCollection.length > 0){
              item["Contents"][i]["docType"] = matchedCollection[0].DocumentType;
              item["Contents"][i]['docPageCount'] = matchedCollection[0].Files.length || 0;
              item["totalPageCount"] += (matchedCollection[0].Files.length || 0);
            }
            if(matchedCollection[0].Files.length >0){
              for(let page in matchedCollection[0].Files){

                let dynamicfileType = matchedCollection[0].Files[page].FileName.split(".")
                let detectFileType = ()=>{
                  if( dynamicfileType.length === 1 || ( dynamicfileType[0] === "" && dynamicfileType.length === 2 ) ) {
                      return DEFAULT_DOC_TYPE_ICON_CONFIG;
                  }
                  return dynamicfileType.pop();
                  };
                const getDynamicFileType = detectFileType();
                let fileType = DEFAULT_DOC_TYPE_ICON_CONFIG;
                if (getDynamicFileType !=DEFAULT_DOC_TYPE_ICON_CONFIG){
                  fileType = DOC_TYPE_ICON_CONFIG[getDynamicFileType.toUpperCase()]?
                  DOC_TYPE_ICON_CONFIG[getDynamicFileType.toUpperCase()].icon:
                  DEFAULT_DOC_TYPE_ICON_CONFIG;
                }
                else{
                  fileType = DEFAULT_DOC_TYPE_ICON_CONFIG;
                }
                let fileAtrributes = {
                  'fileName': matchedCollection[0].Files[page].FileName,
                  'fileType': fileType
                }
                pages.push(fileAtrributes);
              }
              item["Contents"][i]["pages"] = pages;
            }
          }
        }
        return item;
      })
      return {
        currentDocumentList: tempDoc,
		documentDetails: {}
      };
    } 
    case fromDocuments.LOAD_DOCUMENT_INDIVIDUAL_FAIL : {
      const gerErrorInfo = action["payload"];
      console.log(gerErrorInfo, "gerErrorInfo");
    }
    case fromDocuments.DOWNLOAD_DOCS_SUCCESS: {
      const documentDetails = action["payload"];
      if(documentDetails.Files){
        let pageData = documentDetails.Files[0].FileBase64;
        let binary = atob(pageData);
        let testArray = [];
        for (let  i = 0; i < binary.length; i++) {
          testArray.push(binary.charCodeAt(i));
       }
          let byteArray = new Uint8Array(testArray);
          let blob = new Blob([byteArray], {type: "octet/stream"});
          importedSaveAs(blob, documentDetails.Files[0].FileName);
      }
		return {
          currentDocumentList: state.currentDocumentList,
          documentDetails: documentDetails
        };
    }
    case fromDocuments.DOWNLOAD_DOCS_FAIL: {
      return{
      currentDocumentList: state.currentDocumentList,
      documentDetails: {}
      }
    }
    default: {
      return state;
    }
  }
}
export const getDocumentList = (state: DocumentState) => state.currentDocumentList;
