import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player-panel-sidebar',
  templateUrl: './player-panel-sidebar.component.html',
  styleUrls: ['./player-panel-sidebar.component.css']
})
export class PlayerPanelSidebarComponent implements OnInit {

  opened = true;
  events = [];

  constructor() { }

  ngOnInit() {
  }

}
