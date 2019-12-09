import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-player-panel-character-cards',
  templateUrl: './player-panel-character-cards.component.html',
  styleUrls: ['./player-panel-character-cards.component.css']
})
export class PlayerPanelCharacterCardsComponent implements OnInit {

  breakpoint: number;

  constructor() { }

  ngOnInit() {
    this.breakpoint = (window.innerWidth <= 400) ? 1 : 5;
  }

  onResize(event) {
    this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 5;
  }

}
