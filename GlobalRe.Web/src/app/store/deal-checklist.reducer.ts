import * as fromCheckList from "../actions/deals/deal-checkList.actions";
import { EntityApiData } from "../shared/models/entity-api-data";

export interface DealChecklist{
    currentchecklist: EntityApiData<any>[] | null;
}

export const initialState: DealChecklist = {
    currentchecklist: []
};

export function reducer(
    state = initialState,
    action: fromCheckList.DealCheckListAction
): DealChecklist{
    switch(action.type){
        case fromCheckList.LOAD_DEAL_CHECKLIST_SUCCESS:{
            const currentchecklist= action["payload"];
            return{
                currentchecklist: currentchecklist
            };
        }

        case fromCheckList.LOAD_DEAL_CHECKLIST_FAIL:{
          return{
              currentchecklist: []
          }
        }

        default:{
            return state;
        }
    }
}

export const getDealChecklist = (state: DealChecklist) => state.currentchecklist
