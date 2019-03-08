import {Component,OnDestroy, ViewChild, ElementRef} from "@angular/core";
import * as moment from 'moment';
import {IDateAngularComp} from "ag-grid-angular/main";
// import {IDate} from "ag-grid/src/ts/rendering/dateComponent";
declare var require:any
import * as _flatpickr from 'flatpickr';
const flatpickr: any = require("flatpickr")


@Component({
  selector: 'ag-calender-render-component',
  template: `
    
  <div #flatpickrEl class="ag-input-text-wrapper">
  <input type='text' data-input style="background-color:white" />
</div>
   
  `,
  styleUrls: ['./ag-grid-filter.component.scss']
})
export class CustomDateComponent implements OnDestroy,IDateAngularComp {
  @ViewChild("flatpickrEl", {read: ElementRef}) flatpickrEl: ElementRef;
  private date: Date;
  private params: any;
  private picker: any;
  //private flatpickr: any; 

  agInit(params: any): void {
      this.params = params;
      
      
  }

  ngAfterViewInit(): void {
      //outputs `I am span`
      this.picker = flatpickr(this.flatpickrEl.nativeElement, {
          onChange: this.onDateChanged.bind(this),
          wrap: true
      });

      this.picker.calendarContainer.classList.add('ag-custom-component-popup');
      
  }

  ngOnDestroy() {
      console.log(`Destroying DateComponent`);
  }

  onDateChanged(selectedDates) {
      this.date = selectedDates[0] || null;
      this.params.onDateChanged();
      
  }

  getDate(): Date {
      return this.date;
  }

  setDate(date: Date): void {
     this.date = date || null;
     this.picker.setDate(date);
  }
}