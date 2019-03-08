import * as fromNotes from "../actions/deals/deal-notes.actions";
import { EntityApiData } from "../shared/models/entity-api-data";
import { Note } from "../models/note";

export interface NoteState {
  currentNoteList: EntityApiData<Note>[] | null;
}
export const initialState: NoteState = {
  currentNoteList: []
};

export function reducer(
  state = initialState,
  action: fromNotes.NotesAction
): NoteState {
  switch (action.type) {
    case fromNotes.LOAD_NOTES_SUCCESS: {
      const currentNoteList = action["payload"];
      return {
        currentNoteList: currentNoteList
      };
    }
    case fromNotes.LOAD_NOTES_FAIL: {
      return {
        currentNoteList: []
      };
    }
    case fromNotes.CLEAR_NOTES: {
      return {
        currentNoteList: []
      };
    } 
    default: {
      return state;
    }
  }
}
export const getNoteList = (state: NoteState) => state.currentNoteList;
