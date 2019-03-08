import { Injectable } from "@angular/core";
import { BroadcastEvent } from "../models/broadcast-event";
import { Subject } from "rxjs/Subject";
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from "rxjs";

@Injectable()
export class SharedEventService {

    private _event: BroadcastEvent
    private _subject: Subject<BroadcastEvent> = new Subject<BroadcastEvent>();
    // public _viewSubmission = new BehaviorSubject<string>("My Submissions");

    setEvent(event: BroadcastEvent): void {
        this._event = event;
        this._subject.next(event);
    }

    getEvent(): Observable<BroadcastEvent> {
        return this._subject.asObservable();
    }

    // setSubmissionValue(value) {
    //     this._viewSubmission.next(value);
    // }

    // getSubmissionValue() {
    //     return this._viewSubmission.asObservable()
    // }









}