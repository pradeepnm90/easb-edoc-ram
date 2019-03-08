import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationService } from './services/authentication.service';
import { CoreService } from './services/core.service';
import { AgGridListComponent } from './components/ag-grid-list/ag-grid-list.component';
import { AgGridModule } from "ag-grid-angular";
import { SharedEventService } from "./services/shared-event.service";
import { CustomDateComponent } from './components/ag-date-filter/ag-date-filter.component';
import { MaterialModule } from './material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { KeyboardKeyDirective } from './directives/keyboard-key.directive';
import { MomentDateAdapter, MOMENT_DATE_FORMATS } from "./services/mat-datepicker-formatter";
import { SanitizeHtmlPipe } from './pipes/sanitize-html-pipe';
import { CancelPopup } from "./components/close-conformation/close-conformationpopup.component";
import { DocumentService } from './services/document.service';
import { ExtendedSearchComponent } from './components/extended-search/extended-search.component';
import { CustomSelectDropdownComponent } from './components/custom-select-dropdown/custom-select-dropdown';
import { SelectedItemCardComponent } from './components/selected-item-card/selected-item-card';
import { ViewSubmissionComponent } from './components/view-submission/view-submission.component';
import { ViewComponent } from './components/view-submission/view/view.component';
import { AgGridActionComponentRenderer } from './components/ag-grid-action/ag-grid-action.component';
import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
import { UserNotificationPanelComponent } from './components/user-notification-panel/user-notification-panel.component';
import { UserNotificationComponent } from './components/user-notification/user-notification.component';

@NgModule({
    imports: [
        CommonModule, FormsModule, ReactiveFormsModule, AgGridModule.withComponents([CustomDateComponent]), TextMaskModule, MaterialModule,],
    providers: [AuthenticationService, CoreService, SharedEventService, {provide: MAT_DATE_FORMATS, useValue: MOMENT_DATE_FORMATS},  {provide: DateAdapter, useClass: MomentDateAdapter}, DocumentService],
    entryComponents: [AgGridActionComponentRenderer ],
    declarations: [CancelPopup, AgGridListComponent, AgGridActionComponentRenderer,  CustomDateComponent, SanitizeHtmlPipe, ExtendedSearchComponent, 
        CustomSelectDropdownComponent, SelectedItemCardComponent, ViewSubmissionComponent, ViewComponent, UserNotificationPanelComponent, UserNotificationComponent],
    exports: [CancelPopup, AgGridListComponent, AgGridActionComponentRenderer,  SanitizeHtmlPipe, ExtendedSearchComponent, CustomSelectDropdownComponent, 
        SelectedItemCardComponent, ViewSubmissionComponent, ViewComponent, UserNotificationPanelComponent, UserNotificationComponent]
})
export class SharedModule { }
