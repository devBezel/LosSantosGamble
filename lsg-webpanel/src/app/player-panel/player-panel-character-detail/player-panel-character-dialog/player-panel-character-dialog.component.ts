import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-player-panel-character-dialog',
  templateUrl: './player-panel-character-dialog.component.html',
  styleUrls: ['./player-panel-character-dialog.component.css']
})
export class PlayerPanelCharacterDialogComponent {

  constructor(public dialogRef: MatDialogRef<PlayerPanelCharacterDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {}

  onNoClick() {
    this.dialogRef.close();
  }

}
