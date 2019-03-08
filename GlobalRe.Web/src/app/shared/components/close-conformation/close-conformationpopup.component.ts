import { Component, Inject, Output, EventEmitter, Input, OnChanges, ElementRef } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
@Component({
  selector: "cancel-popup",
  template: `  
  <div class="cancel_popover">
   
    <label class="title">{{popUpText}}</label>
    <div>
    <button
      mat-button
      type="button"

    class="mat-button"  (click)="closeDialogNo()"
    >
      No
    </button>
    <button
      mat-button 
      type="button"
      class="warning"
      (click) = "closeDialogYes() || closeDialogYes1()"
    >
      Yes
    </button>
  </div>
  </div>
  `,
  styleUrls: ["close-conformation.popup.component.scss"]
})
export class CancelPopup implements OnChanges{
  @Output() closeWholePop=new  EventEmitter();
  @Output() closePopUp = new EventEmitter();
  @Input() popUpText: string;
  @Input() position;
  
  constructor(
    public dialog: MatDialog,
    public eleRef: ElementRef
   
  ) { }

  closeDialogYes(): void {
    this.closePopUp.emit();
    this.closeWholePop.emit();
  }

  ngOnChanges(){
    console.log(this.position)
   // this.eleRef.nativeElement.querySelector('.cancel_popover').style.top = (this.positionLeftbottom.positionBottom + 40) + 'px';
   // this.eleRef.nativeElement.querySelector('.cancel_popover').style.left = (this.positionLeftbottom.positionRight - 200) + 'px';
   if(this.position){
    if(this.position.from === 'DealDetails'){
      this.eleRef.nativeElement.querySelector('.cancel_popover').style.top = this.position.top + 'px';
      this.eleRef.nativeElement.querySelector('.cancel_popover').style.right = this.position.right + 'px';
    }

    if(this.position.from === 'CheckListnote'){
      this.eleRef.nativeElement.querySelector('.cancel_popover').style.left = this.position.left + 'px';
      this.eleRef.nativeElement.querySelector('.cancel_popover').style.bottom = this.position.bottom + 'px';
    }

    if(this.position.from === 'DealNote'){
      this.eleRef.nativeElement.querySelector('.cancel_popover').style.left = this.position.left + 'px';
      this.eleRef.nativeElement.querySelector('.cancel_popover').style.bottom = this.position.bottom + 'px';
    }
   } 
  }

  closeDialogYes1(){
    this.dialog.closeAll();
  }
  
  closeDialogNo() {
    console.log("no button");
    this.closePopUp.emit()
  }
}
