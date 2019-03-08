import {Action} from '@ngrx/store';

export const LOAD_NOTES = '[Notes] Load notes';
export const LOAD_NOTES_FAIL = '[Notes] Load notes fail';
export const LOAD_NOTES_SUCCESS = '[Notes] Load notes success';
export const CLEAR_NOTES = '[Notes] Clear notes';

export class LoadNotes implements Action {
    readonly type  = LOAD_NOTES;
    constructor(public payload: any) {}
}
export class LoadNotesFail implements Action {
    readonly type  = LOAD_NOTES_FAIL;
    constructor(public payload: any){}
}
export class LoadNotesSuccess implements Action {
    readonly type  = LOAD_NOTES_SUCCESS;
    constructor(public payload: any){}
}
export class ClearNotes implements Action {
    readonly type  = CLEAR_NOTES;
    constructor(){}
}


export type NotesAction = LoadNotes | LoadNotesFail | LoadNotesSuccess | ClearNotes;