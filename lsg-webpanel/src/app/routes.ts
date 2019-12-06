import { Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: LoginComponent },
    { path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [

        ]
     },
     { path: '**', redirectTo: '', pathMatch: 'full' }
];
