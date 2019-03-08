import {Component, OnInit} from "@angular/core";
import { Observable } from "rxjs";
import { UserNotification } from "../../../store/user-notification.reducer";
import * as fromRoot from '../../../store/index';
import { Store } from "@ngrx/store";
import { DeleteUserNotificationAction, AddUserNotificationAction } from "../../../actions/user-notification.action";
@Component({
    selector: 'user-notification-panel',
    templateUrl: 'user-notification-panel.component.html',
    styleUrls: ['user-notification-panel.component.scss']
})
export class UserNotificationPanelComponent implements OnInit{
   notificationList: any;
   userNotificationList$: Observable<UserNotification[]>;
    constructor(private _store: Store<fromRoot.AppState>){
        this.userNotificationList$ = this._store.select(fromRoot.getUserNotificationList);
        
    }
    ngOnInit(){
        this.userNotificationList$.subscribe(val => {
            this.setNotificationList(val);
        });
    }
    closeNotificationHandler($event){
        this.notificationList = this.notificationList.filter(item => item.id !== $event.id );
        this._store.dispatch(new DeleteUserNotificationAction($event));
    }
    setNotificationList(val){
        if(val && val.length){
            for (const nItem of val) {
                if(!this.notificationList.find(item => item.id == nItem.id)){
                    this.notificationList.push({
                        id: nItem.id,
                        severity: nItem.severity,
                        detail: nItem.detail,
                        field: nItem.field,
                        iconCls: nItem.iconCls
                    });
                }
            }
        }else{
            this.notificationList = [];
        }
    }
    
}