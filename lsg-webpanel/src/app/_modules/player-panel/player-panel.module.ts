import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerPanelHomeComponent } from 'src/app/player-panel/player-panel-home/player-panel-home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { PlayerPanelNavbarComponent } from 'src/app/player-panel/player-panel-navbar/player-panel-navbar.component';
import { PlayerPanelSidebarComponent } from 'src/app/player-panel/player-panel-sidebar/player-panel-sidebar.component';
// tslint:disable-next-line:max-line-length
import { PlayerPanelCreateCharacterComponent } from 'src/app/player-panel/player-panel-characters/player-panel-create-character/player-panel-create-character.component';
// tslint:disable-next-line:max-line-length
import { PlayerPanelCharacterCardsComponent } from 'src/app/player-panel/player-panel-character-cards/player-panel-character-cards.component';
// tslint:disable-next-line:max-line-length
import { PlayerPanelAccountPunishmentListComponent } from 'src/app/player-panel/player-panel-account-punishment-list/player-panel-account-punishment-list.component';
// tslint:disable-next-line:max-line-length
import { PlayerPanelCharacterDetailComponent } from 'src/app/player-panel/player-panel-character-detail/player-panel-character-detail.component';
// tslint:disable-next-line:max-line-length
import { PlayerPanelCharacterDialogComponent } from 'src/app/player-panel/player-panel-character-detail/player-panel-character-dialog/player-panel-character-dialog.component';
import { CharacterService } from 'src/app/_services/character.service';
import { CharacterCardResolver } from 'src/app/_resolvers/character-card.resolver';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    MaterialModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  declarations: [
    PlayerPanelHomeComponent,
    PlayerPanelNavbarComponent,
    PlayerPanelSidebarComponent,
    PlayerPanelCreateCharacterComponent,
    PlayerPanelCharacterCardsComponent,
    PlayerPanelAccountPunishmentListComponent,
    PlayerPanelCharacterDetailComponent,
    PlayerPanelCharacterDialogComponent
  ],
  providers: [
    CharacterService,
    CharacterCardResolver
  ],
  exports: [
    PlayerPanelSidebarComponent,

  ],
  entryComponents: [
    PlayerPanelCharacterDialogComponent
  ]

})
export class PlayerPanelModule { }
