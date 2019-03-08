import * as fromKeyNonKeyDocuments from "../actions/deals/deal-key-nonKey-documents.actions";
import { EntityApiData } from "../shared/models/entity-api-data";
import { KeyNonKeyDocument } from "../models/key-non-key-document";
import {saveAs as importedSaveAs} from "file-saver";
import { DEFAULT_DOC_TYPE_ICON_CONFIG, DOC_TYPE_ICON_CONFIG } from "../app.config";

export interface KeyNonKeyDocumentState {
  currentKeyNonKeyDocumentList: EntityApiData<KeyNonKeyDocument>[] | null;
  documentDetails: any;
  alldocCollection: any;
}
export const initialState: KeyNonKeyDocumentState = {
  currentKeyNonKeyDocumentList: [],
  documentDetails :{},
  alldocCollection: []
};

export function reducer(
  state = initialState,
  action: fromKeyNonKeyDocuments.KeyNonKeyDocumentsAction
): KeyNonKeyDocumentState {
  switch (action.type) {
    case fromKeyNonKeyDocuments.LOAD_KEY_NON_KEY_DOCUMENTS_SUCCESS: {
      const currentKeyNonKeyDocumentList = action["payload"];
      return {
        currentKeyNonKeyDocumentList: currentKeyNonKeyDocumentList,
        documentDetails: {},
        alldocCollection: []
      };
    }
    case fromKeyNonKeyDocuments.LOAD_KEY_NON_KEY_DOCUMENTS_FAIL: {
      return {
        currentKeyNonKeyDocumentList: [],
        documentDetails: {},
        alldocCollection: []
      };
    }
    case fromKeyNonKeyDocuments.CLEAR_LOAD_KEY_NON_KEY_DOCUMENTS: {
      return {
        currentKeyNonKeyDocumentList: [],
        documentDetails: {},
        alldocCollection: []
      };
    }
    case fromKeyNonKeyDocuments.LOAD_KEY_NON_KEY_DOCUMENT_INDIVIDUAL_SUCCESS: {
      const individualDocData = action["payload"];
      let tempDoc  = state.currentKeyNonKeyDocumentList.map(item => {   //currentKeyNonKeyList docid + non docID
        for(let i = 0; i <individualDocData.length; i++){
          if(item.data.docid == individualDocData[i].ID){
            item.data['pages'] = [];let pages = [];
            item.data['pageCount'] = individualDocData[i].Files.length;
            if(individualDocData[i].Files.length >0){
              for(let page in individualDocData[i].Files){
                let dynamicfileType = individualDocData[i].Files[page].FileName.split(".")
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
                  'fileName': individualDocData[i].Files[page].FileName,
                  'fileType': fileType
                }
                pages.push(fileAtrributes);
              }
              item.data['pages'] = pages;
            }
          }
        }
        return item; 
      });
      return {
        currentKeyNonKeyDocumentList: tempDoc,
        documentDetails: {},
        alldocCollection: []
      };
    }
    case fromKeyNonKeyDocuments.DOWNLOAD_KEY_DOCS_SUCCESS: {
      const documentDetails = action["payload"];
        return {
          currentKeyNonKeyDocumentList: state.currentKeyNonKeyDocumentList,
          documentDetails: documentDetails,
          alldocCollection: state.alldocCollection.push(documentDetails)
        };
      
    }
    case fromKeyNonKeyDocuments.DOWNLOAD_KEY_DOCS_FAIL: {
      return {
        currentKeyNonKeyDocumentList: state.currentKeyNonKeyDocumentList,
        documentDetails: {},
        alldocCollection: state.alldocCollection
      };
    }
    case fromKeyNonKeyDocuments.DOWNLOAD_KEY_PAGE: {
      const pageToDownload = action["payload"]["pageIndex"];
      const pageDetailInfo = action["payload"]["pageInfo$"];
      if(pageDetailInfo.Files.length > 0){
        let pageData = pageDetailInfo.Files[pageToDownload].FileBase64;
        let binary = atob(pageData);
        let testArray = [];
        for (let  i = 0; i < binary.length; i++) {
          testArray.push(binary.charCodeAt(i));
       }
          let byteArray = new Uint8Array(testArray);
          let blob = new Blob([byteArray], {type: "octet/stream"});
          importedSaveAs(blob, pageDetailInfo.Files[pageToDownload].FileName);
      }
      else{
        
      }
      return{
        currentKeyNonKeyDocumentList: state.currentKeyNonKeyDocumentList,
        documentDetails: state.documentDetails,
        alldocCollection: state.alldocCollection
      }
    }
    case fromKeyNonKeyDocuments.CLEAR_PAGE_INFO:{
      return{
        currentKeyNonKeyDocumentList: state.currentKeyNonKeyDocumentList,
        documentDetails: {},
        alldocCollection: state.alldocCollection
      }
    }
    case fromKeyNonKeyDocuments.DOWNLOAD_KEY_PAGE_FAIL: {}
    default: {
      return state;
    }
  }
}
export const getKeyNonKeyDocumentList = (state: KeyNonKeyDocumentState) => state.currentKeyNonKeyDocumentList;
  export const getAllPageDetail = (state: KeyNonKeyDocumentState) =>{
    return state.alldocCollection;
  } 
