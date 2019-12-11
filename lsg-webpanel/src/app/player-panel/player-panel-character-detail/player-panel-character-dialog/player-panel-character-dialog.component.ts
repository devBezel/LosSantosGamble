import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Vehicle } from 'src/app/_models/vehicle';
import { NestedTreeControl } from '@angular/cdk/tree';

@Component({
  selector: 'app-player-panel-character-dialog',
  templateUrl: './player-panel-character-dialog.component.html',
  styleUrls: ['./player-panel-character-dialog.component.css']
})
export class PlayerPanelCharacterDialogComponent {

  constructor(public dialogRef: MatDialogRef<PlayerPanelCharacterDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {}

  ELEMENT_DATA: Vehicle[] = this.data.character.vehicles;

  displayedColumns: string[] = ['id', 'model', 'health', 'option'];
  dataSource = this.ELEMENT_DATA;

  onNoClick() {
    this.dialogRef.close();
  }

}
