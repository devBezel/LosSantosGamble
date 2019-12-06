import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
    providedIn: 'root'
})

export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {}

    canActivate(): boolean {
        if (this.authService.loggedIn()) {
            return true;
        }
        console.log('Nie masz uprawnień, aby wejść na tą stronę');
        this.router.navigate(['']);
        return false;
    }
}

