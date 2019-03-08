import {Component,OnDestroy, OnInit, ViewEncapsulation, Input, Output, EventEmitter, ElementRef} from "@angular/core";
@Component({
  selector: 'selected-item-card',
  templateUrl: 'selected-item-card.html',
  styleUrls: ['selected-item-card.scss']
})
export class SelectedItemCardComponent implements OnInit,OnDestroy {
selectedOptionItem: any;
@Input() set optionItem(value) {
    if(value) {
        this.selectedOptionItem = value;
    }else{
        this.selectedOptionItem = {};
    }
}
@Output() optionCloseClicked = new EventEmitter();
constructor(){}
ngOnInit(){
    
}
closeClickHandler($event, selectedOptionItem){
    if($event){
        $event.preventDefault();
        $event.stopPropagation();
    }
    this.optionCloseClicked.emit(selectedOptionItem);
}

ngOnDestroy(){}
}
