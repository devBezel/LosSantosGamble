import { Component, OnInit, Input } from '@angular/core';
import { Character } from 'src/app/_models/character';

@Component({
  selector: 'app-player-panel-character-cards',
  templateUrl: './player-panel-character-cards.component.html',
  styleUrls: ['./player-panel-character-cards.component.css']
})
export class PlayerPanelCharacterCardsComponent implements OnInit {

  breakpoint: number;
  @Input() characters: Character[];

  constructor() { }

  ngOnInit() {
    console.log(this.characters);
    this.breakpoint = (window.innerWidth <= 400) ? 1 : 5;
  }

  onResize(event) {
    this.breakpoint = (event.target.innerWidth <= 400) ? 1 : 5;
  }

}
