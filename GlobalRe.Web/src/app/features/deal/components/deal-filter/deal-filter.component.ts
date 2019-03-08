import {Component, EventEmitter, OnInit, Input,Output} from '@angular/core';
import {FilterType} from '../../../../app.config';
@Component({
  selector: 'deal-filter',
  template: `
  <label class="dealCount" *ngIf="rowDealCount">
    <label class="count">Displaying {{rowDealCount.x}} of {{rowDealCount.y}}</label>
  </label>
  <mat-card class="panel-datafilter"> 
    <ul>
      <li><a href="#" id="deal-dealfilter-al" [ngClass]="(currentFilterType==filterType.All)?'active':''" (click)="onFilterClick(filterType.All,$event)">All</a></li>
      <li><a href="#" id="deal-dealfilter-pastinception" [ngClass]="(currentFilterType==filterType.PastInception)?'active':''" (click)="onFilterClick(filterType.PastInception,$event)">Past Inception</a></li>
      <li><a href="#" id="deal-dealfilter-within30days" [ngClass]="(currentFilterType==filterType.Within30Days)?'active':''" (click)="onFilterClick(filterType.Within30Days,$event)">Within 30 days</a></li>
      <li><a href="#" id="deal-dealfilter-over30days" [ngClass]="(currentFilterType==filterType.Over30Days)?'active':''" (click)="onFilterClick(filterType.Over30Days,$event)">Over 30 days</a></li>
    </ul>
  </mat-card>`,
  styleUrls: ['./deal-filter.component.css']
})
export class DealFilterComponent implements OnInit {
  filterType=FilterType;
  currentFilterType;
  @Input() rowDealCount;
  @Input() set resetSelection(value) {
    
    if (value===true)
      this.currentFilterType = FilterType.All;
    // this.resetSelection = false;
  }
  @Input() set inputSelectedFilter(value){
    if(value){
      this.currentFilterType = value;
    }else{
      this.currentFilterType=FilterType.All;
    }
  }
  @Output() filterSelected = new EventEmitter();
  constructor() {
    this.currentFilterType=this.filterType.All;
  }

  ngOnInit() {
  }
  onFilterClick(filterCriteria,event)
  {
    event.preventDefault();
    this.currentFilterType=filterCriteria;
    this.filterSelected.emit({criteria:filterCriteria});
  } 
}
