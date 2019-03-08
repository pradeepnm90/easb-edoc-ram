import { DealStatusActions, DealStatusActionTypes } from '../actions/deals/deal-status.actions';
import { DealStatus } from '../models/deal-status';
import {EntityApiData} from '../shared/models/entity-api-data';

export interface DealStatusState {
  dealsCount: EntityApiData<DealStatus>[] | null;
}

export const initialState: DealStatusState = {
  dealsCount: [],
};

export function reducer(state = initialState, action: DealStatusActions): DealStatusState {
  switch (action.type) {
    case DealStatusActionTypes.GET_DEAL_STATUS_SUCCESS: {
      const dealsStatus:EntityApiData<DealStatus>[]=action.payload;
      if(state.dealsCount && state.dealsCount.length == 0){
        const dealsStatus:EntityApiData<DealStatus>[]=action.payload;
        let subDealSum: number = 0;
        //const dealsStatus:EntityApiData<DealStatus>[]=JSON.parse(JSON.stringify(state.dealsCount)); 
          dealsStatus.map(deal=>{
            deal.data.isDisabled=(deal.data.count===0)? true:false;
            deal.data.isSelected = deal.data.isSelected || false;
              deal.data.isCtrlKey = deal.data.isCtrlKey || false;
            if(deal.data.statusSummary&&deal.data.statusSummary.length>0) {
              deal.data.statusSummary.map(statussummary => {
                statussummary.isDisabled=(statussummary.count===0)? true:false; 
                statussummary.isSelected=statussummary.isSelected == undefined? true : statussummary.isSelected;
              });
              subDealSum = deal.data.statusSummary.reduce((s, f) => {
                
               if(f.isSelected && f.count)
                  return f.count + s; // return the sum of the accumulator and the current time. (as the the new accumulator)
               else
                  return s;
             }, 0);
            }
          
          });
      }else{
        dealsStatus.map(deal=>{
          deal.data.isDisabled=(deal.data.count===0)?true:false;
          let isSelected=false;
          if (state.dealsCount && state.dealsCount.length > 0) {
            const olddeal = state.dealsCount
                             .find(dd => dd.data.statusCode === deal.data.statusCode
                                      && dd.data.statusName === deal.data.statusName
                                      && dd.data.workflowId === deal.data.workflowId);
  
            if (olddeal)
              isSelected = olddeal.data.isSelected&&!deal.data.isDisabled;
          }
          if(deal.data.statusSummary&&deal.data.statusSummary.length>0) {
            let subDealSum=deal.data.count;
            if (state.dealsCount && state.dealsCount.length > 0) {
             let tempDeal= state.dealsCount
                           .find(dd => dd.data.statusCode==deal.data.statusCode
                                     &&dd.data.statusName==deal.data.statusName);
               subDealSum = deal.data.statusSummary.reduce((s, f) => {
                 let tempSubdfeal=tempDeal.data.statusSummary
                                  .find(sdeal=>sdeal.statusCode==f.statusCode
                                             &&sdeal.statusName==f.statusName);
  
                if(tempSubdfeal&&tempSubdfeal.isSelected)
                   return f.count + s; // return the sum of the accumulator and the current time. (as the the new accumulator)
                else
                   return s;
              }, 0);
  
              const olddeal = state.dealsCount
                              .find(dd => dd.data.statusCode === deal.data.statusCode
                                       && dd.data.statusName === deal.data.statusName
                                       && dd.data.workflowId === deal.data.workflowId);
  
              deal.data.statusSummary.map(statussummary => {
                if (olddeal.data.statusSummary && olddeal.data.statusSummary.length > 0) {
                  const oldsubDela = olddeal.data.statusSummary
                                     .find(dd => dd.statusCode === statussummary.statusCode
                                               &&dd.workflowId === statussummary.workflowId);
  
                  statussummary.isSelected=oldsubDela.isSelected;
                }
              });
            }
            else {
              deal.data.statusSummary.map(statussummary => {
                statussummary.isSelected = statussummary.count > 0
                                         ? true : false;
              });
            }
            deal.data.isDisabled = (subDealSum <= 0 && deal.data.statusSummary.length<=0)
                                 ?true:false;
            deal.data.count=subDealSum;
          }
         deal.data.isSelected = isSelected;
        });
      }
      
      return {
        dealsCount: dealsStatus
      };
    }
    case DealStatusActionTypes.UPDATE_DEAL_STATUS_ACTIONS: {
      const dealsStatus:EntityApiData<DealStatus>=action.payload;
      let isDeSelectPanel: boolean = false;
      if(state.dealsCount.length >0) {
        let currentDeal = state.dealsCount.find(dealcount =>
                          dealcount.data.statusCode == action.payload.data.statusCode
                          && dealcount.data.statusName == action.payload.data.statusName);
        if (currentDeal) {
          currentDeal.data.isSelected = dealsStatus.data.isSelected;
          currentDeal.data.isCtrlKey = dealsStatus.data.isCtrlKey;

          let selectedDeals = state.dealsCount.filter(deal =>
                                deal.data.workflowId === action.payload.data.workflowId &&
                                deal.data.isSelected === true );
          isDeSelectPanel = (currentDeal.data.isSelected === false && currentDeal.data.isCtrlKey === false &&
                             selectedDeals && selectedDeals.length > 0) ? true : false;
        }
      }
      state.dealsCount.map(deals =>{
          if(deals.data.workflowId != action.payload.data.workflowId ||
            (dealsStatus.data.isCtrlKey == false &&
             dealsStatus.data.statusCode != deals.data.statusCode &&
             dealsStatus.data.statusName != deals.data.statusName &&
             dealsStatus.data.isSelected == true)
          ) {
              deals.data.isSelected = false;
              deals.data.isCtrlKey = false;
          }
      });

      /* This is for deselecting panel without control key pressed */
      if(isDeSelectPanel) {
        state.dealsCount.map(deals => {
          if (deals.data.workflowId === action.payload.data.workflowId &&
              dealsStatus.data.statusName !== deals.data.statusName) {
            deals.data.isSelected = false;
            deals.data.isCtrlKey = false;
          }
          if(deals.data.workflowId === action.payload.data.workflowId &&
             dealsStatus.data.statusName === deals.data.statusName){
                deals.data.isSelected = true;
                deals.data.isCtrlKey = true;
          }
        });
      }

      return {...state};
   }
   case DealStatusActionTypes.UPDATE_SUB_DEAL_STATUS_ACTIONS: {
    let dealsStatus:DealStatus=action.payload['dealSubsSatus'];
    state.dealsCount.map(deals=>{
      if(deals.data.statusSummary!=null&&deals.data.statusSummary.length>0){
         deals.data.statusSummary.map(substatus=>{
          if(substatus.statusCode==dealsStatus.statusCode)
          {
            if(dealsStatus.isSelected==true)
              deals.data.count+=dealsStatus.count;
            else
              deals.data.count-=dealsStatus.count;
          }
        });
     dealsStatus.isSelected=deals.data.statusSummary.find(deal =>
              deal.statusCode==dealsStatus.statusCode
              &&deal.statusName==dealsStatus.statusName)!=undefined
              ? deals.data.statusSummary.find(deal =>
                deal.statusCode==dealsStatus.statusCode
                &&deal.statusName==dealsStatus.statusName).isSelected
              : dealsStatus.isSelected;
      }
    });

  return {
    ...state
  };
 }

  case DealStatusActionTypes.UPDATE_DEAL_STATUS_STATE_ACTION: {
    const dealsStatusViewSate:EntityApiData<DealStatus>[]=action.payload;
    const dealsStatus:EntityApiData<DealStatus>[]=JSON.parse(JSON.stringify(state.dealsCount)); 
      dealsStatus.map(deal=>{
        const olddeal = dealsStatusViewSate
                           .find(dd => dd.data.statusCode === deal.data.statusCode
                                    && dd.data.statusName === deal.data.statusName
                                    && dd.data.workflowId === deal.data.workflowId);

        if (olddeal){
          deal.data.isSelected = olddeal.data.isSelected;
          deal.data.isCtrlKey = olddeal.data.isCtrlKey;
        }
        if(deal.data.statusSummary&&deal.data.statusSummary.length>0) {
          deal.data.statusSummary.map(statussummary => {
            if (olddeal.data.statusSummary && olddeal.data.statusSummary.length > 0) {
              const oldsubDela = olddeal.data.statusSummary
                                 .find(dd => dd.statusCode === statussummary.statusCode
                                           &&dd.workflowId === statussummary.workflowId);

              statussummary.isSelected=oldsubDela.isSelected;
            }
          });
        }
       
      });
      return {
        dealsCount: dealsStatus
      };
  }
  case DealStatusActionTypes.CLEAR_DEAL_STATUS_STATE_ACTION:{
    return {
      dealsCount: []
    };
  }
    default: {
      return state;
    }
  }
}

export const getDealStatus = (state: DealStatusState) => state.dealsCount;

