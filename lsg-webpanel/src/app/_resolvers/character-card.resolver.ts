import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Character } from '../_models/character';
import { CharacterService } from '../_services/character.service';
import { Observable, of } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import { catchError } from 'rxjs/operators';

@Injectable()
export class CharacterCardResolver implements Resolve<Character[]> {
    constructor(private characterService: CharacterService, private authService: AuthService, private router: Router) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Character[]> {
        return this.characterService.getAccountCharacters(this.authService.decodedToken.nameid).pipe(
            catchError(error => {
                console.log('Problem z wyświetleniem danych');
                this.router.navigate(['']);

                return of(null);
            })
        );
    }
}
