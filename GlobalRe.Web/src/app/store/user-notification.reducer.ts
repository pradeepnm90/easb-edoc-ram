import * as fromUserNotificationAction from '../actions/user-notification.action';
import { GlobalNotificationMessageType } from '../app.config';
export interface UserNotification{
    id: string;
    severity: string;
    detail: string;
    field: string;
    iconCls: string;
}
export const iconClasNames = {
    'Info': 'fa-check-circle',
    'Warn': 'fa-warning',
    'Error': 'fa-ban',
    'Fatal': 'fa-ban'
};
export interface UserNotificationState {
    currentUserNotificationList: UserNotification[];
}
export const initialState: UserNotificationState = {
    currentUserNotificationList: []
};

export function reducer(
    state = initialState,
    action: fromUserNotificationAction.UserNotificationActions
): UserNotificationState {
    switch (action.type) {
        case fromUserNotificationAction.UserNotificationActionTypes.ADD_USER_NOTIFICATION: {
            let notificationList = action["payload"];
            let tempNotificationList = state.currentUserNotificationList.map(item => item);
            for (const nItem of notificationList) {
                if(nItem.severity != GlobalNotificationMessageType.UNDEFINED_TYPE && nItem.severity != GlobalNotificationMessageType.DEBUG_TYPE ){
                    nItem['id'] = nItem.severity + Math.random();
                    nItem['iconCls'] = iconClasNames[nItem.severity];
                    tempNotificationList.push(nItem);   
                }
            }
            
            return {
                currentUserNotificationList: tempNotificationList
            };
        }
        case fromUserNotificationAction.UserNotificationActionTypes.DELETE_USER_NOTIFICATION: {
            let userNotification = action["payload"];
            let tempNotificationList = state.currentUserNotificationList.filter(nItem => nItem.id !== userNotification.id);
            return {
                currentUserNotificationList: tempNotificationList
            };
        }
        default: {
            return state;
        }
    }
}
export const getUserNotificationList = (state: UserNotificationState) => state.currentUserNotificationList;