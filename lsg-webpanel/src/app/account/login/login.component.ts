import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    console.log(this.model);
    this.authService.login(this.model).subscribe(next => {
      console.log('Zalogowano się do aplikacji');
    }, error => {
      console.log(error);
    }, () => {
      this.router.navigate(['player/panel/home']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

}
