import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-player-panel-sidebar',
  templateUrl: './player-panel-sidebar.component.html',
  styleUrls: ['./player-panel-sidebar.component.css']
})
export class PlayerPanelSidebarComponent implements OnInit {

  opened = true;
  events = [];

  constructor(private router: Router, public authService: AuthService) { }

  ngOnInit() {
  }

  logout() {
    localStorage.removeItem('token');
    console.log('Zostałeś wylogowany');
    this.router.navigate(['/home']);
  }

}
