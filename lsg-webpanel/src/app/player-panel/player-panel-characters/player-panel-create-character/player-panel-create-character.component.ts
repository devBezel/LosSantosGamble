import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-player-panel-create-character',
  templateUrl: './player-panel-create-character.component.html',
  styleUrls: ['./player-panel-create-character.component.css']
})
export class PlayerPanelCreateCharacterComponent implements OnInit {
  isLinear = true;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
  threeFormGroup: FormGroup;
  itemSelected: string;
  items: string[] = ['telephone', 'notepad', 'handbag', 'cigarette', 'watch'];

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.firstFormGroup = this.formBuilder.group({
      name: ['', Validators.required]
    });
    this.secondFormGroup = this.formBuilder.group({
      itemSelected: ['']
    });
    this.threeFormGroup = this.formBuilder.group({

    });
  }

}
