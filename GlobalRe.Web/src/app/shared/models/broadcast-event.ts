import {EntityType, GlobalEventType} from "../../app.config";
export class BroadcastEvent{
    eventType: GlobalEventType;
    sender: EntityType;
    message?: string;
    data?;any;

    constructor(type?: GlobalEventType,
                entity?: EntityType,
                message?: string,data?:any){
        this.eventType = type;
        this.sender = entity || EntityType.Deals;
        this.message = message;
        this.data=data?data:null;
        // if(type == GlobalEventType.notification && !this.message)
        //     this.message = new NotificationMessage(GlobalEventType.toastInfo, , err, false)
    }
}
