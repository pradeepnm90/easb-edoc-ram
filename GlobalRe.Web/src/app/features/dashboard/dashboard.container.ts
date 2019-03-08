import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Store } from "@ngrx/store";
import * as apiUrlCollection from "../../actions/apiurl.action";
import * as lookup from '../../actions/look-up-values.action';
import * as fromRoot from "../../store";
import { ApiUrl } from "../../shared/models/api-url";

import * as fromUserView from '../../actions/deals/user-views.actions';

import {
  EntityType, DEALSTATUSSUMMARIES_API_URL,
  DEFAULTSUBDIVISION_API_URL, UNDERWRITERS_API_URL, TAs_UAs_API_URL,
  ACTUARIES_API_URL, MODELERES_API_URL, STATUSNAME_API_URL, NOTE_TYPE_URL, EXTENDED_SEARCH_URL, USERViEW_API_URL, USER_VIEW_SCREEN_NAME
} from '../../app.config';
import { LoadExtendedSearchData } from '../../actions/deals/extended-search.action';
@Component({
  selector: 'dashboard-container',
  template: `
  <deal-container></deal-container> `,

  styleUrls: ['./dashboard.container.css']
})
export class DashboardContainer implements OnInit {
 
  constructor(private _store: Store<fromRoot.AppState>) {
  }

  ngOnInit() {
    const componentObj = this;
    /* this._store.dispatch(new apiUrlCollection.AddApiUrlAction([new ApiUrl({key:EntityType.DealStatusSummaries,url:DEALSTATUSSUMMARIES_API_URL})]));    
    this._store.dispatch(new lookup.getLookupAction([STATUSNAME_API_URL,
      UNDERWRITERS_API_URL, TAs_UAs_API_URL, ACTUARIES_API_URL, MODELERES_API_URL, NOTE_TYPE_URL]));
    this._store.dispatch(new LoadExtendedSearchData(EXTENDED_SEARCH_URL)); */

    /* this._store.dispatch( // loading list User View
      new fromUserView.LoadUserViews(USERViEW_API_URL + USER_VIEW_SCREEN_NAME)
    ); */

  }

}

