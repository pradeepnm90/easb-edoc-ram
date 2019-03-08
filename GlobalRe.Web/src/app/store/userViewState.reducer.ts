import * as fromUserViews from "../actions/deals/user-views.actions";
import { EntityApiData } from "../shared/models/entity-api-data";
import { FilterType } from "../app.config";
// import { Note } from "../models/note";

export interface customUserViewState {
userviewAgGridState: any;
inceptionFilterState: any;
customViewLoadMsg: string;
}

export interface customViewState{
    customViewConfigState:customUserViewState
}

export const initialState: customViewState = {
    customViewConfigState: {
        userviewAgGridState: {
            colState: [],
            sortState: [],
            filterState: {}
        },
        inceptionFilterState: FilterType.All,
        customViewLoadMsg: ''
    }
};

export function reducer(
    state = initialState,
    action: fromUserViews.UserViewAction
): customViewState {
    switch (action.type) {
        case fromUserViews.LOAD_USER_VIEW_AG_GRID_STATE: {
            const currentUserviewConfigState = action["payload"];
            return {
                customViewConfigState: {
                    userviewAgGridState: currentUserviewConfigState.userviewAgGridState,
                    inceptionFilterState:currentUserviewConfigState.inceptionFilterState || state.customViewConfigState.inceptionFilterState,
                    customViewLoadMsg: currentUserviewConfigState.customViewLoadMsg
                }
              
            };
        }
        case fromUserViews.UPDATE_USER_VIEWS_STATE_MSG: {
            const currentUserviewConfigStateMsg = action["payload"];
            state.customViewConfigState.customViewLoadMsg = currentUserviewConfigStateMsg;
            return state;
        }
        default: {
            return state;
        }
    }
}
export const getCustomUserViewState = (state: customViewState) => state.customViewConfigState;
