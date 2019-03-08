import {Component,OnDestroy, OnInit, ViewEncapsulation, Input, Output, EventEmitter, ElementRef} from "@angular/core";
import { select } from "@ngrx/core";
@Component({
  selector: 'custom-select-dropdown',
  templateUrl: 'custom-select-dropdown.html',
  styleUrls: ['custom-select-dropdown.scss'],
  host: {
    '(document:click)': 'onOutsideClick($event)',
  }
})
export class CustomSelectDropdownComponent implements OnInit,OnDestroy {
optionItemList: any[];
selectedItemList: any[];
optionItemOriginalList: any[];
placeholderTxt : string;
showOptionPanel: boolean = false;
custId: string = '';
selectAll: any = {
    'name': 'Select All',
    'isSelected': false
};
searchKeyWord: string;
@Input() set customId(value){
    this.custId = value;
}
@Input() set optionList(value) {
    if(value && value.length>0) {
        this.optionItemList = value;
        this.optionItemOriginalList = value;
    }else{
        this.optionItemList = [];
        this.optionItemOriginalList = [];
    }
}
@Input() set selectedOptionList(value) {
    if(value && value.length>0) {
        this.selectedItemList = value;
    }else{
        this.selectedItemList = [];
    }
    this.checkForSelected();
}
@Input() set placeholderText(value) {
    if(value && value.length>0) {
        this.placeholderTxt = value;
    }else{
        this.placeholderTxt = 'Search';
    }
}
@Output() optionSelectionChange = new EventEmitter();
@Output() optionPanelClosed = new EventEmitter();
constructor(private _eref: ElementRef){}
ngOnInit(){
    this.searchKeyWord = '';
    this.checkForSelected();
}
checkForSelected(){
    let found = 0;
    if(this.optionItemList && this.optionItemList.length > 0 && this.selectedItemList){
        this.optionItemList = this.optionItemList.map(item => {
            let tempOption = this.selectedItemList.find(sItem => { return item.name == sItem.name});
            if(!tempOption){
                item.isSelected = false;
                found++;
            }else{
                item.isSelected = true;
            }
            return item;
        });
    }
    if(found || this.optionItemList.length == 0){
        this.selectAll.isSelected = false;
    }else{
        this.selectAll.isSelected = true;
    }
}
showOptionPanelHandler($event){
    if($event){
        $event.preventDefault();
    }
    this.showOptionPanel = !this.showOptionPanel;
    this.searchKeyWord = '';
    this.onSearchKeyWordChange($event);
    if(!this.showOptionPanel){
        this.optionPanelClosed.emit(this.custId);
    }
}
customSelectOptionChange($event, option){
    if($event){
        $event.preventDefault();
    }
    option.isSelected = !option.isSelected;
    option.isVisible = option.isSelected;
    this.optionSelectionChange.emit(this.prepareSelectedList([option]));
}
customSelectAllOptionChange($event, selectAll){
    if($event){
        $event.preventDefault();
    }
    selectAll.isSelected = !selectAll.isSelected;
    if(selectAll.isSelected){
        this.optionItemList = this.optionItemList.map(item => { item.isSelected = true; item.isVisible = true; return item; });
    }else{
        this.optionItemList = this.optionItemList.map(item => { item.isSelected = false; item.isVisible = false; return item; });
    }
    this.optionSelectionChange.emit(this.prepareSelectedList([]));
}
prepareSelectedList(options){
    let selectedList = this.optionItemOriginalList.filter(item => { return item.isSelected; });
    return selectedList;
}
onSearchKeyWordChange($event){
    if($event){
        $event.preventDefault();
    }
    this.optionItemList = this.optionItemOriginalList.filter(item => { return item.name.toUpperCase().match(this.searchKeyWord.toUpperCase())});
    this.checkForSelected();
}
clearKeyWordText($event){
    if($event){
        $event.preventDefault();
        $event.stopPropagation();
    }
    this.searchKeyWord = '';
    this.onSearchKeyWordChange($event);
}
onOutsideClick($event){
    if (!this._eref.nativeElement.contains($event.target)){
        let oldVal = this.showOptionPanel;
        this.showOptionPanel = false;
        if(oldVal && !this.showOptionPanel){
            this.optionPanelClosed.emit(this.custId);
        }
    }
}
ngOnDestroy(){
    if(this.showOptionPanel){
        this.optionPanelClosed.emit(this.custId);
    }
}
}
