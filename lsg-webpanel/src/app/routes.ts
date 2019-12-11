import { Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { PlayerPanelHomeComponent } from './player-panel/player-panel-home/player-panel-home.component';
// tslint:disable-next-line:max-line-length
import { PlayerPanelCreateCharacterComponent } from './player-panel/player-panel-characters/player-panel-create-character/player-panel-create-character.component';
import { CharacterCardResolver } from './_resolvers/character-card.resolver';

export const appRoutes: Routes = [
    { path: 'logowanie', component: LoginComponent },
    { path: '', component: PlayerPanelHomeComponent, canActivate: [AuthGuard], resolve: { characters: CharacterCardResolver } },
    { path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            // { path: 'player/panel/home', component: PlayerPanelHomeComponent, resolve: { characters: CharacterCardResolver } },
            { path: 'player/panel/character/create', component: PlayerPanelCreateCharacterComponent }
        ]
     },
     { path: '**', redirectTo: '', pathMatch: 'full' }
];
