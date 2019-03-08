import {Component, ElementRef, Input, Output, EventEmitter} from "@angular/core";
import { GlobalNotificationMessageType, NOTIFICATION_MESSAGE_TIMER_VALUE } from "../../../app.config";

@Component({
    selector: 'user-notification',
    templateUrl: 'user-notification.component.html',
    styleUrls: ['user-notification.component.scss']
})
export class UserNotificationComponent {
    data: any;
    timeoutObj: any;
    @Input() set notificationMsg(value){
        if(value){
            this.data = value;
        }else{
            this.data = {};
        }
        this.setCloseOption();
    }
    @Output() closeNotification = new EventEmitter();
    constructor(){}
    setCloseOption(){
        if(this.timeoutObj){
            clearTimeout(this.timeoutObj);
        }
        if(this.data && this.data.severity == GlobalNotificationMessageType.INFO_TYPE){
            this.timeoutObj = setTimeout(() => {
                this.closeHandler();
            }, NOTIFICATION_MESSAGE_TIMER_VALUE);
        }
    }
    closeHandler(){
        if(this.timeoutObj){
            clearTimeout(this.timeoutObj);
        }
        this.closeNotification.emit(this.data);
    }
    
}