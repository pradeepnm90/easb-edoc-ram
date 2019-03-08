import * as extendedSearchaction from '../actions/deals/extended-search.action';
export interface ExtendedSearchInnerState {
    subdivision: any[];
    productLine2: any[];
    exposureGroup: any[];
    exposureName: any[];
    keyMemberName: any[];
}
export interface ExtendedSearchState {
    extendSearchMainDataState: ExtendedSearchInnerState,
    extendedSearchActiveState: ExtendedSearchInnerState;
    extendedSearchSaveState: ExtendedSearchInnerState;
}

export const initialState: ExtendedSearchState = {
    extendSearchMainDataState: {
        subdivision: [],
        productLine2: [],
        exposureGroup: [],
        exposureName: [],
        keyMemberName: []
    },
    extendedSearchActiveState: {
        subdivision: [],
        productLine2: [],
        exposureGroup: [],
        exposureName: [],
        keyMemberName: []
    },
    extendedSearchSaveState: {
        subdivision: [],
        productLine2: [],
        exposureGroup: [],
        exposureName: [],
        keyMemberName: []
    }
};
interface ExtendedSearchDataModel{
    code: number;
    name: string;
    isSelected: boolean;
    isVisible: boolean;
    parent: number[];
    child: number[];
    grandParent: number [];
    grandChild: number [];
}

function prepareExtendedSearchRawData(rawData){
    let sd = [], pl2 = [], eg = [], en = [];
    let mainSDObj: any = {}, mainPL2Obj: any = {}, mainEGObj: any = {}, mainENObj: any = {};
    for (const iterator of rawData) {
        if(mainSDObj[iterator.data.subdivisioncode]){
            if(mainSDObj[iterator.data.subdivisioncode].child.indexOf(iterator.data.productLinecode) < 0){
                mainSDObj[iterator.data.subdivisioncode].child.push(iterator.data.productLinecode);
            }
            if(mainSDObj[iterator.data.subdivisioncode].grandChild.indexOf(iterator.data.exposuregroupcode) < 0){
                mainSDObj[iterator.data.subdivisioncode].grandChild.push(iterator.data.exposuregroupcode);
            }
        }else{
            mainSDObj[iterator.data.subdivisioncode] = {
                code: iterator.data.subdivisioncode,
                name: iterator.data.subdivisionname,
                isSelected: false,
                isVisible: false,
                parent: [],
                child: [iterator.data.productLinecode],
                grandParent: [],
                grandChild: [iterator.data.exposuregroupcode]
            };
        }
        if(mainPL2Obj[iterator.data.productLinecode]){
            if(mainPL2Obj[iterator.data.productLinecode].child.indexOf(iterator.data.exposuregroupcode) < 0){
                mainPL2Obj[iterator.data.productLinecode].child.push(iterator.data.exposuregroupcode);
            }
            if(mainPL2Obj[iterator.data.productLinecode].parent.indexOf(iterator.data.subdivisioncode) < 0){
                mainPL2Obj[iterator.data.productLinecode].parent.push(iterator.data.subdivisioncode);
            }
            if(mainPL2Obj[iterator.data.productLinecode].grandChild.indexOf(iterator.data.exposuretypecode) < 0){
                mainPL2Obj[iterator.data.productLinecode].grandChild.push(iterator.data.exposuretypecode);
            }
        }else{
            mainPL2Obj[iterator.data.productLinecode] = {
                code: iterator.data.productLinecode,
                name: iterator.data.productLinename,
                isSelected: false,
                isVisible: false,
                parent: [iterator.data.subdivisioncode],
                child: [iterator.data.exposuregroupcode],
                grandParent: [],
                grandChild: [iterator.data.exposuretypecode]
            };
        }
        if(mainEGObj[iterator.data.exposuregroupcode]){
            if(mainEGObj[iterator.data.exposuregroupcode].child.indexOf(iterator.data.exposuretypecode) < 0){
                mainEGObj[iterator.data.exposuregroupcode].child.push(iterator.data.exposuretypecode);
            }
            if(mainEGObj[iterator.data.exposuregroupcode].parent.indexOf(iterator.data.productLinecode) < 0){
                mainEGObj[iterator.data.exposuregroupcode].parent.push(iterator.data.productLinecode);
            }
            if(mainEGObj[iterator.data.exposuregroupcode].grandParent.indexOf(iterator.data.subdivisioncode) < 0){
                mainEGObj[iterator.data.exposuregroupcode].grandParent.push(iterator.data.subdivisioncode);
            }
        }else{
            mainEGObj[iterator.data.exposuregroupcode] = {
                code: iterator.data.exposuregroupcode,
                name: iterator.data.exposuregroupname,
                isSelected: false,
                isVisible: false,
                parent: [iterator.data.productLinecode],
                child: [iterator.data.exposuretypecode],
                grandParent: [iterator.data.subdivisioncode],
                grandChild: []
            };
        }
        if(mainENObj[iterator.data.exposuretypecode]){
            if(mainENObj[iterator.data.exposuretypecode].parent.indexOf(iterator.data.exposuregroupcode) < 0){
                mainENObj[iterator.data.exposuretypecode].parent.push(iterator.data.exposuregroupcode);
            }
            if(mainENObj[iterator.data.exposuretypecode].grandParent.indexOf(iterator.data.productLinecode) < 0){
                mainENObj[iterator.data.exposuretypecode].grandParent.push(iterator.data.productLinecode);
            }
        }else{
            mainENObj[iterator.data.exposuretypecode] = {
                code: iterator.data.exposuretypecode,
                name: iterator.data.exposuretypename,
                isSelected: false,
                isVisible: false,
                parent: [iterator.data.exposuregroupcode],
                child: [],
                grandParent: [iterator.data.productLinecode],
                grandChild: []
            };
        }
    }
    sd = returnArray(mainSDObj);
    pl2 = returnArray(mainPL2Obj);
    eg = returnArray(mainEGObj);
    en = returnArray(mainENObj);
    return { sd: sd, pl2: pl2, eg: eg, en: en };
}
function returnArray(objVal){
    let tempArr: any[] = [];
    let keyArr = Object.keys(objVal);
    for (let iterator of keyArr) {
        tempArr.push(objVal[iterator]);
    }
    tempArr.sort((a, b) => {
        if(a.name > b.name){
            return 1;
        }else if(a.name < b.name){
            return -1;
        }else{
            return 0;
        }
    });
    return tempArr;
}
function prepareKeyMemberNameData(rawData){
    console.log('prepareKeyMemberNameData', rawData);
    let keyMemberNames = {};
    for(let i=1; i<5; i++){
        for (const iterator of rawData[i].results) {
            if(!keyMemberNames[iterator.value]){
                keyMemberNames[iterator.value] = {
                    code: iterator.value,
                    name: iterator.name,
                    isSelected: false,
                    isVisible: false,
                    parent: [],
                    child: [],
                    grandParent: [],
                    grandChild: []
                };
            }
        }
    }
    return returnArray(keyMemberNames);
}
export function reducer(state = initialState, action: extendedSearchaction.ExtendedSearchActions): ExtendedSearchState {
  switch (action.type) {
    case extendedSearchaction.LOAD_EXTENDED_SEARCH_DATA_SUCCESS: {
      const extendedSearchData = prepareExtendedSearchRawData(action['payload']);
      //console.log('extendedSearchData', extendedSearchData);
      return {
        extendSearchMainDataState: {
            subdivision: extendedSearchData.sd,
            productLine2: extendedSearchData.pl2,
            exposureGroup: extendedSearchData.eg,
            exposureName: extendedSearchData.en,
            keyMemberName: state.extendSearchMainDataState.keyMemberName
        },
        extendedSearchActiveState: {
            subdivision: JSON.parse(JSON.stringify(extendedSearchData.sd)),
            productLine2: JSON.parse(JSON.stringify(extendedSearchData.pl2)),
            exposureGroup: JSON.parse(JSON.stringify(extendedSearchData.eg)),
            exposureName: JSON.parse(JSON.stringify(extendedSearchData.en)),
            keyMemberName: state.extendedSearchActiveState.keyMemberName
        },
        extendedSearchSaveState: {
            subdivision: JSON.parse(JSON.stringify(extendedSearchData.sd)),
            productLine2: JSON.parse(JSON.stringify(extendedSearchData.pl2)),
            exposureGroup: JSON.parse(JSON.stringify(extendedSearchData.eg)),
            exposureName: JSON.parse(JSON.stringify(extendedSearchData.en)),
            keyMemberName: state.extendedSearchActiveState.keyMemberName
        }
    };
    }
    case extendedSearchaction.LOAD_EXTENDED_SERACH_KEY_MEMBER_NAME_DATE: {
        let keyMemberNames = prepareKeyMemberNameData(action['payload']);
        return {
            extendSearchMainDataState: {
                subdivision: state.extendSearchMainDataState.subdivision,
                productLine2: state.extendSearchMainDataState.productLine2,
                exposureGroup: state.extendSearchMainDataState.exposureGroup,
                exposureName: state.extendSearchMainDataState.exposureName,
                keyMemberName: keyMemberNames
            },
            extendedSearchActiveState: {
                subdivision: state.extendedSearchActiveState.subdivision,
                productLine2: state.extendedSearchActiveState.productLine2,
                exposureGroup: state.extendedSearchActiveState.exposureGroup,
                exposureName: state.extendedSearchActiveState.exposureName,
                keyMemberName: JSON.parse(JSON.stringify(keyMemberNames))
            },
            extendedSearchSaveState: {
                subdivision: state.extendedSearchSaveState.subdivision,
                productLine2: state.extendedSearchSaveState.productLine2,
                exposureGroup: state.extendedSearchSaveState.exposureGroup,
                exposureName: state.extendedSearchSaveState.exposureName,
                keyMemberName: JSON.parse(JSON.stringify(keyMemberNames))
            }
        };
    }
    case extendedSearchaction.UPDATE_ACTIVE_EXTENDED_SEARCH_STATE: {
        let updatedActiveState = action['payload'];
        return {
            extendSearchMainDataState: state.extendSearchMainDataState,
            extendedSearchActiveState: updatedActiveState,
            extendedSearchSaveState: state.extendedSearchSaveState
        };
    }
    case extendedSearchaction.SAVE_ACTIVE_EXTENDED_SEARCH_STATE: {
        let updatedActiveState = action['payload'];
        let updatedSaveState = JSON.parse(JSON.stringify(action['payload']));
        return {
            extendSearchMainDataState: state.extendSearchMainDataState,
            extendedSearchActiveState: updatedActiveState,
            extendedSearchSaveState: updatedSaveState
        };
    }
    case extendedSearchaction.CLEAR_ACTIVE_EXTENDED_SEARCH_STATE: {
        let updatedActiveState = JSON.parse(JSON.stringify(state.extendSearchMainDataState));
        return {
            extendSearchMainDataState: state.extendSearchMainDataState,
            extendedSearchActiveState: updatedActiveState,
            extendedSearchSaveState: state.extendedSearchSaveState
        };
    }
    case extendedSearchaction.CLEAR_ACTIVE_N_SAVE_EXTENDED_SEARCH_STATE: {
        let updatedActiveState = JSON.parse(JSON.stringify(state.extendSearchMainDataState));
        let updatedSaveState = JSON.parse(JSON.stringify(state.extendSearchMainDataState));
        return {
            extendSearchMainDataState: state.extendSearchMainDataState,
            extendedSearchActiveState: updatedActiveState,
            extendedSearchSaveState: updatedSaveState
        };
    }
    default: {
      return state;
    }
  }
}

export const extendedSearchMainData = (state: ExtendedSearchState) => state.extendSearchMainDataState;
export const extendedSearchSaveData = (state: ExtendedSearchState) => state.extendedSearchSaveState;
export const extendedSearchActiveData = (state: ExtendedSearchState) => state.extendedSearchActiveState;

