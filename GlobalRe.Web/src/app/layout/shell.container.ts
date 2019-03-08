import { Component, OnInit } from '@angular/core';
import { Store } from "@ngrx/store";
import { LoginUser } from '../shared/models/login-user';
import * as fromRoot from '../store/index';
import { Observable } from 'rxjs/Rx';

@Component({
  selector: 'shell-container',
  templateUrl: './shell.container.html',
  styleUrls: ['./shell.container.scss']
})
export class ShellContainerComponent implements OnInit {
  IMAGE_RIGHT: string;
  ERMS_URL:string; 
  QLIKVIEW_URL:string;
  AMBEST_URL:string;
  SNL_URL:string;
  GUYCARPENTER_URL:string;
  AON_URL:string;
  SERVICEDESK_EMAIL:string;
  EXPENSES_URL:string;
  WORKDAY_URL:string;
  MARKELEXTERNAL_URL:string;
  MYMARKEL_URL:string;
  ERMRCM_URL:string;
  emailUrl = 'mailto:'+this.SERVICEDESK_EMAIL;
  title = 'GRS App';
  readonly loginUser$: Observable<LoginUser>;
  showquicklinkscontent: boolean = false;
  constructor(private _store: Store<fromRoot.AppState>
    ) {
    this.loginUser$ = this._store.select<LoginUser>(fromRoot.getAuthenticatedUser);
    this.loginUser$.subscribe(val=>{
    this.IMAGE_RIGHT = val.LINK_IMAGE_RIGHT;
    this.QLIKVIEW_URL = val.LINK_QLIKVIEW_URL;
    this.ERMS_URL = val.LINK_ERMRCM_URL;
    this.EXPENSES_URL = val.LINK_EXPENSES_URL;
    this.ERMRCM_URL = val.LINK_ERMRCM_URL;
    this.AMBEST_URL = val.LINK_AMBEST_URL;
    this.SNL_URL = val.LINK_SNL_URL;
    this.GUYCARPENTER_URL = val.LINK_GUYCARPENTER_URL;
    this.AON_URL = val.LINK_AON_URL;
    this.SERVICEDESK_EMAIL = val.LINK_SERVICEDESK_EMAIL;
    this.WORKDAY_URL = val.LINK_WORKDAY_URL;
    this.MARKELEXTERNAL_URL =val.LINK_MARKELEXTERNAL_URL;
    this.MYMARKEL_URL = val.LINK_MYMARKEL_URL;
  })
}
  onquicklinksClick(event){
    this.showquicklinkscontent = !this.showquicklinkscontent;     
  }
  ngOnInit() {

  }
  OpenImageRight()
  {
    window.open(this.IMAGE_RIGHT, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openQlikView() {
    window.open(this.QLIKVIEW_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openERMS() {
    window.open(this.ERMS_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openCM(){
    window.open(this.ERMRCM_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openAMBEST() {
    window.open(this.AMBEST_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openSNL() {
    window.open(this.SNL_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openGUYCARPENTER() {
    window.open(this.GUYCARPENTER_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openAON() {
    window.open(this.AON_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openServiceDesk() {
    window.open(this.SERVICEDESK_EMAIL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openExpenses() {
    window.open(this.EXPENSES_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openWorkday() {
    window.open(this.WORKDAY_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }
  openMarklExternal() {
    window.open(this.MARKELEXTERNAL_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000")
  }
  openMyMarkel() {
    window.open(this.MYMARKEL_URL, '_blank', "directories=yes,height=1000,location=yes,resizable=yes,scrollbars=yes,status=yes,titlebar=yes,width=1000");
  }

}
