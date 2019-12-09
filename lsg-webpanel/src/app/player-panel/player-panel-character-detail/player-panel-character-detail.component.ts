import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { PlayerPanelCharacterDialogComponent } from './player-panel-character-dialog/player-panel-character-dialog.component';

@Component({
  selector: 'app-player-panel-character-detail',
  templateUrl: './player-panel-character-detail.component.html',
  styleUrls: ['./player-panel-character-detail.component.css']
})
export class PlayerPanelCharacterDetailComponent implements OnInit {

  constructor(public dialog: MatDialog) { }

  ngOnInit() {
  }

  openDialog() {
    const dialogRef = this.dialog.open(PlayerPanelCharacterDialogComponent, {
      width: '500px',
      // height: '500px',
      data: { name: 'Richard McCartney' }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Dialog został zamknięty');
    });
  }

}

