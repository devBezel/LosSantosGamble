import { Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { PlayerPanelHomeComponent } from './player-panel/player-panel-home/player-panel-home.component';

export const appRoutes: Routes = [
    { path: '', component: LoginComponent },
    { path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'player/panel/home', component: PlayerPanelHomeComponent }
        ]
     },
     { path: '**', redirectTo: '', pathMatch: 'full' }
];
