import { Component, OnInit } from '@angular/core';
import { Character } from 'src/app/_models/character';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-player-panel-home',
  templateUrl: './player-panel-home.component.html',
  styleUrls: ['./player-panel-home.component.css']
})
export class PlayerPanelHomeComponent implements OnInit {

  constructor(private route: ActivatedRoute) { }

  characters: Character[];

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.characters = data.characters;
    });
  }

}
