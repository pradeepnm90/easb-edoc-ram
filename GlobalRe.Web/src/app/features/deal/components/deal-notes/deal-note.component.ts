import { Component, Input } from "@angular/core";

@Component({
  selector: "deal-note",
  template: `
    <mat-card
      class="notes-card"
      style="border-left: 6px solid;"
      [ngStyle]="{ 'border-left-color': noteVal.noteTypeBackgroundColor }"
    >
      <div
        class="note-circle mar-5-t mar-5-b"
        [ngStyle]="{
          'background-color': noteVal.noteTypeBackgroundColor,
          color: noteVal.noteTypeFontColor
        }"
      >
      
        <span *ngIf="noteVal.noteTypeName"  matTooltip="{{noteVal.fullName}}" 
        [matTooltipPosition]="'above'" 
        aria-label="note tooltip">{{ noteVal.noteTypeName}}</span>
      </div>
      <div class="note-area pad-10-l pad-10-r pad-5-t">
        <div class="note-top textSize12">
          <span>{{ noteVal.notedate | date: "MM/dd/yyyy" }}</span>
          <span class="floatR ">{{
            noteVal.firstName + " " + noteVal.lastName
          }}</span>
        </div>
        <div class="note-bottom textSize10" [innerHTML]= "noteVal.notes | truncateCharacters: 200 | sanitizeHtml">
          
        </div>
      </div>  
    </mat-card>
  `,
  styleUrls: ["deal-notes.component.scss"]
})
export class DealNoteComponent {
  noteVal: any;
  constructor() {}
  @Input() set note(value) {
    this.noteVal = value.data;
    this.noteVal.notes  = this.noteVal.notes.replace(/(?:\\[rn]|[\r\n])/g, '<br/>');
    //console.log('this.noteVal.notes', this.noteVal.notes);
  }
}
