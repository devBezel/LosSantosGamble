import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PlayerPanelHomeComponent } from 'src/app/player-panel/player-panel-home/player-panel-home.component';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from '../material/material.module';
import { AppRoutingModule } from 'src/app/app-routing.module';
import { PlayerPanelNavbarComponent } from 'src/app/player-panel/player-panel-navbar/player-panel-navbar.component';
import { PlayerPanelSidebarComponent } from 'src/app/player-panel/player-panel-sidebar/player-panel-sidebar.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    AppRoutingModule
  ],
  declarations: [
    PlayerPanelHomeComponent,
    PlayerPanelNavbarComponent,
    PlayerPanelSidebarComponent
  ]

})
export class PlayerPanelModule { }
