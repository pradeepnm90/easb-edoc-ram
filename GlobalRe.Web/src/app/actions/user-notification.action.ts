import { Action } from '@ngrx/store';


export const UserNotificationActionTypes = {
  ADD_USER_NOTIFICATION: '[User Notification] Add',
  DELETE_USER_NOTIFICATION: '[User Notification] Delete',
}

export class AddUserNotificationAction implements Action {
  readonly type = UserNotificationActionTypes.ADD_USER_NOTIFICATION;
  constructor(public payload: any[]) { }
}

export class DeleteUserNotificationAction implements Action {
  readonly type = UserNotificationActionTypes.DELETE_USER_NOTIFICATION;
  constructor(public payload: any) { }
}


export type UserNotificationActions =
  | AddUserNotificationAction
  | DeleteUserNotificationAction;

