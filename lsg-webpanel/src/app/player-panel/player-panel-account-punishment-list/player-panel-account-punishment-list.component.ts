import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player-panel-account-punishment-list',
  templateUrl: './player-panel-account-punishment-list.component.html',
  styleUrls: ['./player-panel-account-punishment-list.component.css']
})
export class PlayerPanelAccountPunishmentListComponent implements OnInit {

  displayedColumns = ['Date', 'Character', 'Type', 'Administrator'];

  constructor() { }


  elementData: PeriodicElement[] = [
    { date: '07.12.2019', character: 'Richard McCartney', type: 'Kick', administrator: 'Algorytm' },
    { date: '07.12.2019', character: 'Richard McCartney', type: 'Kick', administrator: 'Algorytm' },
    { date: '07.12.2019', character: 'Richard McCartney', type: 'Kick', administrator: 'Algorytm' },
  ];
  ngOnInit() {
  }

}

export interface PeriodicElement {
  date: string;
  character: string;
  type: string;
  administrator: string;
}
