import {CurrentDealActionTypes, CurrentDealActions} from '../actions/deals/deal-details.actions';
import {Deal} from "../models/deal";
import {EntityApiData} from "../shared/models/entity-api-data";

export interface DealDetailsState {
  currentDealDetail: EntityApiData<Deal> | null;
}

export const initialState: DealDetailsState = {
  currentDealDetail : null
};

export function reducer(state = initialState, action: CurrentDealActions): DealDetailsState {
  switch (action.type) {
    case CurrentDealActionTypes.LOAD_DEAL_SUCCESS: {
      const deals:EntityApiData<Deal>=action['payload'];
      return {currentDealDetail:deals};
    }
    case CurrentDealActionTypes.UPDATE_DEAL_SUCCESS: {
      {
        const deals:EntityApiData<Deal>=action['payload'];
        return {currentDealDetail:deals};
      }
    }
    default: {
      return state;
    }
  }
}

export const updateDealDetail = (state: DealDetailsState) => state.currentDealDetail;

