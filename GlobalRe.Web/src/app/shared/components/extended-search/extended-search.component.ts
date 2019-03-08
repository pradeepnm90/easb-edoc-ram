import {Component,OnDestroy, OnInit, ViewEncapsulation, Input, Output, EventEmitter} from "@angular/core";
import { Store} from '@ngrx/store';
import * as fromRoot from '../../../store/index';
import { UpdateActiveSateExtendedSearchData, SaveActiveStateExtendedSearchData, ClearActiveSateExtendedSearchData } from "../../../actions/deals/extended-search.action";
@Component({
  selector: 'extended-search',
  templateUrl: 'extended-search.component.html',
  styleUrls: ['extended-search.component.scss']
})
export class ExtendedSearchComponent implements OnInit,OnDestroy {
showBtnPanel:boolean = false;
pl2CustomId: string = 'ProductLine2';
egCustomId: string = 'ExposureGroup';
enCustomId: string = 'ExposureName';
kmnCustomId: string = 'KeyMemberName';

subdivisionList: any[] = [];
subdivisionListOrig: any[] = [];
sdSelectedList: any[] = [];

plOptionList: any[] = [];
plOptionListOrig: any[] = [];
plSelectedList: any[] = [];

egOptionList: any[] = [];
egOptionListOrig: any[] = [];
egSelectedList: any[] = [];

enOptionList: any[] = [];
enOptionListOrig: any[] = [];
enSelectedList: any[] = [];

kmnOptionList: any[] = [];
kmnOptionListOrig: any[] = [];
kmnSelectedList: any[] = [];

sdSelectedListOrig: any;
plSelectedListOrig: any;
egSelectedListOrig: any;
kmnSelectedListOrig: any;
enSelectedListOrig: any;

isSearchPanelDataChanged: boolean = false;
isClearBtnEnabled: boolean = false;
activeStateDataBackup: any;

@Input() set activeStateData(value){
    if(value){

        this.activeStateDataBackup = JSON.parse(JSON.stringify(value));

        this.subdivisionListOrig = value.subdivision;
        this.plOptionListOrig = value.productLine2;
        this.egOptionListOrig = value.exposureGroup;
        this.enOptionListOrig = value.exposureName;
        this.kmnOptionListOrig = value.keyMemberName;

        this.subdivisionList = value.subdivision;
        this.plOptionList = value.productLine2;
        this.egOptionList = value.exposureGroup;
        this.enOptionList = value.exposureName;
        this.kmnOptionList = value.keyMemberName;
        
        this.sdSelectedList = this.subdivisionList.filter(item => item.isSelected);
        this.plSelectedList = this.plOptionList.filter(item => item.isSelected);
        this.egSelectedList = this.egOptionList.filter(item => item.isSelected);
        this.enSelectedList = this.enOptionList.filter(item => item.isSelected);
        this.kmnSelectedList = this.kmnOptionList.filter(item => item.isSelected);

        this.sdSelectedListOrig = JSON.parse(JSON.stringify(this.sdSelectedList));
        this.plSelectedListOrig = JSON.parse(JSON.stringify(this.plSelectedList));
        this.egSelectedListOrig = JSON.parse(JSON.stringify(this.egSelectedList));
        this.enSelectedListOrig = JSON.parse(JSON.stringify(this.enSelectedList));
        this.kmnSelectedListOrig = JSON.parse(JSON.stringify(this.kmnSelectedList));

        this.selectOptionListSelection();
    }else{
        this.subdivisionListOrig = [];
        this.plOptionListOrig = [];
        this.egOptionListOrig = [];
        this.enOptionListOrig = [];
        this.kmnOptionListOrig = [];

        this.subdivisionList = [];
        this.plOptionList = [];
        this.egOptionList = [];
        this.enOptionList = [];
        this.kmnOptionList = [];
        
        this.sdSelectedList = [];
        this.plSelectedList = [];
        this.egSelectedList = [];
        this.enSelectedList = [];
        this.kmnSelectedList = [];

        this.sdSelectedListOrig = [];
        this.plSelectedListOrig = [];
        this.egSelectedListOrig = [];
        this.enSelectedListOrig = [];
        this.kmnSelectedListOrig = [];
    }
    this.detectChangeForClear();
}
@Output() onApply = new EventEmitter();
constructor(private _store: Store<fromRoot.AppState>){}
ngOnInit(){}
openBtnPanel($event){
    if($event){
        $event.preventDefault();
    }
    this.showBtnPanel = !this.showBtnPanel;
}
hideBtnPanel(){
    this.showBtnPanel = false;
}
subdivisionOptionChange($event, subdivision){
    if($event){
        $event.preventDefault();
    }
    subdivision.isSelected = !subdivision.isSelected;
    this.sdSelectedList = this.subdivisionList.filter(item => item.isSelected);
    if(!subdivision.isSelected){
        this.closeChildForSD();
        this.closeChildForPL();
        this.closeChildForEG();
    }
    this.selectOptionListSelection();
}
plOptionSelectionChange($event){
    this.plSelectedList = $event;
}
egOptionSelectionChange($event){
    this.egSelectedList = $event;
}
enOptionSelectionChange($event){
    this.enSelectedList = $event;
}
kmnOptionSelectionChange($event){
    this.kmnSelectedList = $event;
}
selectedplCloseHandler($event){
    console.log('selectedPlCloseHandler', $event);
    this.plSelectedList = this.plSelectedList.filter(item => { return item.name != $event.name; });
    this.closeChildForPL();
    this.closeChildForEG();
    this.selectOptionListSelection();
}
selectedegCloseHandler($event){
    console.log('selectedPlCloseHandler', $event);
    this.egSelectedList = this.egSelectedList.filter(item => { return item.name != $event.name; });
    this.closeChildForEG();
    this.selectOptionListSelection();
}
selectedenCloseHandler($event){
    console.log('selectedPlCloseHandler', $event);
    this.enSelectedList = this.enSelectedList.filter(item => { return item.name != $event.name; });
    this.detectChangeInSearchPanel();
    this.detectChangeForClear();
}
selectedkmnCloseHandler($event){
    console.log('selectedPlCloseHandler', $event);
    this.kmnSelectedList = this.kmnSelectedList.filter(item => { return item.name != $event.name; });
    this.detectChangeInSearchPanel();
    this.detectChangeForClear();
}
closeChildForSD(){
    this.plOptionList.map(item => {
        if(this.sdSelectedList.length > 0 && this.sdSelectedList.find(sItem => sItem.child.indexOf(item.code) > -1) && this.plSelectedList.find(sItem => sItem.code == item.code) ){
            item.isSelected = true;
        }else{
            item.isSelected = false;
            item.isVisible = false;
        }
        if(item.isSelected && item.parent.length > 1){
            item.isVisible = false;
        }
        return item;
    });
    this.plSelectedList = this.plOptionList.filter(item => item.isSelected);
}
closeChildForPL(){
    this.egOptionList.map(item => {
        if(this.sdSelectedList.length > 0 && this.sdSelectedList.find(sItem => sItem.grandChild.indexOf(item.code) > -1) && this.plSelectedList.length > 0 && this.plSelectedList.find(sItem => sItem.child.indexOf(item.code) > -1) && this.egSelectedList.find(sItem => sItem.code == item.code)){
            item.isSelected = true;
        }else{
            item.isSelected = false;
            item.isVisible = false;
        }
        return item;
    });
    this.egSelectedList = this.egOptionList.filter(item => item.isSelected);
}
closeChildForEG(){
    this.enOptionList.map(item => {
        if(this.egSelectedList.length > 0 && this.egSelectedList.find(sItem => sItem.child.indexOf(item.code) > -1) && this.enSelectedList.find(sItem => sItem.code == item.code) ){
            item.isSelected = true;
        }else{
            item.isSelected = false;
            item.isVisible = false;
        }
        return item;
    });
    this.enSelectedList = this.enOptionList.filter(item => item.isSelected);
}
optionPanelCloseHandler($event){
    switch($event){
        case this.enCustomId: {
            this.selectParentEN();
        }
        case this.egCustomId: {
            this.selectParentEG();
        }
        case this.pl2CustomId: {
            this.selectParentPL($event);
            this.selectOptionListSelection();
            break;
        }
        case this.kmnCustomId: {
            this.detectChangeInSearchPanel();
            this.detectChangeForClear();
        }
    }
}
selectParentPL($event){
    if($event == this.pl2CustomId){
        this.subdivisionList.map(item => {
            if((this.plSelectedList.length > 0 && this.plSelectedList.find(sItem => (sItem.isVisible && sItem.parent.indexOf(item.code) > -1)))
                 || (this.sdSelectedList.length> 0 && this.sdSelectedList.find(sItem => sItem.code == item.code))){
                item.isSelected = true;
            }
            return item;
        });
    }else{
        this.subdivisionList.map(item => {
            if(this.egSelectedList.length > 0 && this.egSelectedList.find(sItem => sItem.grandParent.indexOf(item.code) > -1) && this.plSelectedList.length > 0 && this.plSelectedList.find(sItem => sItem.parent.indexOf(item.code) > -1)){
                item.isSelected = true;
            }
            return item;
        });
    }
    
    this.sdSelectedList = this.subdivisionList.filter(item => item.isSelected);
}
selectParentEG(){
    this.plOptionList.map(item => {
        if(this.egSelectedList.length > 0 && this.egSelectedList.find(sItem => sItem.parent.indexOf(item.code) > -1)){
            item.isSelected = true;
        }
        return item;
    });
    this.plSelectedList = this.plOptionList.filter(item => item.isSelected);
}
selectParentEN(){
    console.log('this.enSelectedList', this.enSelectedList);
    this.egOptionList.map(item => {
        if(this.enSelectedList.length > 0 && this.enSelectedList.find(sItem => sItem.parent.indexOf(item.code) > -1)){
            item.isSelected = true;
        }
        return item;
    });
    this.egSelectedList = this.egOptionList.filter(item => item.isSelected);
}
selectOptionListSelection(){
    let plOptions, egOptions, enOptions; 
    plOptions = this.plOptionListOrig.filter(item => {
        let found = false;
        for (const iterator of item.parent) {
            let sub2Found = (this.sdSelectedList.length > 0)? (this.sdSelectedList.find(sdItem => sdItem.code == iterator)? true : false) : true;
            if(sub2Found){
                found = true;
            }
        }
        return found;
    });
    egOptions = this.egOptionListOrig.filter(item => {
        let found = false, found1 = false;
        for (const iterator of item.parent) {
            let subFound = plOptions.find(plItem => iterator == plItem.code);
            let sub2Found = (this.plSelectedList.length > 0)? (this.plSelectedList.find(plItem => plItem.code == iterator)? true : false) : (subFound? true : false);
            if(sub2Found){
                found = true;
            }
        }
        for (const iterator1 of item.grandParent) {
            let sub2Found2 = (this.sdSelectedList.length > 0)? (this.sdSelectedList.find(plItem => plItem.code == iterator1)? true : false) : true;
            if(sub2Found2){
                found1 = true;
            }
        }
        return (found && found1);
    });
    enOptions = this.enOptionListOrig.filter(item => {
        let found = false;
        for (const iterator of item.parent) {
            let subFound = egOptions.find(egItem => iterator == egItem.code);
            let sub2Found = (this.egSelectedList.length > 0)? (this.egSelectedList.find(egItem => egItem.code == iterator)? true : false) :  (subFound? true : false);
            if(sub2Found){
                found = true;
            }
        }
        return found;
    });
    this.plOptionList = plOptions;
    this.egOptionList = egOptions;
    this.enOptionList = enOptions;
    this.detectChangeInSearchPanel();
    this.detectChangeForClear();
}
isChangeHappen(newArray, oldArray){
    if(newArray.length != oldArray.length){
        return true;
    }else{
        let notFound = false;
        for (const iterator of newArray) {
            if(!oldArray.find(item => item.code == iterator.code)){
                notFound = true;
            }
        }
        return notFound;
    }
}
detectChangeInSearchPanel(){
    let isSDChanged, isPLChanged, isEGChanged, isENChanged, isKMNChanged;
    isSDChanged = this.isChangeHappen(this.sdSelectedList, this.sdSelectedListOrig);
    isPLChanged = this.isChangeHappen(this.plSelectedList, this.plSelectedListOrig);
    isEGChanged = this.isChangeHappen(this.egSelectedList, this.egSelectedListOrig);
    isENChanged = this.isChangeHappen(this.enSelectedList, this.enSelectedListOrig);
    isKMNChanged = this.isChangeHappen(this.kmnSelectedList, this.kmnSelectedListOrig);
    this.isSearchPanelDataChanged = (isSDChanged || isPLChanged || isEGChanged || isENChanged || isKMNChanged);
}
detectChangeForClear(){
    this.isClearBtnEnabled = (this.sdSelectedList.length || this.plSelectedList.length || this.egSelectedList.length || this.enSelectedList.length || this.kmnSelectedList.length)? true : false;
}
clearChanges($event){
    if($event){
        $event.preventDefault();
    }
    this.subdivisionList.map(item => {
        item.isSelected = false;
        return item;
    });
    this.sdSelectedList = [];
    this.plSelectedList = [];
    this.egSelectedList = [];
    this.enSelectedList = [];
    this.kmnSelectedList = [];
    this.selectOptionListSelection();
    //this._store.dispatch(new ClearActiveSateExtendedSearchData());
}
prepareLblText(){
    let lblText: string = '', kmn: string [] = [], tempStr =  '', tooltipText = '', className='', kClassname='class-k';
    if(this.enSelectedList.length > 0){
        lblText = 'Exposure Name';
        className = 'class-en';
        kClassname = 'class-k-en';
    }else if(this.egSelectedList.length > 0){
        lblText = 'Exposure Group';
        className = 'class-eg';
        kClassname = 'class-k-eg';
    }else if(this.plSelectedList.length > 0){
        lblText = 'Product Line 2';
        className = 'class-p';
        kClassname = 'class-k-p';
    }else if(this.sdSelectedList.length > 0){
        lblText = 'Subdivision';
        className = 'class-s';
        kClassname = 'class-k-s';
    }
    this.kmnSelectedList.map(item => {
        kmn.push(item.name);
        return item;
    });
    tempStr = kmn.join(', ');
    if(lblText && tempStr){
        tooltipText = lblText + ', ' + tempStr;
    }else if(lblText){
        tooltipText = lblText;
    }else if(tempStr){
        tooltipText = tempStr;
    }
    return {lblText: lblText, kmn: tempStr, tooltipText: tooltipText, className: className, kClassname: kClassname };
}
applyChanges($event){
    if($event){
        $event.preventDefault();
    }
    let newObj = {
        subdivision: this.subdivisionListOrig,
        productLine2: this.plOptionListOrig,
        exposureGroup: this.egOptionListOrig,
        exposureName: this.enOptionListOrig,
        keyMemberName: this.kmnOptionListOrig
    };
    this._store.dispatch(new SaveActiveStateExtendedSearchData(newObj));
    this.onApply.emit(this.prepareLblText());
    this.hideBtnPanel();
}
ngOnDestroy(){}
}
