import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { DealContainer } from "./deal.container";
import { TruncatePipe } from "./components/deal-notes/note-text-pipe";
import { DealDetailsComponent } from "./components/deal-details/deal-details.component";
import { DealListComponent } from "./components/deal-list/deal-list.component";
import { DealSubStatusComponent } from "./components/deal-substatus/deal-substatus.component";
import { DealsService } from "./deals.service";
import { EffectsModule } from "@ngrx/effects";
import { DealStatusComponent } from "./components/deal-status/deal-status.component";
import { MaterialModule } from "../../shared/material/material.module";
import { SharedModule } from "../../shared/shared.module";
import { DealEffects } from "../../effects/deal.effects";
import { KeyNonKeyDocumentEffects } from "../../effects/key-non-key-document.effects";
import { NoteEffects } from "../../effects/note.effects";
import { DocumentEffects } from "../../effects/document.effects";
import { MomentModule } from "angular2-moment";
import { DealFilterComponent } from "./components/deal-filter/deal-filter.component";
import { DealDeclineComponent } from "./components/deal-decline/deal-decline.component"
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { TextMaskModule } from "angular2-text-mask";
import { DealNoteComponent } from "./components/deal-notes/deal-note.component";
import { DealNoteListComponent } from "./components/deal-notes/deal-notelist.component";
import { DealNoteDetail } from "./components/deal-notes/deal-notedetail.component";
import { DealDocumentListComponent } from "./components/deal-documents/deal-document-list.component";
import { DealDocumentComponent } from "./components/deal-documents/deal-document.component";
import { TruncateDocumentTextPipe } from "./components/deal-documents/document-text-pipe";
import { DealKeyNonKeyDocumentListComponent } from "./components/deal-key-non-key-documents/deal-key-non-key-document-list.component";
import { DealKeyDocumentComponent } from "./components/deal-key-non-key-documents/deal-document.component";
import { DealChecklistComponent } from './components/deal-checklist/deal-checklist.component';
import { DealChecklistNoteComponent } from "./components/deal-checklist/checklist-note/deal-checklist-note.component";

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule,
    TextMaskModule,
    MomentModule
  ],
  entryComponents: [DealNoteDetail,DealDeclineComponent,DealChecklistNoteComponent],
  declarations: [
    DealContainer,
    TruncatePipe,
    DealDeclineComponent,
    TruncateDocumentTextPipe,
    DealDetailsComponent,
    DealNoteDetail,
    DealDocumentListComponent,
    DealNoteListComponent,
    DealNoteComponent,
    DealDocumentComponent,
    DealListComponent,
    DealStatusComponent,
    DealSubStatusComponent,
    DealFilterComponent,
    DealKeyNonKeyDocumentListComponent,
    DealKeyDocumentComponent,
    DealChecklistComponent,
    DealChecklistNoteComponent
  ],
  providers: [DealsService],
  exports: [
    DealContainer,
    DealDeclineComponent,
    DealDetailsComponent,
    TruncatePipe,
    DealListComponent,
    DealStatusComponent,
    DealSubStatusComponent,
    DealDocumentListComponent,
    DealNoteDetail,
    DealChecklistNoteComponent
  ]
})
export class DealModule {}
