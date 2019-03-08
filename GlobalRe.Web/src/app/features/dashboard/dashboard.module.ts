import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { DashboardContainer } from "./dashboard.container";
import { DealModule } from "../deal/deal.module";

import { DashboardRoutingModule } from "./dashboard.routing";
import { DealEffects } from "../../effects/deal.effects";
import { EffectsModule } from "@ngrx/effects";
import { LookupValueEffects } from "../../effects/lookup-value.effect";
import { DealDetailEffects } from "../../effects/deal-detail.effect";

import { SharedModule } from "../../shared/shared.module";
import { KeyboardKeyDirective } from "../../shared/directives/keyboard-key.directive";
import { NoteEffects } from "../../effects/note.effects";
import { DocumentEffects } from "../../effects/document.effects";
import { TruncatePipe } from "../deal/components/deal-notes/note-text-pipe";
import { KeyNonKeyDocumentEffects } from "../../effects/key-non-key-document.effects";
import { ExtendedSearchEffects } from "../../effects/extended-search.effect";

import { UserViewsEffects } from "../../effects/user-view.effects";
import {DealChecklistEffects} from "../../effects/deal-checklist.effect";

@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    DealModule,

    EffectsModule.forRoot([
      DealDetailEffects,
      
      DealEffects,
      NoteEffects,
      KeyNonKeyDocumentEffects,
      DocumentEffects,
      DealChecklistEffects
    ])
  ],
  declarations: [DashboardContainer, KeyboardKeyDirective],
  exports: [DashboardContainer, TruncatePipe]
})
export class DashboardModule {}
