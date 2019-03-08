import * as fromUserViews from "../actions/deals/user-views.actions";
import { EntityApiData } from "../shared/models/entity-api-data";
// import { Note } from "../models/note";

export interface UserViewState {
    currentUserViewList: EntityApiData<any>[] | null;
    currentUserViewMsg: string;
}
export const initialState: UserViewState = {
    currentUserViewList: [],
    currentUserViewMsg: 'no_data'
};

export function reducer(
    state = initialState,
    action: fromUserViews.UserViewAction
): UserViewState {
    switch (action.type) {
        case fromUserViews.LOAD_USER_VIEWS_SUCCESS: {
            const currentUserViewList = action["payload"].data? action["payload"].data : action["payload"];
            return {
                currentUserViewList: currentUserViewList,
                currentUserViewMsg: action["payload"].msg || 'not_first_time_data'
            };
        }
        case fromUserViews.LOAD_USER_VIEWS_FAIL: {
            return {
                currentUserViewList: [],
                currentUserViewMsg: 'no_data'
            };
        }
        case fromUserViews.UPADTE_USER_VIEWS_MSG: {
            return {
                currentUserViewList: state.currentUserViewList,
                currentUserViewMsg: action["payload"]
            };
        }
        default: {
            return state;
        }
    }
}
export const getUserViewList = (state: UserViewState) => state.currentUserViewList;
export const getUserViewListMsg = (state: UserViewState) => state.currentUserViewMsg;